using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CSharp;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;




using AutoHotkey.Interop;
using System.Diagnostics;

namespace Automation_Project.src.automation {
    public class AutomationHandler {
        const string AHKWrapperInstanceName = "ahk";
        public static string pythonSourceLocation = @"C:\Users\HansW\AppData\Local\Programs\Python\Python39\python.exe";
        private string? pythonScriptLocation;

        public AutomationHandler() {
            this.pythonScriptLocation = null;
        }

        public static string testProgram =
            "public class AutoHelmWindowsAutomation {\n" +
            "\tstatic void Main(string[] args) {\n" +
            //"\t\tSystem.Console.WriteLine(\"Hello World!\");" +
            //"\t\tSystem.Console.ReadKey();" +
            "\t}\n" +
            "}";

        public static string testPythonProgram =
            "print(\"Hello world!\")";

        public static string withAHKWrapper(string code) {
            return $"{AHKWrapperInstanceName}.run_script(\"{code}\")";
        }

        public static string withAHKWrapperCSharp(string code) {
            return $"{AHKWrapperInstanceName}.ExecRaw(@\"{code}\");";
        }

        public static string formatAsCSharpFile(string code) {
            string output =
                "using AutoHotkey.Interop;\n" +
                "\n" +
                "public class AutoHelmWindowsAutomation {\n" +
                "\tstatic void Main(string[] args) {\n" +
                "\t\tAutoHotkeyEngine ahk = AutoHotkeyEngine.Instance;\n" +
                $"{indentCode(code, 2)}\n" +
                "\t}\n" +
                "}";
            return output;
        }

        public static string formatAsPythonFile(string code) {
            string output =
                "from ahk import AHK\n" +
                "\n" +
                $"{AHKWrapperInstanceName} = AHK()\n" +
                "\n" +
                $"{code}\n";
            return output;
        }

        public static string indentCode(string code, int n) {
            string[] splitByLine = code.Trim().Split("\n");
            //if (splitByLine[splitByLine.Length-1].tri)
            for (int i = 0; i < splitByLine.Length; i++) {
                //for (int j = 0; j < n; j++) { 
                //}
                splitByLine[i] = $"{String.Concat(Enumerable.Repeat("\t", n))}{splitByLine[i]}";
            }
            return String.Join("\n", splitByLine);
        }

        public static string wrapWithQuotations(string str) {
            return '"' + str + '"';
        }

        public static bool compileToFileold(string code) {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            //ICodeCompiler icc = provider.CreateCompiler();
            String exeName = String.Format(@"{0}\{1}.exe", System.Environment.CurrentDirectory, "out");
            CompilerParameters cp = new CompilerParameters();

            cp.GenerateExecutable = true;
            cp.OutputAssembly = exeName;
            cp.GenerateInMemory = false;
            cp.TreatWarningsAsErrors = false;

            CompilerResults cr = provider.CompileAssemblyFromSource(cp, code);

            if (cr.Errors.Count > 0) {
                Console.WriteLine($"Errors building automation executable");
                foreach (CompilerError ce in cr.Errors) {
                    Console.WriteLine("  {0}", ce.ToString());
                    Console.WriteLine();
                }
                return false;
            } else {
                Console.WriteLine("Successfully built automation executable");
                return true;
            }
        }

        public static string? compileToFile(string code) {
            string outputDirectory = System.Environment.CurrentDirectory;
            string exeName = "out.exe";
            string outputPath = Path.Combine(outputDirectory, exeName);

            List<SyntaxTree> trees = new List<SyntaxTree>();
            trees.Add(CSharpSyntaxTree.ParseText(code));

            var assemblyPath = Path.GetDirectoryName(typeof(object).Assembly.Location);
            if (assemblyPath == null) {
                return null;
            }
            //Console.WriteLine(assemblyPath);

            MetadataReference AHKLib = MetadataReference.CreateFromFile(typeof(AutoHotkeyEngine).Assembly.Location);
            //MetadataReference PrivateCoreLib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            MetadataReference MSCoreLib = MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "mscorlib.dll"));
            MetadataReference SystemLib = MetadataReference.CreateFromFile(typeof(Console).Assembly.Location);
            MetadataReference codeAnalysisLib = MetadataReference.CreateFromFile(typeof(SyntaxTree).Assembly.Location);
            MetadataReference netstandard = MetadataReference.CreateFromFile(Assembly.Load("netstandard, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51").Location);
            MetadataReference PrivateCoreLib = MetadataReference.CreateFromFile(Assembly.Load("System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e").Location);
            List<MetadataReference> references = new List<MetadataReference> { AHKLib, PrivateCoreLib, MSCoreLib, SystemLib, codeAnalysisLib, netstandard};
            Assembly.GetEntryAssembly()?.GetReferencedAssemblies().ToList()
                .ForEach(a => {
                    references.Add(MetadataReference.CreateFromFile(Assembly.Load(a).Location));
                    //Console.WriteLine(a.ToString());
                });

            CSharpCompilation compilation = CSharpCompilation.Create(
                "out.exe",
                trees,
                references,
                new CSharpCompilationOptions(OutputKind.ConsoleApplication));

            var result = compilation.Emit(outputPath);

            if (result.Success) {
                Console.WriteLine("Successfully built automation executable");
                return outputPath;
            } else {
                Console.WriteLine($"Errors building automation executable");
                foreach (Diagnostic diagnostic in result.Diagnostics) { 
                    Console.WriteLine(diagnostic.ToString());
                }
                return null;
            }
        }

        public void saveToFile(string code) {
            string workingDirectory = System.Environment.CurrentDirectory;
            string? binDirectory = Directory.GetParent(workingDirectory)?.Parent?.FullName;
            if (binDirectory == null) {
                Console.WriteLine("failed to find bin directory");
                return;
            }
            string outputDirectory = Path.Combine(binDirectory, "automation");
            string fileName = "out.py";
            string outputPath = Path.Combine(outputDirectory, fileName);

            using (StreamWriter writer = new StreamWriter(outputPath)) {
                writer.Write(code);
            }
            Console.WriteLine("saved to: " + outputPath);
            pythonScriptLocation = outputPath;
        }

        public bool execute() {
            if (pythonScriptLocation == null) {
                return false;
            }

            Console.WriteLine("Executing generated automation script");

            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(pythonSourceLocation, '"'+pythonScriptLocation+'"') {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            Console.WriteLine(output);

            return true;
        }
    }    
}

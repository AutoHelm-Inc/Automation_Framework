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

namespace Automation_Project.src.ast {
    public static class AutomationFunctions {
        const string AHKWrapperInstanceName = "ahk";

        public static string testProgram =
            "public class AutoHelmWindowsAutomation {\n" +
            "\tstatic void Main(string[] args) {\n" +
            //"\t\tSystem.Console.WriteLine(\"Hello World!\");" +
            //"\t\tSystem.Console.ReadKey();" +
            "\t}\n" +
            "}";

        public static string withAHKWrapper(string code) {
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
    }
    public static class Run {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "Run ";

            if (args.Count > 1) {
                output += args[1].ToString() + " ";
            }
            output += args[0].ToString();

            output = AutomationFunctions.withAHKWrapper(output);
            return output;
        }
    }

    public static class SwitchWindow {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "SwitchWindow";

            output = AutomationFunctions.withAHKWrapper(output);
            return output;
        }
    }

    public static class Close {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "Close";

            output = AutomationFunctions.withAHKWrapper(output);
            return output;
        }
    }

    public static class Create {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "Create";

            output = AutomationFunctions.withAHKWrapper(output);
            return output;
        }
    }

    public static class Save {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "Save";

            output = AutomationFunctions.withAHKWrapper(output);
            return output;
        }
    }

    public static class Move {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "Move";

            output = AutomationFunctions.withAHKWrapper(output);
            return output;
        }
    }

    public static class Del {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "Del";

            output = AutomationFunctions.withAHKWrapper(output);
            return output;
        }
    }

    public static class WrtLine {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "SendText ";

            for (int i = 0; i < args.Count(); i++) {
                output += args[i].ToString();
            }

            output += "\n" +
                "SendText `n";

            output = AutomationFunctions.withAHKWrapper(output);
            return output;
        }
    }

    public static class Write {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "SendText ";

            for (int i = 0; i < args.Count(); i++) {
                output += args[i].ToString();
            }

            output = AutomationFunctions.withAHKWrapper(output);
            return output;
        }
    }

    public static class PressKey {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "Send ";

            for (int i = 0; i < args.Count(); i++) {
                output += args[i].ToString();
            }

            output = AutomationFunctions.withAHKWrapper(output);
            return output;
        }
    }

    public static class EmailsGet {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "GetEmails ";

            for (int i = 0; i < args.Count(); i++) {
                output += args[i].ToString();
            }

            output = AutomationFunctions.withAHKWrapper(output);
            return output;
        }
    }

    public static class FilesGet {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "GetFiles ";

            for (int i = 0; i < args.Count(); i++) {
                output += args[i].ToString();
            }

            output = AutomationFunctions.withAHKWrapper(output);
            return output;
        }
    }

    public static class Click {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "Click ";

            for (int i = 0; i < args.Count(); i++) {
                output += args[i].ToString();
            }

            output = AutomationFunctions.withAHKWrapper(output);
            return output;
        }
    }

    public static class SaveAs {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "SaveAs ";

            for (int i = 0; i < args.Count(); i++) {
                output += args[i].ToString();
            }

            output = AutomationFunctions.withAHKWrapper(output);
            return output;
        }
    }
}

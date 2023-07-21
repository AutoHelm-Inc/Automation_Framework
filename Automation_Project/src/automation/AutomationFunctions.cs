using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CSharp;
using System.Reflection;
using System.CodeDom.Compiler;

namespace Automation_Project.src.ast {
    public static class AutomationFunctions {
        const string AHKWrapperInstanceName = "ahk";

        public static string withAHKWrapper(string code) {
            return $"{AHKWrapperInstanceName}.ExecRaw(@\"{code}\");";
        }

        public static string formatAsCSharpFile(string code) {
            string output =
                "using AutoHotkey.Interop;\n" +
                "\n" +
                "public class AutoHelmWindowsAutomation {\n" +
                "\tAutoHotkeyEngine ahk = AutoHotkeyEngine.Instance;\n" +
                $"{indentOnce(code)}\n" +
                "}";
            return output;
        }

        public static string indentOnce(string code) {
            string[] splitByLine = code.Trim().Split("\n");
            //if (splitByLine[splitByLine.Length-1].tri)
            for (int i = 0; i < splitByLine.Length; i++) {
                splitByLine[i] = $"\t{splitByLine[i]}";
            }
            return String.Join("\n", splitByLine);
        }

        public static string wrapWithQuotations(string str) {
            return '"' + str + '"';
        }

        public static bool compileToFile(string code) {
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

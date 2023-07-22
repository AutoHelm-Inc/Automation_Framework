using Automation_Project.src.ast;
using static Automation_Project.src.ast.Constants;
using System.Diagnostics;

namespace Automation_Project.src.automation {
    public interface Function {
        public string toPythonCode(List<dynamic> args);
    }
    public static class FunctionFactory {
        private static readonly Run runInstance = new Run();
        private static readonly SwitchWindow switchWindowInstance = new SwitchWindow();
        private static readonly Close closeInstance = new Close();
        private static readonly Create createInstance = new Create();
        private static readonly Save saveInstance = new Save();
        private static readonly Move moveInstance = new Move();
        private static readonly Del delInstance = new Del();
        private static readonly WrtLine wrtLineInstance = new WrtLine();
        private static readonly Write writeInstance = new Write();
        private static readonly PressKey pressKeyInstance = new PressKey();
        private static readonly EmailsGet emailsGetInstance = new EmailsGet();
        private static readonly FilesGet filesGetInstance = new FilesGet();
        private static readonly Click clickInstance = new Click();
        private static readonly SaveAs saveAsInstance = new SaveAs();
        private static readonly Sleep sleepInstance = new Sleep();

        public static Function? fromEnum(Functions @enum) {
            return @enum switch {
                Functions.None => null,
                Functions.Run => runInstance,
                Functions.SwitchWindow => switchWindowInstance,
                Functions.Close => closeInstance,
                Functions.Create => createInstance,
                Functions.Save => saveInstance,
                Functions.Move => moveInstance,
                Functions.Del => delInstance,
                Functions.WrtLine => wrtLineInstance,
                Functions.Write => writeInstance,
                Functions.PressKey => pressKeyInstance,
                Functions.EmailsGet => emailsGetInstance,
                Functions.FilesGet => filesGetInstance,
                Functions.Click => clickInstance,
                Functions.SaveAs => saveAsInstance,
                Functions.Sleep => sleepInstance,
                _ => null,
            };
        }

        public static void assertType(dynamic arg, Type type) {
            if (!arg.GetType().Equals(type)) {
                throw new AHILIllegalArgumentTypeException(arg, type);
            }
        }

        private class Run : Function {
            public string toPythonCode(List<dynamic> args) {
                if (args.Count == 0 || args.Count > 2) {
                    throw new AHILIncorrectArgumentsCountException(args.Count, "1 or 2");
                }
                args.ForEach(a => {
                    assertType(a, typeof(string));
                });

                string ahkCode = "Run ";

                if (args.Count > 1) {
                    ahkCode += args[1] + " ";
                }
                ahkCode += args[0];

                return AutomationHandler.AHKExecRaw(ahkCode);
            }
        }

        private class SwitchWindow : Function {
            public string toPythonCode(List<dynamic> args) {
                if (args.Count == 0 || args.Count > 1) {
                    throw new AHILIncorrectArgumentsCountException(args.Count, "1");
                }
                if (!args[0].GetType().Equals(typeof(string))) {
                    throw new AHILIllegalArgumentTypeException(args[0], typeof(string));
                }

                string pyCode = "";

                if (args.Count > 0) {
                    // assume first arg is a filename or process id to get the window
                    pyCode += $"win = {AHK}.win_get(title='{args[0]}')\n";
                }
                else {
                    // no args, get the window in focus
                    pyCode += $"win = {AHK}.active_window\n";
                }
                pyCode += "win.close()\n";

                return pyCode;
            }
        }

        private class Close : Function {
            public string toPythonCode(List<dynamic> args) {
                if (args.Count > 1) {
                    throw new AHILIncorrectArgumentsCountException(args.Count, "0 or 1");
                }

                string pyCode = "";

                if (args.Count > 0) {
                    // assume first arg is a filename or process id to get the window
                    assertType(args[0], typeof(string));
                    Console.WriteLine("here");
                    pyCode += $"win = {AHK}.win_get(title='{args[0]}')\n";
                } else {
                    // no args, get the window in focus
                    pyCode += $"win = {AHK}.active_window\n";
                }
                pyCode += "win.close()\n";

                return pyCode;
            }
        }

        private class Create : Function {
            public string toPythonCode(List<dynamic> args) {
                string output = "Create";

                output = AutomationHandler.AHKExecRaw(output);
                return output;
            }
        }

        private class Save : Function {
            public string toPythonCode(List<dynamic> args) {
                string output = "Save";

                output = AutomationHandler.AHKExecRaw(output);
                return output;
            }
        }

        private class Move : Function {
            public string toPythonCode(List<dynamic> args) {
                string output = "Move";

                output = AutomationHandler.AHKExecRaw(output);
                return output;
            }
        }

        private class Del : Function {
            public string toPythonCode(List<dynamic> args) {
                string output = "Del";

                output = AutomationHandler.AHKExecRaw(output);
                return output;
            }
        }

        private class WrtLine : Function {
            public string toPythonCode(List<dynamic> args) {
                string output = "SendText ";

                for (int i = 0; i < args.Count(); i++) {
                    output += args[i].ToString();
                }

                output += "\n" +
                    "SendText `n";

                output = AutomationHandler.AHKExecRaw(output);
                return output;
            }
        }

        private class Write : Function {
            public string toPythonCode(List<dynamic> args) {
                string output = "SendText ";

                for (int i = 0; i < args.Count(); i++) {
                    output += args[i].ToString();
                }

                output = AutomationHandler.AHKExecRaw(output);
                return output;
            }
        }

        private class PressKey : Function {
            public string toPythonCode(List<dynamic> args) {
                string output = "Send ";

                for (int i = 0; i < args.Count(); i++) {
                    output += args[i].ToString();
                }

                output = AutomationHandler.AHKExecRaw(output);
                return output;
            }
        }

        private class EmailsGet : Function {
            public string toPythonCode(List<dynamic> args) {
                string output = "GetEmails ";

                for (int i = 0; i < args.Count(); i++) {
                    output += args[i].ToString();
                }

                output = AutomationHandler.AHKExecRaw(output);
                return output;
            }
        }

        private class FilesGet : Function {
            public string toPythonCode(List<dynamic> args) {
                string output = "GetFiles ";

                for (int i = 0; i < args.Count(); i++) {
                    output += args[i].ToString();
                }

                output = AutomationHandler.AHKExecRaw(output);
                return output;
            }
        }

        private class MoveMouse : Function {
            public string toPythonCode(List<dynamic> args) {
                string output = "MoveMouse ";

                for (int i = 0; i < args.Count(); i++) {
                    output += args[i].ToString();
                }

                output = AutomationHandler.AHKExecRaw(output);
                return output;
            }
        }

        private class Click : Function {
            public string toPythonCode(List<dynamic> args) {
                string output = "Click ";

                for (int i = 0; i < args.Count(); i++) {
                    output += args[i].ToString();
                }

                output = AutomationHandler.AHKExecRaw(output);
                return output;
            }
        }

        private class SaveAs : Function {
            public string toPythonCode(List<dynamic> args) {
                string output = "SaveAs ";

                for (int i = 0; i < args.Count(); i++) {
                    output += args[i].ToString();
                }

                output = AutomationHandler.AHKExecRaw(output);
                return output;
            }
        }

        private class Sleep : Function {
            public string toPythonCode(List<dynamic> args) {
                if (args.Count == 0 || args.Count > 1) {
                    throw new AHILIncorrectArgumentsCountException(args.Count, "1");
                }
                assertType(args[0], typeof(int));
                string pycode = $"time.sleep({args[0]}/1000)";

                return pycode;
            }
        }
    }

    public class AHILIncorrectArgumentsCountException : Exception {
        public AHILIncorrectArgumentsCountException() { }

        public AHILIncorrectArgumentsCountException(string message)
            : base($"Incorrect Arguments Count: {message}") { }

        public AHILIncorrectArgumentsCountException(int count, string expected)
            : base($"Incorrect Arguments Count: {count.ToString()}. Expected: {expected}") { }
    }

    public class AHILIllegalArgumentTypeException : Exception {
        public AHILIllegalArgumentTypeException() { }

        public AHILIllegalArgumentTypeException(string message) 
            : base($"Illegal Argument: {message}") { }

        public AHILIllegalArgumentTypeException(dynamic arg, Type expected)
            : base($"Illegal Argument: {arg.ToString()} of type {arg.GetType().ToString()}. Expected {expected.ToString()}") { }

    }
}

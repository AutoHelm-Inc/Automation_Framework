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
        private static readonly SavAs savAsInstance = new SavAs();
        private static readonly Sleep sleepInstance = new Sleep();

        public static Function? fromEnum(Functions? @enum) {
            return @enum switch {
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
                Functions.SavAs => savAsInstance,
                Functions.Sleep => sleepInstance,
                _ => null,
            };
        }

        private class Run : Function {
            public string toPythonCode(List<dynamic> args) {
                assertArgsCount(args.Count, 1, 2);
                args.ForEach(a => {
                    assertType(a, typeof(string));
                });

                string _filename = args[0];
                string? _program = args.Count == 2 ? args[1] : null;

                string ahkCode = "Run ";

                if (args.Count > 1) {
                    ahkCode += _program + " ";
                }
                ahkCode += _filename;

                return AutomationHandler.AHKExecRaw(ahkCode);
            }
        }

        private class SwitchWindow : Function {
            public string toPythonCode(List<dynamic> args) {
                assertArgsCount(args.Count, 1);
                assertType(args[0], typeof(string));
                
                string _filename = args[0];

                string pyCode = "";

                pyCode +=
                    $"win = {AHK}.win_wait(title='{_filename}',timeout=1)\n" +
                    "win.activate()\n";

                return pyCode;
            }
        }

        private class Close : Function {
            public string toPythonCode(List<dynamic> args) {
                assertArgsCount(args.Count, 0, 1);

                string? _filename = args.Count == 1 ? args[0] : null;

                string pyCode = "";

                if (args.Count > 0) {
                    // assume first arg is a filename or process id to get the window
                    assertType(args[0], typeof(string));
                    Console.WriteLine("here");
                    pyCode += $"win = {AHK}.win_wait(title='{_filename}', timeout=1)\n";
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
                assertArgsCount(args.Count, 1);
                assertType(args[0], typeof(string));

                string _str = args[0];
                string pyCode = "";

                pyCode +=
                    $"{AHK}.type(\"{_str}\\n\")";

                return pyCode;
            }
        }

        private class Write : Function {
            public string toPythonCode(List<dynamic> args) {
                assertArgsCount(args.Count, 1);
                assertType(args[0], typeof(string));

                string _str = args[0];
                string pyCode = "";

                pyCode +=
                    $"{AHK}.type(\"{_str}\")";

                return pyCode;
            }
        }

        private class PressKey : Function {
            public string toPythonCode(List<dynamic> args) {
                assertArgsCount(args.Count, 1);
                assertType(args[0], typeof(string));

                string keystrokes = args[0];
                string pyCode = "";

                pyCode +=
                    $"{AHK}.send_input(\"{keystrokes}\")";

                return pyCode;
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

        private class SavAs : Function {
            public string toPythonCode(List<dynamic> args) {
                string output = "SavAs ";

                for (int i = 0; i < args.Count(); i++) {
                    output += args[i].ToString();
                }

                output = AutomationHandler.AHKExecRaw(output);
                return output;
            }
        }

        private class Sleep : Function {
            public string toPythonCode(List<dynamic> args) {
                assertArgsCount(args.Count, 1);
                assertType(args[0], typeof(int));
                string pycode = $"time.sleep({args[0]}/1000)";

                return pycode;
            }
        }

        public static void assertType(dynamic arg, Type expected) {
            if (!arg.GetType().Equals(expected)) {
                throw new AHILIllegalArgumentTypeException(arg, expected);
            }
        }

        public static void assertArgsCount(int count, int lower, int upper) {
            if (count < lower || count > upper) {
                throw new AHILIncorrectArgumentsCountException(count, $"{lower}-{upper}");
            }
        }

        public static void assertArgsCount(int count, int expected) {
            if (count != expected) {
                throw new AHILIncorrectArgumentsCountException(count, $"{expected}");
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

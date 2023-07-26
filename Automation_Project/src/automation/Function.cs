using Automation_Project.src.ast;
using static Automation_Project.src.ast.Constants;
using System.Diagnostics;

namespace Automation_Project.src.automation {
    /// <summary>
    /// Interface each AHIL Function must implement.
    /// </summary>
    public interface Function {
        /// <summary>
        /// Generate Python code for this AHIL Function.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public string toPythonCode(List<dynamic> args);
    }

    /// <summary>
    /// Main purpose is to convert Function Enums into Function class instances.
    /// </summary>
    public static class FunctionFactory {
        private static readonly Run _runInstance = new Run();
        private static readonly SwitchWindow _switchWindowInstance = new SwitchWindow();
        private static readonly Close _closeInstance = new Close();
        private static readonly FileCreate _fileCreateInstance = new FileCreate();
        private static readonly DirCreate _dirCreateInstance = new DirCreate();
        private static readonly Save _saveInstance = new Save();
        private static readonly Move _moveInstance = new Move();
        private static readonly Del _delInstance = new Del();
        private static readonly WrtLine _wrtLineInstance = new WrtLine();
        private static readonly Write _writeInstance = new Write();
        private static readonly PressKey _pressKeyInstance = new PressKey();
        private static readonly EmailsGet _emailsGetInstance = new EmailsGet();
        private static readonly FilesGet _filesGetInstance = new FilesGet();
        private static readonly MouseMove _mouseMoveInstance = new MouseMove();
        private static readonly Click _clickInstance = new Click();
        private static readonly SavAs _savAsInstance = new SavAs();
        private static readonly Sleep _sleepInstance = new Sleep();

        public static Function? fromEnum(Functions? @enum) {
            return @enum switch {
                Functions.Run => _runInstance,
                Functions.SwitchWindow => _switchWindowInstance,
                Functions.Close => _closeInstance,
                Functions.FileCreate => _fileCreateInstance,
                Functions.DirCreate => _dirCreateInstance,
                Functions.Save => _saveInstance,
                Functions.Move => _moveInstance,
                Functions.Del => _delInstance,
                Functions.WrtLine => _wrtLineInstance,
                Functions.Write => _writeInstance,
                Functions.PressKey => _pressKeyInstance,
                Functions.EmailsGet => _emailsGetInstance,
                Functions.FilesGet => _filesGetInstance,
                Functions.MouseMove => _mouseMoveInstance,
                Functions.Click => _clickInstance,
                Functions.SavAs => _savAsInstance,
                Functions.Sleep => _sleepInstance,
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

                string ahkCode = "";
                string pyCode = "";

                ahkCode += "Run ";
                if (args.Count > 1) {
                    ahkCode += _program + " ";
                }
                ahkCode += _filename;
                pyCode = AutomationHandler.AHKRunScriptWrapper(ahkCode) + "\n";
                pyCode += "time.sleep(0.5)";

                return pyCode;
            }
        }

        private class SwitchWindow : Function {
            public string toPythonCode(List<dynamic> args) {
                assertArgsCount(args.Count, 1);
                assertType(args[0], typeof(string));
                
                string _filename = args[0];

                string pyCode = "";

                pyCode +=
                    $"win = {AHK}.win_wait(title=rf'{_filename}',timeout=1)\n" +
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
                    pyCode += $"win = {AHK}.win_wait(title=rf\"{_filename}\", timeout=1)\n";
                } else {
                    // no args, get the window in focus
                    pyCode += $"win = {AHK}.active_window\n";
                }
                pyCode += "win.close()\n";

                return pyCode;
            }
        }

        private class FileCreate : Function {
            public string toPythonCode(List<dynamic> args) {
                assertArgsCount(args.Count, 1);
                assertType(args[0], typeof(string));

                string _filepath = args[0];

                string pyCode = "";

                pyCode +=
                    "try:\n" +
                    $"\topen(rf\"{_filepath}\", 'x')\n" +
                    "except FileExistsError:\n" +
                    "\tpass";

                return pyCode;
            }
        }

        private class DirCreate : Function {
            public string toPythonCode(List<dynamic> args) {
                assertArgsCount(args.Count, 1);
                assertType(args[0], typeof(string));

                string _dirpath = args[0];

                string pyCode = "";

                pyCode +=
                    "try:\n" +
                    $"\tos.mkdir(rf\"{_dirpath}\")\n" +
                    "except FileExistsError:\n" +
                    "\tpass";

                return pyCode;
            }
        }

        private class Save : Function {
            public string toPythonCode(List<dynamic> args) {
                assertArgsCount(args.Count, 0, 1);
                if (args.Count == 1) {
                    assertType(args[0], typeof(string));
                }

                string? _filename = args.Count == 1 ? args[0] : null;

                string pyCode = "";

                if (_filename != null) {
                    pyCode +=
                        $"win = {AHK}.win_wait(title=rf'{_filename}',timeout=1)\n" +
                        "win.activate()\n";
                }

                pyCode +=
                    $"{AHK}.send_input(\"^s\")\n";


                return pyCode;
            }
        }

        private class Move : Function {
            public string toPythonCode(List<dynamic> args) {
                assertArgsCount(args.Count, 2);
                args.ForEach(a => {
                    assertType(a, typeof(string));
                });

                string _srcpath = args[0];
                string _destpath = args[1];

                string pyCode = "";

                pyCode +=
                    $"srcpath = r\"{_srcpath}\"\n" +
                    $"destpath = r\"{_destpath}\"\n" +
                    "try:\n" +
                    "\tshutil.move(srcpath, destpath)\n" +
                    "except:\n" +
                    "\tpass";

                return pyCode;
            }
        }

        private class Del : Function {
            public string toPythonCode(List<dynamic> args) {
                assertArgsCount(args.Count, 1);
                assertType(args[0], typeof(string));

                string _path = args[0];

                string pyCode = "";

                pyCode +=
                    $"path = r\"{_path}\"\n" +
                    $"if (os.path.isfile(path)):\n" +
                    "\tos.remove(path)\n" +
                    "elif (os.path.isdir(path)):\n" +
                    "\tshutil.rmtree(path)";

                return pyCode;
            }
        }

        private class WrtLine : Function {
            public string toPythonCode(List<dynamic> args) {
                assertArgsCount(args.Count, 1);
                assertType(args[0], typeof(string));

                string _str = args[0];

                string pyCode = "";

                pyCode +=
                    $"{AHK}.type(f\"{_str}\\n\")";

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
                    $"{AHK}.type(f\"{_str}\")";

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
                throw new NotImplementedException("EmailsGet not implemented");

                //assertArgsCount(args.Count, 1);
                //assertType(args[0], typeof(string));

                //string _email = args[0];


                //string pyCode = "";

                //return pyCode;
            }
        }

        private class FilesGet : Function {
            public string toPythonCode(List<dynamic> args) {
                throw new NotImplementedException("FilesGet not implemented");
                //assertArgsCount(args.Count, 1);
                //assertType(args[0], typeof(string));

                //string _dirpath = args[0];

                //string pyCode = "";

                //return pyCode;
            }
        }

        private class MouseMove : Function {
            public string toPythonCode(List<dynamic> args) {
                assertArgsCount(args.Count, 2, 3);
                assertType(args[0], typeof(int));
                assertType(args[1], typeof(int));
                if (args.Count == 3) { 
                    assertType(args[2], typeof(string));
                }

                int _x = args[0];
                int _y = args[1];
                string _relative = args.Count == 3 ? args[2] : "False";

                string pyCode = "";

                pyCode +=
                    $"{AHK}.mouse_move({_x.ToString()}, {_y.ToString()}, relative={_relative})";

                return pyCode;
            }
        }

        private class Click : Function {
            public string toPythonCode(List<dynamic> args) {
                int? _x = null; 
                int? _y = null;
                string _button = "L";
                assertArgsCount(args.Count, 0, 3);
                if (args.Count == 1) {
                    assertType(args[0], typeof(string));
                    _button = args[0];
                } 
                if (args.Count > 1) {
                    assertType(args[1], typeof(int));
                    assertType(args[0], typeof(int));
                    _x = args[0];
                    _y = args[1];
                }
                if (args.Count == 3) {
                    assertType(args[2], typeof(string));
                    _button = args[2];
                }

                string pyCode = $"{AHK}.click(";

                if (_x != null & _y != null) {
                    pyCode += $"{_x.ToString()}, {_y.ToString()}, ";
                }
                pyCode += $"button=\"{_button}\")\n";

                return pyCode;
            }
        }

        private class SavAs : Function {
            public string toPythonCode(List<dynamic> args) {
                assertArgsCount(args.Count, 1);
                assertType(args[0], typeof(string));

                string _filepath = args[0];

                string pyCode = "";

                pyCode +=
                    $"{AHK}.send_input(\"^+s\")\n" +
                    $"{AHK}.win_wait(title=\"Save As\", timeout=1)\n" +
                    $"{AHK}.type(rf\"{_filepath}\")\n" +
                    $"{AHK}.send_input(\"{{Enter}}\")\n" +
                    "try:\n" +
                    $"\t{AHK}.win_wait(title=\"Confirm Save As\", timeout=1)\n" +
                    $"\t{AHK}.send_input(\"{{Left}}\")\n" +
                    $"\t{AHK}.send_input(\"{{Enter}}\")\n" +
                    $"except TimeoutError:\n" +
                    $"\tpass";

                return pyCode;
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

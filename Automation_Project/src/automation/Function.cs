using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Automation_Project.src.ast;

namespace Automation_Project.src.automation {

    public interface Function {
        public string toWindowsCode(List<dynamic> args);
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

        public static Function? fromEnum(Functions @enum) {
            return @enum switch {
                Functions.None => null,
                Functions.Run => runInstance,
                Functions.SwitchWindow => switchWindowInstance,
                Functions.Close => closeInstance,
                Functions.Create => createInstance,
                Functions.Save => saveAsInstance,
                Functions.Move => moveInstance,
                Functions.Del => delInstance,
                Functions.WrtLine => wrtLineInstance,
                Functions.Write => writeInstance,
                Functions.PressKey => pressKeyInstance,
                Functions.EmailsGet => emailsGetInstance,
                Functions.FilesGet => filesGetInstance,
                Functions.Click => clickInstance,
                Functions.SaveAs => saveAsInstance,
                _ => null,
            };
        }

        private class Run : Function {
            public string toWindowsCode(List<dynamic> args) {
                string output = "Run ";

                if (args.Count > 1) {
                    output += args[1].ToString() + " ";
                }
                output += args[0].ToString();

                output = AutomationHandler.withAHKWrapper(output);
                return output;
            }
        }

        private class SwitchWindow : Function {
            public string toWindowsCode(List<dynamic> args) {
                string output = "SwitchWindow";

                output = AutomationHandler.withAHKWrapper(output);
                return output;
            }
        }

        private class Close : Function {
            public string toWindowsCode(List<dynamic> args) {
                string output = "Close";

                output = AutomationHandler.withAHKWrapper(output);
                return output;
            }
        }

        private class Create : Function {
            public string toWindowsCode(List<dynamic> args) {
                string output = "Create";

                output = AutomationHandler.withAHKWrapper(output);
                return output;
            }
        }

        private class Save : Function {
            public string toWindowsCode(List<dynamic> args) {
                string output = "Save";

                output = AutomationHandler.withAHKWrapper(output);
                return output;
            }
        }

        private class Move : Function {
            public string toWindowsCode(List<dynamic> args) {
                string output = "Move";

                output = AutomationHandler.withAHKWrapper(output);
                return output;
            }
        }

        private class Del : Function {
            public string toWindowsCode(List<dynamic> args) {
                string output = "Del";

                output = AutomationHandler.withAHKWrapper(output);
                return output;
            }
        }

        private class WrtLine : Function {
            public string toWindowsCode(List<dynamic> args) {
                string output = "SendText ";

                for (int i = 0; i < args.Count(); i++) {
                    output += args[i].ToString();
                }

                output += "\n" +
                    "SendText `n";

                output = AutomationHandler.withAHKWrapper(output);
                return output;
            }
        }

        private class Write : Function {
            public string toWindowsCode(List<dynamic> args) {
                string output = "SendText ";

                for (int i = 0; i < args.Count(); i++) {
                    output += args[i].ToString();
                }

                output = AutomationHandler.withAHKWrapper(output);
                return output;
            }
        }

        private class PressKey : Function {
            public string toWindowsCode(List<dynamic> args) {
                string output = "Send ";

                for (int i = 0; i < args.Count(); i++) {
                    output += args[i].ToString();
                }

                output = AutomationHandler.withAHKWrapper(output);
                return output;
            }
        }

        private class EmailsGet : Function {
            public string toWindowsCode(List<dynamic> args) {
                string output = "GetEmails ";

                for (int i = 0; i < args.Count(); i++) {
                    output += args[i].ToString();
                }

                output = AutomationHandler.withAHKWrapper(output);
                return output;
            }
        }

        private class FilesGet : Function {
            public string toWindowsCode(List<dynamic> args) {
                string output = "GetFiles ";

                for (int i = 0; i < args.Count(); i++) {
                    output += args[i].ToString();
                }

                output = AutomationHandler.withAHKWrapper(output);
                return output;
            }
        }

        private class Click : Function {
            public string toWindowsCode(List<dynamic> args) {
                string output = "Click ";

                for (int i = 0; i < args.Count(); i++) {
                    output += args[i].ToString();
                }

                output = AutomationHandler.withAHKWrapper(output);
                return output;
            }
        }

        private class SaveAs : Function {
            public string toWindowsCode(List<dynamic> args) {
                string output = "SaveAs ";

                for (int i = 0; i < args.Count(); i++) {
                    output += args[i].ToString();
                }

                output = AutomationHandler.withAHKWrapper(output);
                return output;
            }
        }
    }
}

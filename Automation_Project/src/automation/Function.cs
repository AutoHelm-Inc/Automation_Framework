using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation_Project.src.ast;

namespace Automation_Project.src.automation {
    public abstract class Function {

        public abstract string toWindowsCode(List<dynamic> args);

        public static Function? fromEnum(Functions @enum) {
            return @enum switch {
                Functions.None => null,
                Functions.Run => Run.getInstance(),
                _ => null,
            };
        }
    }

    public sealed class Run : Function{
        private Run() { }

        private static Run? instance;

        public static Run getInstance() {
            if (instance == null) {
                instance = new Run();
            }
            return instance;
        }

        public override string toWindowsCode(List<dynamic> args) {
            string output = "Run ";

            if (args.Count > 1) {
                output += args[1].ToString() + " ";
            }
            output += args[0].ToString();

            output = AutomationHandler.withAHKWrapper(output);
            return output;
        }
    }

    public static class SwitchWindow {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "SwitchWindow";

            output = AutomationHandler.withAHKWrapper(output);
            return output;
        }
    }

    public static class Close {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "Close";

            output = AutomationHandler.withAHKWrapper(output);
            return output;
        }
    }

    public static class Create {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "Create";

            output = AutomationHandler.withAHKWrapper(output);
            return output;
        }
    }

    public static class Save {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "Save";

            output = AutomationHandler.withAHKWrapper(output);
            return output;
        }
    }

    public static class Move {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "Move";

            output = AutomationHandler.withAHKWrapper(output);
            return output;
        }
    }

    public static class Del {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "Del";

            output = AutomationHandler.withAHKWrapper(output);
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

            output = AutomationHandler.withAHKWrapper(output);
            return output;
        }
    }

    public static class Write {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "SendText ";

            for (int i = 0; i < args.Count(); i++) {
                output += args[i].ToString();
            }

            output = AutomationHandler.withAHKWrapper(output);
            return output;
        }
    }

    public static class PressKey {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "Send ";

            for (int i = 0; i < args.Count(); i++) {
                output += args[i].ToString();
            }

            output = AutomationHandler.withAHKWrapper(output);
            return output;
        }
    }

    public static class EmailsGet {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "GetEmails ";

            for (int i = 0; i < args.Count(); i++) {
                output += args[i].ToString();
            }

            output = AutomationHandler.withAHKWrapper(output);
            return output;
        }
    }

    public static class FilesGet {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "GetFiles ";

            for (int i = 0; i < args.Count(); i++) {
                output += args[i].ToString();
            }

            output = AutomationHandler.withAHKWrapper(output);
            return output;
        }
    }

    public static class Click {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "Click ";

            for (int i = 0; i < args.Count(); i++) {
                output += args[i].ToString();
            }

            output = AutomationHandler.withAHKWrapper(output);
            return output;
        }
    }

    public static class SaveAs {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "SaveAs ";

            for (int i = 0; i < args.Count(); i++) {
                output += args[i].ToString();
            }

            output = AutomationHandler.withAHKWrapper(output);
            return output;
        }
    }
}

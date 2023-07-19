using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Project.src.ast {
    public static class AutomationFunctions {
        const string AHKWrapperInstanceName = "ahk";

        public static string withCSharpWrapper(string code) {
            return $"{AHKWrapperInstanceName}.ExecRaw(\"{code}\")";
        }
    }
    public static class Run {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "run ";

            for (int i = 0; i < args.Count(); i++) {
                output += args[i].ToString();
            }

            output = AutomationFunctions.withCSharpWrapper(output);
            return output;
        }
    }

    public static class SwitchWindow {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "SwitchWindow";

            output = AutomationFunctions.withCSharpWrapper(output);
            return output;
        }
    }

    public static class Close {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "Close";

            output = AutomationFunctions.withCSharpWrapper(output);
            return output;
        }
    }
}

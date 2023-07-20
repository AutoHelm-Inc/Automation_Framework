﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Project.src.ast {
    public static class AutomationFunctions {
        const string AHKWrapperInstanceName = "ahk";

        public static string withAHKWrapper(string code) {
            return $"{AHKWrapperInstanceName}.ExecRaw(\"{code}\");";
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
    }
    public static class Run {
        public static string toWindowsCode(List<dynamic> args) {
            string output = "Run ";

            for (int i = 0; i < args.Count(); i++) {
                output += args[i].ToString();
            }

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
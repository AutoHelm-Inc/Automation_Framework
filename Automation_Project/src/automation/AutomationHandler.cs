using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using static Automation_Project.src.ast.Constants;

namespace Automation_Project.src.automation {
    public class AutomationHandler {
        private static string? pythonSourceLocation;
        private string? pythonScriptLocation;
        private static string pythonImports =
            "from ahk import AHK\n" +
            "import time\n" +
            "import os\n" +
            "import shutil\n";

        public AutomationHandler() {
            this.pythonScriptLocation = null;
            if (pythonSourceLocation == null) {
                pythonSourceLocation = findPythonSource();
            }
        }

        private static string findPythonSource() {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo("cmd.exe", "/c where python") {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return output;
        }

        public static string testPythonProgram =
            "print(\"Hello world!\")";

        public static string AHKExecRaw(string code) {
            return $"{AHK}.run_script(r\"{code}\")";
        }

        public static string formatAsPythonFile(string code) {
            string output =
                $"{pythonImports}" +
                "\n" +
                $"{AHK} = AHK()\n" +
                "\n" +
                $"{code}\n";
            return output;
        }

        public static string indentCode(string code, int n) {
            string[] splitByLine = code.Trim().Split("\n");
            for (int i = 0; i < splitByLine.Length; i++) {
                splitByLine[i] = $"{String.Concat(Enumerable.Repeat("\t", n))}{splitByLine[i]}";
            }
            return String.Join("\n", splitByLine);
        }

        //public static string linux

        public void saveToFile(string code) {
            string workingDirectory = System.Environment.CurrentDirectory;
            string? binDirectory = Directory.GetParent(workingDirectory)?.Parent?.FullName;
            if (binDirectory == null) {
                Console.WriteLine("failed to find bin directory");
                return;
            }
            string outputDirectory = Path.Combine(binDirectory, "automation");
            if (!File.Exists(outputDirectory)) {
                Directory.CreateDirectory(outputDirectory);
            }
            string fileName = "out.py";
            string outputPath = Path.Combine(outputDirectory, fileName);

            using (StreamWriter writer = new StreamWriter(outputPath)) {
                writer.Write(code);
            }
            Console.WriteLine("saved to: " + outputPath);
            pythonScriptLocation = outputPath;
        }

        public bool execute() {
            if (pythonScriptLocation == null || pythonSourceLocation == null) {
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

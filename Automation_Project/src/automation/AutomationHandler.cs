using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace Automation_Project.src.automation {
    public class AutomationHandler {
        const string AHKWrapperInstanceName = "ahk";
        public static string pythonSourceLocation = @"C:\Users\HansW\AppData\Local\Programs\Python\Python39\python.exe";
        private string? pythonScriptLocation;

        public AutomationHandler() {
            this.pythonScriptLocation = null;
        }

        public static string testPythonProgram =
            "print(\"Hello world!\")";

        public static string withAHKWrapper(string code) {
            return $"{AHKWrapperInstanceName}.run_script(\"{code}\")";
        }

        public static string formatAsPythonFile(string code) {
            string output =
                "from ahk import AHK\n" +
                "\n" +
                $"{AHKWrapperInstanceName} = AHK()\n" +
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

        public void saveToFile(string code) {
            string workingDirectory = System.Environment.CurrentDirectory;
            string? binDirectory = Directory.GetParent(workingDirectory)?.Parent?.FullName;
            if (binDirectory == null) {
                Console.WriteLine("failed to find bin directory");
                return;
            }
            string outputDirectory = Path.Combine(binDirectory, "automation");
            string fileName = "out.py";
            string outputPath = Path.Combine(outputDirectory, fileName);

            using (StreamWriter writer = new StreamWriter(outputPath)) {
                writer.Write(code);
            }
            Console.WriteLine("saved to: " + outputPath);
            pythonScriptLocation = outputPath;
        }

        public bool execute() {
            if (pythonScriptLocation == null) {
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

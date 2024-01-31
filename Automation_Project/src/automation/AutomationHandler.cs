using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

using static Automation_Project.src.ast.Constants;

namespace Automation_Project.src.automation
{
    public class AutomationHandler
    {
        private static string? pythonSourceLocation;
        private string? pythonScriptLocation;
        private Process? workflowProcess;

        private static string pythonImports =
            "from ahk import AHK\n" +
            "import time\n" +
            "import os\n" +
            "import shutil\n";

        public AutomationHandler()
        {
            this.pythonScriptLocation = null;
            if (pythonSourceLocation == null)
            {
                pythonSourceLocation = findPythonSource();
            }
        }

        /// <summary>
        /// Find the location of Python source.
        /// </summary>
        /// <returns></returns>
        private static string findPythonSource()
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo("cmd.exe", "/c where python")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            p.Start();

            // Read only 1 line of output incase multiple installations of python are found
            string? output = p.StandardOutput.ReadLine();
            p.WaitForExit();
            if (output == null)
            {
                output = "";
            }
            return output;
        }

        public static string testPythonProgram =
            "print(\"Hello world!\")";

        /// <summary>
        /// Wrap code into commonly used ahk.run_script() call.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string AHKRunScriptWrapper(string code)
        {
            return $"{AHK}.run_script(rf\"{code}\")";
        }

        /// <summary>
        /// Format code into a python program with imports and AHK instance declaration.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string formatAsPythonFile(string code)
        {
            string output =
                $"{pythonImports}" +
                "\n" +
                $"{AHK} = AHK()\n" +
                $"{AHK}.set_coord_mode('Mouse', 'Screen')\n" +
                "\n" +
                $"{code}\n";
            return output;
        }

        /// <summary>
        /// Indent code n times with tabs.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string indentCodeByTabs(string code, int n)
        {
            string[] splitByLine = code.Trim().Split("\n");
            for (int i = 0; i < splitByLine.Length; i++)
            {
                splitByLine[i] = $"{String.Concat(Enumerable.Repeat("\t", n))}{splitByLine[i]}";
            }
            return String.Join("\n", splitByLine);
        }

        /// <summary>
        /// Indent code n times using numSpaces spaces.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="n"></param>
        /// <param name="numSpaces"></param>
        /// <returns></returns>
        public static string indentCodeBySpaces(string code, int n, int numSpaces = 4)
        {
            string spaces = String.Concat(Enumerable.Repeat(" ", numSpaces));
            string[] splitByLine = code.Trim().Split("\n");
            for (int i = 0; i < splitByLine.Length; i++)
            {
                splitByLine[i] = $"{String.Concat(Enumerable.Repeat(spaces, n))}{splitByLine[i]}";
            }
            return String.Join("\n", splitByLine);
        }

        /// <summary>
        /// Save generated python code to bin.
        /// </summary>
        /// <param name="code"></param>
        public bool saveToFile(string code)
        {
            // Find bin directory
            string workingDirectory = System.Environment.CurrentDirectory;
            string? binDirectory = Directory.GetParent(workingDirectory)?.Parent?.FullName;
            if (binDirectory == null)
            {
                Console.WriteLine("failed to find bin directory");
                return false;
            }

            // Find or make output directory within /bin
            string outputDirectory = Path.Combine(binDirectory, "automation");
            if (!File.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }
            string fileName = "out.py";
            string outputPath = Path.Combine(outputDirectory, fileName);

            // Write to output file
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                writer.Write(code);
            }
            Console.WriteLine("saved to: " + outputPath);
            pythonScriptLocation = outputPath;

            return true;
        }

        /// <summary>
        /// Execute generated python code in a new process.
        /// </summary>
        /// <returns></returns>
        public bool execute()
        {
            if (pythonScriptLocation == null)
            {
                return false;
            }
            if (pythonSourceLocation == null)
            {
                pythonSourceLocation = findPythonSource();
            }
            if (pythonSourceLocation.Equals(""))
            {
                throw new Exception("Cannot find Python on this system");
            }


            Console.WriteLine("Executing generated automation script");

            // Run generated python script with a new c# process
            workflowProcess = new Process();
            workflowProcess.StartInfo = new ProcessStartInfo(pythonSourceLocation, '"' + pythonScriptLocation + '"')
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            workflowProcess.Start();

            // Capture output and errors to c# console
            //commented out for now so the autohelm process isnt blocked while a workflow is run
            // TODO make in new thread

            //string output = workflowProcess.StandardOutput.ReadToEnd();
            //string errors = workflowProcess.StandardError.ReadToEnd();

            //workflowProcess.WaitForExit();

            //Console.WriteLine(output);
            //Console.WriteLine(errors);

            return true;
        }

        public void killWorkflow()
        {
            if (workflowProcess != null)
            {
                workflowProcess.Kill();
            }
            else
            {
                Console.WriteLine("No workflow to kill :(");
            }
        }
    }
}

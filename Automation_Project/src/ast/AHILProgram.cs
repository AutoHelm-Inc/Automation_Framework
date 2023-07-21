using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoHotkey.Interop;

namespace Automation_Project.src.ast
{
    public class AHILProgram{
        private static AutoHotkeyEngine ahk = AutoHotkeyEngine.Instance;
        private List<StatementAbstract> statements;

        public AHILProgram(){
            this.statements = new List<StatementAbstract>();
        }

        public AHILProgram(List<StatementAbstract> statements){
            this.statements = statements;
        }

        public string generateProgramAHILCode(){
            string programAHILCode = "";

            for(int i =  0; i < statements.Count; i++){
                programAHILCode += statements[i].toAHILCode();
            }

            return programAHILCode;
        }

        public string generateAutomationCode() {
            string platform = getPlatform();
            return platform switch {
                "Windows" => toWindowsCode(),
                _ => "",
            };
        }

        private string toWindowsCode() {
            string windowsCode = "";

            for (int i = 0; i < statements.Count; i++) {
                windowsCode += statements[i].toWindowsCode();
            }
            return AutomationFunctions.formatAsCSharpFile(windowsCode);
        }

        private string getPlatform() {
            // just returns "Windows" for now but in the future can include system checks to include "MacOS" or "Linux" or "Web" etc.
            return "Windows";
        }

        // Compile and run the generated automation code
        public void execute() {
            //string code = generateAutomationCode();
            string code = AutomationFunctions.testProgram;
            string? outputFile = AutomationFunctions.compileToFile(code);
            if (outputFile != null) { 
                Console.WriteLine("Automation executable saved to:");
                Console.WriteLine("  " + outputFile);
            }
        }

        public List<StatementAbstract> getStatements(){
            return statements;
        }

        public void setStatements(List<StatementAbstract> statements){
            this.statements=statements;
        }

        public void addStatement(StatementAbstract statementToAdd){
            this.statements.Add(statementToAdd);
        }

        public void removeStatement(StatementAbstract statementToRemove){
            this.statements.Remove(statementToRemove);
        }

        public void removeStatementByIndex(int index){
            this.statements.RemoveAt(index);
        }

        public void removeLastStatement(){
            this.statements.RemoveAt(this.statements.Count() - 1);
        }
    }
}

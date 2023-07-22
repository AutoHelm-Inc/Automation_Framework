using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Automation_Project.src.automation;

namespace Automation_Project.src.ast
{
    public class AHILProgram{
        private List<StatementAbstract> statements;
        private AutomationHandler auto;

        public AHILProgram(){
            this.statements = new List<StatementAbstract>();
            this.auto = new AutomationHandler();
        }

        public AHILProgram(List<StatementAbstract> statements){
            this.statements = statements;
            this.auto = new AutomationHandler();
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
                "Windows" => toPythonCode(),
                _ => "",
            };
        }

        private string toPythonCode() {
            string code = "";

            for (int i = 0; i < statements.Count; i++) {
                code += statements[i].toPythonCode();
            }
            return AutomationHandler.formatAsPythonFile(code);
        }

        private string getPlatform() {
            // just returns "Windows" for now but in the future can include system checks to include "MacOS" or "Linux" or "Web" etc.
            return "Windows";
        }

        // Compile and run the generated automation code
        public void saveToFile() {
            string code = generateAutomationCode();
            auto.saveToFile(code);
        }

        public bool execute() {
            return auto.execute();
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

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

        /// <summary>
        /// Generate automation code for this AHIL program depending on the platform (Windows, Web, MacOS, Linux etc.)
        /// </summary>
        /// <returns></returns>
        public string generateAutomationCode() {
            string platform = getPlatform();
            return platform switch {
                "Windows" => toPythonCode(),
                _ => "",
            };
        }

        /// <summary>
        /// Generate Python and AutoHotKey code for automating on Windows.
        /// </summary>
        /// <returns></returns>
        private string toPythonCode() {
            string code = "";

            for (int i = 0; i < statements.Count; i++) {
                code += statements[i].toPythonCode();
            }
            return AutomationHandler.formatAsPythonFile(code);
        }

        /// <summary>
        /// Get the platform this code is running on. 
        /// Just returns "Windows" for now but in the future can include system checks to include "MacOS" or "Linux" or "Web" etc.
        /// </summary>
        /// <returns></returns>
        private string getPlatform() { 
            return "Windows";
        }

        /// <summary>
        /// Generate automation code and save it to a file.
        /// </summary>
        public void saveToFile() {
            string code = generateAutomationCode();
            if (!auto.saveToFile(code)) {
                throw new Exception("Failed to save generated file");
            }
        }

        /// <summary>
        /// Call on the AutomationHandler to execute the automation code.
        /// </summary>
        /// <returns></returns>
        public void execute() {
            if (!auto.execute()) {
                throw new Exception("Failed to execute generated code");
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

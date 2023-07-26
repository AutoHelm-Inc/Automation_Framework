using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Automation_Project.src.automation;

namespace Automation_Project.src.ast
{
    public class ForLoop : NestedStructure, Statement{
        private int repititionCount;

        public ForLoop() {
            this.repititionCount = 0;
        }

        public ForLoop(int repititionCount) {
            this.repititionCount = repititionCount;
        }

        public ForLoop(int repititionCount, List<Statement> statements) : base(statements) {
            this.repititionCount = repititionCount;
        }

        public string toAHILCode()
        {
            string AHILCodeInterpretation = "For (" + repititionCount.ToString() + "){\n";
            string body = "";
            for (int i = 0; i < statements.Count; i++)
            {
                body += statements[i].toAHILCode();
            }
            AHILCodeInterpretation += AutomationHandler.indentCodeBySpaces(body, 1) + "\n";
            AHILCodeInterpretation += "}\n";
            return AHILCodeInterpretation;
        }

        public string toPythonCode() {
            string pyCode = $"for i in range({repititionCount.ToString()}):\n";
            string body = "";
            for (int i = 0; i < statements.Count; i++) {
                body += $"{statements[i].toPythonCode()}";
            }
            pyCode += AutomationHandler.indentCodeBySpaces(body, 1) + "\n";
            pyCode += "\n";
            return pyCode;
        }

        public void setRepititionCount(int repititionCount){
            this.repititionCount = repititionCount;
        }

        public int getRepititionCount(){
            return repititionCount;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Project.src.ast
{
    public class ForLoop : StatementAbstract{
        private int repititionCount;
        private List<StatementAbstract> statements;

        public ForLoop(){
            this.repititionCount = 0;
            this.statements = new List<StatementAbstract>();
        }

        public ForLoop(int repititionCount){
            this.statements = new List<StatementAbstract>();
            this.repititionCount = repititionCount;
        }

        public ForLoop(int repititionCount, List<StatementAbstract> statements){
            this.statements = statements;
            this.repititionCount = repititionCount;
        }

        public override string toAHILCode()
        {
            string AHILCodeInterpretation = "For (" + repititionCount.ToString() + "){\n";
            for (int i = 0; i < statements.Count; i++)
            {
                AHILCodeInterpretation += "  " + statements[i].toAHILCode();
            }
            AHILCodeInterpretation += "}\n";
            return AHILCodeInterpretation;

        }

        public override string toCSharpOrAHK(){
            return "";
        }

        public void setRepititionCount(int repititionCount){
            this.repititionCount = repititionCount;
        }

        public int getRepititionCount(){
            return repititionCount;
        }

        public List<StatementAbstract> getStatements(){
            return statements;
        }

        public void setStatements(List<StatementAbstract> statements){
            this.statements = statements;
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
            this.statements.RemoveAt(this.statements.Count()-1);
        }
    }
}

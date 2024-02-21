using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Project.src.ast {
    public abstract class NestedStructure {
        protected List<Statement> statements;

        protected NestedStructure() {
            statements = new List<Statement>();
        }
        protected NestedStructure(List<Statement> statements) {
            this.statements = statements;
        }

        public List<Statement> getStatements() {
            return statements;
        }

        public void setStatements(List<Statement> statements) {
            this.statements = statements;
        }

        public void addStatement(Statement statementToAdd) {
            this.statements.Add(statementToAdd);
        }

        public bool removeStatement(Statement statementToRemove) {
            return statements.Remove(statementToRemove);
        }

        public void removeStatementByIndex(int index) {
            this.statements.RemoveAt(index);
        }

        public void removeLastStatement() {
            this.statements.RemoveAt(this.statements.Count() - 1);
        }

        public List<NestedStructure> GetNestedStructures() {
            return statements.OfType<NestedStructure>().ToList();
        }

        public bool removeStatementRecursive(Statement statement) {
            if (removeStatement(statement)) {
                return true;
            }
            foreach (NestedStructure ns in GetNestedStructures()) {
                if (ns.removeStatementRecursive(statement)) {
                    return true;
                }
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Project.src.ast
{
    public abstract class StatementAbstract{
        public abstract string toAHILCode();
        public abstract string toWindowsCode();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Project.src.ast
{
    public class Macro
    {
        private MacroKeyword? keyword;
        private List<dynamic> arguments;
        public Macro(MacroKeyword? keyword)
        {
            this.keyword = keyword;
            this.arguments = new List<dynamic>();
        }
        public Macro(MacroKeyword? keyword, dynamic argument)
        {
            this.keyword = keyword;
            this.arguments = new List<dynamic> { argument };
        }

        public Macro(MacroKeyword? keyword, List<dynamic> arguments)
        {
            this.keyword = keyword;
            this.arguments = arguments;
        }

        public void addArgument(dynamic argument)
        {
            arguments.Add(argument);
        }

        public MacroKeyword? getKeyword()
        {
            return keyword;
        }

        public List<dynamic> getArguments()
        {
            return arguments;
        }

        public string toAHILCode()
        {
            string AHILCodeInterpretation = keyword.ToString() + " ";
            for (int i = 0; i < arguments.Count; i++)
            {
                if (i > 0)
                {
                    AHILCodeInterpretation += ", ";
                }

                if (arguments[i] is string)
                {
                    AHILCodeInterpretation += "\"" + arguments[i] + "\"";
                }
                else if ((arguments[i] is int) || (arguments[i] is double))
                {
                    AHILCodeInterpretation += ((int)arguments[i]).ToString();
                }
            }
            AHILCodeInterpretation += "\n";
            return AHILCodeInterpretation;
        }

    }
}

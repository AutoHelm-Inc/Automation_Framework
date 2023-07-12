using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Project.src.ast{

    public class SimpleStatement : StatementAbstract{
        private Functions function;
        private List<dynamic>arguments;

        public SimpleStatement(Functions function)
        {
            this.function = function;
            this.arguments = new List<dynamic>();
        }

        public SimpleStatement(Functions function, dynamic argument)
        {
            this.function = function;
            this.arguments = new List<dynamic>();
            addArgument(argument);
        }

        public SimpleStatement(Functions function, List<dynamic> arguments){
            this.function = function;
            this.arguments = arguments;
        }

        public void addArgument(dynamic argumentToAdd){
            this.arguments.Add(argumentToAdd);
        }

        public void removeArgument(dynamic argumentToRemove){
            this.arguments.Remove(argumentToRemove);
        }

        public void removeArgumentByIndex(int index){
            this.arguments.RemoveAt(index);
        }

        public void removeLastArgument(){
            this.arguments.RemoveAt(this.arguments.Count() - 1);
        }

        public override string toAHILCode(){
            string AHILCodeInterpretation = function.ToString() + " ";
            for(int i = 0; i < arguments.Count; i++){
                if (i > 0){
                    AHILCodeInterpretation += ", ";
                }

                if (arguments[i] is string){
                    AHILCodeInterpretation += "\"" + arguments[i] + "\"";
                }else if ((arguments[i] is int) || (arguments[i] is double)){
                    AHILCodeInterpretation += ((int)arguments[i]).ToString();
                }
            }
            AHILCodeInterpretation += "\n";
            return AHILCodeInterpretation;
        }

        public override string toCSharpOrAHK(){
            return "";
        }
    }
}

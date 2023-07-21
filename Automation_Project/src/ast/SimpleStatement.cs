using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Automation_Project.src.automation;

namespace Automation_Project.src.ast{

    public class SimpleStatement : StatementAbstract{
        private Function? function;
        private Functions functionType;
        private List<dynamic> arguments;

        public SimpleStatement(Functions function)
        {
            this.function = Function.fromEnum(function);
            this.functionType = function;
            this.arguments = new List<dynamic>();
        }

        public SimpleStatement(Functions function, dynamic argument)
        {
            this.functionType = function;
            this.arguments = new List<dynamic>();
            addArgument(argument);
        }

        public SimpleStatement(Functions function, List<dynamic> arguments){
            this.functionType = function;
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
            string AHILCodeInterpretation = functionType.ToString() + " ";
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

        //public override string toWindowsCode(){
        //    string windowsCode = functionType switch {
        //        Functions.None => "",
        //        Functions.Run => Run.toWindowsCode(arguments),
        //        Functions.SwitchWindow => SwitchWindow.toWindowsCode(arguments),
        //        Functions.Close => Close.toWindowsCode(arguments),
        //        Functions.Create => Create.toWindowsCode(arguments),
        //        Functions.Save => Save.toWindowsCode(arguments),
        //        Functions.Move => Move.toWindowsCode(arguments),
        //        Functions.Del => Del.toWindowsCode(arguments),
        //        Functions.WrtLine => WrtLine.toWindowsCode(arguments),
        //        Functions.Write => Write.toWindowsCode(arguments),
        //        Functions.PressKey => PressKey.toWindowsCode(arguments),
        //        Functions.EmailsGet => EmailsGet.toWindowsCode(arguments),
        //        Functions.FilesGet => FilesGet.toWindowsCode(arguments),
        //        Functions.Click => Click.toWindowsCode(arguments),
        //        Functions.SaveAs => SaveAs.toWindowsCode(arguments),
        //        _ => "",
        //    };
        //    return windowsCode + "\n";
        //}

        public override string toWindowsCode() {
            if (function == null) {
                return "";
            }
            return function.toWindowsCode(arguments) + "\n";
        }
    }
}

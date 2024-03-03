using Automation_Project.src.ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Project.src.parser
{
    public class Parser
    {
        private Lexer lexer;
        private AHILProgram? ahilProgram;

        public Parser()
        {
            this.lexer = new Lexer();
        }

        public Parser(string fileName)
        {
            this.lexer = new Lexer(fileName);
        }

        public static Parser fromAHILCode(string AHILCode)
        {
            Parser parser = new Parser();
            parser.lexer = Lexer.fromAHILCode(AHILCode);
            return parser;
        }

        public AHILProgram parse()
        {
            return program();
        }

        AHILProgram program()
        {
            AHILProgram ahilProgram = new AHILProgram();
            this.ahilProgram = ahilProgram;
            while (lexer.inspect("#"))
            {
                lexer.consume("#");
                ahilProgram.addMacros(macro());
            }
            do
            {
                ahilProgram.addStatement(statements());
            } while (!lexer.inspectEOF());
            lexer.consumeEOF();
            return ahilProgram;
        }

        Macro macro() {
            MacroKeyword? macroKeyword = null;

            foreach (MacroKeyword keyword in Enum.GetValues(typeof(MacroKeyword)))
            {
                if (lexer.inspect(keyword.ToString()))
                {
                    string macroName = lexer.consume(keyword.ToString());
                    macroKeyword = (MacroKeyword)Enum.Parse(typeof(MacroKeyword), macroName);
                    break;
                }
            }

            Macro m = new Macro(macroKeyword);
            while (lexer.inspectString() || lexer.inspectNumber())
            {
                if (lexer.inspectString())
                {
                    m.addArgument(lexer.consumeString());
                }
                else
                {
                    m.addArgument(lexer.consumeNumber());
                }
                if (lexer.inspect(","))
                {
                    lexer.consume(",");
                }
                else
                {
                    break;
                }
            }
            return m;
        }

        Statement statements()
        {
            if (lexer.inspect("For"))
            {
                return forLoop();
            }
            else
            {
                return simpleStatement();
            }
        }

        ForLoop forLoop()
        {
            ForLoop forLoop = new ForLoop();
            lexer.consume("For");
            lexer.consume("(");
            forLoop.setRepititionCount(lexer.consumeNumber());
            lexer.consume(")");
            lexer.consume("{");
            while (!lexer.inspect("}"))
            {
                forLoop.addStatement(statements());
            }
            lexer.consume("}");
            return forLoop;
        }

        SimpleStatement simpleStatement()
        {

            Functions? function = null;

            foreach (Functions functions in Enum.GetValues(typeof(Functions)))
            {
                if (lexer.inspect(functions.ToString()))
                {
                    string functionName = lexer.consume(functions.ToString());
                    function = (Functions)Enum.Parse(typeof(Functions), functionName);
                    break;
                }
            }

            SimpleStatement simple = new SimpleStatement(function);
            simple.setAHILProgram(this.ahilProgram);
            while (lexer.inspectString() || lexer.inspectNumber())
            {
                if (lexer.inspectString())
                {
                    simple.addArgument(lexer.consumeString());
                }
                else
                {
                    simple.addArgument(lexer.consumeNumber());
                }
                if (lexer.inspect(","))
                {
                    lexer.consume(",");
                }
                else
                {
                    break;
                }
            }
            return simple;
        }

    }
}

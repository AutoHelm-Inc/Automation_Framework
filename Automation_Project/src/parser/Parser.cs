using Automation_Project.src.ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Project.src.parser
{
    public class Parser
    {
        private Lexer lexer;

        public Parser(string fileName)
        {
            this.lexer = new Lexer(fileName);
        }

        public AHILProgram parse()
        {
            return program();
        }

        AHILProgram program()
        {
            AHILProgram ahilProgram = new AHILProgram();
            do
            {
                ahilProgram.addStatement(statements());
            } while (!lexer.inspectEOF());
            lexer.consumeEOF();
            return ahilProgram;
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

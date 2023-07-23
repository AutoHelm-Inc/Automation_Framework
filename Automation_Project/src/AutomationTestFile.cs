using Automation_Project.src.ast;
using Automation_Project.src.parser;

public class AutomationTestFile
{
    static void Main(string[] args){
        //System.Console.WriteLine("AST Testing");

        //AHILProgram testProgram = new AHILProgram();
        //testProgram.addStatement(new SimpleStatement(Functions.Run, "Notepad.exe"));
        //testProgram.addStatement(new SimpleStatement(Functions.Click, 5));

        //ForLoop loop = new ForLoop(3);
        //loop.addStatement(new SimpleStatement(Functions.WrtLine, "Hello world"));
        //loop.addStatement(new SimpleStatement(Functions.Close, "Notepad.exe"));
        //testProgram.addStatement(loop);

        //testProgram.addStatement(new SimpleStatement(Functions.EmailsGet));
        //testProgram.removeLastStatement();

        //System.Console.WriteLine(testProgram.generateProgramAHILCode());

        //Lexer lex = new Lexer("../../../src/lexer/fydplex.txt");
        //System.Console.WriteLine("testing lexer...");
        //lex.tokenize();
        //lex.printTokens();
        //System.Console.WriteLine("Is open the next token? " + lex.inspect("Run"));
        //lex.consume("Run");
        //System.Console.WriteLine("Is filename a next token? " + lex.inspectString());
        //lex.consumeString();

        Parser parser = new Parser("../../../tests/functions/Save/Save.ahil");
        AHILProgram program = parser.parse();

        System.Console.WriteLine("----------------\n");
        System.Console.WriteLine(program.generateProgramAHILCode());
        System.Console.WriteLine("----------------\n");
        System.Console.WriteLine(program.generateAutomationCode());
        System.Console.WriteLine("----------------\n");

        program.saveToFile();
        program.execute();
    }
}
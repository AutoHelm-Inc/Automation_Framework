using Automation_Project.src.ast;

public class AutomationTestFile
{
    static void Main(string[] args){
        System.Console.WriteLine("AST Testing");

        AHILProgram testProgram = new AHILProgram();
        testProgram.addStatement(new SimpleStatement(Functions.Run, "Notepad.exe"));
        testProgram.addStatement(new SimpleStatement(Functions.Click, 5));
        
        ForLoop loop = new ForLoop(3);
        loop.addStatement(new SimpleStatement(Functions.WrtLine, "Hello world"));
        loop.addStatement(new SimpleStatement(Functions.Close, "Notepad.exe"));
        testProgram.addStatement(loop);

        testProgram.addStatement(new SimpleStatement(Functions.EmailsGet));
        testProgram.removeLastStatement();

        System.Console.WriteLine(testProgram.generateProgramAHILCode());

        Lexer lex = new Lexer("../../../src/lexer/fydplex.txt");
        System.Console.WriteLine("testing lexer...");
        lex.tokenize();
        lex.printTokens();
        System.Console.WriteLine("Is Run the next token? " + lex.inspect("Run"));
        if (lex.inspect("Run"))
        {
            lex.consume("Run");
        }
    }
}
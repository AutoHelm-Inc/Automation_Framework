using Automation_Project.src.ast;

public class AutomationTestFile
{
    static void Main(string[] args){
        System.Console.WriteLine("AST Testing");

        AHILProgram testProgram = new AHILProgram();
        testProgram.addStatement(new SimpleStatement(Functions.run, "Notepad.exe"));
        testProgram.addStatement(new SimpleStatement(Functions.click, 5));
        
        ForLoop loop = new ForLoop(3);
        loop.addStatement(new SimpleStatement(Functions.writeLine, "Hello world"));
        loop.addStatement(new SimpleStatement(Functions.close, "Notepad.exe"));
        testProgram.addStatement(loop);

        testProgram.addStatement(new SimpleStatement(Functions.getEmails));
        testProgram.removeLastStatement();

        System.Console.WriteLine(testProgram.generateProgramAHILCode());

        Lexer lex = new Lexer("../../../src/lexer/fydplex.txt");
        System.Console.WriteLine("testing lexer...");
        lex.tokenize();
        lex.printTokens();
        System.Console.WriteLine("Is open the next token? " + lex.inspect("open"));
        lex.consume("open");
        System.Console.WriteLine("Is filename a next token? " + lex.inspectString());
        lex.consumeString();
    }
}
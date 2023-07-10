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
    }
}
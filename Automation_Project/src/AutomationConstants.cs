using Automation_Project.src.ast;

namespace Automation_Project.src.ast
{
    public enum Functions
    {
        Run,
        SwitchWindow,
        Close,
        FileCreate,
        DirCreate,
        Save,
        Move,
        Del,
        WriteLine,
        Write,
        PressKey,
        EmailsGet,
        FilesGet,
        MouseMove,
        Click,
        SaveAs,
        Sleep
    };

    public enum Keywords
    {
        If,
        Elif,
        Else,
        For,
        True,
        False
    }

    public static class Constants {
        // Python AHK instance name
        public const string AHK = "ahk";
    }
}
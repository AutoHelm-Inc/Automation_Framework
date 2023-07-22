using Automation_Project.src.ast;

namespace Automation_Project.src.ast
{
    public enum Functions
    {
        None = 0,
        Run,
        SwitchWindow,
        Close,
        Create,
        Save,
        Move,
        Del,
        WrtLine,
        Write,
        PressKey,
        EmailsGet,
        FilesGet,
        MoveMouse,
        Click,
        SaveAs,
        Sleep,
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
﻿using Automation_Project.src.ast;

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
        //EmailsGet,
        //FilesGet,
        MouseMove,
        Click,
        SaveAs,
        Sleep,
        MouseToWord
    };

    public enum Keywords
    {
        //If,
        //Elif,
        //Else,
        For,
        //True,
        //False
    }

    public enum MacroKeyword
    {
        GlobalDelay
    }

    public static class Constants {
        // Python AHK instance name
        public const string AHK = "ahk";
    }
}
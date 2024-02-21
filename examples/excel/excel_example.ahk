Run "Excel.exe excel_example.xlsx",,, &excel_pid
WinWaitActive "ahk_pid " excel_pid

Send "^{Home}"
Send "{Right}"

Loop 5 {
    Send "{Down}"
    SendText "100"
}


Send "^s"
WinClose "excel_example.xlsx"
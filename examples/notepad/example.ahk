Run "Notepad",,, &notepad_pid
WinWaitActive "ahk_pid " notepad_pid

Loop 5 {
    SendText "Hello World!"
    SendText "`n"
}


Send "^s"
WinWaitActive "Save As"
Send "hello.txt"
Send "{Enter}"
WinWaitClose
WinClose "hello.txt - Notepad"
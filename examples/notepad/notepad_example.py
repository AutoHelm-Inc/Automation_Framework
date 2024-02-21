from ahk import AHK
import time

ahk = AHK()

ahk.run_script("Run Notepad.exe")
win = ahk.win_wait_active("Untitled - Notepad", timeout=1)
for i in range(5):
    ahk.type("hello world\n")
ahk.send_input("^s")
ahk.type(r"C:\Users\HansW\Desktop\Stuff\School\4A\ECE 498A\Automation_Framework\Automation_Project\tests\hello.txt")
ahk.send_input("{Enter}")
win = ahk.win_wait(title="hello.txt - Notepad", timeout=1)
win.close()
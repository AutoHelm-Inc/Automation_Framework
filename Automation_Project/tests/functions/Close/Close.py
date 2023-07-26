from ahk import AHK
import time

ahk = AHK()

ahk.run_script(r"Run Notepad.exe C:\Users\HansW\Desktop\Stuff\School\4A\ECE 498A\Automation_Framework\Automation_Project\tests\hello.txt")
time.sleep(1000/1000)
win = ahk.win_wait(title=r"hello.txt", timeout=1)
win.close()


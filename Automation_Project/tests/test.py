from ahk import AHK
import time
import os
import shutil

ahk = AHK()

ahk.run_script(rf"Run Notepad.exe C:\Users\Hans\Documents\Stuff\School\4A\ECE 498A\Automation_Framework\Automation_Project\tests\functions\Save\test1.txt")
time.sleep(0.5)
ahk.type(f"Hello!")
time.sleep(0.5)
ahk.send_input("^s")
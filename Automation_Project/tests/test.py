from ahk import AHK
import time
import os
import shutil

ahk = AHK()

ahk.run_script(r"Run C:\Users\HansW\Desktop\Stuff\School\4A\ECE 498A\Automation_Framework\Automation_Project\tests\hello.txt")


ahk.mouse_move(0, 1080, relative=False)
ahk.mouse_move(-1920, 0, relative=True)
ahk.click()
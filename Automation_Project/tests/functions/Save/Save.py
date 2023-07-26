from ahk import AHK
import time
import os
import shutil

ahk = AHK()

path = r"C:\Users\HansW\Desktop\Stuff\School\4A\ECE 498A\Automation_Framework\Automation_Project\tests\functions\Save\test1.txt"
if (os.path.isfile(path)):
	os.remove(path)
elif (os.path.isdir(path)):
	shutil.rmtree(path)
path = r"C:\Users\HansW\Desktop\Stuff\School\4A\ECE 498A\Automation_Framework\Automation_Project\tests\functions\Save\test2.txt"
if (os.path.isfile(path)):
	os.remove(path)
elif (os.path.isdir(path)):
	shutil.rmtree(path)
try:
	open(r"C:\Users\HansW\Desktop\Stuff\School\4A\ECE 498A\Automation_Framework\Automation_Project\tests\functions\Save\test1.txt", 'x')
except FileExistsError:
	pass
try:
	open(r"C:\Users\HansW\Desktop\Stuff\School\4A\ECE 498A\Automation_Framework\Automation_Project\tests\functions\Save\test2.txt", 'x')
except FileExistsError:
	pass
ahk.run_script(r"Run C:\Users\HansW\Desktop\Stuff\School\4A\ECE 498A\Automation_Framework\Automation_Project\tests\functions\Save\test1.txt")
ahk.run_script(r"Run C:\Users\HansW\Desktop\Stuff\School\4A\ECE 498A\Automation_Framework\Automation_Project\tests\functions\Save\test2.txt")
ahk.type("in test2\n")
time.sleep(500/1000)
win = ahk.win_wait(title='test1.txt - Notepad',timeout=1)
win.activate()

time.sleep(500/1000)
ahk.type("in test1\n")
time.sleep(500/1000)
ahk.send_input("^s")

win = ahk.win_wait(title='*test2.txt - Notepad',timeout=1)
win.activate()
ahk.send_input("^s")

win = ahk.win_wait(title=r"test1.txt - Notepad", timeout=1)
win.close()

win = ahk.win_wait(title=r"test2.txt - Notepad", timeout=1)
win.close()



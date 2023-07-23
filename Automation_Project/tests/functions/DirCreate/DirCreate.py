from ahk import AHK
import os

ahk = AHK()

try:
	os.mkdir(r"C:\Users\HansW\Desktop\Stuff\School\4A\ECE 498A\Automation_Framework\Automation_Project\tests\functions\DirCreate\test")
except FileExistsError:
	pass
try:
	open(r"C:\Users\HansW\Desktop\Stuff\School\4A\ECE 498A\Automation_Framework\Automation_Project\tests\functions\DirCreate\test\test.txt", 'x')
except FileExistsError:
	pass
ahk.run_script(r"Run C:\Users\HansW\Desktop\Stuff\School\4A\ECE 498A\Automation_Framework\Automation_Project\tests\functions\DirCreate\test\test.txt")


from ahk import AHK
import time
import os
import shutil

ahk = AHK()

try:
	os.mkdir(r"C:\Users\HansW\Desktop\Stuff\School\4A\ECE 498A\Automation_Framework\Automation_Project\tests\functions\Del\test")
except FileExistsError:
	pass
try:
	open(r"C:\Users\HansW\Desktop\Stuff\School\4A\ECE 498A\Automation_Framework\Automation_Project\tests\functions\Del\test\test.txt", 'x')
except FileExistsError:
	pass
time.sleep(2000/1000)
path = r"C:\Users\HansW\Desktop\Stuff\School\4A\ECE 498A\Automation_Framework\Automation_Project\tests\functions\Del\test\test.txt"
if (os.path.isfile(path)):
	os.remove(path)
elif (os.path.isdir(path)):
	shutil.rmtree(path)
path = r"C:\Users\HansW\Desktop\Stuff\School\4A\ECE 498A\Automation_Framework\Automation_Project\tests\functions\Del\test"
if (os.path.isfile(path)):
	os.remove(path)
elif (os.path.isdir(path)):
	shutil.rmtree(path)


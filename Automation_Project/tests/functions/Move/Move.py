from ahk import AHK
import time
import os
import shutil

ahk = AHK()

path = "c:/Users/HansW/Desktop/Stuff/School/4A/ECE 498A/Automation_Framework/Automation_Project/tests/functions/Move/dir1"
if (os.path.isfile(path)):
	os.remove(path)
elif (os.path.isdir(path)):
	shutil.rmtree(path)
try:
	os.mkdir("c:/Users/HansW/Desktop/Stuff/School/4A/ECE 498A/Automation_Framework/Automation_Project/tests/functions/Move/dir1")
except FileExistsError:
	pass
time.sleep(1000/1000)
try:
	os.mkdir("c:/Users/HansW/Desktop/Stuff/School/4A/ECE 498A/Automation_Framework/Automation_Project/tests/functions/Move/dir2")
except FileExistsError:
	pass
try:
	open("c:/Users/HansW/Desktop/Stuff/School/4A/ECE 498A/Automation_Framework/Automation_Project/tests/functions/Move/dir2/file.txt", 'x')
except FileExistsError:
	pass
time.sleep(1000/1000)
srcpath = "c:/Users/HansW/Desktop/Stuff/School/4A/ECE 498A/Automation_Framework/Automation_Project/tests/functions/Move/dir2"
destpath = "c:/Users/HansW/Desktop/Stuff/School/4A/ECE 498A/Automation_Framework/Automation_Project/tests/functions/Move/dir1"
try:
	shutil.move(srcpath, destpath)
except:
	pass
time.sleep(1000/1000)
path = "c:/Users/HansW/Desktop/Stuff/School/4A/ECE 498A/Automation_Framework/Automation_Project/tests/functions/Move/dir1"
if (os.path.isfile(path)):
	os.remove(path)
elif (os.path.isdir(path)):
	shutil.rmtree(path)


from ahk import AHK

ahk = AHK()

try:
	open(r"C:\Users\HansW\Desktop\Stuff\School\4A\ECE 498A\Automation_Framework\Automation_Project\tests\functions\FileCreate\test.txt", 'x')
except FileExistsError:
	pass
ahk.run_script(r"Run C:\Users\HansW\Desktop\Stuff\School\4A\ECE 498A\Automation_Framework\Automation_Project\tests\functions\FileCreate\test.txt")


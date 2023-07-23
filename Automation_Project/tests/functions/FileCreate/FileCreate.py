from ahk import AHK

ahk = AHK()

try:
	open("c:/Users/HansW/Desktop/Stuff/School/4A/ECE 498A/Automation_Framework/Automation_Project/tests/functions/FileCreate/test.txt", 'x')
except:
	pass
ahk.run_script("Run c:/Users/HansW/Desktop/Stuff/School/4A/ECE 498A/Automation_Framework/Automation_Project/tests/functions/FileCreate/test.txt")


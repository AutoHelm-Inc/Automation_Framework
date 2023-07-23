from ahk import AHK
import time

ahk = AHK()

ahk.run_script("Run Notepad.exe c:/Users/HansW/Desktop/Stuff/School/4A/ECE 498A/Automation_Framework/Automation_Project/tests/hello.txt")
ahk.run_script("Run Notepad.exe c:/Users/HansW/Desktop/Stuff/School/4A/ECE 498A/Automation_Framework/Automation_Project/tests/hello2.txt")
win = ahk.win_wait("hello.txt - Notepad", timeout=1)
win2 = ahk.win_wait("hello2.txt - Notepad", timeout=1)

win.activate()
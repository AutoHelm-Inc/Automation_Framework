from ahk import AHK
import time

ahk = AHK()

ahk.run_script("Run Notepad.exe c:/Users/HansW/Desktop/Stuff/School/4A/ECE 498A/Automation_Framework/Automation_Project/tests/hello.txt")
time.sleep(500/1000)
ahk.run_script("Run Notepad.exe c:/Users/HansW/Desktop/Stuff/School/4A/ECE 498A/Automation_Framework/Automation_Project/tests/hello2.txt")
time.sleep(500/1000)
win = ahk.win_wait(title='hello.txt - Notepad',timeout=1)
win.activate()

time.sleep(500/1000)
win = ahk.win_wait(title='hello2.txt - Notepad', timeout=1)
win.close()
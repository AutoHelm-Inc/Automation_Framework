from ahk import AHK

ahk = AHK()

ahk.run_script("Run Notepad.exe \"c:/Users/HansW/Desktop/Stuff/School/4A/ECE 498A/Automation_Framework/Automation_Project/tests/hello.txt\"")
win = ahk.win_get(title='hello.txt')
win.close()
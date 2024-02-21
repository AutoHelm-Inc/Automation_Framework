from ahk import AHK
ahk = AHK()

ahk.run_script(r"Run Notepad.exe")
ahk.type("Hello!")
ahk.send_input("^+s")
ahk.win_wait(title="Save As", timeout=1)
ahk.type(r"C:\Users\HansW\Desktop\Stuff\School\4A\ECE 498A\Automation_Framework\Automation_Project\tests\functions\SavAs\saved.txt")
ahk.send_input("{Enter}")
try:
	ahk.win_wait(title="Confirm Save As", timeout=1)
	ahk.send_input("{Left}")
	ahk.send_input("{Enter}")
except TimeoutError:
	pass


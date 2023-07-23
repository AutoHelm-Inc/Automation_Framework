from ahk import AHK
import time

ahk = AHK()

ahk.run_script("Run Notepad.exe")
for i in range(5):
	ahk.type("Testing for loop\n")



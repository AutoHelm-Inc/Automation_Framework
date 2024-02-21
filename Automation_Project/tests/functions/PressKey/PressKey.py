from ahk import AHK
import time

ahk = AHK()

ahk.run_script("Run Notepad.exe")
ahk.type("PressKey Demo\n")
ahk.type("Find this string\n")
ahk.type("hello!\n")
ahk.send_input("^f")
time.sleep(500/1000)
ahk.type("find")
time.sleep(500/1000)
ahk.send_input("{Enter}")
time.sleep(500/1000)
ahk.send_input("{Esc}")
ahk.send_input("{Esc}")


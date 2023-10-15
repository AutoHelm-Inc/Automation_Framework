from ahk import AHK
import time
import os
import shutil

ahk = AHK()

ahk.run_script(rf"Run Wordpad.exe")
time.sleep(0.5)
ahk.type(f"Hello!")
time.sleep(0.5)
ahk.send_input("^s")
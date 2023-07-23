from ahk import AHK
import time
import os
import shutil

ahk = AHK()

ahk.mouse_move(0, 1080, relative=False)
ahk.mouse_move(-1920, 0, relative=True)
ahk.click()
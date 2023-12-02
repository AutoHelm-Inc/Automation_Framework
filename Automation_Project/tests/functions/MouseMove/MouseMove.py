from ahk import AHK
import time

ahk = AHK()

ahk.mouse_move(960, 540, relative=False)
time.sleep(500/1000)
ahk.mouse_move(100, 100, relative=False)
time.sleep(500/1000)
ahk.mouse_move(1820, 100, relative=False)
time.sleep(500/1000)
ahk.mouse_move(1820, 980, relative=False)
time.sleep(500/1000)
ahk.mouse_move(100, 980, relative=False)
time.sleep(500/1000)
ahk.mouse_move(0, 0, relative=False)
time.sleep(500/1000)
ahk.mouse_move(10, 10, relative=True)


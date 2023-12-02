from ahk import AHK
import time

ahk = AHK()

ahk.mouse_move(900, 500, relative=False)
ahk.click(button="L")

time.sleep(500/1000)
ahk.click(900, 500, button="R")

ahk.click(button="L")



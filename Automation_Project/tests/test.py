from ahk import AHK
import time
import os
import shutil

ahk = AHK()

windows = ahk.list_windows()

# for win in windows:
#     print(win.get_title())

win1 = ahk.win_wait(title="*test1.txt", timeout=1)
win2 = ahk.win_wait(title="*test2.txt", timeout=1)

print(win1)
print(win2)
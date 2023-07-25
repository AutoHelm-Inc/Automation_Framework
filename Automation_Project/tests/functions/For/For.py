from ahk import AHK

ahk = AHK()

ahk.run_script(r"Run Notepad.exe")
for i in range(5):
    for i in range(2):
        ahk.type("Testing for loop\n")



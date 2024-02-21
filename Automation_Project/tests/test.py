from ahk import AHK
import time
import os
import shutil

import sys
sys.path.append('../src/ocr')
from ocr_controller import get_coords_of_word

ahk = AHK()

print(get_coords_of_word("pythonOCR", False))
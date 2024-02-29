import pytesseract
import cv2 
from PIL import ImageGrab
import Levenshtein
import os

#OCR parameters
image_resize_scale = 3
kernel_size = 1
sigma_size = 0
threshold_block_size = 31
threshold_c = 0.03
screenshot_save_name = "\\screenshot.png"
save_image_filename = "\\Grey.png"
custom_config = """--dpi 96 --psm 6 -c tessedit_char_blacklist=*|&^%$#~<>\\"\\'"""

#static install location
pytesseract.pytesseract.tesseract_cmd = 'C:/Program Files/Tesseract-OCR/tesseract.exe'
save_location = os.path.abspath(os.path.join(os.path.dirname( __file__ ), "..", "..")) + "\\bin\\ocr"

#-----------------------------------------------------------INTERNAL FUNCTIONS-----------------------------------------------------------------------------
#Take screenshot of entire screen
def take_screenshot():
    if not os.path.exists(save_location):
        os.makedirs(save_location)
    path = save_location + screenshot_save_name
    screenshot = ImageGrab.grab()
    screenshot.save(path)
    screenshot.close()
    return path

#pull an image from the specified filepath
def get_image(file_path):
    image = cv2.imread(file_path)
    return image

#converts out color image to gray scale
def get_greyscale(color_image):
    return cv2.cvtColor(color_image, cv2.COLOR_BGR2GRAY)

#digitally upscale the image for better text recognition
def resize_image(image):
    return cv2.resize(image, (image.shape[1] * image_resize_scale, image.shape[0] * image_resize_scale), interpolation = cv2.INTER_CUBIC)

#blur the image to reduce noise
def image_blurring(image):
    return cv2.GaussianBlur(image, (kernel_size, kernel_size), sigma_size, sigma_size, 0)

#use grey_scaled image to adaptively threshold
def thresholding(grey_image, is_dark_mode):
    if not is_dark_mode:
        return cv2.adaptiveThreshold(grey_image ,255, cv2.ADAPTIVE_THRESH_GAUSSIAN_C, cv2.THRESH_BINARY, threshold_block_size , threshold_c)
    else:
        return cv2.adaptiveThreshold(grey_image ,255, cv2.ADAPTIVE_THRESH_GAUSSIAN_C, cv2.THRESH_BINARY_INV, threshold_block_size , threshold_c)

#save an array into an image
def save_image(image):
    cv2.imwrite(save_location + save_image_filename, image)

#set whitelist of characters for OCR
def set_whitelist(whitelist_string):
    whitelist = whitelist_string

#function to perform ocr and get the text from an image (image should be in greyscale)
def perform_ocr(image):
    return pytesseract.image_to_string(image, lang="eng", config=custom_config)

#function to find the pixel location of a given word in an image after OCR (should be in greyscale)
def find_word_coords(image, word):
    
    closest_word = ""
    closest_distance = 0
    x_coord = 0
    y_coord = 0
    word = word.lower()
    
    data = pytesseract.image_to_data(image, output_type=pytesseract.Output.DICT, config=custom_config)

    for i in range(0, len(data["text"])):
        #code to print all words and their bounding boxes
        current_word = data["text"][i].lower()
        current_distance = Levenshtein.ratio(current_word, word)

        if(word == current_word):
            current_distance = 2
        elif (word in current_word):
            current_distance = 2 - current_distance - (abs(len(current_word) - len(word))/100)

        if (current_distance > closest_distance):
            closest_distance = current_distance
            closest_word = data["text"][i]
            x_coord = (data["left"][i] + (data["width"][i]/2)) / image_resize_scale
            y_coord = (data["top"][i] + (data["height"][i]/2)) / image_resize_scale
            #print(data["text"][i] + "=> x: " + str(data["left"][i]) + " y: " +  str(data["top"][i]))
    
    return (closest_word, closest_distance, x_coord, y_coord)

#-----------------------------------------------------------EXTERNAL FUNCTIONS-----------------------------------------------------------------------------
#given an input of a specified word and whether you want dark/light OCR, the coordinates of the closest matching word on your screen will be returned
def get_coords_of_word(word, is_dark_mode):
    path = take_screenshot()
    img = get_image(path)
    img = get_greyscale(img)
    img = resize_image(img)
    img = image_blurring(img)
    img = thresholding(img, is_dark_mode)
    save_image(img)
    ret = find_word_coords(img, word)
    return ret

#given an input of a specified word and whether you want dark/light OCR, the coordinates of the closest matching word on your image will be returned
def get_coords_of_word_from_image(word, path, is_dark_mode):
    img = get_image(path)
    img = get_greyscale(img)
    img = resize_image(img)
    img = image_blurring(img)
    img = thresholding(img, is_dark_mode)
    save_image(img)
    ret = find_word_coords(img, word)
    return ret

#given an image path, the text found through ocr will be returned
def image_to_text(path, is_dark_mode):
    img = get_image(path)
    img = get_greyscale(img)
    img = resize_image(img)
    img = image_blurring(img)
    img = thresholding(img, is_dark_mode)
    save_image(img)
    ret = perform_ocr(img)
    return ret

![application icon](https://cdn-ak.f.st-hatena.com/images/fotolife/e/espio999/20211223/20211223212918.png)

# 1-Push-Snap
With single push of the Print Screen button, take a screenshot and output the image file of the current window or screen.

Usual step as Alt + Ctrl + Print Screen, and Ctrl + C to Paint is nore required.

## Requirement
.NET Framework 4.8

## Feature
### 1 push screenshot
Just pushing Printscreen key, and screenshot of the active window is saved in the preferred folder.

This tool monitors keyboard event.  Finding key down event of Print Screen button, 

1. it takes screenshot
2. it makes it preferred image file as BMP, GIF, JPG or PNG.
3. it saves it to the preferred folder.

The initial save folder is the home of user (%userprofile%).  
If the specified folder doesn't exist when screenshot is saved, an image file is saved to the initial save folder.

### Disable input from keyboard and mouse
This if for
- vacuum a keyboard
- polish a mouse, trackball and trackpad, etc
- brush suport rollers and balls inside trackball

## Usage - Context menu
This tool has no form, and is stationed in Taskbar.  
Context menu is displayed with right click.  
![context menu](https://cdn-ak.f.st-hatena.com/images/fotolife/e/espio999/20220203/20220203212554.png)

### Information
The current save folder is indicated.  
Screenshot image files are saved here.

![menu item - Information](https://cdn-ak.f.st-hatena.com/images/fotolife/e/espio999/20220112/20220112230454.jpg)

### Save To...
User can change the save folder here.  
![menu item - Save To...](https://cdn-ak.f.st-hatena.com/images/fotolife/e/espio999/20220112/20220112230501.jpg)

### Image format
User can change the image format.  
![menu item - Image format](https://cdn-ak.f.st-hatena.com/images/fotolife/e/espio999/20220112/20220112230451.jpg)

### Start - 1 Push screenshot
The tool starts keyboard event monitoring.  
During monitoring,
- taskbar icon turn into orange.
- pushing Printscreen key takes screenshot.
- pushing Pause key stops keyboard event monitoring.
-- taskbar icon turn into white.

### Start - Ignore key type
Disable inputs from a keyboard.  
Inputs from a mouse is available.

### Start - Ignore key type
Disable inputs from a mouse.
Inputs from a keybaord is aviable.

### Stop
Cancel Start tasks above.

### Close
Close the tool, and the taskbar icon is disappeared.

## Reference
[Technically Impossible - 1 Push Snap](https://impsbl.hatenablog.jp/archive/category/1%20Push%20Snap)

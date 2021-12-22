# 1-Push-Snap
Take a screenshot and output the image file of the current window or screen with single push of the Print Screen button.

## Requirement
.NET Framework 4.8

## Feature
Push PrtScn button, and screenshot of the active window is saved in JPEG format to the specified folder.

This tool monitors keyboard event.  Finding key down event of Print Screen button, it takes screenshot and saves it as JPEG file to the folder specified in advance.
The initial save folder is the home of user (%userprofile%).  If the specified folder doesn't exist when screenshot is saved, an image file is saved to the initial save folder.

## Usage - Context menu
This tool has no form, and is stationed in Taskbar.  
Context menu is displayed with right click.  
![context menu](https://cdn-ak.f.st-hatena.com/images/fotolife/e/espio999/20211219/20211219220240.png)

### Information
The current save folder is indicated.  
Screenshot image files are saved here.  
![menu item - Information](https://cdn-ak.f.st-hatena.com/images/fotolife/e/espio999/20211219/20211219220315.png)

### Save To...
User can change the save folder here.  
![menu item - Save To...](https://cdn-ak.f.st-hatena.com/images/fotolife/e/espio999/20211219/20211219220332.png)

### Start
The tool starts keyboard event monitoring.  
During monitoring, taskbar icon turn into orange.  
![menu item - Start](https://cdn-ak.f.st-hatena.com/images/fotolife/e/espio999/20211219/20211219220345.png)

### Stop
The tool stops keyboard event monitoring.  
Taskbar bar icon turn into black.  
![menu item - Start](https://cdn-ak.f.st-hatena.com/images/fotolife/e/espio999/20211219/20211219220407.png)

### Close
Close the tool, and the taskbar icon is disappeared.

## Reference
[Technically Impossible - 1 Push Snap](https://impsbl.hatenablog.jp/entry/1PushSnap)

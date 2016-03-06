# bounds-test
An "on screen ruler" utility program for Windows.

Homepage: http://www.jonschneider.com/utilities.html

This utility is useful to roughly gauge the size and position of items on the screen, like a simple on-screen ruler. The program's window background is partially transparent; by resizing it to fit over an item on the screen, an approximation of the item's size and position in pixels (corresponding to the program window's own size and position) is displayed.

The utility also shows information on the bounds of the screen, corresponding to the WorkingArea property of the .NET Framework's System.Windows.Forms.Screen class. These properties will update as the utility is dragged between different monitors in a system with multiple monitors.

The application window can be resized by dragging any corner or edge, and repositioned by dragging anywhere on the window other than the edges.

Tic marks are shown every 25 pixels from the top and left edges of the window. Larger tic marks are shown every 100 pixels. 

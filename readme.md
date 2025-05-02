A simple Hanko application
==============================
This is a simple hanko application configured by a text file.


The configuration file
--------------
- Windows `.ini` style configuration,
    but allow multiple option keys.
- the configuration file is named `hanko.ini` by default.
    also can be specified from the command line argument.

```
# format
# [section]
# option = value
[section1]
include = 99, _aaa
text = abc, 12, 50, 50
text = bottom, 12, 50, 80

[_aaa]
line = 1, 0, 0, 100, 100
``` 

- the below table is options and parameters

option    | parameters
----------|-------------------------
[text]    | text to show, font size, center-x, center-y
[date]    | 0, font size, center-x, center-y
[line]    | 0, x1, y1, x2, y2
[circle]  | 0, center-x, center-y, diameter
[include] | 99, section name

[text]:    #option-text
[date]:    #option-date
[line]:    #option-line
[circle]:  #option-circle
[include]: #option-include


#### option: text
- specify the text, font size, center x and y.
- see [Configs.parse_text] and [HankoDraw.draw_text]


#### option: date
- specify the font size, center x and y.
- see [Configs.parse_date] and [HankoDraw.draw_date]


#### option: line
- specify the number (not used), left, top, right and bottom position.
- see [Configs.parse_line] and [HankoDraw.draw_line]


#### option: circle
- specify the number (not used), center x, y and diameter.
- see [Configs.parse_circle] and [HankoDraw.draw_circle]


#### option: include
- specify the number (not used), section name to insert.
- see [Configs.parse_include] and [HankoDraw.draw_include]


---


Todo
-----------------------------------------
- specify the fonts.
- specify the colors.
- specify the pen width.
- specify the date format.


---


License
-----------------------------------------
MIT, see LICENSE


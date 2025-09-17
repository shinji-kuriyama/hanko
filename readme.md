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
[date]    | 0, font size, center-x, center-y, date-time format
[line]    | 0, x1, y1, x2, y2
[circle]  | 0, center-x, center-y, diameter
[rotate]  | degree, x, y
[mode]    | mode
[include] | 99, section name

[text]:    #option-text
[date]:    #option-date
[line]:    #option-line
[circle]:  #option-circle
[rotate]:  #option-rotate
[mode]:    #option-mode
[include]: #option-include


#### option: text
- specify the text, font size, center x and y.
- see [Configs.parse_text] and [HankoDraw.draw_text]


#### option: date
- specify: `font size`, `center x`, `center y`, `date-time format`.
- see [DrawDateFormat.parse] and [DrawDateFormat.draw] in
    draws/dateformat.cs


#### option: line
- specify the number (not used), left, top, right and bottom position.
- see [Configs.parse_line] and [HankoDraw.draw_line]


#### option: circle
- specify the number (not used), center x, y and diameter.
- see [Configs.parse_circle] and [HankoDraw.draw_circle]


#### option: rotate
- specify:
    - 1. rotate degree (0-360)
    - 2. x ... adjustment x position to centerize.
    - 3. y ... adjustment y position to centerize.
- see [DrawRotate.parse] and [DrawRotate.draw] in draws/rotate.cs


#### option: mode
- specify: `mode`
- `mode` will be choosed from belows:
    `0`, `1`, `2`, `3`, `4`,
    `default` `highspeed`, `highquality`, `none`, `antialias`
    see [SmoothingMode](https://learn.microsoft.com/ja-jp/dotnet/api/system.drawing.drawing2d.smoothingmode)
    in MS Reference.
- see [DrawMode.parse] and [DrawMode.draw] in draws/config_modes.cs


#### option: include
- specify the number (not used), section name to insert.
- see [Configs.parse_include] and [HankoDraw.draw_include]


---


Todo
-----------------------------------------
- specify the fonts.
- specify the colors.
- specify the pen width.


---


License
-----------------------------------------
MIT, see LICENSE


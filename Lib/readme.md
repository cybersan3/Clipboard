# SygenicTemplateLib

- Works with SygenicTemplateConsole, SygenicTemplateXUnit and SygenicCommonLib, so:
- Start empty solution. 
- Add new project based on SygenicTemplateLib.
- Add dependency to SygenicCommonLib if current is wrong or missing
- Remove not needed DEL-ME-LOGO.png and README.MD examples
- Add new project based on SygenicTemplateConsole.
- Reference Lib project from Console
- Add global using [App]Lib; to Globals.cs
- Add new project based on SygenicTemplateXUnit.
- Reference Lib project from XUnit
- - Add global using [App]Lib; to Globals.cs
- Edit Lib/Globals.cs and change names of friendly projects
- Add nupkg Pastel, Newtonsoft to Lib
- Make readme.md "copy if newer" in Properties

# Rest is for removal - just examples of .MD syntaxt (including DEL-ME-LOGO.PNG file)

Files from Sygenic Console App Template:
- Settings/AppSettings.cs
- Settings/LastingSettings.cs
- appsettings.json
- appsettings.Machine.Han.json
- appsettings.User.CyberSan.json
- Const.cs
- DEL-ME-LOGO.png (to remove)
- EXAMPLE_SYGENIC_COMMON_USAGE.cs (to remove)
- Globals.cs
- Logic.cs
- Program.cs
- readme.md

# This is readme.md for AppName, press Shift+F7 for VS preview

Information

1. One
2. Two
    1. Two one
    2. Two two

- unordered list item one
- unordered list item two

## this is section with two hashtags

### section with 3 hashtags

#### section with 4 hashtags

##### section with 5 hashtags

normal text and *italic* and  **bold** and ***italic bold***

> and this is block quote
>> and inner block
> and block

and text after

![Del-me-logo](DEL-ME-LOGO.png)

something should be `code`

some text

    this is code
    this is code also
some text

line

---
after line

Some link [LINK NAME](https://duckduckgo.com "This is tooltip").

<https://www.markdownguide.org>
<fake@example.com>

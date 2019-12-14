### RedM External
This repository serves as an intermediary state as we work on integration into the RedM API.

### To-Dos
- Clean up classes to be more in-line with already existing External API in V
- Implement WeaponCollection
- Relabel Font Enum

### Set-Up
Recent Windows versions:
Go to your RedM directory, and from there navigate to citizen\clr2\lib\mono\4.5.
Drag and drop CitizenFX.Core.dll to "make-external-link.bat" - it will create symbolic links that will automatically update as you run RedM.

Older Windows versions:
If you are on older windows versions, the mklink command requires admin privileges, so open an admin command prompt in the repository directory. Shift+right click CitizenFX.Core.dll and 'copy as path'
In it, write `make-external-link.bat ` and paste the path you copied.

### License (MIT)
Copyright 2019 Mooshe

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge,
publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

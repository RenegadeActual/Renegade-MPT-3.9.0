@echo off
set outputFile=%1
set sourceDir=%2
set buildDir=%sourceDir%..\build
set bepinexDir=%buildDir%\BepInEx
set pluginsDir=%bepinexDir%\plugins

if exist %pluginsDir% (
    if exist %pluginsDir%\Renegade.Core.dll (
        del /f /q %pluginsDir%\Renegade.Core.dll
    )
)

if not exist %buildDir% (
    mkdir %buildDir%
)

if not exist %bepinexDir% (
    mkdir %bepinexDir%
)

if not exist %pluginsDir% (
    mkdir %pluginsDir%
)

copy %outputFile% %pluginsDir%\Renegade.Core.dll

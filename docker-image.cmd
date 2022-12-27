@for /f "delims=" %%x in (App\VERSION) do set version=%%x
@echo -- Budowanie obrazu konterera z tagiem latest i %version% --
call docker build -t kalkulator:latest App || goto error
call docker tag kalkulator:latest kalkulator:%version% || goto error
@REM --progress plain
call kind load docker-image kalkulator:%version% || goto error

@echo.
@echo -- Obraz zbudowany i zainstalowany w KinD --

@goto :EOF

:error
@echo Blad! %errorlevel%.
@exit /b %errorlevel%
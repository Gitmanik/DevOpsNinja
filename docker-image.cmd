@for /f "delims=" %%x in (App\VERSION) do set version=%%x
@echo Building Docker image version: %version%
docker build -t kalkulator:latest App\KalkulatorKredytowy
docker tag kalkulator:latest kalkulator:%version%
@REM --progress plain
kind load docker-image kalkulator:%version%
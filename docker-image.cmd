docker build -t kalkulator App\KalkulatorKredytowy
docker tag kalkulator:latest kalkulator:0.1.0 
REM --progress plain
kind load docker-image kalkulator:0.1.0 --name kkcluster
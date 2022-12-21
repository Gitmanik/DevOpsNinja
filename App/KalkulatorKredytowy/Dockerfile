FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ENV ASPNETCORE_URLS=http://+:80

WORKDIR /app
COPY . .

RUN dotnet restore

RUN dotnet build
WORKDIR /app/KalkulatorKredytowy
ENTRYPOINT dotnet run --no-launch-profile
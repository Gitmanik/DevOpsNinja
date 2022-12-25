FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore --verbosity n
RUN dotnet clean
RUN rm -rf KalkulatorKredytowy/obj
RUN rm -rf KalkulatorKredytowy/bin

RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT dotnet "KalkulatorKredytowy.dll"
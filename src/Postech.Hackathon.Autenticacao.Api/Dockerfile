#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5010
ENV ASPNETCORE_HTTP_PORTS=5010

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore src/Postech.Hackathon.Autenticacao.Api/Postech.Hackathon.Autenticacao.Api.csproj
RUN dotnet publish src/Postech.Hackathon.Autenticacao.Api/Postech.Hackathon.Autenticacao.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Postech.Hackathon.Autenticacao.Api.dll"]

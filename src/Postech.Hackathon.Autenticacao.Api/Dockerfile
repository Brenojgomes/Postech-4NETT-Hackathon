FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5010
ENV ASPNETCORE_URLS=http://+:5010
ENV ASPNETCORE_HTTP_PORTS=5010

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Postech.Hackathon.Autenticacao.Api/Postech.Hackathon.Autenticacao.Api.csproj", "src/Postech.Hackathon.Autenticacao.Api/"]
RUN dotnet restore "src/Postech.Hackathon.Autenticacao.Api/Postech.Hackathon.Autenticacao.Api.csproj"
COPY . .
WORKDIR "/src/src/Postech.Hackathon.Autenticacao.Api"
RUN dotnet build "Postech.Hackathon.Autenticacao.Api.csproj" -c "$configuration" -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Postech.Hackathon.Autenticacao.Api.csproj" \
    -c "$configuration" \
    -o /app/publish \
    /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Postech.Hackathon.Autenticacao.Api.dll"]

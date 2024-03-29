#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["xkcd-bot.csproj", "xkcd-bot/"]
RUN dotnet restore "xkcd-bot/xkcd-bot.csproj"

WORKDIR "/src/xkcd-bot"
COPY . .
RUN dotnet build "xkcd-bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "xkcd-bot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
VOLUME /app/config.json
ENTRYPOINT ["dotnet", "xkcd-bot.dll"]

# xkcd-bot
A simple Discord bot for posting xkcd comics written in C# and .NET 6.0
## How to use
### Manual
You need to have dotnet installed on your system<br/>
```shell
dotnet restore "xkcd-bot/xkcd-bot.csproj"
dotnet build "xkcd-bot.csproj" -c Release -o /app/build
dotnet publish "xkcd-bot.csproj" -c Release -o /app/publish /p:UseAppHost=false
cd xkcdc-bot/xkcd-bot/app/publish
```
Place the config.json file with your bot token in the same directory as the executable<br/>
You can find a template for the config.json in `xkcd-bot/xkcd-bot/config.json`
```shell
dotnet xkcd-bot.dll
```
### Docker/Podman
```shell
cd xkcd-bot/xkcd-bot
docker build -t xkcd-bot .
docker run -d -v /path/to/config.json:/app/config.json --name xkcd <image-id>
```

## Commands 
`[Prefix]` can be configured in config.json

`[Prefix]show <id>`&emsp;&emsp;Show specific xkcd.<br/>
`[Prefix]latest`&emsp;&emsp;&emsp;&ensp;Show latest xkcd comic.<br/>
`[Prefix]random`&emsp;&emsp;&emsp;&ensp;Show a random xkcd comic.<br/>
`[Prefix]help`&emsp;&emsp;&emsp;&emsp;&ensp;Show this help message.<br/>

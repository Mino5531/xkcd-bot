# xkcd-bot
A simple Discord bot for posting xkcd comics written in C# and .NET 6.0
## How to use
Add your Discord-API token in the `/xkcd-bot/config.json` file. Then you can either compile and deploy manually or use Docker.<br/>
When deploying manually, please make sure that the `config.json` is placed in the same directory as the executeable.

## Commands 
`[Prefix]` can be configured in config.json

`[Prefix]show <id>`&emsp;&emsp;Show specific xkcd.<br/>
`[Prefix]latest`&emsp;&emsp;&emsp;&ensp;Show latest xkcd comic.<br/>
`[Prefix]random`&emsp;&emsp;&emsp;&ensp;Show a random xkcd comic.<br/>
`[Prefix]help`&emsp;&emsp;&emsp;&emsp;&ensp;Show this help message.<br/>

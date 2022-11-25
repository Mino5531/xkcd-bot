using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Discord.Commands;
using Microsoft.Extensions.Configuration;

namespace xkcd_bot
{
    public class Program
    {

        public static Task Main(string[] args)
            => Startup.RunAsync(args);
    }
}
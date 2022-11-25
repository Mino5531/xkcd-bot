using Discord.Commands;
using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using xkcd_bot.Services;

namespace xkcd_bot
{
    public class Startup
    {
        public IConfiguration config { get; }

        public Startup(string[] args)
        {
            config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("config.json").Build();
        }

        public static async Task RunAsync(string[] args)
        {
            Startup startup= new(args);
            await startup.RunAsync();
        }

        public async Task RunAsync()
        {
            var provider = CreateServices();
            provider.GetRequiredService<CommandHandler>();
            provider.GetRequiredService<LoggingService>();
            provider.GetRequiredService<CommandHandler>();

            await provider.GetRequiredService<StartupService>().StartAsync();
            await Task.Delay(-1);
        }

        private IServiceProvider CreateServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
            {                                       // Add discord to the collection
                LogLevel = LogSeverity.Verbose,     // Tell the logger to give Verbose amount of info
                MessageCacheSize = 1000,             // Cache 1,000 messages per channel
                GatewayIntents = GatewayIntents.MessageContent | GatewayIntents.AllUnprivileged
            }))
            .AddSingleton(new CommandService(new CommandServiceConfig
            {                                       // Add the command service to the collection
                LogLevel = LogSeverity.Verbose,     // Tell the logger to give Verbose amount of info
                DefaultRunMode = RunMode.Async,     // Force all commands to run async by default
            }))
            .AddSingleton<LoggingService>()
            .AddSingleton<CommandHandler>()
            .AddSingleton<StartupService>()
            .AddSingleton(config);
            return services.BuildServiceProvider();
        }
    }
}

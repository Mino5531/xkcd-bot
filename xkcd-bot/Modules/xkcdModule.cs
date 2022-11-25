using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace xkcd_bot.Modules
{
    public class xkcdModule : ModuleBase<SocketCommandContext>
    {
        //instantiate only once per application but since this is the only command handling class this should be fine
        static readonly HttpClient client = new HttpClient();

        private readonly IConfiguration configuration;

        public xkcdModule(IConfiguration config)
        {
            configuration = config;
        }

        [Command("random")]
        [Summary("Show a random xkcd")]
        public async Task RandomAsync()
        {
            try
            {
                long max = (await get_latest()).num;
                Random rnd = new();
                long xkcd = rnd.NextInt64(1, max);
                xkcdData data = await get_xkcd(xkcd);
                var builder = new EmbedBuilder()
                {
                    Url = $"https://xkcd.com/{data.num}",
                    ImageUrl = data.img,
                    Title = $"xkcd #{data.num} - {data.title}",
                    Description = data.alt
                };
                await ReplyAsync(embed: builder.Build());
            }
            catch (HttpRequestException)
            {
                await ReplyAsync("Network error cannot reach xkcd.com");
                return;
            }
        }

        [Command("latest")]
        [Summary("Show the latest xkcd")]
        public async Task LatestAsync()
        {
            try
            {
                xkcdData data = await get_latest();
                var builder = new EmbedBuilder()
                {
                    Url = $"https://xkcd.com/{data.num}",
                    ImageUrl = data.img,
                    Title = $"xkcd #{data.num} - {data.title}",
                    Description = data.alt
                };
                await ReplyAsync(embed: builder.Build());
            }
            catch (HttpRequestException)
            {
                await ReplyAsync("Network error cannot reach xkcd.com");
                return;
            }
        }

        [Command("show")]
        [Summary("Show the latest xkcd")]
        public async Task ShowAsync(long id)
        {
            try
            {
                xkcdData data = await get_xkcd(id);
                var builder = new EmbedBuilder()
                {
                    Url = $"https://xkcd.com/{data.num}",
                    ImageUrl = data.img,
                    Title = $"xkcd #{data.num} - {data.title}",
                    Description = data.alt
                };
                await ReplyAsync(embed: builder.Build());
            }
            catch (HttpRequestException)
            {
                await ReplyAsync("Network error cannot reach xkcd.com");
                return;
            }
        }

        [Command("help")]
        [Summary("Show help page")]
        public async Task HelpAsync()
        {
            await ReplyAsync("```\n" +
                $"{configuration["prefix"]}show <id>:  Shows specific comic by id.\n" +
                $"{configuration["prefix"]}latest:     Shows latest comic.\n" +
                $"{configuration["prefix"]}random:     Shows random comic\n" +
                $"{configuration["prefix"]}help:       Shows this message.```");
        }

        private async Task<xkcdData> get_xkcd(long id)
        {
            using HttpResponseMessage response = await client.GetAsync($"https://xkcd.com/{id}/info.0.json");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<xkcdData>();
        }

        private async Task<xkcdData> get_latest()
        {
            using HttpResponseMessage response = await client.GetAsync($"https://xkcd.com/info.0.json");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<xkcdData>();
        }

        private struct xkcdData
        {
            public string month { get; set; }
            public long num { get; set; }
            public string title { get; set; }
            public string alt { get; set; }
            public string img { get; set; }
        }
    }
}

using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using System.IO;

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteveBot
{
    class main
    {
        public static void Main(string[] args)
            => new main().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;

            var token = "NzYxNzUzNTQ0NDE1MjQ4NDE1.X3fMRQ.S5NREiGGN3Pkk_OLu47P6ywvCwk";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Run(Hello);
            await Task.Delay(-1);
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        private Task Hello()
        {
            Console.WriteLine("Hello!");
            return Task.CompletedTask;
        }
    }
}

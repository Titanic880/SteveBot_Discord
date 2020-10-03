using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SteveBot
{
    class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        public CommandHandler(DiscordSocketClient client, CommandService commands)
        {
            _commands = commands;
            _client = client;
        }

        public async Task InstallCommandsAsync()
        {
            //checks to see if a message has been sent
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            //Stores the message in a variable to be used
            var message = messageParam as SocketUserMessage;
            if (message == null)
                return;

            //Start of command after prefix
            int argPos = 0;

            //checks to see if the user starts with the prefix, or if the author is a bot
            if (!(message.HasCharPrefix('!', ref argPos) ||
                 message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                 message.Author.IsBot)
                return;

            //Create a WebSocket based command context from the message
            var context = new SocketCommandContext(_client, message);

            await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null);
        }
    }
}

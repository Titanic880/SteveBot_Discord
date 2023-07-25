using System.Threading.Tasks;
using System.Reflection;
using System.Timers;
using System;
using Discord.WebSocket;
using Discord.Commands;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using SteveBot.Modules;

namespace SteveBot
{
    internal class BotProgram
    {
        public const char PrefixChar = '!';

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public BotProgram(string token) => new BotProgram().MainAsync(token).GetAwaiter().GetResult();
        private BotProgram() { }

        public async Task MainAsync(string token)
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            //Not 100% sure what this does in its entirity; .AddSingleton == static
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            _client.Log += Client_Log;
            await RegisterCommandsAsync();

            //logs the bot into discord
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            //Sets up a Timer so that the bot is acted on every hour
            Timer wake = new Timer(3.6e+6);
            wake.Elapsed += Wake_Tick;
            wake.AutoReset = true;
            wake.Start();

            //tells the bot to wait for input
            await Task.Delay(-1);
        }

        //outputs to Console
        private Task Client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        private void Wake_Tick(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Wakeup Stevebot! the coffee is calling to you!");

        }
        //Adds commands to the bot
        public async Task RegisterCommandsAsync()
        {
            //registers anything tagged as 'Task' to the set of commands that can be called
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        //Command Handler
        private async Task HandleCommandAsync(SocketMessage arg)
        {
            int argPos = 0;
            //IGuildChannel bots;   //TODO: Implement channel restriction
            
            //Stores user message and initilizes message position
            if (!(arg is SocketUserMessage message))
                return;
            //checks to see if the user is a bot
            else
                //Checks for prefix or specified passthrough commands
                if (message.HasCharPrefix(PrefixChar, ref argPos)
                 || message.Content.ToLower() == "help"
                 || message.Content.ToLower() == "linking"
                 || message.Content.ToLower() == "calculator"
                 || message.Content.ToLower() == "blackjack"
                 || message.Content.ToLower() == "k")
            {
                //Saves user Input to a debug file for later inspection
                CommandFunctions.UserCommand(message);
                if (message.Author.IsBot)
                    return;
                //generates an object from the user message
                SocketCommandContext context = new SocketCommandContext(_client, message);

                //Attempts to run the command and outputs accordingly
                IResult result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);
                    await message.Channel.SendMessageAsync(result.ErrorReason);
                }
                if (result.Error.Equals(CommandError.UnmetPrecondition))
                    await message.Channel.SendMessageAsync(result.ErrorReason);
            }
            //if something fails the Prefix check it just returns
            else
            {
                if (!message.Author.IsBot)
                    CommandFunctions.UserMessages(message);
                return;
            }
        }

    }
}

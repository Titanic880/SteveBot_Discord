using Discord.WebSocket;
using Discord.Commands;
using Discord;

using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;
using System.Timers;
using System;


using SteveBot.Modules;

namespace SteveBot
{

    internal class BotProgram
    {
        public const char PrefixChar = '$';
        public static readonly Emoji[] emojis = new Emoji[]
    {
            "1️⃣" ,
            "2️⃣" ,
            "3️⃣" ,
            "4️⃣" ,
            "5️⃣" ,
            "6️⃣" ,
            "7️⃣" ,
            "8️⃣" ,
            "✅"
    };

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public BotProgram(string token) => new BotProgram().MainAsync(token).GetAwaiter().GetResult();
        private BotProgram() { }

        public async Task MainAsync(string token)
        {

            //Allows bot to see messages https://discordnet.dev/guides/v2_v3_guide/v2_to_v3_guide.html
            var config = new DiscordSocketConfig()
            { GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent };

            _client = new DiscordSocketClient(config);
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
            
            await Task.Delay(Timeout.Infinite);
        }
        /// <summary>
        /// To Fix hot unloading/loading while debugging
        /// </summary>
        ~BotProgram()
        {
          _client.LogoutAsync();
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
            //_client.SlashCommandExecuted += HandleSlashAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
            _client.ReactionAdded += HandleReactionAdd;
            _client.ReactionRemoved += HandleReactionRemove;
        }

        private Task HandleReactionRemove(Cacheable<IUserMessage, ulong> msgCache, Cacheable<IMessageChannel, ulong> msgChannel, SocketReaction reaction)
        {
            _ = Task.Run(async () =>
            {
                if (reaction.User.Value.IsBot) return;

            });
            return Task.CompletedTask;
        }

        private Task HandleReactionAdd(Cacheable<IUserMessage, ulong> msgCache, Cacheable<IMessageChannel, ulong> msgChannel, SocketReaction reaction)
        {
            _ = Task.Run(async () =>
            {
                if (reaction.User.Value.IsBot) return;
                var message = await msgCache.GetOrDownloadAsync();
                var debug = message.Reactions;
            });
            return Task.CompletedTask;
        }

        //Command Handler
        private Task HandleCommandAsync(SocketMessage arg)
        {
            _ = Task.Run(async () =>
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
                     //|| message.Content.ToLower() == "help"
                     //|| message.Content.ToLower() == "linking"
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
            });
            return Task.CompletedTask;
        }
    }
}

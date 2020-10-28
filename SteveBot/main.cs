using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using System.Timers;
using SteveBot.Modules;

namespace SteveBot
{
    class main
    {
        private bool auth_Exists = true;
        public static readonly Random rand = new Random();
        //the bot cannot run within a static main function, so we build one with
        //Async and awaiter built into it for ease of use as a bot
        public static void Main(string[] args)
        {
            //Checks to see if all nessecary directories exist, if not it generates them.
            main m = new main();
            m.File_Check();

            //This shall never be seen by anyone but me... (Sign in token for the bot)
            string token = null;

            if (m.auth_Exists)
                token = File.ReadAllText("Files/auth.json");
            if(token == "")
            {
                Console.WriteLine("Auth Token not found, please add it to auth.json in the files folder.");
                Console.WriteLine();
                Console.ReadLine();
                return;
            }
            //Starts the Bot
            maim(token);
        }
           
        static void maim(string token)=> new main().MainAsync(token).GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public async Task MainAsync(string token)
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();



            //Not 100% sure what this does in its entirity; .AddSingleton == static
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            _client.Log += _client_Log;
            await RegisterCommandsAsync();

            //logs the bot into discord
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            //Sets up a Timer so that the bot is acted on every hour
            System.Timers.Timer wake = new System.Timers.Timer(3.6e+6);
            wake.Elapsed += wake_Tick;
            wake.AutoReset = true;
            wake.Start();
            
            //tells the bot to wait for input
            await Task.Delay(-1);
        }

        //outputs to Console
        private Task _client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        private void wake_Tick(Object source, ElapsedEventArgs e)
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
            //Stores user message and initilizes message position
            SocketUserMessage message = arg as SocketUserMessage;
            int argPos = 0;
            IGuildChannel bots;
            if (message == null)
                return;
            //checks to see if the user is a bot
            else
            {
                //Checks for prefix or specified passthrough commands
                if (message.HasStringPrefix("!", ref argPos)
                 || message.Content.ToLower() == "help"
                 || message.Content.ToLower() == "linking"
                 || message.Content.ToLower() == "calculator"
                 || message.Content.ToLower() == "blackjack"
                 || message.Content.ToLower() == "k")
                {
                    //Saves user Input to a debug file for later inspection
                    Modules.CommandFunctions.UserCommand(message);
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
                    if (!message.Author.IsBot) Modules.CommandFunctions.UserMessages(message);
                    return;
                }
            }
        }
        //Checks all Files on runtime
        private void File_Check()
        {
            if (!Directory.Exists("Files/"))
                Directory.CreateDirectory("Files/");

            if (!File.Exists("Files/auth.json"))
            {
                File.Create("Files/auth.json");
                auth_Exists = false;
            }
            if (!File.Exists(CommandFunctions.linkPath))
                File.Create(CommandFunctions.linkPath).Close();
            else
                CommandFunctions.UpdateLinks(File.ReadAllLines("Files/Links.txt").ToList<string>());

            if (!File.Exists(CommandFunctions.usercommandsPath))
                File.Create(CommandFunctions.usercommandsPath).Close();

            if (!File.Exists(CommandFunctions.usermessagesPath))
                File.Create(CommandFunctions.usermessagesPath).Close();
        }
    }
}
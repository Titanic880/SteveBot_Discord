using Discord.Commands;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;

namespace SteveBot.Modules
{
    public class MainCommands : ModuleBase<SocketCommandContext>
    {
        public bool LongTask = false;
        private int timerSeconds = 0;
        #region Help Commands
        [Command("help")]
        public async Task Help()
        {
            EmbedBuilder EmbedBuilder = new EmbedBuilder()
                    .WithTitle($"Command prefix is '{BotProgram.PrefixChar}'")
                    .WithDescription("  help : displays this command" +
                        "\nrshelp : Runescape Help Command" +
                        "\nping : Pong?!" +
                        "\npong : What?" +
                        "\nslap : you what?" +
                        "\nkek : lol" +
                        "\nroll : Rolls a specified dice; 4,6,8,10,20,100 (default:6)" +
                        "\nban  : ban <User> <Comment>" +
                        "\nunban: unban <User> <Comment>" +
                        "\nkick : kick <User> <Comment>" + "" +
                        "\nlinking : Info list for links!" +
                        "\nblackjack : WIP" +
                        "\nmath : Accepts an equation and will output a single number! (comma seperated PostNotation)" +
                        "\ncalculator : Calculator Help")
                    .WithCurrentTimestamp();
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        [Command("calculator")]
        public async Task Calc()
        {
            EmbedBuilder EmbedBuilder = new EmbedBuilder()
        .WithTitle("Command prefix is '!'")
        .WithDescription("  calculator : displays this command" +
            "\nadd : Basic Addition <Num1>  <Num2>" +
            "\nsub  : Basic Subtraction <Num2>  <Num1>" +
            "\nmult:  Multiplication <Num1>  <Num2>" +
            "\ndiv : Division <Num1>  <Num2>" +
            "\ndec2hex : Converts a decmial number to a hexidecimal number <Num1>" +
            "\nhex2dec : Converts a hexidecimal to a decimal number <Num1>" +
            "\nfact : Factorial of the number <Num1>")
        .WithCurrentTimestamp();
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        [Command("blackjack")]
        public async Task BJ()
        {
            await ReplyAsync("Under Development");
        }
        [Command("linking")]
        public async Task Linking()
        {
            EmbedBuilder EmbedBuilder = new EmbedBuilder()
        .WithTitle("Command prefix is '!'")
        .WithDescription("  linking : displays this command" +
            "\naddlink : Adds a link to the list" +
            "\nrandomlink : gets a random link out of the list" +
            "\ngetlink# : pulls a link from the specific index" +
            "\nLLL : returns length of the list" +
            "\ndellink : Removes link from the list")
        .WithCurrentTimestamp();
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        #endregion Help Commands
        #region Standard Commands
        [Command("ping")]
        public async Task Ping()
        {
            await ReplyAsync("Pong");
        }
        [Command("pong")]
        public async Task Pong()
        {
            await ReplyAsync("That's my line!");
        }
        [Command("kek")]
        public async Task Kek()
        {
            await ReplyAsync("LOL");
        }
        [Command("slap")]
        public async Task Slap(IGuildUser user = null)
        {
            if (user == null)
                await ReplyAsync("You Slapped yourself!");
            else
                await ReplyAsync("You Slapped " + user.Mention);
        }
        [Command("roll")]
        public async Task Roll(int dice_size = 6)
        {
            await ReplyAsync($"You rolled a  {CommandFunctions.DiceRoll(dice_size + 1)}");
        }
        [Command("k")]
        public async Task K()
        {
            await ReplyAsync("https://tenor.com/view/bet-gif-5301020");
        }


        #endregion Standard Commands
        #region Media
        [Command("addlink")]
        public async Task AddLink(string link)
        {
            string linktest = link.Substring(0, 5);
            if (linktest.ToLower() != "https")
            {
                await ReplyAsync("Please provide a link with https at the beginning");
            }
            int tmp = CommandFunctions.AddLink(link);
            await ReplyAsync($"Link added successfully and is #{tmp}");
        }

        [Command("randomlink")]
        public async Task RandomLink()
        {
            string link = CommandFunctions.LinksPub[new Random().Next(CommandFunctions.LinksPub.Count)];
            await ReplyAsync(link);
        }
        [Command("getlink")]
        public async Task GetLinknum(int input)
        {
            if (input == 0) input = 1;
            if (input > CommandFunctions.LinksPub.Count)
            {
                await ReplyAsync($"Your index exceeds the size of the list, the list is currently: {input} Links long");
                return;
            }
            await ReplyAsync(CommandFunctions.LinksPub[input - 1]);
        }
        [Command("LLL")]
        public async Task LinkListLength()
        {
            await ReplyAsync($"The list is: {CommandFunctions.LinksPub.Count} links long!!!");
        }
        [Command("dellink")]
        public async Task DeleteLink(int input)
        {
            if (input == 0) input = 1;
            if (input > CommandFunctions.LinksPub.Count)
            {
                await ReplyAsync($"Your index exceeds the size of the list, the list is currently: {input}" +
                                  "\nLinks long");
                return;
            }

            CommandFunctions.RemoveLink(input - 1);
            await ReplyAsync("Link removed from list!");
        }
        #endregion Media
        #region TEST
        [Command("test")]
        public async Task Test()
        {

            await ReplyAsync("");
        }
        [Command("Tower")]
        public async Task Tower()
        {
            string desc = @"Many try and fail to climb\nWill you succeed?\nChoose your class!"
                          + "\n1.) Warrior"
                          + "\n2.) Ranger"
                          + "\n3.) The Nothing";
            EmbedBuilder EmbedBuilder = new EmbedBuilder()
                .WithTitle("Welcome to the Tower!")
                .WithDescription(desc)
                .WithCurrentTimestamp();
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        /*
        [Command("Tower Start")]
        public async Task StartTower(int Startclass, SocketUser user = null)
        {
            bool inlist = false;
            string desc = "Many try and fail to climb\nWill you succeed?\nChoose your class!" +
                "\n1.) Warrior" +
                "\n2.) Ranger" +
                "\n3.) The Nothing";
            for (int i = 0; i > towerplayers.Count; i++)
            {
                if (user.Id == towerplayers[i].playerID)
                {
                    desc = "Welcome back\nShall we begin?";
                    inlist = true;
                }
            }
            if (!inlist)
            {
                Towerplayers tp = new Towerplayers();
                tp.playerID = user.Id;
                TheTower.main start = new TheTower.main(user.Id, Startclass);
                towerplayers.Add(tp);
            }


            EmbedBuilder EmbedBuilder = new EmbedBuilder()
               .WithTitle("Welcome to the Tower!")
               .WithDescription(desc)
               .WithCurrentTimestamp();
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }*/

        #endregion TEST
        #region Bans
        [Command("ban")]
        [RequireUserPermission(GuildPermission.Administrator, ErrorMessage = "YOU DONT GOT ENOUGH COFFEE FOR THIS!")]
        public async Task BanMember(IGuildUser user = null, [Remainder] string reason = null)
        {
            if (user == null)
            {
                await ReplyAsync("Please specify a user!");
                return;
            }
            if (reason == null)
                reason = "N/A";

            await Context.Guild.AddBanAsync(user, 1, reason);

            EmbedBuilder EmbedBuilder = new EmbedBuilder()
                .WithDescription($"{user.Mention} was banned \n **Reason** {reason}")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("User Banned");
                });
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }

        [Command("unban")]
        [RequireUserPermission(GuildPermission.Administrator, ErrorMessage = "You have no coffee for me, find some.")]
        public async Task UnBanMember(IGuildUser user = null, [Remainder] string reason = null)
        {
            if (user == null)
            {
                await ReplyAsync("Please specify a user!");
                return;
            }
            if (reason == null)
                reason = "N/A";

            await Context.Guild.RemoveBanAsync(user);

            EmbedBuilder EmbedBuilder = new EmbedBuilder()
                .WithDescription($"{user.Mention} was unbanned \n **Reason** {reason}")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("User unBanned");
                });
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        [Command("kick")]
        [RequireUserPermission(GuildPermission.Administrator, ErrorMessage = "I need some coffee before I kick someone.")]
        public async Task Kick(IGuildUser user = null, [Remainder] string reason = null)
        {
            if (user == null)
            {
                await ReplyAsync("Please specify a user!");
                return;
            }
            if (reason == null)
                reason = "N/A";

            await Context.Guild.AddBanAsync(user, 0, reason);
            await Context.Guild.RemoveBanAsync(user);

            EmbedBuilder EmbedBuilder = new EmbedBuilder()
                .WithDescription($"{user.Mention} was kicked \n **Reason** {reason}")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("User was kicked");
                });
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        #endregion Bans
        #region Calc    
        [Command("math")]
        public async Task Math(string userinput)
        {
            double output = Calculator.Complex_Equation(userinput);
            EmbedBuilder EmbedBuilder = new EmbedBuilder()
                .WithDescription($"Your final output is {output}")
                .WithCurrentTimestamp();
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        [Command("dec2hex")]
        public async Task Dec2Hex(int Num1)
        {
            string output = Calculator.Dec2hex(Num1);

            EmbedBuilder EmbedBuilder = new EmbedBuilder()
                 .WithDescription($"{Num1} = {output}")
                 .WithCurrentTimestamp();
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        [Command("hex2dec")]
        public async Task Hex2Dec(string Num1)
        {
            string output = Calculator.Hex2dec(Num1).ToString();

            EmbedBuilder EmbedBuilder = new EmbedBuilder()
                 .WithDescription($"{Num1} = {output}")
                 .WithCurrentTimestamp();
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        [Command("fact")]
        public async Task Factorial(int Num1)
        {
            string output = Calculator.Factorial(Num1).ToString();

            EmbedBuilder EmbedBuilder = new EmbedBuilder()
                 .WithDescription($"!{Num1} = {output}")
                 .WithCurrentTimestamp();
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        #endregion Calc
        #region BlackJack
        [Command("blackjack deal")]
        public async Task BlackJackDeal(IGuildUser user = null)
        {
            if (user.IsBot)
                return;

            await ReplyAsync("This does nothing");
        }

        [Command("blackjack Autoplay")]
        public async Task BJAutoPlay(int games = 1)
        {
            if (LongTask)
            {
                await ReplyAsync("There is already a designated long task running" +
                                 "\nPlease wait for it to finish");
            }
            else
            {

                if (games >= 100000)
                {
                    await ReplyAsync("Too many games selected, I don't wanna play that much!");
                    return;
                }
                if (games >= 10000)
                    LongTask = true;
                if (games == 0)
                    games = 1;
                string output = "";
                if (games == 1)
                {
                    BlackJack.Blackjack bj = new BlackJack.Blackjack();
                    BlackJack.Player winner = bj.playgame();
                    output = bj.Win();
                }
                else
                {
                    System.Timers.Timer Time = new System.Timers.Timer(1000);
                    Time.Elapsed += Time_Elapsed;
                    int gamesplayed = games;
                    int[] output1 = new int[] { 0, 0 };

                    Time.Start();
                    for (int i = 0; i < games; i++)
                    {
                        BlackJack.Blackjack game = new BlackJack.Blackjack();
                        BlackJack.Player winner = game.playgame();
                        Console.WriteLine(i + "/" + games);
                        if (winner.IsDealer)
                            output1[0]++;
                        else
                            output1[1]++;
                    }
                    Time.Stop();

                    if (timerSeconds == 0)
                        timerSeconds = 1;

                    LongTask = false;
                    timerSeconds = 0;

                    output = $"In {gamesplayed} games" +
                         $"\nThe dealer won {output1[0]} times " +
                         $"\nThe player won {output1[1]} times" +
                         $"\nand took: {timerSeconds} Seconds!";
                }

                EmbedBuilder EmbedBuilder = new EmbedBuilder()
                    .WithTitle("Black Jack")
                    .WithDescription(output)
                    .WithCurrentTimestamp();
                Embed embed = EmbedBuilder.Build();

                await ReplyAsync(embed: embed);
            }
        }

        private void Time_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timerSeconds++;
        }
        #endregion BlackJack
        /*
        #region Reactions
        [Command("")]
        public async Task gg(IEmote emote, RequestOptions options = null)
        {
            await MailSettingsSectionGroup.
        }
        IAsyncEnumerable<IReadOnlyCollection<IUser>> GetReactionUsersAsync(IEmote emoji, in T limit, RequestOptions options = null)
        {
            FlattenAsync<T>(IAsyncEnumerable<IEnumerable<>>);
        }
        #endregion Reactions*/
        //Move to own file when figured out
        #region Runescape 

        /// <summary>
        /// Gets local File contents
        /// </summary>
        /// <returns></returns>
        private List<string> GetRSFile(){
            List<string> FileContents = new List<string>();
            using (StreamReader sr = new StreamReader("Files/Runescape.json"))
            {
                while (!sr.EndOfStream)
                    FileContents.Add(sr.ReadLine());
            }
            return FileContents;
        }


        [Command("rshelp")]
        public async Task RShelp()
        {
            EmbedBuilder EmbedBuilder = new EmbedBuilder()
        .WithTitle($"Command prefix is '{BotProgram.PrefixChar}'")
        .WithDescription("  rshelp : displays this command" +
            "\nritual Help" +
            "\nrs"
            )
        .WithCurrentTimestamp();
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        [Command("rssetprice")]
        public async Task RSSetPrice(string item, string number)
        {
            //reg_ink,gre_ink,pow_ink
            //les_plas,gre_plas,pow_plas



        }
        [Command("rsGetPrices")]
        public async Task RsGetPrices()
        {
            List<string> fileinfo = GetRSFile();
            

            EmbedBuilder EmbedBuilder = new EmbedBuilder()
.WithTitle($"Command prefix is '{BotProgram.PrefixChar}'")
.WithDescription("  rshelp : displays this command" +
"\nritual Help" +
"\nrs"
)
.WithCurrentTimestamp();
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        [Command("RSritual")]
        public async Task RSRitualMoney(string Data)
        {
            //Data Points needed: Price_inks Price_Plasm, Type  

            string[] Datapoints = Data.Split(" "[0]);

        }

        #endregion Runescape
    }
}
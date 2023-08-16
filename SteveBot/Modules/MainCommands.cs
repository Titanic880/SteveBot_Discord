using Discord.Commands;
using Discord;

using System.Threading.Tasks;
using System.IO;
using System;

using SteveBot.Content.Runescape;

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
        private RSJson GetRSFile() {
            string FileContents = "";
            using (StreamReader sr = new StreamReader("Files/Runescape.json"))
            {
                FileContents = sr.ReadToEndAsync().Result;
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<RSJson>(FileContents);
        }
        private bool SetRSFile(RSJson json)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("Files/Runescape.json"))
                {
                    sw.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(json, Newtonsoft.Json.Formatting.Indented));
                }
                return true;
            }
            catch (Exception e)
            {
                CommandFunctions.ErrorMessages(e.Message);
                return false;
            }
        }
        [Command("rsupdateprice")]
        public async Task UpdatePrice()
        {
            await ReplyAsync("Command under construction, Check back later!");
            return;

            DateTime CompTime = DateTime.UtcNow;
            RSJson rsf = GetRSFile();

            //Adjust this to change update interval (API Dependant)
            CompTime = CompTime.AddHours(-1); 

            //Check for spam update call
            if (rsf.LastUpdated >= CompTime && !rsf.UpdatedCall)
            {
                rsf.UpdatedCall = true;
                SetRSFile(rsf);
                await ReplyAsync("File has been updated too recently!");
                return;
            }
            if (rsf.UpdatedCall)
                return;
                
            try
            {
                //Get Data from GE


                //Apply to format


                //push to file
                SetRSFile(rsf);
                await ReplyAsync("Prices have been updated!");
            }
            catch (Exception e)
            {
                CommandFunctions.ErrorMessages(e.Message);
                await ReplyAsync("A problem has occoured when fetching new data.");
            }
        }
        [Command("rshelp")]
        public async Task RShelp()
        {
            EmbedBuilder EmbedBuilder = new EmbedBuilder()
        .WithTitle($"Command prefix is '{BotProgram.PrefixChar}' (Commands are not case sensitive)")
        .WithDescription("  rshelp : displays this command" +
            "\nritual Help" +
            "\nRSpriceHelp : specific help for setting prices" +
            "\nrssetprice : used to set price of an item" +
            "\nrsGetPrices : used to get all prices" +
            "\nRSritual : Get the price of a given ritual (no spaces in ritual)" +
            "\nrsAlteration : GlyphX(X being tier) <amount>" + 
            ""
            )
        .WithCurrentTimestamp();
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        [Command("rssetprice")]
        public async Task RSSetPrice(string item, int price)
        {
            item = item.ToLower();
            //Data Points needed: Price_inks Price_Plasm, Type  
            RSJson rsf = GetRSFile();
            switch (item)
            {
                case "ash":
                    rsf.AshPrice = price;
                    break;
                case "vial":
                    rsf.VialOfWater = price;
                    break;
                case "ecto":
                    rsf.Ectoplasm = price;
                    break;
                case "lplasm":
                    rsf.NecroplasmPrices[0] = price;
                    break;
                case "gplasm":
                    rsf.NecroplasmPrices[1] = price;
                    break;
                case "pplasm":
                    rsf.NecroplasmPrices[2] = price;
                    break;
                case "rink":
                    rsf.InkPrices[0] = price;
                    break;
                case "gink":
                    rsf.InkPrices[1] = price;
                    break;
                case "pink":
                    rsf.InkPrices[2] = price;
                    break;
                default:
                    await ReplyAsync("Invalid input, action cancelled");
                    return;
            }
            SetRSFile(rsf);
            await ReplyAsync($"Price of {item} updated to {price}!");
        }
        [Command("rsGetPrices")]
        public async Task RsGetPrices()
        {
            RSJson rsf = GetRSFile();

            EmbedBuilder EmbedBuilder = new EmbedBuilder()
                .WithTitle($"Current Bot Prices:")
                .WithDescription($"AshPrice: {rsf.AshPrice}" +
                                 "\nEctoplasm: " + rsf.Ectoplasm + 
                                 "\nLesser Necroplasm: " + rsf.NecroplasmPrices[0] +
                                 "\nGreater Necroplasm: " + rsf.NecroplasmPrices[1] +
                                 "\nPowerful Necroplasm: " + rsf.NecroplasmPrices[2] +
                                 "\nRegular Ink: " + rsf.InkPrices[0] +
                                 "\nGreater Ink: " + rsf.InkPrices[1] +
                                 "\nPowerful Ink: " + rsf.InkPrices[2] 
).WithCurrentTimestamp();
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        [Command("RSpriceHelp")]
        public async Task RSPriceHelp()
        {
            EmbedBuilder embedBuilder = new EmbedBuilder()
                .WithTitle("Ritual Price Set Guide (Command then provided)")
                .WithFooter("For rssetprice Command")
                .WithDescription("ash <price>" +
                "\necto <price>" +
                "\nlplasm <price>" +
                "\ngplasm <price>" +
                "\npplasm <price>" +
                "\nrink <price>" +
                "\ngink <price>" +
                "\npink <price>"
).WithCurrentTimestamp();
            Embed embed = embedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        [Command("RSritualHelp")]
        public async Task RSRitualHelp()
        {
            EmbedBuilder embedBuilder = new EmbedBuilder()
                .WithTitle("Ritual specific Info")
                .WithFooter("For rsritual Command")
                .WithDescription(
                "\nlplasm : Lesser Necroplasm" +
                "\nless   : Lesser Essence" +
                "\nlcomm  : Lesser Communion" +
                "\nlens   : Lesser Ensoul" +
                "\ngplasm : Greater Necroplasm" +
                "\ngess   : Greater Essence" + 
                "\ngcomm  : Greater Communion" +
                "\nens    : Ensoul" +
                "\npplasm : Powerful Necroplasm" +
                "\npess   : Powerful Essence" +
                "\npcomm  : Powerful Communion" +
                "\ngens   : Greater Ensoul"
).WithCurrentTimestamp();
            Embed embed = embedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        [Command("RSritual")]
        public async Task RSRitualMoney(string Data)
        {
            RSJson rsf = GetRSFile();
            string msg = "Setup Cost: ";
            switch (Data)
            {
               case "lplasm":
                    msg += rsf.RitualSetup_Cost(RS3Rituals.LesNecro);
                   break;
               case "less":
                    msg += rsf.RitualSetup_Cost(RS3Rituals.LesEss);
                    break;
               case "lcomm":
                    msg += rsf.RitualSetup_Cost(RS3Rituals.LesCommun);
                    break;
               case "lens":
                    msg += rsf.RitualSetup_Cost(RS3Rituals.LesEnsoul);
                    break;
               case "gplasm":
                    msg += rsf.RitualSetup_Cost(RS3Rituals.GreNecro);
                    break;
               case "gess":
                    msg += rsf.RitualSetup_Cost(RS3Rituals.GreEss);
                    break;
               case "gcomm":
                    msg += rsf.RitualSetup_Cost(RS3Rituals.GreCommun);
                    break;
               case "ens":
                    msg += rsf.RitualSetup_Cost(RS3Rituals.GreEnsoul);
                    break;
               case "pplasm":
                    msg += rsf.RitualSetup_Cost(RS3Rituals.PowNecro);
                    break;
               case "pess":
                    msg += rsf.RitualSetup_Cost(RS3Rituals.PowEss);
                    break;
               case "pcomm":
                    msg += rsf.RitualSetup_Cost(RS3Rituals.PowCommun);
                    break;
               case "gens":
                    msg += rsf.RitualSetup_Cost(RS3Rituals.PowEnsoul);
                    break;
                default:
                    msg = "ritual not found";
                    break;
            }
            await ReplyAsync(msg);
        }

        
        /// <summary>
        /// Takes the glyph/rank and the amount and outputs the cost
        /// </summary>
        /// <param name="input"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [Command("RSAlteration")]
        public async Task RSAlteration(string input, int amount = 1)
        {
            RSJson rsf = GetRSFile();
            RS3Glyphs glyph;
            switch (input)
            {
                case "multiply1":
                    glyph = RS3Glyphs.Multiply1;
                    break;
                case "multiply2":
                    glyph = RS3Glyphs.Multiply2;
                    break;
                case "multiply3":
                    glyph = RS3Glyphs.Multiply3;
                    break;
                case "protection1":
                    glyph = RS3Glyphs.Protection1;
                    break;
                case "protection2":
                    glyph = RS3Glyphs.Protection2;
                    break;
                case "protection3":
                    glyph = RS3Glyphs.Protection3;
                    break;
                case "speed1":
                    glyph = RS3Glyphs.Speed1;
                    break;
                case "speed2":
                    glyph = RS3Glyphs.Speed2;
                    break;
                case "speed3":
                    glyph = RS3Glyphs.Speed3;
                    break;
                case "attraction1":
                    glyph = RS3Glyphs.Attraction1;
                    break;
                case "attraction2":
                    glyph = RS3Glyphs.Attraction2;
                    break;
                case "attraction3":
                    glyph = RS3Glyphs.Attraction3;
                    break;
                default:
                    await ReplyAsync("invalid Glyph");
                    return;
            }
            int cost = rsf.AlterationCost(glyph);
            cost *= amount;

            await ReplyAsync($"The total cost of {amount} of {input} is: {cost}");
        }
        #endregion Runescape
    }
}
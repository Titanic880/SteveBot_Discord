﻿using Discord.Commands;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using System.Runtime.CompilerServices;
using System;

namespace SteveBot.Modules
{
    public class MainCommands : ModuleBase<SocketCommandContext>
    {
        #region Help Commands
        [Command("help")]
        public async Task Help()
        {
            EmbedBuilder EmbedBuilder = new EmbedBuilder()
                    .WithTitle("Command prefix is '!'")
                    .WithDescription("  help : displays this command" +
                        "\nping : Pong?!" +
                        "\npong : What?" +
                        "\nslap : you what?" +
                        "\nkek : lol" +
                        "\nban  : ban <User> <Comment>" +
                        "\nunban: unban <User> <Comment>" +
                        "\nkick : kick <User> <Comment>" +"" +
                        "\nlinking : Info list for links!"+
                        "\nblackjack : WIP" +
                        "\ncalculator : Calculator Help")
                    .WithCurrentTimestamp();
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }

        [Command("calculator")]
        public async Task calc()
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
        public async Task blackjac()
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
            await ReplyAsync("LOL ");
        }
        [Command("slap")]
        public async Task Slap()
        {
            await ReplyAsync("You slapped yourself!");
        }
        [Command("slap")]
        public async Task Slap(IGuildUser user = null)
        {
            await ReplyAsync("You Slapped " + user.Mention);
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
            string link = CommandFunctions.LinksPub[Blackjack.rand.Next(CommandFunctions.LinksPub.Count)];
            await ReplyAsync(link);
        }
        [Command("getlink")]
        public async Task GetLinknum(int input)
        {
            if (input == 0) input = 1;
            if(input > CommandFunctions.LinksPub.Count)
            {
                await ReplyAsync($"Your index exceeds the size of the list, the list is currently: {input} Links long");
                return;
            }
            
            await ReplyAsync(CommandFunctions.LinksPub[input-1]);
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
            
            CommandFunctions.RemoveLink(input-1);
            await ReplyAsync("Link removed from list!");
           
        }
        #endregion Media

        #region TEST
        [Command("test")]
        public async Task test()
        {


            await ReplyAsync("");
        }
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
        [Command("add")]
        public async Task Addition(double Num1, double Num2)
        {
            string output = Calculator.add(Num1, Num2).ToString();

            EmbedBuilder EmbedBuilder = new EmbedBuilder()
                 .WithDescription($"{Num1} + {Num2} = {output}")
                 .WithCurrentTimestamp();
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        [Command("sub")]
        public async Task Subtraction(double Num1, double Num2)
        {
            string output = Calculator.sub(Num1, Num2).ToString();

            EmbedBuilder EmbedBuilder = new EmbedBuilder()
                 .WithDescription($"{Num1} - {Num2} = {output}")
                 .WithCurrentTimestamp();
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        [Command("mult")]
        public async Task Multiplication(double Num1, double Num2)
        {
            string output = Calculator.mult(Num1, Num2).ToString();

            EmbedBuilder EmbedBuilder = new EmbedBuilder()
                 .WithDescription($"{Num1} * {Num2} = {output}")
                 .WithCurrentTimestamp();
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        [Command("div")]
        public async Task Division(double Num1, double Num2)
        {
            string output = Calculator.div(Num1, Num2).ToString();

            EmbedBuilder EmbedBuilder = new EmbedBuilder()
                 .WithDescription($"{Num1} / {Num2} = {output}")
                 .WithCurrentTimestamp();
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        [Command("dec2hex")]
        public async Task Dec2Hex(int Num1)
        {
            string output = Calculator.dec2hex(Num1);

            EmbedBuilder EmbedBuilder = new EmbedBuilder()
                 .WithDescription($"{Num1} = {output}")
                 .WithCurrentTimestamp();
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        [Command("hex2dec")]
        public async Task Hex2Dec(string Num1)
        {
            string output = Calculator.hex2dec(Num1).ToString();

            EmbedBuilder EmbedBuilder = new EmbedBuilder()
                 .WithDescription($"{Num1} = {output}")
                 .WithCurrentTimestamp();
             Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        [Command("fact")]
        public async Task Factorial(int Num1)
        {
            string output = Calculator.Fact(Num1).ToString();

            EmbedBuilder EmbedBuilder = new EmbedBuilder()
                 .WithDescription($"!{Num1} = {output}")
                 .WithCurrentTimestamp();
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        #endregion Calc
    }
}

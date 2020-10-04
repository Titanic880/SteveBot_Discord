using Discord.Commands;
using System.Threading.Tasks;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteveBot.Modules
{
    public class MainCommands : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task Ping()
        {
            await ReplyAsync("Pong");
        }
    }
}

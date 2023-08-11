using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveBot.Modules
{
    internal class TowerCommands : ModuleBase<SocketCommandContext>
    {
        [Command("Start")]
        public async Task GameStart()
        {
            await ReplyAsync("To be Determined");
        }
        [Command("Attack")]
        public async Task Attack(string Level)
        {
            await ReplyAsync("TBF");
        }
    }
}

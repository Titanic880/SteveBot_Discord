using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace SteveBot
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        [Summary("List of commands.")]
        public Task SayAsync([Remainder][Summary("")] string help)
            => ReplyAsync(help);
    }
    [Group("sample")]
    public class SampleModule : ModuleBase<SocketCommandContext>
    {

	}
}

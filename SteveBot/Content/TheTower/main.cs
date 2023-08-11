using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveBot.Modules.TheTower
{
    class main
    {
        public ulong UserID { get; private set; } 
        public main(ulong userid, int startclass)
        {
            Player player = new Player(true, 1);
            player.SetClass(interfaces.Classes.Ranger);
            UserID = userid;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveBot.Modules.TheTower
{
    class Towermain
    {
        public ulong UserID { get; private set; } 
        public Towermain(ulong userid, int startclass)
        {
            Player player = new(true, 1);
            player.SetClass(Interfaces.Classes.Ranger);
            UserID = userid;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SteveBot.Modules.TheTower.Interfaces;

namespace SteveBot.Modules.TheTower
{
    class Enemy : Player
    {
        public Enemy(bool player, int Class) : base(player, Class)
        {
        }
        
        public void InitilizePlayer(bool player)
        {
            throw new NotImplementedException();
        }

        public int ModifierSet(Player opponent)
        {
            throw new NotImplementedException();
        }
    }
}

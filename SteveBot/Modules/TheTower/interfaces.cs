using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveBot.Modules.TheTower
{
    static class interfaces
    {
        public enum Classes
        {
            Warrior,
            Ranger,
            Mage,
            Orc,
            Goblin,
            Human
        }
        public interface player
        {
            string classtype { get; }
            bool IsPlayer { get; }
            int Health { get; }
            int Strength { get; }
            int Stamina { get; }
            int[] Potions { get; }
            int modifier { get; }
            int Level { get; }
            int Experience { get; }
            int ExperienceLevel { get; }

            void InitilizePlayer(bool player);
            string SetClass(Classes classnum);
            //bool isPlayer();
            int DealDamage(player opponent);
            int TakeDamage(int damage);
            int StaminaIncrease(int amount, bool increase);
            int DrinkPotion(int potionlocation);
            int SetLevel();
            int SetExperience(player opponent);
            int SetExperienceMax(int level);
            int ModifierSet(player opponent);
        }

    }
}

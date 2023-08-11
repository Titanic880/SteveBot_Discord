using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SteveBot.Modules.TheTower.interfaces;

namespace SteveBot.Modules.TheTower
{
    class Enemy : player
    {
        public string classtype { get; private set; }

        public int Strength { get; private set; }

        public bool IsPlayer { get; private set; }

        public int Health { get; private set; }

        public int Stamina { get; private set; }

        public int[] Potions { get; private set; }

        public int modifier { get; private set; }

        public int Level { get; private set; }

        public int Experience { get; private set; }

        public int ExperienceLevel { get; private set; }

        public int DealDamage(player opponent)
        {
            throw new NotImplementedException();
        }

        public int DrinkPotion(int potionlocation)
        {
            throw new NotImplementedException();
        }

        public void InitilizePlayer(bool player)
        {
            throw new NotImplementedException();
        }

        public bool isPlayer()
        {
            throw new NotImplementedException();
        }

        public int ModifierSet(player opponent)
        {
            throw new NotImplementedException();
        }

        public string SetClass(Classes classnum)
        {
            throw new NotImplementedException();
        }

        public int SetExperience(player opponent)
        {
            throw new NotImplementedException();
        }

        public int SetExperienceMax(int level)
        {
            throw new NotImplementedException();
        }

        public int SetLevel()
        {
            throw new NotImplementedException();
        }

        public int StaminaIncrease(int amount, bool increase)
        {
            throw new NotImplementedException();
        }

        public int TakeDamage(int damage)
        {
            throw new NotImplementedException();
        }
    }
}

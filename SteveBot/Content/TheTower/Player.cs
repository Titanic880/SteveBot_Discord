using System;
using static SteveBot.Modules.TheTower.Interfaces;

namespace SteveBot.Modules.TheTower
{
    class Player
    {
        
        enum Potion_Types
        {
            Health,
            Strength,
            Stamina,
            Mystery
        }
        public string Classtype { get; private set; }

        public int Strength { get; private set; }

        public bool IsPlayer { get; private set; }

        public int Health { get; private set; }

        public int Stamina { get; private set; }

        public int[] Potions { get; private set; }

        public double Modifier { get; private set; }

        public int Level { get; private set; }

        public int Experience { get; private set; }

        public int ExperienceLevel { get; private set; }

        public Player(bool player, int Class)
        {
            IsPlayer = player;
            Level = 0;
            ExperienceLevel = (Level + 1) * 83;

            if (IsPlayer)
            {
                Modifier = 1;
                SetClass((Classes)Class);
                Stamina = 50;
                Potions = new int[5];
            }
            else
            {
                ModifierSet(Level);
            }
        }

        public int DealDamage(Player opponent)
        {
            int damage = Convert.ToInt32(Math.Round(opponent.Strength * opponent.Modifier));
            Health -= damage;
            return damage;
        }

        public int DrinkPotion(int Potionlocation)
        {
            //Potions[potionlocation];

            return 1;
        }

        //To be used by the enemy class
        public int ModifierSet(int level)
        {
            Modifier = level * 2;
            return Convert.ToInt32(Math.Round(Modifier));
        }

        public string SetClass(Classes classnum)
        {
            if (IsPlayer)
            {
                switch (classnum)
                {
                    case Classes.Warrior:
                        Classtype = "Warrior";
                        Health = 100;
                        break;
                    case Classes.Ranger:
                        Classtype = "Ranger"; 
                        Health = 75;
                        break;
                    case Classes.Mage:
                        Classtype = "Mage";
                        Health = 50;
                        break;
                }
                return Classtype;
            }
            else
            {
                //Sets classes to Enemy, easier generation
                classnum += 3;

                switch (classnum)
                {
                    case Classes.Orc:
                        Classtype = "Orc";
                        break;
                    case Classes.Goblin:
                        Classtype = "Goblin";
                        break;
                    case Classes.Human:
                        Classtype = "Human";
                        break;
                }
                return Classtype;
            }
        }

        public void SetExperience(Player opponent)
        {
            Experience += opponent.Health/3;
        }

        public void SetExperienceMax(int level)
        {
            ExperienceLevel = 83*(level*level);
        }

        public int SetLevel()
        {
            int x = Level % 4;
            switch (x)
            {
                case 0:
                    Stamina += 5;
                    break;
                case 1:
                    Strength++;
                    break;
                case 2:
                    Health += 5;
                    break;
                case 3:
                    Modifier += 0.1;
                    break;
            }
            Level++;
            return Level;
        }

        public int StaminaIncrease(int amount, bool increase)
        {
            if (increase)
            {
                Stamina += amount;
                return amount;
            }
            else
            {
                Stamina -= amount;
                return amount * -1;
            }
        }

        public int TakeDamage(int damage)
        {
            if (Health - damage > 0)
            {
                Health -= damage;
                return Health;
            }
            else
            {
                Health = 0;
                return Health;
            }
        }
    }
}
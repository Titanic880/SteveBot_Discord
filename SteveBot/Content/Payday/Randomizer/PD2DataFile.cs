using static Content.Payday.Randomizer.Generic_Data;
using System.Linq;
using System;

namespace Content.Payday.Randomizer
{
    public class PD2DataFile
    {
        private readonly Random rand = new Random(DateTime.Now.Millisecond);
        #region Options
        /// <summary>
        /// Determines if random primary will only roll Akimbos when hitman it rolled
        /// </summary>
        private bool HitmanSafeGuard { get; set; } = true;
        private bool GrinderSafeGuard { get; set; } = true;
        private bool Allow_OneDown { get; set; } = true;
        #endregion Options
        #region Randomized Items
        /// <summary>
        /// Randomly Generated deck
        /// </summary>
        private string Current_Deck { get; set; } = null;
        /// <summary>
        /// Randomly Generated Throwable
        /// </summary>
        private static string Throwable { get; set; } = null;
        /// <summary>
        /// Randomly Generated Primary
        /// </summary>
        private static string PrimaryCat { get; set; } = null;
        /// <summary>
        /// Randomly Generated Secondary
        /// </summary>
        private static string SecondaryCat { get; set; } = null;
        /// <summary>
        /// Randomly Generated Melee type
        /// </summary>
        private static string MeleeCat { get; set; } = null;
        /// <summary>
        /// Randomly Generated Deployable
        /// </summary>
        private static string Deployable { get; set; } = null;
        /// <summary>
        /// Randomly generated Armor Tier
        /// </summary>
        private static string ArmorLv { get; set; } = null;
        private string Difficulty { get; set; } = null;

        #endregion Randomized Items

        #region SetRandomized
        public string GetResult()
        {
            return
            "Deck: " + Current_Deck +
            "\nPrimary: " + PrimaryCat +
            "\nSecondary: " + SecondaryCat +
            "\nMelee: " + MeleeCat +
            "\nArmor: " + ArmorLv +
            "\nThrowable: " + Throwable +
            "\nDeployable: " + Deployable +
            "\nDifficulty: " + Difficulty;
        }

        /// <summary>
        /// Sets all Random Values
        /// </summary>
        public void RandomizeAll()
        {
            RandPerkDeck();
            RandThrowable();
            RandPrimary();
            RandSecondary();
            RandMelee();
            RandDeployable();
            RandArmor();
            RandDifficulty();
        }

        public void RandPerkDeck() => Current_Deck = PerkDecks[rand.Next(PerkDecks.Length - 1)];
        /// <summary>
        /// Sets a random Throwable, Checks for perk deck first
        /// </summary>
        public void RandThrowable()
        {
            if (!DeckEquips.Contains(Current_Deck))
            {
                Throwable = Throwables[rand.Next(Throwables.Length - 1)];
                return;
            }
            //Checks if the current perk deck has an equipable, if it does sets it
            for (int i = 0; i < DeckEquips.Length; i++)
                if (Current_Deck == DeckEquips[i])
                {
                    Throwable = DeckThrowables[i];
                    return;
                }
            //if no deck specific throwable is found, run it against full list
            Throwable = Throwables[rand.Next(Throwables.Length - 1)];
        }

        public void RandPrimary()
        {
            if (Current_Deck == "Hitman" && HitmanSafeGuard)
                PrimaryCat = Primaries[rand.Next(3) + 5];
            else
                PrimaryCat = Primaries[rand.Next(Primaries.Length - 1)];
        }
        public void RandSecondary() => SecondaryCat = Secondaries[rand.Next(Secondaries.Length - 1)];
        public void RandMelee() => MeleeCat = Melees[rand.Next(Melees.Length - 1)];
        public void RandDeployable() => Deployable = Deployables[rand.Next(Deployables.Length - 1)]; 
        public void RandArmor() 
        {
            //Checks for the grinder deck and checks the safeguard
            if(Current_Deck == "Grinder" && GrinderSafeGuard)
            {
                int rnd = rand.Next(2);
                if (rnd == 1)
                    ArmorLv = "LightWeight Ballistic";
                else
                    ArmorLv = "Two Piece Suit";
            }
            else
                ArmorLv = Armor[rand.Next(Armor.Length - 1)]; 
        }
        public void RandDifficulty()
        {
            Difficulty = Difficulties[rand.Next(Difficulties.Length) + 1];
            //Check for One down mechanic addition
            if (Allow_OneDown && rand.Next(2) == 1)
                Difficulty += ": One Down";
        }
        #endregion SetRandomized
    }
}
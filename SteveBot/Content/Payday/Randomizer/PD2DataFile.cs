using static Content.Payday.Randomizer.Generic_Data;
using System;
using System.Linq;

namespace Content.Payday.Randomizer
{
    public class PD2DataFile
    {
        private readonly Random rand = new Random(DateTime.Now.Millisecond);
        #region Options
        /// <summary>
        /// Determines if random primary will only roll Akimbos when hitman it rolled
        /// </summary>
        public bool HitmanSafeGuard { get; set; } = true;
        public bool GrinderSafeGuard { get; set; } = true;
        //TODO: Find original intent
        public bool PerkDeck_SafeGuard { get; private set; } = false;
        public bool Allow_OneDown { get; private set; } = true;
        #endregion Options
        #region Randomized Items
        /// <summary>
        /// Randomly Generated deck
        /// </summary>
        public string Current_Deck { get; private set; } = null;
        /// <summary>
        /// Randomly Generated Throwable
        /// </summary>
        public static string Throwable { get; private set; } = null;
        /// <summary>
        /// Randomly Generated Primary
        /// </summary>
        public static string PrimaryCat { get; private set; } = null;
        /// <summary>
        /// Randomly Generated Secondary
        /// </summary>
        public static string SecondaryCat { get; private set; } = null;
        /// <summary>
        /// Randomly Generated Melee type
        /// </summary>
        public static string MeleeCat { get; private set; } = null;
        /// <summary>
        /// Randomly Generated Deployable
        /// </summary>
        public static string Deployable { get; private set; } = null;
        /// <summary>
        /// Randomly generated Armor Tier
        /// </summary>
        public static string ArmorLv { get; private set; } = null;
        public string Difficulty { get; private set; } = null;

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
        public void SetAll()
        {
            SetPerkDeck();
            SetThrowable();
            SetPrimary();
            SetSecondary();
            SetMelee();
            SetDeployable();
            SetArmor();
            SetDifficulty();
        }

        public void SetPerkDeck() => Current_Deck = PerkDecks[rand.Next(PerkDecks.Length - 1)];
        /// <summary>
        /// Sets a random Throwable, Checks for perk deck first
        /// </summary>
        public void SetThrowable()
        {
            if(DeckEquips.Contains(Current_Deck))
            {
                Throwable = Throwables[rand.Next(Throwables.Length - 1)];
                return;
            }
            ///Checks if the current perk deck has an equipable, if it does sets it
            for (int i = 0; i < DeckEquips.Length; i++)
                if (Current_Deck == DeckEquips[i])
                {
                    Throwable = DeckThrowables[i];
                    break;
                }

            if (Throwable == null)
                Throwable = Throwables[rand.Next(Throwables.Length - 1)];
        }

        public void SetPrimary()
        {
            if (Current_Deck == "Hitman" && HitmanSafeGuard)
                PrimaryCat = Primaries[rand.Next(3) + 5];
            else
                PrimaryCat = Primaries[rand.Next(Primaries.Length - 1)];
        }
        public void SetSecondary() => SecondaryCat = Secondaries[rand.Next(Secondaries.Length - 1)];
        public void SetMelee() => MeleeCat = Melees[rand.Next(Melees.Length - 1)];
        public void SetDeployable() => Deployable = Deployables[rand.Next(Deployables.Length - 1)]; 
        public void SetArmor() 
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
        public void SetDifficulty()
        {
            Difficulty = Difficulties[rand.Next(Difficulties.Length) + 1];
            //Check for One down mechanic addition
            if (Allow_OneDown && rand.Next(2) == 1)
                Difficulty += ": One Down";
        }
        #endregion SetRandomized

        #region Randomized Items

        #endregion Randomized Items
    }
}
using static PD2RandomizerData.Random_Settings;
using static PD2RandomizerData.Generic_Data;
using System;

namespace PD2RandomizerData
{
    public static class PD2Randomize
    {
        private readonly static Random rand = new Random();
        #region SetRandomized
        public static void SetAll()
        {
            Rand_PerkDeck();
            Rand_Throwable();
            Rand_Primary();
            Rand_Secondary();
            Rand_Melee();
            Rand_Deployable();
            Rand_Armor();
        }

        public static void Rand_PerkDeck() { Current_Deck = PerkDecks[rand.Next(PerkDecks.Length - 1)]; }
        /// <summary>
        /// Sets a random Throwable, Checks for perk deck first
        /// </summary>
        public static void Rand_Throwable()
        {
            if (Current_Deck != DeckEquips[0] 
             || Current_Deck != DeckEquips[1] 
             || Current_Deck != DeckEquips[2] 
             || Current_Deck != DeckEquips[3] 
             || Current_Deck != DeckEquips[4])
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
        public static void Rand_Primary()
        {
            if (Current_Deck == PerkDecks[4] && Hitman_SafeGuard)
                PrimaryCat = Primaries[rand.Next(3) + 5];
            else
                PrimaryCat = Primaries[rand.Next(Primaries.Length - 1)];
        }
        public static void Rand_Secondary() => SecondaryCat = Secondaries[rand.Next(Secondaries.Length - 1)]; 
        public static void Rand_Melee() => MeleeCat = Melees[rand.Next(Melees.Length - 1)]; 
        public static void Rand_Deployable() => Deployable = Deployables[rand.Next(Deployables.Length - 1)]; 
        public static void Rand_Armor() 
        {
            //Checks for the grinder deck and checks the safeguard
            if(Current_Deck == PerkDecks[10] && Grinder_SafeGuard)
            {
                int rnd = rand.Next(2);
                if (rnd == 1)
                    ArmorLv = Armor[2];
                else
                    ArmorLv = Armor[0];
            }
            else
                ArmorLv = Armor[rand.Next(Armor.Length - 1)]; 
        }
        public static void Rand_Difficulty()
        {
            Difficulty = Difficulties[rand.Next(Difficulties.Length - 1) + 1];
            if (Allow_OneDown && rand.Next(2) == 1)
                Difficulty += ": " + Difficulties[0];
        }
        #endregion SetRandomized
    }
}
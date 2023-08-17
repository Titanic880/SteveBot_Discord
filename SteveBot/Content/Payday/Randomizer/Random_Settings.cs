using System.Linq;

namespace Content.Payday.Randomizer
{
    public static class Random_Settings
    {
        #region Randomized Items
        public static bool Hitman_SafeGuard;
        public static bool Grinder_SafeGuard;
        public static bool PerkDeck_SafeGuard;
        public static bool Allow_OneDown;

        public static string Current_Deck;
        public static string Throwable;
        public static string PrimaryCat;
        public static string SecondaryCat;
        public static string MeleeCat;
        public static string Deployable;
        public static string ArmorLv;
        public static string Difficulty;
        #endregion Randomized Items

        public static bool Check_PerkDeck_SafeGuard()
        {
            if (string.IsNullOrEmpty(Current_Deck)) return false;
            if(Generic_Data.DeckThrowables.Contains(Current_Deck))
            { }

            throw new System.Exception("Not implemented");
        }
    }
}

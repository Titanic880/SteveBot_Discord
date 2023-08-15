using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD2RandomizerData
{
    public static class PD2DataFile
    {
        private readonly static Random rand = new Random();
        #region Options
        /// <summary>
        /// Determines if random primary will only roll Akimbos when hitman it rolled
        /// </summary>
        public static bool HitmanSafeGuard { get; private set; } = true;
        public static bool GrinderSafeGuard { get; private set; } = true;
        #endregion Options

        #region Base information
        /// <summary>
        /// All Perk Decks
        /// </summary>
        readonly static string[] PerkDecks =
            {"Crew Chief",
            "Muscle",
            "Armorer",
            "Rogue",
            "Hitman",
            "Crook",
            "Burglar",
            "Infiltrator",
            "Scociopath",
            "Gambler",
            "Grinder",
            "Yakuza",
            "Ex-President",
            "Anarchist",
            "Biker",
            "Kingpin",
            "Sicario",
            "Stoic",
            "Tag Team",
            "Hacker"
        };
        /// <summary>
        /// All perk decks with an equipable weapon
        /// </summary>
        readonly static string[] DeckEquips =
        {
            "Kingpin",
            "Sicario",
            "Stoic",
            "Tag Team",
            "Hacker"
        };
        /// <summary>
        /// Throwable items from perk decks
        /// </summary>
        readonly static string[] DeckThrowables =
        {
            "Injector",
            "Smoke Bomb",
            "Stoic's Hip Flask",
            "Gas Dispenser",
            "Pocket ECM Device"
        };
        /// <summary>
        /// All Primary weapon Categories
        /// </summary>
        readonly static string[] Primaries =
        {
            "Assault Rifles",
            "Shotguns",
            "LMG's",
            "Sniper",
            "Akimbo Pistol",
            "Akimbo SMG",
            "Akimbo Shotgun",
            "Special"
        };
        /// <summary>
        /// All Secondary weapon Categories
        /// </summary>
        readonly static string[] Secondaries =
        {
            "Light Pistol", //Sub 100 damage
            "Heavy Pistol", //Post 100 damage
            "SMG",
            "Special",
            "Shotgun"
        };
        /// <summary>
        /// Specified melee weapon types
        /// </summary>
        readonly static string[] Melees =
        {
            "None",      //Bare handed or weapon butt
            "Stun Type", //things that stop enemies from attacking
            "Blades",
            "Blunted",
            "MONEY!!!"
        };
        /// <summary>
        /// Throwable types
        /// </summary>
        readonly static string[] Throwables =
        {
            "Explosive",
            "Flashbang",
            "Renewable",
            "Perk Deck"
        };
        /// <summary>
        /// All Equipable armors
        /// </summary>
        readonly static string[] Armor =
        {
            "Suit",
            "Ballistic Vest",
            "LightWeight Ballistic",
            "Heavy Ballistic",
            "Flak Jacket",
            "Combined Tactical",
            "ICTV"
        };
        /// <summary>
        /// All Deployables
        /// </summary>
        readonly static string[] Deployables =
        {
            "Ammo Bag",
            "Armor Bag",
            "Body Bag Case",
            "Doctor Bag",
            "Ecm",
            "FaK",
            "Sentry",
            "Suppressed Sentry",
            "Trip Mine",
            "Empty"

        };
        #endregion Base information
        #region Randomized Items
        /// <summary>
        /// Randomly Generated deck
        /// </summary>
        public static string Current_Deck { get; private set; } = null;
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
        #endregion Randomized Items

        #region GetMethods
        public static string[] GetPerkDeck() { return PerkDecks; }
        public static string[] GetPerkDecksWithEquip() { return DeckEquips; }
        public static string[] GetPerkDeckThrowables() { return DeckThrowables; }
        public static string[] GetPrimaries() { return Primaries; }
        public static string[] GetMeleeCats() { return Melees; }
        public static string[] GetThrowables() { return Throwables; }
        public static string[] GetArmorLv() { return Armor; }
        public static string[] GetDeployables() { return Deployables; }
        #endregion GetMethods

        #region PullSpecific
        /// <summary>
        /// Searches for the specific index, if not found, returns 0
        /// </summary>
        /// <param name="Location"></param>
        /// <returns></returns>
        public static string GetSpecificPerkDeck(int Location)
        {
            if (Location >= PerkDecks.Length)
                return PerkDecks[0];
            else
                return PerkDecks[Location];
        }
        /// <summary>
        /// Searches for the specific index, if not found, returns 0
        /// </summary>
        /// <param name="Location"></param>
        /// <returns></returns>
        public static string GetSpecificPerkDecksWithEquip(int Location)
        {
            if (Location < DeckEquips.Length)
                return DeckEquips[0];
            else
                return DeckEquips[Location];
        }
        /// <summary>
        /// Searches for the specific index, if not found, returns 0
        /// </summary>
        /// <param name="Location"></param>
        /// <returns></returns>
        public static string GetSpecificPerkDeckThrowables(int Location)
        {
            if (Location < DeckThrowables.Length)
                return DeckThrowables[0];
            else
                return DeckThrowables[Location];
        }
        /// <summary>
        /// Searches for the specific index, if not found, returns 0
        /// </summary>
        /// <param name="Location"></param>
        /// <returns></returns>
        public static string GetSpecificPrimaries(int Location)
        {
            if (Location < Primaries.Length)
                return Primaries[0];
            else
                return Primaries[Location];
        }
        /// <summary>
        /// Searches for the specific index, if not found, returns 0
        /// </summary>
        /// <param name="Location"></param>
        /// <returns></returns>
        public static string GetSpecificMeleeCats(int Location)
        {
            if (Location < Melees.Length)
                return Melees[0];
            else
                return Melees[Location];
        }
        /// <summary>
        /// Searches for the specific index, if not found, returns 0
        /// </summary>
        /// <param name="Location"></param>
        /// <returns></returns>
        public static string GetSpecificThrowables(int Location)
        {
            if (Location < Throwables.Length)
                return Throwables[0];
            else
                return Throwables[Location];
        }
        /// <summary>
        /// Searches for the specific index, if not found, returns 0
        /// </summary>
        /// <param name="Location"></param>
        /// <returns></returns>
        public static string GetSpecificArmorLv(int Location)
        {
            if (Location < Armor.Length)
                return Armor[0];
            else
                return Armor[Location];
        }
        /// <summary>
        /// Searches for the specific index, if not found, returns 0
        /// </summary>
        /// <param name="Location"></param>
        /// <returns></returns>
        public static string GetSpecificDeployables(int Location)
        {
            if (Location < Deployables.Length)
                return Deployables[0];
            else
                return Deployables[Location];
        }
        #endregion PullSpecific

        #region SetRandomized
        /// <summary>
        /// Sets all Random Values
        /// </summary>
        public static void SetAll()
        {
            SetPerkDeck();
            SetThrowable();
            SetPrimary();
            SetSecondary();
            SetMelee();
            SetDeployable();
            SetArmor();
        }

        /// <summary>
        /// Sets a random Perk Deck
        /// </summary>
        public static void SetPerkDeck() { Current_Deck = PerkDecks[rand.Next(PerkDecks.Length - 1)]; }
        /// <summary>
        /// Sets a random Throwable, Checks for perk deck first
        /// </summary>
        public static void SetThrowable()
        {
            if (Current_Deck != DeckEquips[0] || Current_Deck != DeckEquips[1] || Current_Deck != DeckEquips[2] || Current_Deck != DeckEquips[3] || Current_Deck != DeckEquips[4])
            {
                Throwable = Throwables[rand.Next(Throwables.Length - 1)];
                return;
            }
            ///Checks if the current perk deck has an equipable, if it does sets it
            for (int i = 0; i < DeckEquips.Length; i++)
            {
                if (Current_Deck == DeckEquips[i])
                {
                    Throwable = DeckThrowables[i];
                    break;
                }
                else
                    continue;
            }
            if (Throwable == null)
                Throwable = Throwables[rand.Next(Throwables.Length - 1)];
        }
        public static void SetPrimary()
        {
            if (HitmanSafeGuard)
                if (Current_Deck == "Hitman")
                {
                    int rnd = rand.Next(3) + 5;
                    PrimaryCat = Primaries[rnd];
                    return;
                }
            PrimaryCat = Primaries[rand.Next(Primaries.Length - 1)];
        }
        public static void SetSecondary() { SecondaryCat = Secondaries[rand.Next(Secondaries.Length - 1)]; }
        public static void SetMelee() { MeleeCat = Melees[rand.Next(Melees.Length - 1)]; }
        public static void SetDeployable() { Deployable = Deployables[rand.Next(Deployables.Length - 1)]; }
        public static void SetArmor() 
        {
            //Checks for the grinder deck and checks the safeguard
            if(Current_Deck == "Grinder" && GrinderSafeGuard)
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

        public static void SetHitmanSafe(bool guard)
        {
            HitmanSafeGuard = guard;
        }
        public static void SetGrinderSafe(bool guard)
        {
            GrinderSafeGuard = guard;
        }
        #endregion SetRandomized
    }
}
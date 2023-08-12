namespace SteveBot.Content.Runescape
{
    public enum RS3Rituals
    {
        LesNecro = 0,
        LesCommun = 1,
        LesEnsoul,
        LesEss,
        GreNecro,
        GreCommun,
        GreEnsoul,
        GreEss,
        PowNecro,
        PowCommun,
        PowEnsoul,
        PowEss
    }
    public enum RS3Glyphs
    {
        Commune1, Commune2, Commune3,
        Elemental1, Elemental2, Elemental3,
        Reagent1, Reagent2, Reagent3,
        Change1, Change2, Change3,
        Multiply1, Multiply2, Multiply3,
        Protection1, Protection2, Protection3,
        Speed1, Speed2, Speed3,
        Attraction1, Attraction2, Attraction3,
    }
    public enum RS3GlyphLifetime
    {
        Core1,
        Core2,
        Core3,
        Alt1,
        Alt2,
        Alt3,
        BasicCandle,
        WhiteCandle,
        RegularCandle,
        GreaterCandle,
        SkullCandle
    }
    internal class RSJson
    {
        public int[] NecroplasmPrices = new int[3] { 0, 1, 2 }; //Basic ink not included as price is always 1 (Buying from shop)
        public int[] InkPrices = new int[3] { 0, 1, 2 };
        private readonly int[] RitualDurations = { 30, 60, 84 }; //Amount of time stated in game for the ritual to take (Assumed to be seconds)
        private readonly int[] RitualsPerHour = { 119, 59, 42 }; //Theoretical Max amount of rituals per hour (-1 for est inbetweens)
        public int AshPrice = 69;
        public int VialOfWater = 10; //Shop Value
        /// <summary>
        /// outputs total setup cost (not running/1 ritual)
        /// </summary>
        /// <param name="ritual"></param>
        /// <returns></returns>
        public int RitualSetup_Cost(RS3Rituals ritual)
        {
            //Pure numbers being added is the price of the Basic Ink ex: LesNecro returns 7 always
            switch (ritual)
            {
                case RS3Rituals.LesNecro:
                    return 7;
                case RS3Rituals.LesCommun:
                    return 11;
                case RS3Rituals.LesEnsoul:
                    return 7 + (InkPrices[0] * 2);
                case RS3Rituals.LesEss:
                    return 8 + (InkPrices[0] * 4);
                case RS3Rituals.GreNecro:
                    return 13 + (InkPrices[0] * 10);
                case RS3Rituals.GreCommun:
                    return 8 + (InkPrices[0] * 6) + (InkPrices[1] * 8);
                case RS3Rituals.GreEnsoul:
                    return 7 + (InkPrices[0] * 8) + (InkPrices[1] * 4);
                case RS3Rituals.GreEss:
                    return 4 + (InkPrices[0] * 9) + (InkPrices[1] * 6);
                case RS3Rituals.PowNecro:
                    return 4 + (InkPrices[0] * 20) + (InkPrices[1] * 16);
                case RS3Rituals.PowCommun:
                    return (InkPrices[0] * 8) + (InkPrices[1] * 20) + (InkPrices[2] * 8);
                case RS3Rituals.PowEss:
                    return (InkPrices[0] * 10) + (InkPrices[1] * 18) + (InkPrices[2] * 4);
                case RS3Rituals.PowEnsoul:
                    return 6 + (InkPrices[0] * 11) + (InkPrices[1] * 4);
                default:
                    return -1;
            }
        }
        /// <summary>
        /// Cost of a single Run
        /// </summary>
        /// <param name="ritual"></param>
        /// <returns></returns>
        public int RitualRun_Cost(RS3Rituals ritual)
        {
            int InitCost = RitualSetup_Cost(ritual);
            return -1;
        }
        public int GlyphLifeSpan(RS3GlyphLifetime Lifetime)
        {
            switch (Lifetime)
            {
                case RS3GlyphLifetime.Core1:
                    return 6;
                case RS3GlyphLifetime.Core2:
                    return 12;
                case RS3GlyphLifetime.Core3:
                    return 18;
                case RS3GlyphLifetime.Alt1:
                    return 3;
                case RS3GlyphLifetime.Alt2:
                    return 6;
                case RS3GlyphLifetime.Alt3:
                    return 9;
                case RS3GlyphLifetime.BasicCandle:
                    return 6;
                case RS3GlyphLifetime.WhiteCandle:
                    return 9;
                case RS3GlyphLifetime.RegularCandle:
                    return 12;
                case RS3GlyphLifetime.GreaterCandle:
                    return 18;
                case RS3GlyphLifetime.SkullCandle:
                    return 36;
                default:
                    return -1;
            }
        }
        /*
        public RS3Glyphs[] RitualGlyphs(RS3Rituals ritual)
        {
            switch (ritual)
            {
                case RS3Rituals.LesNecro:
                    return new RS3Glyphs[] { RS3Glyphs.Elemental1, 
                                             RS3Glyphs.Reagent1 };
                case RS3Rituals.LesCommun:
                    return new RS3Glyphs[] { RS3Glyphs.Elemental1,
                                             RS3Glyphs.Commune1,RS3Glyphs.Commune1 };
                case RS3Rituals.LesEnsoul:
                    break;
                case RS3Rituals.LesEss:
                    break;
                case RS3Rituals.GreNecro:
                    return new RS3Glyphs[] { RS3Glyphs.Elemental1, 
                                             RS3Glyphs.Elemental2, RS3Glyphs.Elemental2, 
                                             RS3Glyphs.Reagent2,RS3Glyphs.Reagent2 };
                case RS3Rituals.GreCommun:
                    return new RS3Glyphs[] { RS3Glyphs.Elemental2,RS3Glyphs.Elemental2,
                                             RS3Glyphs.Commune1,  RS3Glyphs.Commune2,RS3Glyphs.Commune2 };
                case RS3Rituals.GreEnsoul:
                    break;
                case RS3Rituals.GreEss:
                    break;
                case RS3Rituals.PowNecro:
                    return new RS3Glyphs[] { RS3Glyphs.Elemental2,RS3Glyphs.Elemental2,RS3Glyphs.Elemental2,
                                             RS3Glyphs.Elemental3,RS3Glyphs.Elemental3,RS3Glyphs.Elemental3,RS3Glyphs.Elemental3,
                                             RS3Glyphs.Reagent3  ,RS3Glyphs.Reagent3};
                case RS3Rituals.PowCommun:
                    return new RS3Glyphs[] { RS3Glyphs.Elemental3, };
                case RS3Rituals.PowEss:
                    break;
                default:
                    return new RS3Glyphs[0];
            }*/
    }
}


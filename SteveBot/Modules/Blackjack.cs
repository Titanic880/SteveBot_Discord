using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SteveBot.Modules
{
    struct Pframe
    {
        public bool Dealer;
        public int[] Hand;
        public IGuildUser ID;
    }
    public enum CardSuite
    {
        Hearts,
        Spades,
        Clubs,
        Diamonds
    }
    public struct Card
    {
        public int Value;
        public CardSuite Suit;
    }
    public class Blackjack : ModuleBase<SocketCommandContext>
    { 
        private Pframe player;
        private Pframe dealer;
        private int[] Deck;

        [Command("blackjack deal")]
        public async Task BlackJackDeal(IGuildUser user = null)
        {
            if (user.IsBot)
                return;

            await ReplyAsync("");
        }
        Blackjack()
        {
            Deck = new int[52];
        }
        private void Shuffle()
        {
            //Pick a random card from the deck
            //to swap with another card
            int num1 = main.rand.Next(0, 52);
            int num2 = main.rand.Next(0, 52);
            //Do this a lot to try and ensure randomness
            for (int i = 0; i < 10000; i++)
            {
                num1 = main.rand.Next(0, 52);
                num2 = main.rand.Next(0, 52);
                //Stashing the old card in memory
                //Card tmp = carddeck.Deck[num1];
                //lstCards[num1] = lstCards[num2];
                //lstCards[num2] = tmp;
            }
        }
    }
}

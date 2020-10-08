using Discord;
using Discord.Commands;
using System.Threading.Tasks;


namespace SteveBot.Modules
{
    struct Pframe
    {
        public bool Dealer;
        public int[] Hand;
        public IGuildUser ID;
    }
    public class Card
    {
        public enum CardSuit
        {
            Hearts,
            Spades,
            Clubs,
            Diamonds
        }
        public int Value;
        public CardSuit Suit;

        public Card(int arg, CardSuit suit)
        {
            this.Value = arg;
            this.Suit = suit;
        }
    }
    public class Blackjack : ModuleBase<SocketCommandContext>
    { 
        private Pframe player;
        private Pframe dealer;
        private int[] Deck;


        Blackjack()
        {
            Deck = new int[52];
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SteveBot.Modules.BlackJack
{
    class BJSIM
    {
        List<string> lst = new List<string>();
        private int Pwin, Dwin;
        private double dealer_ratio;
        private double player_ratio;
        public double Dealer_Ratio { get { return dealer_ratio; } }
        public double Player_Ratio { get { return player_ratio; } }
        private int gamesplayed;
        private int[] Output;
        public BJSIM()
        {

        }
        public async System.Threading.Tasks.Task Simulator(int input)
        {
            gamesplayed = input;
            int[] output = new int[2];
            if (input == 0) input = 1;
            for (int i = 0; i < input; i++)
            { 
                Blackjack game = new Blackjack();
                Player winner = game.playgame();
                Console.WriteLine(i+"/"+input);
                if (winner.IsDealer)
                    Dwin++;
                else
                    Pwin++;
            }
            Winratio(Dwin, Pwin, input);
            output[0] = Dwin;
            output[1] = Pwin;
            Output = output;
        }
        private void Winratio(int dealer, int player, int total)
        {
            dealer_ratio = dealer / total;
            player_ratio = player / total;
        }
        public string Buildoutput()
        {
            string tmp = $"In {gamesplayed} games" +
                         $"\nThe dealer won {Output[0]} times " +
                         $"\nThe player won {Output[1]} times";
            return tmp;
        }
    }
}

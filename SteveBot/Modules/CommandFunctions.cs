using System;
using System.Collections.Generic;
using System.IO;

namespace SteveBot.Modules
{
    static class CommandFunctions
    {
        #region Links
        private static List<string> Links;
        public static List<string> LinksPub { get { return Links; } }
        private static string linkPath = "Files/Links.txt";
        private static string usercommandsPath = "Files/UserCommands.txt";
        private static string usermessagesPath = "Files/UserMessages.txt";

        public static void UpdateLinks(List<string> strlst)
        {
            //Checks to see if list is empty, if so output console command
            if (strlst == null)
            {
                Console.WriteLine("Links list was empty, Please check Links File");
                return;
            }
            Links = strlst;
        }
        public static int AddLink(string link)
        {
            Links.Add(link);
            File.AppendAllText(linkPath, "\n" + link);
            return Links.Count;
        }
        public static void RemoveLink(int input)
        {
            Links.RemoveAt(input);
            foreach (string tmp in Links)
                File.WriteAllText(linkPath, "\n" + tmp);
        }
        #endregion Links
        #region Logging
        public static void UserCommand(Discord.WebSocket.SocketUserMessage message)
        {
            //Checks for file
            if (!File.Exists(usercommandsPath))
                File.Create(usercommandsPath).Close();
            //Writes to file
            StreamWriter sW = File.AppendText(usercommandsPath);
            sW.WriteLine($"{message.Content}, {message.Author}, {message.CreatedAt.DateTime}");
            sW.Close();
        }
        public static void UserMessages(Discord.WebSocket.SocketUserMessage message)
        {
            //Checks for file
            if (!File.Exists(usermessagesPath))
                File.Create(usermessagesPath).Close();
            //Writes to file
            StreamWriter sW = File.AppendText(usermessagesPath);
            sW.WriteLine($"{message.Content}, {message.Author}, {message.CreatedAt.DateTime}");
            sW.Close();
        }
        #endregion Logging
        #region DiceGames
        public static string DiceRoll(int dice_size)
        {
            string output = Blackjack.rand.Next(dice_size).ToString();
            return output;
        }

        #endregion DiceGames
    }
}
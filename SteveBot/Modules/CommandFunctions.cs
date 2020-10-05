using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveBot.Modules
{
    static class CommandFunctions
    {
        private static List<string> Links;
        public static List<string> LinksPub { get { return Links; } }
        public static void UpdateLinks(List<string> strlst)
        {
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
            File.AppendAllText("../../Links.txt","\n"+link);
            return Links.Count;
        }
        public static void RemoveLink(int input)
        {
            Links.RemoveAt(input);

            foreach(string tmp in Links)
            File.WriteAllText("../../Links.txt", "\n" + tmp);
        }
    }
}

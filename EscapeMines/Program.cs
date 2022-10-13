using EscapeMines.Logic;
using EscapeMines.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EscapeMines
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SettingsLoad settings = new SettingsLoad("settings.txt");
            GamePlay gamePlay = new GamePlay(settings.LoadSettings(), settings.Moves(), settings.StartingDir, settings.StartingPos);
            gamePlay.Play();

            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static EscapeMines.Models.Enums;

namespace EscapeMines.Models
{
    public class SettingsLoad
    {
        public string[] TextFile { get; set; }
        public List<(int,int)> Mines { get; set; }
        public (int,int) Exit { get; set; }
        public (int,int) StartingPos { get; set; }
        public string StartingDir { get; set; }
        public string[,] GameBoard { get; set; }

        public SettingsLoad(string textFile)
        {
            TextFile = File.ReadAllLines(textFile).Select(x => x == null ? null : x.Trim()).ToArray();
        }

        //Putting it all together
        public string[,] LoadSettings()
        {
            SettingsValidation();
            BoardInitialize();
            MineInitialize();
            ExitInitialize();
            StartInitialize();
            PointsSet();
            return GameBoard;
        }
        //Set the board
        private void BoardInitialize()
        {
            GameBoard = new Board(int.Parse(this.TextFile[0].Split(' ')[0]), int.Parse(this.TextFile[0].Split(' ')[1])).SetBoard();
        }
        //Set mines
        public void MineInitialize()
        {
            this.Mines = new List<(int, int)>();
            if (this.TextFile[1].Length < 2)
            {
                Console.WriteLine("Wrong mines line");
                return;
            }
            this.TextFile[1].Split(' ').Select(d => d.Trim()).Where(t => t != "").ToList().ForEach(t =>
            {
                try
                {
                    Point mine = new Point(int.Parse(t.Split(',')[0]), int.Parse(t.Split(',')[1]));
                    Mines.Add(mine.SetPoint());
                }
                catch
                {
                    Console.WriteLine("Mine point initialize failed\n");
                    Environment.Exit(0);
                }
            });
        }
        //Set exit point
        public void ExitInitialize()
        {
            if (this.TextFile[2].Length < 2)
            {
                Console.WriteLine("Wrong exit line");
                return;
            }
            string[] exits = this.TextFile[2].Split(' ').Select(d => d.Trim()).Where(t => t != "").ToArray();
            try
            {
                Point exitPoint = new Point(int.Parse(exits[0]), int.Parse(exits[1]));
                Exit = exitPoint.SetPoint();
            }
            catch
            {
                Console.WriteLine("Exit point initialize failed");
                Environment.Exit(0);
            }
        }
        //Set start point
        public void StartInitialize()
        {
            if (this.TextFile[3].Length < 2)
            {
                Console.WriteLine("Wrong start line");
                return;
            }
            string[] start = this.TextFile[3].Split(' ').Select(d => d.Trim()).Where(t => t != "").ToArray();
            try
            {
                Point startPoint = new Point(int.Parse(start[0]), int.Parse(start[1]));
                StartingPos = startPoint.SetPoint();
                StartingDir = start[2];
            }
            catch
            {
                Console.WriteLine("Start point initialize failed");
                Environment.Exit(0);
            }
        }
        //Add move lines to list
        public List<string> Moves()
        {
            List<string> moves = new List<string>();

            for (int i = 4; i < this.TextFile.Length; i++)
            {
                moves.Add(this.TextFile[i]);
            }
            return moves;
        }
        //Set up the points to the board
        public void PointsSet()
        {
            Mines.ForEach(m =>
            {
                GameBoard[m.Item1, m.Item2] = PointType.Mine.ToString();
            });
            GameBoard[Exit.Item1, Exit.Item2] = PointType.Exit.ToString();
            GameBoard[StartingPos.Item1, StartingPos.Item2] = PointType.Start.ToString();
        }
        //Validate settings text file
        public void SettingsValidation()
        {
            switch (this.TextFile.Length)
            {
                case 0:
                    Console.WriteLine("File does not contain any game info");
                    break;
                case 1:
                    Console.WriteLine("File does not contain mine info");
                    break;
                case 2:
                    Console.WriteLine("File does not contain exit point");
                    break;
                case 3:
                    Console.WriteLine("File does not contain starting position");
                    break;
                case 4:
                    Console.WriteLine("Moves are missing");
                    break;
            }
        }

    }
}

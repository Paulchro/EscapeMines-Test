using System;
using System.Collections.Generic;
using System.Linq;
using static EscapeMines.Models.Enums;

namespace EscapeMines.Logic
{
    public class GamePlay
    {
        private readonly List<string> Moves;
        private readonly string[,] BoardNew;
        private readonly string StartingDir;
        private (int,int) Startpoint;
       
        public GamePlay(string[,] board, List<string> moves, string startingDir, (int,int) startpoint)
        {
            Moves = moves;
            BoardNew = board;
            StartingDir = startingDir;
            Startpoint = startpoint;
        }
        //Main play
        public void Play()
        {
            for (int i = 0; i < Moves.Count; i++)
            {
                Console.WriteLine($"For the move lines no:{i + 1}, result is: {ResultEvaluation(Run(i))}");
            }

        }
        //Result evaluation
        private string ResultEvaluation(Results results)
        {
            string result;
            switch(results)
            {
                case Results.Success:
                    result = "Turtle escaped successfully";
                    break;
                case Results.MineHit:
                    result = "Mine hit";
                    break;
                case Results.InDanger:
                    result = "Turtle is still in danger!";
                    break;
                default:
                    result = "Failed to run this round or turtle hit on wall";
                    break;
            }
            return result;
         
        }
        //Cell type check after each movement
        private Results MoveCheck((int,int) curPossition, string[] movesArr, int y)
        {
            if (curPossition.Item1 >= 0 && curPossition.Item2 >= 0 && curPossition.Item1 <= BoardNew.GetLength(0) && curPossition.Item2 <= BoardNew.GetLength(1))
            {
                if (BoardNew[curPossition.Item1, curPossition.Item2] == "Mine")
                {
                    return Results.MineHit;
                }
                else if (BoardNew[curPossition.Item1, curPossition.Item2] == "Exit")
                {
                    return Results.Success;
                }
                else if (y == movesArr.Length - 1)
                {
                    return Results.InDanger;
                }
            }
            else
            {
                return Results.Fail;
            }
            return Results.None;
        }
        //Run moves
        private Results Run(int i)
        {
            (int,int) curPossition = (Startpoint.Item1, Startpoint.Item2);
            Direction direction = (Direction)Enum.Parse(typeof(Direction), StartingDir);
            string[] movesArr = Moves[i].ToString().Split(' ').Select(d => d.Trim()).Where(x => x != "").ToArray();
            Results results = Results.None;
            for (int y = 0; y < movesArr.Length; y++)
            {
                if (results == Results.None && movesArr[y] == "M")
                {
                    switch (direction)
                    {
                        case Direction.N:
                            curPossition.Item2--;
                            break;
                        case Direction.W:
                            curPossition.Item1++;
                            break;
                        case Direction.S:
                            curPossition.Item2++;
                            break;
                        default:
                            curPossition.Item1--;
                            break;
                    }
                    results = MoveCheck(curPossition, movesArr, y);
                }
                direction = DirectionChanger(direction, movesArr, y);
            }
            return results;
        }
        //Direction changer (Move = R or L)
        private static Direction DirectionChanger(Direction direction, string[] movesArr, int y)
        {
            if (movesArr[y] == "R")
            {
                if (direction < Direction.E)
                {
                    direction++;
                }
                else
                {
                    direction = Direction.N;
                }
            }
            if (movesArr[y] == "L")
            {
                if (direction > Direction.N)
                {
                    direction--;
                }
                else
                {
                    direction = Direction.E;
                }
            }
            return direction;
        }
    }
}

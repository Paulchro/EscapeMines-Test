using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeMines.Models
{
    public class Board
    {
        public string[,] BoardField { get; set; }
        public Board(int width, int height)
        {
            BoardField = new string[width, height];
        }

        public string[,] SetBoard()
        {
            return this.BoardField;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EscapeMines.Models.Enums;

namespace EscapeMines.Models
{
    internal class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
       
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public (int,int) SetPoint()
        {
            return (this.X, this.Y);
        }
    }
}

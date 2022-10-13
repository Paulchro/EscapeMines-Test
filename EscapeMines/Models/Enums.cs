using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeMines.Models
{
    internal class Enums
    {
        public enum Direction
        {
            N,
            W,
            S,
            E
        }

        public enum Results
        {
            None,
            Success,
            MineHit,
            InDanger,
            Fail
        }
        public enum PointType
        {
            Mine,
            Exit,
            Start
        }
    }
}

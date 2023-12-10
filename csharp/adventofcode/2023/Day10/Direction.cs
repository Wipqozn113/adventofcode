using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day10
{
    public enum Direction
    {
        North = -1,
        East  = -2,
        South = 1,
        West  = 2
    }

    public static class DirectionHelpers
    {
        public static Direction FlipDirection(Direction direction)
        {
            return (Direction)((int)direction * -1);
        }
    }
}

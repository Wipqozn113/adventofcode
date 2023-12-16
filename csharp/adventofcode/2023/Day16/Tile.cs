using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day16
{
    public class Tile
    {
        public Tile(char occupant, int x, int y)
        {
            Occupant = occupant;
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public bool Energized { get; set; } = false;

        public char Occupant { get; set; }
    }
}

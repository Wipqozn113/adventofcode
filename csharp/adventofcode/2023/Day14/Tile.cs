using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day14
{
    public class Tile
    {
        public Tile(char occupant, int x, int y, int distanceToBottom)
        {
            Occupant = occupant;
            X = x;
            Y = y;
            DistanceToBottom = distanceToBottom;
        }

        public char Occupant { get; set; }

        public int X { get; set; }
        
        public int Y { get; set; }

        public int DistanceToBottom { get; set; }

        public bool IsRoundRock
        {
            get
            {
                return Occupant == 'O';
            }
        }

        public bool IsEmpty
        {
            get
            {
                return Occupant == '.';
            }
        }

        public int Load()
        {
            return Occupant == 'O' ? DistanceToBottom : 0;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day10
{
    public class Tile
    {
        public Tile(int x, int y, char symbol)
        {
            X = x;
            Y = y;
            Symbol = symbol;
            IsStart = (symbol == 'S');
        }

        public int X { get; set; }

        public int Y { get; set; }

        public char Symbol
        {
            get
            {
                return _symbol;
            }
            set
            {
                _symbol = value;
                Directions.Clear();
                North = new [] { '|', 'L', 'J', '.' }.Contains(value);
                East  = new [] { '-', 'L', 'F', '.' }.Contains(value);
                South = new [] { '|', 'F', '7', '.' }.Contains(value);
                West  = new [] { '-', 'J', '7', '.' }.Contains(value);

                if(North) Directions.Add(Direction.North);
                if(East)  Directions.Add(Direction.East);
                if(South) Directions.Add(Direction.South);
                if(West)  Directions.Add(Direction.West);
            }
        }

        private char _symbol;

        public bool North { get; set; }

        public bool South { get; set; }

        public bool East { get; set; }  

        public bool West { get; set; }

        public List<Direction> Directions { get; set; } = new List<Direction>();

        public bool IsStart { get; set; }
    }
}

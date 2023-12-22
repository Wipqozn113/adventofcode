using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day21
{
    public class Garden
    {
        public List<List<Square>> Map { get; set; } = new List<List<Square>>();

        public Square? StartSquare { get; set; }

        public void ParseLine(string line)
        {
            var squares = new List<Square>();
            foreach(var ln in line.Trim())
            {
                var square = new Square(ln, squares.Count, Map.Count);
                squares.Add(square);
                if(square.IsStart)
                {
                    StartSquare = square;
                }
                
            }
            Map.Add(squares);
        }

        public void PrintMap()
        {

            foreach(var squares in Map)
            {
                var line = "";
                foreach (var square in squares)
                {
                    line += square.IsFilled ? 'O' : square.Occupant;
                }
                Console.WriteLine(line);
            }

        }

        public long RunFloodFill()
        {
            StartSquare?.FloodFill(0, this);
           // PrintMap();
            return Map.SelectMany(l => l).Where(s => s.IsFilled && s.StepsToReach % 2 == 0).Count();
        }
    }
}

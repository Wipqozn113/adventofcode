using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AOCUtils.MathUtils;

namespace AOTC2024.Day6
{
    public class Map
    {
        public Map(List<string> input)
        {
            Squares = new List<List<Square>>();
            GuardStartingCoordinate = new CoordinateInt(0, 0);

            var y = 0;
            foreach(var line in input)
            {
                var squares = new List<Square>();
                var x = 0;
                foreach(var c in line)
                {
                    var square = new Square();
                    if(c == '#')
                        square.HasObstacle = true;
                    if (c == '^')
                    {
                        square.Visited = true;
                        GuardStartingCoordinate = new CoordinateInt(x, y);
                    }
                    squares.Add(square);
                    x++;
                }
                Squares.Add(squares);
                y++;
            }
        }        

        private class Square
        {
            public bool Visited { get; set; }

            public bool HasObstacle { get; set; }   
        }

        private List<List<Square>> Squares { get; set; }

        public CoordinateInt GuardStartingCoordinate { get; set; }

        public int SquaresVisited()
        {
            var visited = 0;
            foreach(var square in Squares.SelectMany(x => x))
            {
                if(square.Visited)
                    visited++;
            }
            return visited;
        }

        public void MarkVisited(CoordinateInt coordinate)
        {
            Squares[coordinate.Y][coordinate.X].Visited = true;
        }

        public bool IsBlocked(CoordinateInt coordinate)
        {
            if (IsWithinMap(coordinate))
                return Squares[coordinate.Y][coordinate.X].HasObstacle;

            return false;
        }

        public bool IsWithinMap(CoordinateInt coordinate)
        {
            try
            {
                var square = Squares[coordinate.Y][coordinate.X];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

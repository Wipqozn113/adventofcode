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
        public Map(List<List<Square>> squares, CoordinateInt guardStartingCoordinate)
        {
            Squares = squares;
            GuardStartingCoordinate = guardStartingCoordinate;
        }

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
                        square.HasGuard = true;
                        GuardStartingCoordinate = new CoordinateInt(x, y);
                    }
                    squares.Add(square);
                    x++;
                }
                Squares.Add(squares);
                y++;
            }
        }        

        public class Square
        {
            public bool Visited { get; set; }

            public bool HasObstacle { get; set; }   

            public bool HasGuard { get; set; }

            public List<Facing> Facings { get; set; } = new List<Facing>();

            // Returns true if this located was already visited with this facing
            public bool MarkVisited(Facing facing)
            {
                var insideLoop = Facings.Contains(facing) && Visited;

                Visited = true;
                Facings.Add(facing);

                return insideLoop;
            }

            public Square Copy()
            {
                return new Square()
                {
                    Visited = false,
                    HasObstacle = HasObstacle,
                    HasGuard = HasGuard,    
                    Facings = new List<Facing>()
                };
            }
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

        // Returns true if location already visited with same facing
        public bool MarkVisited(CoordinateInt coordinate, Facing facing)
        {
            return Squares[coordinate.Y][coordinate.X].MarkVisited(facing);           
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

        public List<Map> CreateTheorticalMaps()
        {
            var maps = new List<Map>();
            
            foreach(var square in Squares.SelectMany(s => s))
            {
                if (square.HasObstacle || square.HasGuard)
                    continue;
                
                square.HasObstacle = true;

                var map = Copy();
                maps.Add(map);

                square.HasObstacle = false;
                
            }

            return maps;
        }

        public Map Copy()
        {
            var newSquares = new List<List<Square>>();            

            foreach(var squares in Squares)
            {
                var newSqrs = new List<Square>();
                foreach(var square in squares)
                {
                    var sqr = square.Copy();
                    newSqrs.Add(sqr);
                }
                newSquares.Add(newSqrs);
            }

            return new Map(newSquares, GuardStartingCoordinate);
        }

        public void PrintMe()
        {
            foreach(var squares in Squares)
            {
                var line = "";
                foreach(var square in squares)
                {
                    var l = square.HasObstacle ? "#" : ".";
                    if (square.Visited)
                        l = "V";
                    line += l;
                }
                Console.WriteLine(line);
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}

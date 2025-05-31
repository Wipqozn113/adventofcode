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
                        square.MarkVisited();
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

        private Map(List<List<Square>> squares, CoordinateInt guardStartingCoordinate)
        {
            Squares = squares;
            GuardStartingCoordinate = guardStartingCoordinate;
        }

        private class Square
        {
            public bool Visited { get; private set; }

            public bool HasObstacle { get; set; }   

            public bool HasGuard { get; set; }

            private List<Facing> Facings { get; set; } = new List<Facing>();

            public void MarkVisited()
            {
                Visited = true;
            }

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

        public CoordinateInt GuardStartingCoordinate { get; private set; }

        public bool ContainsLoop { get; set; } = false;

        private List<List<Square>> Squares { get; set; }

        /// <summary>
        /// The number of squared visited by the guard
        /// </summary>
        /// <returns></returns>
        public int SquaresVisited()
        {
            return Squares.SelectMany(x => x).Where(square => square.Visited).Count();
        }

        /// <summary>
        /// Determines if the guard has already visited this location with the same facing.
        /// </summary>
        /// <param name="coordinate">The guards current location</param>
        /// <param name="facing">The guards current facing</param>
        /// <returns>TRUE if the guard has already visited this location with the same facing; FALSE otherwise.</returns>
        public bool HasVisited(CoordinateInt coordinate, Facing facing)
        {
            return Squares[coordinate.Y][coordinate.X].MarkVisited(facing);           
        }

        /// <summary>
        /// Determines if a given square contains an obstacle
        /// </summary>
        /// <param name="coordinate">The square to check</param>
        /// <returns>TRUE if the location conatins an obstacle; FLASE otherwise.</returns>
        public bool IsBlocked(CoordinateInt coordinate)
        {
            if (IsWithinMap(coordinate))
                return Squares[coordinate.Y][coordinate.X].HasObstacle;

            return false;
        }

        /// <summary>
        /// Determines if a given coordinate is within the bounds of the map.
        /// </summary>
        /// <param name="coordinate">The coordinate to check</param>
        /// <returns>TRUE if the given coordinate is within the boounds of the map; FALSE otherwise.</returns>
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

        /// <summary>
        /// Creates 1 map variation per square the guard visited in the original map. 
        /// </summary>
        /// <returns>A List<Map> containing all map variations that could contain a loop.</returns>
        public IEnumerable  <Map> CreateTheorticalMaps()
        {            
            foreach(var square in Squares.SelectMany(s => s).Where(sq => sq.Visited && !sq.HasGuard))
            {                
                square.HasObstacle = true;

                var map = Copy();
                yield return map;

                square.HasObstacle = false;                
            }
        }

        /// <summary>
        /// Creates a copy of the Map
        /// </summary>
        /// <returns>A copy of the Map</returns>
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

        /// <summary>
        /// Used for troubleshooting
        /// </summary>
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

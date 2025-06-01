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
                        VisitedSquares.Add(square);
                        square.GuardStartingLocation = true;
                        GuardStartingCoordinate = new CoordinateInt(x, y);
                    }
                    squares.Add(square);
                    x++;
                }
                Squares.Add(squares);
                y++;
            }
        }

        public CoordinateInt GuardStartingCoordinate { get; private set; }

        private List<List<Square>> Squares { get; set; }

        private List<Square> VisitedSquares { get; set; } = new List<Square>();

        /// <summary>
        /// The number of squared visited by the guard
        /// </summary>
        /// <returns></returns>
        public int SquaresVisited()
        {
            return VisitedSquares.Count;
        }

        /// <summary>
        /// Determines if the guard has already visited this location with the same facing.
        /// </summary>
        /// <param name="coordinate">The guards current location</param>
        /// <param name="facing">The guards current facing</param>
        /// <returns>TRUE if the guard has already visited this location with the same facing; FALSE otherwise.</returns>
        public bool HasVisited(CoordinateInt coordinate, Facing facing)
        {
            if (!Squares[coordinate.Y][coordinate.X].Visited)
                VisitedSquares.Add(Squares[coordinate.Y][coordinate.X]);

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
        public IEnumerable<Map> CreateTheorticalMaps(Guard guard)
        {
            // Need to do an initial run of the map so we can determine which squares were visited.
            // We do this because we only need to place obstacles at locations the guard visited
            // on its patrol on the original map
            guard.PatrolLoops();
            var squares = VisitedSquares.Where(sq => !sq.GuardStartingLocation).ToList();

            foreach (var square in squares)
            {
                Reset();
                square.HasObstacle = true;                
                yield return this;                
                square.HasObstacle = false;                
            }
        }        

        private void Reset()
        {
            Parallel.ForEach(VisitedSquares, square =>
                {
                    square.Reset();
                });
            VisitedSquares.Clear();
            VisitedSquares.Add(Squares[GuardStartingCoordinate.Y][GuardStartingCoordinate.X]);
        }       

        private class Square
        {
            public bool Visited { get; private set; }

            public bool GuardStartingLocation { get; set; }

            public bool HasObstacle { get; set; }

            private List<Facing> Facings { get; set; } = new List<Facing>();

            /// <summary>
            /// Mark this location as visited
            /// </summary>
            public void MarkVisited()
            {
                Visited = true;                
            }

            /// <summary>
            /// Marks a location as visited while also recording guard facing
            /// </summary>
            /// <param name="facing">The guards current facing</param>
            /// <returns>TRUE if this location has already been visited with the same facing; FALSE otherwise.</returns>
            public bool MarkVisited(Facing facing)
            {
                var insideLoop = Facings.Contains(facing) && Visited;

                Visited = true;
                Facings.Add(facing);

                return insideLoop;
            }

            public void Reset()
            {
                Facings.Clear();
                if(!GuardStartingLocation)
                {
                    Visited = false;                    
                }
            }
        }
    }
}

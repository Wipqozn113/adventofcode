using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day23
{
    public enum Direction
    {
        North,
        South,
        West,
        East
    }

    public class Coordinate
    {
        public Coordinate()
        {

        }

        public Coordinate(long x, long y)
        {
            X = x;
            Y = y;
        }

        public bool AreEqual(Coordinate other)
        {
            // Null is never equal
            if (other is null)
                return false;

            return X == other.X && Y == other.Y;
        }

        public Coordinate NorthWest
        {
            get
            {
                return new Coordinate(X - 1, Y - 1);
            }
        }

        public Coordinate North
        {
            get
            {
                return new Coordinate(X, Y - 1);
            }
        }

        public Coordinate NorthEast
        {
            get
            {
                return new Coordinate(X + 1, Y - 1);
            }
        }

        public Coordinate SouthWest
        {
            get
            {
                return new Coordinate(X - 1, Y + 1);
            }
        }

        public Coordinate South
        {
            get
            {
                return new Coordinate(X, Y + 1);
            }
        }

        public Coordinate SouthEast
        {
            get
            {
                return new Coordinate(X + 1, Y + 1);
            }
        }

        public Coordinate West
        {
            get
            {
                return new Coordinate(X - 1, Y);
            }
        }

        public Coordinate East
        {
            get
            {
                return new Coordinate(X + 1, Y);
            }
        }



        public long X { get; set; }
        public long Y { get; set; }
    }

    public class Elf
    {
        public Elf(Coordinate coord, long id)
        {
            Coord = coord;
            Id = id;
        }

        public long Id { get; set; }

        public Coordinate Coord { get; set; }
        public Coordinate? MoveTo { get; set; }

        public bool DetermineMove(Queue<Direction> directions, List<Elf> otherElves)
        {
            if(Id == 0)
            {
                var yoloSwagTown = "Savehamabr";
            }
            var dirList = directions.ToList();
            var otherElvesNearby = otherElves.Where(elf => Coord.NorthWest.AreEqual(elf.Coord) ||
                        Coord.North.AreEqual(elf.Coord) ||
                        Coord.NorthEast.AreEqual(elf.Coord) ||
                        Coord.SouthWest.AreEqual(elf.Coord) ||
                        Coord.South.AreEqual(elf.Coord) ||
                        Coord.SouthEast.AreEqual(elf.Coord) ||
                        Coord.East.AreEqual(elf.Coord) ||
                        Coord.West.AreEqual(elf.Coord)).Any();

            // No other elves nearby, so don't move
            if (!otherElvesNearby)
                return false;

            foreach (var direction in dirList)
            {
                if(direction == Direction.North)
                {
                    var occupied = otherElves.Where(elf => Coord.NorthWest.AreEqual(elf.Coord) || 
                        Coord.North.AreEqual(elf.Coord) ||
                        Coord.NorthEast.AreEqual(elf.Coord)).Any();

                    if(!occupied)
                    {
                        MoveTo = Coord.North;

                        return true;
                    }
                }

                if (direction == Direction.South)
                {
                    var occupied = otherElves.Where(elf => Coord.SouthWest.AreEqual(elf.Coord) ||
                        Coord.South.AreEqual(elf.Coord) ||
                        Coord.SouthEast.AreEqual(elf.Coord)).Any();

                    if (!occupied)
                    {
                        MoveTo = Coord.South;

                        return true;
                    }
                }

                if (direction == Direction.East)
                {
                    var occupied = otherElves.Where(elf => Coord.SouthEast.AreEqual(elf.Coord) ||
                        Coord.East.AreEqual(elf.Coord) ||
                        Coord.NorthEast.AreEqual(elf.Coord)).Any();

                    if (!occupied)
                    {
                        MoveTo = Coord.East;

                        return true;
                    }
                }

                if (direction == Direction.West)
                {
                    var occupied = otherElves.Where(elf => Coord.SouthWest.AreEqual(elf.Coord) ||
                        Coord.West.AreEqual(elf.Coord) ||
                        Coord.NorthWest.AreEqual(elf.Coord)).Any();

                    if (!occupied)
                    {
                        MoveTo = Coord.West;

                        return true;
                    }
                }
            }

            return false;
        }

        public bool AttemptMove(List<Elf> otherElves)
        {
            var me = this;
            var cantMove = otherElves.Where(elf => !ReferenceEquals(elf, me) && MoveTo.AreEqual(elf.MoveTo)).Any();

            // Other elf wants to move here too
            if (cantMove)
                return false;

            // Moving is possible, so move
            Coord.X = MoveTo.X;
            Coord.Y = MoveTo.Y;
            return true;
        }
    }

    public class Elves
    {
        public  Elves()
        {

        }

        public Elves(List<Elf> elves)
        {
            Elfs = elves;
        }
         

        public List<Elf> Elfs { get; set; } = new List<Elf>();

        public List<Elf> MovingElves { get; set; } = new List<Elf>();

        public long Run(bool part2, long rounds = 10)
        {
            // Setup movement queue
            Queue<Direction> directions = new Queue<Direction>();
            directions.Enqueue(Direction.North);
            directions.Enqueue(Direction.South);
            directions.Enqueue(Direction.West);
            directions.Enqueue(Direction.East);

            // Run rounds
            long round = 0;
            while(DoAnotherRound(part2, round, rounds))
            {
                Console.WriteLine($"On round {round}...");
                DetermineElfMoves(directions);
                if (MovingElves.Any())
                {
                    MoveElves();
                    var dir = directions.Dequeue();
                    directions.Enqueue(dir);
                }
                else
                {
                    return round + 1;
                }
                round++;
            }

            return EmptySquares();

        }
        
        private bool DoAnotherRound(bool part2, long round, long rounds)
        {
            // Loop until a break
            if (part2)
                return true;

            return round < rounds;
        }

        public long EmptySquares()
        {
            var maxX = Elfs.Max(elf => elf.Coord.X);
            var minX = Elfs.Min(elf => elf.Coord.X);
            var maxY = Elfs.Max(elf => elf.Coord.Y);
            var minY = Elfs.Min(elf => elf.Coord.Y);
            var height = maxY - minY + 1;
            var width = maxX - minX + 1;
            var squares = height * width;

            return squares - Elfs.Count;          

        }

        public void DetermineElfMoves(Queue<Direction> directions)
        {
            MovingElves = Elfs.Where(elf => elf.DetermineMove(directions, Elfs)).ToList();
        }

        public void MoveElves()
        {
            MovingElves.ForEach(elf => elf.AttemptMove(MovingElves));
            MovingElves.ForEach(elf => elf.MoveTo = null);
            MovingElves.Clear();
        }

    }
}

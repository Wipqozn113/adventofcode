using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day10
{
    public class Explorer
    {
        public Explorer()
        {

        }

        public int X { get; set; }
        
        public int Y { get; set; }

        public Direction Facing { get; set; }

        public void Reset(Tile start)
        {
            X = start.X;
            Y = start.Y;
            Facing = DirectionHelpers.FlipDirection(start.Directions[0]);
        }

        public void Move(Tile tile)
        {
            // Update Facing and move explorer
            var enteredFrom = DirectionHelpers.FlipDirection(Facing);
            Facing = tile.Directions.FirstOrDefault(x => x != enteredFrom);

            // Move one tile
            if (Facing == Direction.North)
                Y -= 1;
            else if (Facing == Direction.East)
                X += 1;
            else if (Facing == Direction.South)
                Y += 1;
            else if (Facing == Direction.West)
                X -= 1;
        }
    }
}

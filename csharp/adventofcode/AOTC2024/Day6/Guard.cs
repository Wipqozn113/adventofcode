using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOCUtils.MathUtils;

namespace AOTC2024.Day6
{
    public class Guard
    {
        private enum Facing
        {
            North,
            East,
            South,
            West
        }

        public  Guard(Map map)
        {
            Map = map;
            CurrentLocation = map.GuardStartingCoordinate;
        }

        public Map Map { get; set; }

        public CoordinateInt CurrentLocation { get; set; }

        private Facing CurrentFacing { get; set; } = Facing.North;

        /// <summary>
        /// Guard takes an action
        /// </summary>
        /// <returns>Returns TRUE if the guard is still within the map; False otherwise.</returns>
        public bool Act()
        {
            if (CanMove())
                return Move();
            
            ChangeFacing();
            return true;
        }

        private bool CanMove()
        {
            if (Map.IsBlocked(NextCoordinate()))
                return false;
            return true;
        }

        private bool Move()
        {
            if (Map.IsWithinMap(NextCoordinate()))
            {
                CurrentLocation = NextCoordinate();
                Map.MarkVisited(CurrentLocation);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ChangeFacing()
        {
            if (CurrentFacing == Facing.North)
                CurrentFacing = Facing.East;
            else if (CurrentFacing == Facing.East)
                CurrentFacing = Facing.South;
            else if (CurrentFacing == Facing.South)
                CurrentFacing = Facing.West;
            else if (CurrentFacing == Facing.West)
                CurrentFacing = Facing.North;
        }

        private CoordinateInt NextCoordinate()
        {
            if (CurrentFacing == Facing.North)
                return new CoordinateInt(CurrentLocation.X, CurrentLocation.Y - 1);
            else if (CurrentFacing == Facing.East)
                return new CoordinateInt(CurrentLocation.X + 1, CurrentLocation.Y);
            else if (CurrentFacing == Facing.South)
                return new CoordinateInt(CurrentLocation.X, CurrentLocation.Y + 1);
            else 
                return new CoordinateInt(CurrentLocation.X - 1, CurrentLocation.Y);
        }

        public int CountUniqueSquaresVisited()
        {
            while (Act()) ;

            return Map.SquaresVisited();
        }
    }
}

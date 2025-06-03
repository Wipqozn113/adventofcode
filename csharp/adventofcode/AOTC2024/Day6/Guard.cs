using AOCUtils.MathUtils;

namespace AOTC2024.Day6
{
    public class Guard
    {
        public Guard(Map map)
        {
            Map = map;
            CurrentLocation = map.GuardStartingCoordinate;
            StartingLocation = new CoordinateInt(CurrentLocation.X, CurrentLocation.Y);
        }

        private Map Map { get; set; }

        private CoordinateInt CurrentLocation { get; set; }

        private CoordinateInt StartingLocation { get; set; }

        private Facing CurrentFacing { get; set; } = Facing.North;

        /// <summary>
        /// Calculates the number of unique squares the guard will visit on its patrol. 
        /// </summary>
        /// <returns>The number of unique squares the guard will visit on its patrol.</returns>
        public int CountUniqueSquaresVisited()
        {
            while (Act() == State.Fine) ;
            CurrentLocation = new CoordinateInt(StartingLocation.X, StartingLocation.Y);
            return Map.SquaresVisited();
        }

        /// <summary>
        /// Calculates the number of map variations which will put the guard into a patrol loop. 
        /// </summary>
        /// <returns>The number of map variations which will put the guard into a patrol loop.</returns>
        public int CountPossibleLoops()
        {
            int loops = 0;
            foreach(var map in Map.CreateTheorticalMaps(this))
            {
                var guard = new Guard(map);
                if (guard.PatrolLoops())
                    loops++;
            };

            return loops;
        }

        /// <summary>
        /// Determines if the guards patrol loops
        /// </summary>
        /// <returns>TRUE if the patrol loops; FALSE otherwise</returns>
        public bool PatrolLoops()
        {
            var state = State.Fine;
            while (state == State.Fine)
            {
                state = Act();
            }

            return state == State.Looping;
        }

        /// <summary>
        /// Guard takes an action
        /// </summary>
        /// <returns>A State enum indicating the guards current state.</returns>
        private State Act()
        {
            if (CanMove())
                return Move();
            
            ChangeFacing();
            return State.Fine;
        }

        /// <summary>
        /// Determines if the Guard can move forward.
        /// </summary>
        /// <returns>TRUE if the guard can move; FALSE otherwise.</returns>
        private bool CanMove()
        {
            if (Map.IsBlocked(NextCoordinate()))
                return false;
            return true;
        }

        /// <summary>
        /// Move the guard
        /// </summary>
        /// <returns>A State enum indicating the guards current state.</returns>
        private State Move()
        {
            if (Map.IsWithinMap(NextCoordinate()))
            {
                CurrentLocation = NextCoordinate();
                if (Map.HasVisited(CurrentLocation, CurrentFacing))
                {
                    return State.Looping;
                }
                return State.Fine;
            }
            else
            {
                return State.OutsideMap;
            }
        }

        /// <summary>
        /// Rotate the guard 90 degrees clockwise
        /// </summary>
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

        /// <summary>
        /// Determine the square the guard wants to move into
        /// </summary>
        /// <returns></returns>
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

        private enum State
        {
            Fine,
            Looping,
            OutsideMap
        }
    }
}

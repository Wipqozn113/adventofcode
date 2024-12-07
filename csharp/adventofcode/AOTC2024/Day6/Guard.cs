using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AOCUtils.MathUtils;

namespace AOTC2024.Day6
{
    public enum Facing
    {
        North,
        East,
        South,
        West
    }

    public class Guard
    {
        private enum State 
        {
            Fine,
            Looping,
            OutsideMap
        }

        public  Guard(Map map)
        {
            Map = map;
            OriginalMap = map;
            CurrentLocation = map.GuardStartingCoordinate;
            StartingLocation = new CoordinateInt(CurrentLocation.X, CurrentLocation.Y);
        }

        public Map OriginalMap { get; set; }

        public Map Map { get; set; }

        public CoordinateInt CurrentLocation { get; set; }

        public CoordinateInt StartingLocation { get; set; }

        private Facing CurrentFacing { get; set; } = Facing.North;

        /// <summary>
        /// Guard takes an action
        /// </summary>
        /// <returns>Returns TRUE if the guard is still within the map; False otherwise.</returns>
        private State Act()
        {
            if (CanMove())
                return Move();
            
            ChangeFacing();
            return State.Fine;
        }

        private bool CanMove()
        {
            if (Map.IsBlocked(NextCoordinate()))
                return false;
            return true;
        }

        private State Move()
        {
            if (Map.IsWithinMap(NextCoordinate()))
            {
                CurrentLocation = NextCoordinate();
                if (Map.MarkVisited(CurrentLocation, CurrentFacing))
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
            while (Act() == State.Fine) ;
            CurrentLocation = new CoordinateInt(StartingLocation.X, StartingLocation.Y);
            return Map.SquaresVisited();
        }

        public int CountPossibleLoops()
        {
            var maps = Map.CreateTheorticalMaps();
            var total = 0;

            foreach (var map in maps) 
            {               
                Map = map;
                
                var state = State.Fine;
                while(state == State.Fine)
                {
                    state = Act();          
                }
                
                if (state == State.Looping)
                    total++;
                ResetMe();
               // map.PrintMe();                    
            }
           
            Map = OriginalMap;
            return total;
        }

        private void ResetMe()
        {
            CurrentLocation = new CoordinateInt(StartingLocation.X, StartingLocation.Y);
            CurrentFacing = Facing.North;
        }
    }
}

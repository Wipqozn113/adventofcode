using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day16
{
    public class Laser
    {
        public Laser(Tile tile, Direction direction)
        {
            Facing = direction;
            CurrentTile = tile;            
        }

        public int X
        {
            get
            {
                return CurrentTile.X;
            }
        }

        public int Y
        {
            get
            {
                return CurrentTile.Y;
            }
        } 

        public Direction Facing { get; set; }

        public Tile CurrentTile { get; set; }

        public bool MoveLaser(Cave cave)
        {
            if (cave.History.Where(x => x.Tile == CurrentTile && x.Facing == Facing).Any())
            {
                return true;
            }
            else
            {
                cave.History.Add(new LaserHistory(CurrentTile, Facing));
            }

            var exitedCave = false;
            Laser? newLaser = null;
            CurrentTile.Energized = true;

            if(CurrentTile.Occupant == '\\')
            {
                if(Facing == Direction.North)
                {
                    Facing = Direction.West;
                }
                else if(Facing == Direction.South)
                {
                    Facing = Direction.East;
                }              
                else if(Facing == Direction.East)
                {
                    Facing = Direction.South;
                }
                else if(Facing == Direction.West)
                {
                    Facing = Direction.North;
                }
            }
            else if(CurrentTile.Occupant == '/')
            {
                if (Facing == Direction.North)
                {
                    Facing = Direction.East;
                }
                else if (Facing == Direction.South)
                {
                    Facing = Direction.West;
                }
                else if (Facing == Direction.East)
                {
                    Facing = Direction.North;
                }
                else if (Facing == Direction.West)
                {
                    Facing = Direction.South;
                }
            }
            else if(CurrentTile.Occupant == '-')
            {
                if (Facing == Direction.North)
                {
                    Facing = Direction.East;
                    newLaser = new Laser(CurrentTile, Direction.West);
                }
                else if (Facing == Direction.South)
                {
                    Facing = Direction.East;
                    newLaser = new Laser(CurrentTile, Direction.West);
                }
            }
            else if(CurrentTile.Occupant == '|')
            {
                if (Facing == Direction.East)
                {
                    Facing = Direction.North;
                    newLaser = new Laser(CurrentTile, Direction.South);
                }
                else if (Facing == Direction.West)
                {
                    Facing = Direction.North;
                    newLaser = new Laser(CurrentTile, Direction.South);
                }
            }

            var nextX = X;
            var nextY = Y;
            if(Facing == Direction.North)
            {
                nextY = Y - 1;
            }
            else if(Facing == Direction.South)
            {
                nextY = Y + 1;
            }
            else if(Facing == Direction.West)
            {
                nextX = X - 1;
            }
            else if(Facing == Direction.East)
            {
                nextX = X + 1;
            }

            try
            {
                CurrentTile = cave.Tiles[nextY][nextX];
            }
            catch (Exception ex)
            {
                exitedCave = true;
            }

            try
            {
                if (newLaser is not null)
                {
                    if (newLaser.Facing == Direction.South)
                    {
                        newLaser.CurrentTile = cave.Tiles[newLaser.Y + 1][newLaser.X];
                    }
                    else if (newLaser.Facing == Direction.West)
                    {
                        newLaser.CurrentTile = cave.Tiles[newLaser.Y][newLaser.X - 1];
                    }
                   cave.Lasers.Add(newLaser);  
                }
            }
            catch (Exception ex)
            {
                var test = ex;
            }

            return exitedCave;
        }
    }

    public enum Direction
    {
        North,
        East,
        South,
        West
    }
}

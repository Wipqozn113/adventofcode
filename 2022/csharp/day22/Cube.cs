using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day22
{
    public class Cube
    {
        public List<CubeSide> Sides { get; set; }
        public CubeSide StartingSide { get; set; }
        public List<CubeCommand> Path { get; set; }

        public CubeSide CurrentSide { get; set; }
        public long Row { set; get; } = 0;
        public long Col { get; set; } = 0;
        public long Facing { get; set; } = 0;

        public long Password
        {
            // The final password is the sum of 1000 times the row, 4 times the column, and the facing.
            get
            {
                return (1000 * Row) + (4 * Col) + Facing;
            }
        }

        public long ExecuteCommands()
        {
            foreach (var command in Path)
            {
                command.UpdateState();
            }

            return Password;
        }
    }

    public enum CubeFace
    {
        Top,
        North,
        East,
        South,
        West,
        Bottom
    }

    public class CubeSide
    {       
        public CubeFace Side { get; set;  }
        public char[,] Map { get; set; }

        public long Height { get; set; }
        public long Width { get; set; }

        public void PopulateSides(CubeSide topSide, CubeSide bottomSide, CubeSide rightSide, CubeSide leftSide)
        {
            TopSide = topSide;
            BottomSide = bottomSide;
            RightSide = rightSide;
            LeftSide = leftSide;
        }

        public CubeSide TopSide { get; set; }
        public CubeSide BottomSide { get; set; }
        public CubeSide RightSide { get; set; }
        public CubeSide LeftSide { get; set; }
    }

    public class CubeCommand : Command
    {
        public Cube Cube { get; set; }

        public CubeSide CurrentSide
        {
            get
            {
                return Cube.CurrentSide;
            }
            set
            {
                Cube.CurrentSide = value;
            }
        }

        public CubeCommand(char facing, Cube cube) : base(facing)
        {
            Cube = cube;
        }

        public CubeCommand(long steps, Cube cube) : base(steps)
        {
            Cube = cube;
        }

        public override void UpdateFacing()
        {
            if (Facing == "R")
            {
                Cube.Facing = (Cube.Facing + 1) % 4;
            }
            else
            {
                if (Cube.Facing == 0)
                {
                    Cube.Facing = 3;
                }
                else
                {
                    Cube.Facing = Cube.Facing - 1;
                }
            }
        }


        public override void UpdatePosition()
        {
            long row = 0;
            long col = 0;
            var nextRow = row;
            var nextCol = col;
            var stepsLeft = Steps;
            var currentSide = CurrentSide;

            while(stepsLeft > 0)
            {
                // Out of steps
                if (stepsLeft == 0)
                    return;

                // Try to move
                if (Cube.Facing == 1 || Cube.Facing == 3)
                {
                    nextRow = Cube.Facing == 1 ? row + 1 : row - 1;

                    // Reached the edge, go to next side (if possible)
                    if (nextRow == currentSide.Height)
                    {
                        CurrentSide = currentSide.BottomSide;
                        currentSide = CurrentSide;
                        nextRow = 0;
                    }
                    else if(nextRow == -1)
                    {
                        Cube.Facing = 3;
                        CurrentSide = currentSide.TopSide;
                        currentSide = CurrentSide;
                        nextRow = currentSide.Height - 1;
                    }
                }
                else
                {
                    if (Cube.Facing == 0 || Cube.Facing == 2)
                    {
                        nextCol = Cube.Facing == 0 ? nextCol + 1 : nextCol - 1;

                        // Reached the edge, go to next side (if possible)
                        if (nextCol == currentSide.Width)
                        {
                            CurrentSide = currentSide.RightSide;
                            currentSide = CurrentSide;
                            nextRow = 0;
                        }   
                        else if(nextCol == -1)
                        {
                            Cube.Facing = 2;
                            CurrentSide = currentSide.LeftSide;
                            currentSide = CurrentSide;
                            nextRow = currentSide.Width - 1;
                        }
                    }
                }

                // Hit a wall, so exit early
                if (currentSide.Map[nextRow, nextCol] == '#')
                {
                    Cube.Row = row;
                    Cube.Col = col;
                    return;
                }
                // I can move, keep going
                else if (currentSide.Map[nextRow, nextCol] == '.')
                {
                    row = nextRow;
                    col = nextCol;
                    stepsLeft -= 1;
                }
            }            

            // Done walking, didn't run into a wall
            Cube.Row = row;
            Cube.Col = col;
            CurrentSide = currentSide;
            
            return;
        }
    }
}

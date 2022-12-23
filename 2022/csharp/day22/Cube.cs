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
        public long FlatRow
        {
            get
            {
                return Row + CurrentSide.RowDisplacement + 1;
            }
        }

        public long Col { get; set; } = 0;
        public long FlatCol
        {
            get
            {
                return Col + CurrentSide.ColDisplacement + 1;
            }
        }
        public MovementFacing Facing { get; set; } = MovementFacing.Right;

        public long Password
        {
            // The final password is the sum of 1000 times the row, 4 times the column, and the facing.
            get
            {
                return (1000 * FlatRow)  + (4 * FlatCol) + (long)Facing;
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

    public enum MovementFacing
    {
        Right = 0,
        Down = 1,
        Left = 2,
        Up = 3
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

        public long RowDisplacement { get; set; }
        public long ColDisplacement { get; set; }
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
                int facing = (int)Cube.Facing;
                facing = (facing + 1) % 4;
                Cube.Facing = (MovementFacing)facing;
            }
            else
            {
                if (Cube.Facing == MovementFacing.Right)
                {
                    Cube.Facing = MovementFacing.Up;
                }
                else
                {
                    int facing = (int)Cube.Facing;
                    facing = facing - 1;
                    Cube.Facing = (MovementFacing)facing;
                }
            }
        }

        public (long, long) UpdatePosition(CubeSide fromCube, CubeSide toCube, long row, long col)
        {
            var newRow = row;
            var newCol = col;

            if (EnteredFromTopEdge(fromCube, toCube))
            {
                newRow = 0;
                if(fromCube.Side == CubeFace.Top)
                {
                    if (toCube.Side == CubeFace.North || toCube.Side == CubeFace.South)
                    {
                        newCol = col;
                    }
                    else if (toCube.Side == CubeFace.East || toCube.Side == CubeFace.West)
                    {
                        newCol = row;
                    }
                }
            }
            else if (EnteredFromBottomEdge(fromCube, toCube))
            {
                newRow = toCube.Height - 1;
                if (fromCube.Side == CubeFace.Bottom)
                {
                    if (toCube.Side == CubeFace.North || toCube.Side == CubeFace.South)
                    {
                        newCol = col;
                    }
                    else if (toCube.Side == CubeFace.East || toCube.Side == CubeFace.West)
                    {
                        newCol = row;
                    }
                }
            }
            else if (EnteredFromLeftEdge(fromCube, toCube))
            {
                newCol = 0;
                if (toCube.Side == CubeFace.Top || toCube.Side == CubeFace.Bottom)
                {
                    newRow = col;
                }
            }
            else if (EnteredFromRightEdge(fromCube, toCube))
            {
                newCol = toCube.Width - 1;
                if (toCube.Side == CubeFace.Top || toCube.Side == CubeFace.Bottom)
                {
                    newRow = col;
                }
            }

            return (newRow, newCol);
        }

        public MovementFacing UpdateFacing(CubeSide fromCube, CubeSide toCube)
        {
            var facing = Cube.Facing;
            if(EnteredFromTopEdge(fromCube, toCube))
            {
                facing = MovementFacing.Down;
            }
            else if(EnteredFromBottomEdge(fromCube, toCube))
            {
                facing = MovementFacing.Up;
            }
            else if(EnteredFromLeftEdge(fromCube, toCube))
            {
                facing = MovementFacing.Right;
            }
            else if (EnteredFromRightEdge(fromCube, toCube))
            {
                facing = MovementFacing.Left;
            }

            return facing;
        }

        private bool EnteredFromTopEdge(CubeSide fromCube, CubeSide toCube)
        {
            return fromCube == toCube.TopSide;

        }

        private bool EnteredFromBottomEdge(CubeSide fromCube, CubeSide toCube)
        {
            return fromCube == toCube.BottomSide;
        }

        private bool EnteredFromLeftEdge(CubeSide fromCube, CubeSide toCube)
        {
            return fromCube == toCube.LeftSide;
        }

        private bool EnteredFromRightEdge(CubeSide fromCube, CubeSide toCube)
        {
            return fromCube == toCube.RightSide;
        }


        public override void UpdatePosition()
        {
            long row = Cube.Row;
            long col = Cube.Col;
            var nextRow = row;
            var nextCol = col;
            var stepsLeft = Steps;
            var currentSide = CurrentSide;
            MovementFacing? facing = null;

            while(stepsLeft > 0)
            {
                // Out of steps
                if (stepsLeft == 0)
                    return;

                // Try to move
                if (Cube.Facing == MovementFacing.Up|| Cube.Facing == MovementFacing.Down)
                {
                    nextRow = Cube.Facing == MovementFacing.Down ? row + 1 : row - 1;

                    // Reached the edge, go to next side (if possible)
                    if (nextRow == currentSide.Height)
                    {
                       facing = UpdateFacing(currentSide, currentSide.BottomSide);
                        (nextRow, nextCol) = UpdatePosition(currentSide, currentSide.BottomSide, nextRow, nextCol);
                        currentSide = currentSide.BottomSide;
                    }
                    else if(nextRow == -1)
                    {
                        facing = UpdateFacing(currentSide, currentSide.TopSide);
                        (nextRow, nextCol) = UpdatePosition(currentSide, currentSide.TopSide, nextRow, nextCol);
                        currentSide = currentSide.TopSide;
                    }
                }
                else
                {
                    if (Cube.Facing == MovementFacing.Right || Cube.Facing == MovementFacing.Left)
                    {
                        nextCol = Cube.Facing == MovementFacing.Right ? nextCol + 1 : nextCol - 1;

                        // Reached the edge, go to next side (if possible)
                        if (nextCol == currentSide.Width)
                        {
                            facing = UpdateFacing(currentSide, currentSide.RightSide);
                            (nextRow, nextCol) = UpdatePosition(currentSide, currentSide.RightSide, nextRow, nextCol);
                            currentSide = currentSide.RightSide;
                        }   
                        else if(nextCol == -1)
                        {
                            facing = UpdateFacing(currentSide, currentSide.LeftSide);
                            (nextRow, nextCol) = UpdatePosition(currentSide, currentSide.LeftSide, nextRow, nextCol);
                            currentSide = currentSide.LeftSide;
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
  
                    Cube.Facing = facing ?? Cube.Facing;
                    facing = null;       
                    row = nextRow;
                    col = nextCol;
                    stepsLeft -= 1;
                    CurrentSide = currentSide;
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

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

            if (fromCube.Side == CubeFace.Top)
            {
                (newCol, newRow) = UpdatePositionFromCube1(fromCube, toCube, row, col);
            }
            else if (fromCube.Side == CubeFace.East)
            {
                (newCol, newRow) = UpdatePositionFromCube2(fromCube, toCube, row, col);
            }
            else if (fromCube.Side == CubeFace.South)
            {
                (newCol, newRow) = UpdatePositionFromCube3(fromCube, toCube, row, col);
            }
            else if (fromCube.Side == CubeFace.West)
            {
                (newCol, newRow) = UpdatePositionFromCube4(fromCube, toCube, row, col);
            }
            else if (fromCube.Side == CubeFace.Bottom)
            {
                (newCol, newRow) = UpdatePositionFromCube5(fromCube, toCube, row, col);
            }
            else if (fromCube.Side == CubeFace.North)
            {
                (newCol, newRow) = UpdatePositionFromCube6(fromCube, toCube, row, col);
            }

            return (newRow, newCol);
        }

        public (long, long)  UpdatePositionFromCube1(CubeSide fromCube, CubeSide toCube, long row, long col)
        {
            long newCol = col;
            long newRow = row;
            if(toCube.Side == CubeFace.East)
            {
                newCol = 0;
                newRow = row;
            }
            else if (toCube.Side == CubeFace.South)
            {
                newCol = col;
                newRow = 0;
            }
            else if (toCube.Side == CubeFace.West)
            {
                newCol = 0;
                newRow = toCube.Width - row;
            }
            else if (toCube.Side == CubeFace.North)
            {
                newCol = 0;
                newRow = row;
            }

            return (newCol, newRow);
        }

        public (long, long) UpdatePositionFromCube2(CubeSide fromCube, CubeSide toCube, long row, long col)
        {
            long newCol = col;
            long newRow = row;
            if (toCube.Side == CubeFace.Top)
            {
                newCol = toCube.Width - 1;
                newRow = row;
            }
            else if (toCube.Side == CubeFace.South)
            {
                newCol = toCube.Width - 1;
                newRow = col;
            }
            else if (toCube.Side == CubeFace.Bottom)
            {
                newCol = toCube.Width - 1;
                newRow = toCube.Width - row;
            }
            else if (toCube.Side == CubeFace.North)
            {
                newCol = toCube.Width - col;
                newRow = toCube.Width - 1;
            }

            return (newCol, newRow);
        }

        public (long, long) UpdatePositionFromCube3(CubeSide fromCube, CubeSide toCube, long row, long col)
        {
            long newCol = col;
            long newRow = row;
            if (toCube.Side == CubeFace.Top)
            {
                newCol = col;
                newRow = toCube.Width - 1;
            }
            else if (toCube.Side == CubeFace.East)
            {
                newCol = row;
                newRow = toCube.Width - 1;
            }
            else if (toCube.Side == CubeFace.West)
            {
                newCol = row;
                newRow = 0;
            }
            else if (toCube.Side == CubeFace.Bottom)
            {
                newCol = col;
                newRow = 0;
            }

            return (newCol, newRow);
        }

        public (long, long) UpdatePositionFromCube4(CubeSide fromCube, CubeSide toCube, long row, long col)
        {
            long newCol = col;
            long newRow = row;
            if (toCube.Side == CubeFace.North)
            {
                newCol = col;
                newRow = 0;
            }
            else if (toCube.Side == CubeFace.South)
            {
                newCol = 0;
                newRow = col;
            }
            else if (toCube.Side == CubeFace.Top)
            {
                newCol = toCube.Width - row;
                newRow = 0;
            }
            else if (toCube.Side == CubeFace.Bottom)
            {
                newCol = col;
                newRow = 0;
            }

            return (newCol, newRow);
        }

        public (long, long) UpdatePositionFromCube5(CubeSide fromCube, CubeSide toCube, long row, long col)
        {
            long newCol = col;
            long newRow = row;
            if (toCube.Side == CubeFace.East)
            {
                newCol = row;
                newRow = toCube.Width - 1;
            }
            else if (toCube.Side == CubeFace.South)
            {
                newCol = col;
                newRow = toCube.Width - 1;
            }
            else if (toCube.Side == CubeFace.West)
            {
                newCol = toCube.Width - 1;
                newRow = row;
            }
            else if (toCube.Side == CubeFace.North)
            {
                newCol = toCube.Width - 1;
                newRow = col;
            }

            return (newCol, newRow);
        }

        public (long, long) UpdatePositionFromCube6(CubeSide fromCube, CubeSide toCube, long row, long col)
        {
            long newCol = col;
            long newRow = row;
            if (toCube.Side == CubeFace.Top)
            {
                newCol = row;
                newRow = 0;
            }
            else if (toCube.Side == CubeFace.Bottom)
            {
                newCol = row;
                newRow = toCube.Width - 1;
            }
            else if (toCube.Side == CubeFace.East)
            {
                newCol = col;
                newRow = 0;
            }
            else if (toCube.Side == CubeFace.West)
            {
                newCol = col;
                newRow = toCube.Width - 1;
            }

            return (newCol, newRow);
        }

        public MovementFacing UpdateFacing(CubeSide fromCube, CubeSide toCube)
        {
            var facing = Cube.Facing;
            if(SetFacingToDown(fromCube, toCube))
            {
                facing = MovementFacing.Down;
            }
            else if(SetFacingToUp(fromCube, toCube))
            {
                facing = MovementFacing.Up;
            }
            else if(SetFacingToRight(fromCube, toCube))
            {
                facing = MovementFacing.Right;
            }
            else if (SetFacingToLeft(fromCube, toCube))
            {
                facing = MovementFacing.Left;
            }

            return facing;
        }

        private bool SetFacingToDown(CubeSide fromCube, CubeSide toCube)
        {
            return fromCube == toCube.TopSide;

        }

        private bool SetFacingToUp(CubeSide fromCube, CubeSide toCube)
        {
            return fromCube == toCube.BottomSide;
        }

        private bool SetFacingToRight(CubeSide fromCube, CubeSide toCube)
        {
            return fromCube == toCube.LeftSide;
        }

        private bool SetFacingToLeft(CubeSide fromCube, CubeSide toCube)
        {
            return fromCube == toCube.RightSide;
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
                        (nextRow, nextCol) = UpdatePosition(currentSide, currentSide.BottomSide, 0, nextCol);
                        currentSide = currentSide.BottomSide;
                    }
                    else if(nextRow == -1)
                    {
                        facing = UpdateFacing(currentSide, currentSide.TopSide);
                        (nextRow, nextCol) = UpdatePosition(currentSide, currentSide.TopSide, currentSide.Height - 1, nextCol);
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
                            (nextRow, nextCol) = UpdatePosition(currentSide, currentSide.RightSide, nextRow, 0);
                            currentSide = currentSide.RightSide;
                        }   
                        else if(nextCol == -1)
                        {
                            facing = UpdateFacing(currentSide, currentSide.LeftSide);
                            (nextRow, nextCol) = UpdatePosition(currentSide, currentSide.LeftSide, nextRow, currentSide.Width - 1);
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

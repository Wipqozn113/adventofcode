using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day22
{
    public class Grove
    {
        public Grove(char[,] map, List<Command> path)
        {
            Map = map;
            Path = path;
            PosRow = 0;

            while(true)
            {
                if (Map[0, PosCol] == '.')
                {
                    break;
                }
                PosCol++;
            }

        }

        public Cube Cube { get; set; }
        public long Width { get; set; }
        public long Height { get; set; }
        public long PosRow { get; set; }

        public long RealRowPos
        {
            get
            {
                return PosRow + 1;
            }
        }

        public long PosCol { get; set; }
        public long RealColPos
        {
            get
            {
                return PosCol + 1;
            }
        }

        public string Facing
        {
            get
            {
                switch(FacingValue)
                {
                    case 0:
                        return ">";
                    case 1:
                        return "v";
                    case 2:
                        return "<";
                    case 3:
                        return "^";
                }

                return "";
            }
        }
        public long FacingValue { get; set; }

        public long Password
        {
            // The final password is the sum of 1000 times the row, 4 times the column, and the facing.
            get
            {
                return (1000 * RealRowPos) + (4 * RealColPos) + FacingValue;
            }
        }

        public long ExecuteCommands()
        {
            foreach(var command in Path)
            {
                command.UpdateState();
            }

            return Password;
        }

        public long ExecuteCubeCommands()
        {
            return Cube.ExecuteCommands();
        }

        public List<Command> Path { get; set; }

        public char[,] Map { get; set; }
    }

    public class Command
    {
        public Grove Grove { get; set; }
        public string Facing { get; set; }
        public long Steps { get; set; }
        public bool IsRotation { get; set; }

        public Command(char facing)
        {
            Facing = facing.ToString();
            IsRotation = true;

        }

        public Command(long steps)
        {
            Steps = steps;
            IsRotation = false;
        }

        public void UpdateState()
        {
            if(IsRotation)
            {
                UpdateFacing();
            }
            else
            {
                UpdatePosition();
            }
        }

        public virtual void UpdatePosition()
        {
            var row = Grove.PosRow;
            var col = Grove.PosCol;
            var nextRow = row;
            var nextCol = col;
            var stepsLeft = Steps;
            var outOfBounds = Grove.Map[nextRow, nextCol] == ' ';
            while (stepsLeft > 0)
            {
                // Out of steps
                if (stepsLeft == 0)
                    return;


                if (Grove.FacingValue == 1 || Grove.FacingValue == 3)
                {
                    nextRow = Grove.FacingValue == 1 ? row + 1 : row - 1;
                    if (nextRow == Grove.Height || nextRow == -1)
                    {
                        nextRow = nextRow == Grove.Height ? 0 : Grove.Height - 1;
                    }

                    while (Grove.Map[nextRow, nextCol] == ' ')
                    {
                        nextRow = Grove.FacingValue == 1 ? nextRow + 1 : nextRow - 1;
                        if (nextRow == Grove.Height || nextRow == -1)
                        {
                            nextRow = nextRow == Grove.Height ? 0 : Grove.Height - 1;
                        }
                    }
                }
                else
                {
                    if (Grove.FacingValue == 0 || Grove.FacingValue == 2)
                    { 
                        nextCol = Grove.FacingValue == 0 ? nextCol + 1 : nextCol - 1;
                        if (nextCol == Grove.Width || nextCol == -1)
                        {
                            nextCol = nextCol == Grove.Width ? 0 : Grove.Width - 1;
                        }

                        while (Grove.Map[nextRow, nextCol] == ' ')
                        {
                            nextCol = Grove.FacingValue == 0 ? nextCol + 1 : nextCol - 1;
                            if (nextCol == Grove.Width || nextCol == -1)
                            {
                                nextCol = nextCol == Grove.Width ? 0 : Grove.Width - 1;
                            }
                        }
                    }
                }
      


                // Hit a wall, so exit early
                if (Grove.Map[nextRow, nextCol] == '#')
                {
                    Grove.PosRow = row;
                    Grove.PosCol = col;
                    return;
                }
                else if(Grove.Map[nextRow, nextCol] == '.')
                {
                    row = nextRow;
                    col = nextCol;
                    stepsLeft -= 1;
                    outOfBounds = false;
                }

            }

            // Down walking, didn't run into a wall
            Grove.PosRow = row;
            Grove.PosCol = col;
            return;
        }

        public virtual void UpdateFacing()
        {
            if(Facing == "R")
            {
                Grove.FacingValue = (Grove.FacingValue + 1) % 4;
            }
            else
            {
                if (Grove.FacingValue == 0)
                {
                    Grove.FacingValue = 3;
                }
                else
                {
                    Grove.FacingValue = Grove.FacingValue - 1;
                }
            }
        }
    }
}

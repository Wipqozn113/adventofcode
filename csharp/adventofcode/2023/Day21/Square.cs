using AOC2023.Day17;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day21
{
    public class Square
    {
        public Square(char occupant, int x, int y)
        {
            Occupant = occupant;
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public char Occupant { get; set; }

        public bool IsFilled { get; set; }

        public bool IsStart
        {
            get
            {
                return Occupant == 'S';
            }
        }

        public bool IsRock
        {
            get
            {
                return Occupant == '#';
            }
        }

        public bool IsGardenPlot
        {
            get
            {
                return !IsRock;
            }
        }

        public int StepsToReach { get; set; } = 999;

        public void FloodFill(int steps, Garden garden)
        {
            if (steps < StepsToReach) 
            {
                StepsToReach = steps;
            }

            IsFilled = true;
           
            if(steps == 64)
            {
                return;
            }

            if(Y + 1 < garden.Map.Count && garden.Map[Y + 1][X].IsGardenPlot
                && (!garden.Map[Y + 1][X].IsFilled 
                || (garden.Map[Y + 1][X].IsFilled && steps + 1 < garden.Map[Y + 1][X].StepsToReach)))
            {
                garden.Map[Y + 1][X].FloodFill(steps + 1, garden);
            }

            if (Y - 1 > 0 && garden.Map[Y - 1][X].IsGardenPlot
                && (!garden.Map[Y - 1][X].IsFilled
                || (garden.Map[Y - 1][X].IsFilled && steps + 1 < garden.Map[Y - 1][X].StepsToReach)))
            {
                garden.Map[Y - 1][X].FloodFill(steps + 1, garden);
            }

            if (X + 1 < garden.Map[Y].Count && garden.Map[Y][X + 1].IsGardenPlot
                && (!garden.Map[Y][X + 1].IsFilled
                || (garden.Map[Y][X + 1].IsFilled && steps + 1 < garden.Map[Y][X + 1].StepsToReach)))            
            {
                garden.Map[Y][X + 1].FloodFill(steps + 1, garden);
            }

            if (X - 1 > 0 && garden.Map[Y][X - 1].IsGardenPlot
                && (!garden.Map[Y][X - 1].IsFilled
                || (garden.Map[Y][X - 1].IsFilled && steps + 1 < garden.Map[Y][X - 1].StepsToReach)))            
            {
                garden.Map[Y][X - 1].FloodFill(steps + 1, garden);
            }
        }
    }
}

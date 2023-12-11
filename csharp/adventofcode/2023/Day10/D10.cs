using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day10
{
    public static class D10
    {
        public static void Part1()
        {
            var grid = CreateGrid();
            Console.WriteLine("Part 1: " + grid.FindLoop().ToString());            
        }

        public static void Part2()
        {
            var grid = CreateGrid();
            grid.FindLoop();
            Console.WriteLine("Part 2: " + grid.CountNestedTiles().ToString());
           ///grid.PrintMe();
        }

        private static Grid CreateGrid()
        {
            var grid = new Grid();
            string path = "C:\\Users\\Owner\\Development\\adventofcode\\csharp\\adventofcode\\2023\\Day10\\input.txt";
            var lines = File.ReadLines(path);
            foreach (var line in lines)
            {
                grid.ParseLine(line);
            }

            return grid;
        }
    }


}

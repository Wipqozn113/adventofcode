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
            var grid = new Grid();
            string path = "C:\\Users\\Owner\\Development\\adventofcode\\csharp\\adventofcode\\2023\\Day10\\input.txt";
            var lines = File.ReadLines(path);
            foreach(var line in lines)
            {
                grid.ParseLine(line);
            }
            Console.WriteLine("Part 1: " + grid.FindLoop().ToString());
             
        }
    }


}

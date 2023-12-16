using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day16
{
    public static class D16
    {
        public static void Part1()
        {
            var cave = CreateCave();
            var energizedTiles = cave.FireLaser();
            Console.WriteLine("Part 1: " + energizedTiles.ToString());
        }

        public static void Part2() 
        {
            var cave = CreateCave();
            var energizedTiles = cave.FireLasers();
            Console.WriteLine("Part 2: " + energizedTiles.ToString());
        }

        public static Cave CreateCave()
        {
            string path = "C:\\Users\\Owner\\Development\\adventofcode\\csharp\\adventofcode\\2023\\Day16\\input.txt";
            var lines = File.ReadLines(path).ToList();
            var cave = new Cave(lines);

            return cave;
        }
    }
}

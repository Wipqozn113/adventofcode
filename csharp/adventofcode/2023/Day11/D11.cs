using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day11
{
    public static class D11
    {
        public static void Part1()
        {
            var universe = CreateUniverse();
            universe.Expand();
            var total = universe.FindDistanceSum();
            Console.WriteLine("Part 1: " + total.ToString());
            //universe.PrintMe();
        }

        public static void Part2()
        {
            var universe = CreateUniverse();
            universe.Expand(1000000 - 1);
            var total = universe.FindDistanceSum();
            Console.WriteLine("Part 2: " + total.ToString());
            //universe.PrintMe();
        }

        private static Universe CreateUniverse()
        {
            var universe = new Universe();
            string path = "C:\\Users\\Owner\\Development\\adventofcode\\csharp\\adventofcode\\2023\\Day11\\input.txt";
            var lines = File.ReadLines(path);
            foreach (var line in lines)
            {
                universe.ParseLine(line);
            }

            return universe;
        }
    }


}

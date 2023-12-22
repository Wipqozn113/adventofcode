using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day21
{
    public static class D21
    {
        public static void Part1()
        {
            var garden = CreateGarden();
            Console.WriteLine("Part 1: " +  garden.RunFloodFill().ToString());
        }

        public static void Part2() { }  

        public static Garden CreateGarden()
        {
            var garden = new Garden();
            string path = "C:\\Users\\Owner\\Development\\adventofcode\\csharp\\adventofcode\\2023\\Day21\\input.txt";
            var lines = File.ReadLines(path).ToList();
            foreach (var line in lines)
            {
                garden.ParseLine(line);
            }

            return garden;
        }
    }
}

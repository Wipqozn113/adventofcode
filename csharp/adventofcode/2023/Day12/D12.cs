using AOC2023.Day11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day12
{
    public static class D12
    {
        public static void Part1()
        {
            string path = "C:\\Users\\Owner\\Development\\adventofcode\\csharp\\adventofcode\\2023\\Day12\\input.txt";
            var lines = File.ReadLines(path);
            long total = 0;
            foreach (var line in lines)
            {
                var springs = new SpringRow(line);
                springs.CalculateValidCombinations();
                total += springs.ValidCombinations;
            }
            Console.WriteLine("Part 1: " + total.ToString());
        }

        public static void Part2()
        {
            string path = "C:\\Users\\Owner\\Development\\adventofcode\\csharp\\adventofcode\\2023\\Day12\\test.txt";
            var lines = File.ReadLines(path);
            long total = 0;
            foreach (var line in lines)
            {
               //Console.WriteLine("On line " + line);
                var springs = new SpringRow(line);
                springs.Rollout();
                springs.CalculateValidCombinations();
                total += springs.ValidCombinations;
            }
            Console.WriteLine("Part 2: " + total.ToString());
        }
    }
}

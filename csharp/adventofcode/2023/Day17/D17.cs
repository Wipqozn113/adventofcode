using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day17
{
    public static class D17
    {
        public static void Part1()
        {
            var graph = CreateGraph(true);
            Console.WriteLine("Part 1: " + graph.FindLowestHeatLoss());
        }

        public static void Part2()
        {
            var graph = CreateGraph(false);
            Console.WriteLine("Part 2: " + graph.FindLowestHeatLoss());
        }

        public static Graph CreateGraph(bool part1)
        {
            // test3 should be 34 (42 is incorrect since I refuse to backtrack)
            string path = "C:\\Users\\Owner\\Development\\adventofcode\\csharp\\adventofcode\\2023\\Day17\\input.txt";
            var lines = File.ReadLines(path).ToList();
            var graph = new Graph(lines, part1);

            return graph;
        }
    }
}

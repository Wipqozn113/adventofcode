using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day20
{
    public static class D20
    {
        public static void Part1()
        {
            var graph = CreateGraph();
            Console.WriteLine("Part 1: " + graph.PressButton(1000));
        }

        public static void Part2()
        { 
            var graph = CreateGraph();
            Console.WriteLine("Part 2: " + graph.PressButton());
        }

        public static Graph CreateGraph()
        {
            var graph = new Graph();
            string path = "C:\\Users\\Owner\\Development\\adventofcode\\csharp\\adventofcode\\2023\\Day20\\input.txt";
            var lines = File.ReadLines(path).ToList();
            foreach(var line in lines)
            {
                graph.AddModule(line);
            }
            graph.PopulateChildren();


            return graph;
        }
    }
}

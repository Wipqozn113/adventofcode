using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;


namespace AOC2023.Day8
{
    public static class D8
    {
        public static void Part1()
        {
            var graph = CreateGraph();
            var steps = graph.FindSteps("AAA", "ZZZ");
            Console.WriteLine("Part 1: " + steps.ToString());  
        }

        public static void Part2()
        {
            var graph = CreateGraph();
            var steps = graph.FindGhostSteps('A', 'Z');
            Console.WriteLine("Part 2: " + steps.ToString());
        }

        public static LRGraph CreateGraph()
        {
            string path = "C:\\Users\\Owner\\Development\\adventofcode\\csharp\\adventofcode\\2023\\Day8\\input.txt";
            var lines = File.ReadLines(path);
            string? instructions = null;
            LRGraph? graph = null;
            Regex rx = new Regex(@"[0-9A-Z]+");
            foreach (var line in lines)
            {
                if(string.IsNullOrWhiteSpace(line)) continue;

                if (instructions is null)
                {
                    instructions = line.Trim();
                    continue;
                }

                MatchCollection matches = rx.Matches(line.Trim());
                var name = matches[0].ToString();
                var leftChild = matches[1].ToString();
                var rightChild = matches[2].ToString();
                var node = new LRNode(name, leftChild, rightChild);

                if(graph is null)
                {
                    graph  = new LRGraph(node, instructions);
                }
                else
                {
                     graph.Nodes.Add(node);
                }
            }

            graph.PopulateChildren();
            return graph;
        }
    }
}

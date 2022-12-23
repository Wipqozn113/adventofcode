using System.Collections;
using System.Diagnostics;
using System.Text;

namespace day16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Part1();
            Part2Basic();
           // Part2(true);
        }

        static void Part1(bool test=false)
        {
            var nodes = Utils.CreateNodes(test);
            Utils.PopulateChildren(nodes);
            var graph = Utils.CreateGraph(nodes);
            Console.Write("Part 1: ");
            Console.WriteLine(graph.FindBestPressure());
        }

        static void Part2Basic(bool test = false)
        {
            var nodes = Utils.CreateNodes(true, test);
            Utils.PopulateChildren(nodes);
            var graph = Utils.CreateGraph(nodes, 26);
            Console.Write("Part 2: ");
            graph.FindBestPressureWithElephant();
            Console.WriteLine(graph.BestTotalPressure);
        }

        static void Part2(bool test = false)
        {
            var nodes = Utils.CreateNodes(true, test);
            Utils.PopulateChildren(nodes);
            var graph = Utils.CreateGraph(nodes, 26);
            var crawlers = new Crawlers(graph, 0);
            Console.WriteLine(crawlers.StartCrawl());
        }
    }
}
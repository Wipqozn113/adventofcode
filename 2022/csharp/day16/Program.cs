using System.Collections;
using System.Diagnostics;
using System.Text;

namespace day16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Part1();
            Part2(true);
        }

        static void Part1(bool test=false)
        {
            var nodes = Utils.CreateNodes(test);
            Utils.PopulateChildren(nodes);
            var graph = Utils.CreateGraph(nodes);
            Console.WriteLine(graph.FindBestPressure());
        }

        static void Part2(bool test = false)
        {
            var nodes = Utils.CreateNodes(test);
            Utils.PopulateChildren(nodes);
            var graph = Utils.CreateGraph(nodes, 26);
            Console.WriteLine(graph.FindBestPressureWithHelper());
        }
    }
}
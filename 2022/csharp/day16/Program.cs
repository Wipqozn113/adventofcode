using System.Collections;
using System.Diagnostics;
using System.Text;

namespace day16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var nodes = Utils.CreateNodes(true);
            Utils.PopulateChildren(nodes);
            var graph = Utils.CreateGraph(nodes);
            Console.WriteLine(graph.FindBestPressure());
        }
    }
}
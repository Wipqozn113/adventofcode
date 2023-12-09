using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCUtils.Graphs
{
    public class DirectedGraph
    {
        public DirectedGraph(string rootName)
        {
            Root = new Node(rootName);
        }

        private Node Root { get; set; }

        private class Node
        {
            public Node(string name)
            {
                Name = name;
            }

            public string Name { get; set; }

            public List<Edge> Edges { get; set; } = new List<Edge>();

            public Dictionary<string, long> DistanceTo { get; set; } = new Dictionary<string, long>();

        }

        private class Edge
        {
            public Edge(Node parent, Node child, long weight)
            {
                Parent = parent;
                Child = child;
                Weight = weight;
            }

            public Node Parent { get; set; }

            public Node Child { get; set; }

            public long Weight { get; set; }
        }
    }
}

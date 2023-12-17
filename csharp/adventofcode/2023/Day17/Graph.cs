using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day17
{
    public class Graph
    {
        public Graph(List<string> lines)
        {
            ParseInput(lines); 
        }

        public List<List<Node>> Nodes { get; set; } = new List<List<Node>>();

        public long FindLowestHeatLoss()
        {
            return 0;
        }

        private void ParseInput(List<string> lines)
        {
            // Populate Nodes
            foreach(var line in lines)
            {
                var nodes = new List<Node>();
                foreach(var c in line.Trim())
                {
                    nodes.Add(new Node(int.Parse(c.ToString())));
                }
                Nodes.Add(nodes);
            }

            // Create edges
            for(var y = 0; y < Nodes.Count; y++)
            {
                for(var x = 0; x < Nodes[y].Count; x++)
                {
                    var node = Nodes[y][x];
                    if(y + 1 < Nodes.Count)
                    {
                        node.CreateEdge(Nodes[y + 1][x], Direction.South);
                    }
                    
                    if (y - 1 >= 0)
                    {
                        node.CreateEdge(Nodes[y - 1][x], Direction.North);
                    }

                    if (x + 1 < Nodes[y].Count)
                    {
                        node.CreateEdge(Nodes[y][x + 1], Direction.East);
                    }

                    if (x - 1 >= 0)
                    {
                        node.CreateEdge(Nodes[y][x - 1], Direction.West);
                    }
                }
            }
        }
    }
}

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

        public Node LavaPool
        {
            get
            {
                return Nodes[0][0];
            }
        }

        public Node Factory
        {
            get
            {
                return Nodes.Last().Last();
            }
        }

        public double FindLowestHeatLoss()
        {
            return Dijkstra(LavaPool, Factory);
        }

        private double Dijkstra(Node source, Node target)
        {
            // Initialize all distances (except source to source) to Infinity
            var nodes = Nodes.SelectMany(n => n).ToList();
            var queue = new List<Node>();
            var distances = new Dictionary<Node, double>();
            distances[source] = 0;
            foreach(var node in nodes)
            {
                if(node != source)
                {
                    distances[node] = double.PositiveInfinity;
                }
                queue.Add(node);
            }

            // Calculate shortest distances
            while(queue.Any())
            {
                var keyValuePair = distances.Where(n => queue.Contains(n.Key)).OrderBy(x => x.Value).First();
                var node = keyValuePair.Key;
                queue.Remove(node);
                
                foreach(var edge in node.Edges)
                {
                    var alt = distances[node] + edge.HeatLoss;
                    if(alt < distances[edge.Target])
                    {
                        distances[edge.Target] = alt;
                    }
                }
            }

            return distances[target];

        }

        /*
         * function Dijkstra(Graph, source):
       dist[source]  := 0                     // Distance from source to source is set to 0
       for each vertex v in Graph:            // Initializations
           if v ≠ source
               dist[v]  := infinity           // Unknown distance function from source to each node set to infinity
           add v to Q                         // All nodes initially in Q

      while Q is not empty:                  // The main loop
          v := vertex in Q with min dist[v]  // In the first run-through, this vertex is the source node
          remove v from Q 

          for each neighbor u of v:           // where neighbor u has not yet been removed from Q.
              alt := dist[v] + length(v, u)
              if alt < dist[u]:               // A shorter path to u has been found
                  dist[u]  := alt            // Update distance of u 

      return dist[]
  end function */

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

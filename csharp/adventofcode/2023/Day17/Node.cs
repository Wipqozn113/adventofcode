using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day17
{
    public class Node
    {
        public Node(int heatLoss)
        {
            HeatLoss = heatLoss;
        }

        public void CreateEdge(Node target, Direction direction)
        {
            var edge = new Edge(this, target, direction);
            Edges.Add(edge);
        }

        public List<Edge> Edges = new List<Edge>();

        public int HeatLoss { get; set; }
    }

    public class Edge
    {
        public Edge(Node source, Node target, Direction direction)
        {
            Source = source;
            Target = target;
            Direction = direction;
        }

        public Node Source { get; set; }

        public Node Target { get; set; }

        public Direction Direction { get; set; }

        public int HeatLoss
        {
            get
            {
                return Target.HeatLoss;
            }
        }
        

    }

    public enum Direction
    {
        North,
        East,
        South,
        West
    }
}

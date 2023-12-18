using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day17
{
    struct NodeKey
    {
        public NodeKey(int id, Direction direction, int stepsInDirection)
        {
            NodeId = id;
            Direction = direction;
            StepsInDirection = stepsInDirection;
        }

        public int NodeId { get; set; }

        public Direction Direction { get; set; }

        public int StepsInDirection { get; set; }

        public string AsString()
        {
            return NodeId.ToString() + ";" + Direction.ToString() + ";" + StepsInDirection.ToString();
        }
    }

    public class Node
    {
        public Node(BasicNode node)
        {
            ID = node.ID;
            HeatLoss = node.HeatLoss;
            X = node.X;
            Y = node.Y;
        }

        public void CreateEdge(Node target, Direction direction)
        {
            var edge = new Edge(this, target, direction);
            Edges.Add(edge);
        }

        public int X { get; set; }

        public int Y { get; set; }

        public List<Edge> Edges = new List<Edge>();

        public int ID { get; set; }

        public int HeatLoss { get; set; }

        public Direction Direction { get; set; }

        public int StepsWithoutTurning { get; set; }

        public List<Node> Path { get; set; } = new List<Node>();

        public string Key
        {
            get
            {
                var key = ID.ToString();
                key += ";" + HeatLoss.ToString();
                key += ";" + Direction.ToString();
                key += ";" + StepsWithoutTurning.ToString();
                return key;
            }
        }
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
}

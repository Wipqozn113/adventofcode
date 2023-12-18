using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day17
{
    struct BasicNodeKey
    {
        public BasicNodeKey(int id, Direction direction, int stepsInDirection)
        {
            BasicNodeId = id;
            Direction = direction;
            StepsInDirection = stepsInDirection;
        }

        public int BasicNodeId { get; set; }

        public Direction Direction { get; set; }

        public int StepsInDirection { get; set; }

        public string AsString()
        {
            return BasicNodeId.ToString() + ";" + Direction.ToString() + ";" + StepsInDirection.ToString();
        }


    }

    public class BasicNode
    {
        public BasicNode(int id, int heatLoss, int x, int y)
        {
            ID = id;
            HeatLoss = heatLoss;
            X = x;
            Y = y;
        }

        public void CreateEdge(BasicNode target, Direction direction)
        {
            var edge = new BasicEdge(this, target, direction);
            Edges.Add(edge);
        }

        public List<BasicEdge> Edges = new List<BasicEdge>();

        public int X { get; set; }  

        public int Y { get; set; }  

        public int ID { get; set; }

        public int HeatLoss { get; set; }

        public double ShortestPath { get; set; }
    }

    public class BasicEdge
    {
        public BasicEdge(BasicNode source, BasicNode target, Direction direction)
        {
            Source = source;
            Target = target;
            Direction = direction;
        }

        public BasicNode Source { get; set; }

        public BasicNode Target { get; set; }

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
        None,
        North,
        East,
        South,
        West
    }
}

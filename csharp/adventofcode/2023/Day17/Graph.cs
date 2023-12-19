using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AOCUtils.MathUtils;

namespace AOC2023.Day17
{
    public class Graph
    {
        public Graph(List<string> lines, bool part1)
        {
            ParseInput(lines, part1); 
        }

        public List<List<BasicNode>> BasicNodes { get; set; } = new List<List<BasicNode>>();

        public List<Node> Nodes { get; set; } = new List<Node>();

        public Dictionary<string, Node> Hashmap = new Dictionary<string, Node>();

        
        public BasicNode LavaPool
        {
            get
            {
                return BasicNodes[0][0];
            }
        }

        public BasicNode Factory
        {
            get
            {
                return BasicNodes.Last().Last();
            }
        }

        
        public double FindLowestHeatLoss()
        {
            //return Dijkstra(LavaPool, Factory);
            return DijkstraState(LavaPool, Factory);
        }

        private double DijkstraState(BasicNode source, BasicNode target)
        {
            // Initialize all distances (except source to source) to Infinity
            var start = Nodes.Where(x => x.ID == source.ID).First();
            var queue = new PriorityQueue<Node, double>();
            var distances = new Dictionary<Node, double>();
            distances[start] = 0;
            foreach (var node in Nodes)
            {
                if (node != start)
                {
                    distances[node] = double.PositiveInfinity;
                }

                queue.Enqueue(node, distances[node]);
            }

            // Calculate shortest distances
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                foreach (var edge in node.Edges)
                {
                    var alt = distances[node] + edge.HeatLoss;
                    var tar = Hashmap[edge.Target.Key];
                    if (alt < distances[tar])
                    {
                        distances[tar] = alt;
                        queue.Enqueue(tar, distances[tar]);
                    } 
                }
            }

            return distances.Where(x => x.Key.ID == target.ID).Select(x => x.Value).Min();

        }

        private double AltApproach()
        {
            // Calculate shortest path to FActory for every node
            var factory = Factory;
            foreach(var node in BasicNodes.SelectMany(t => t).ToList())
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                // the code that you want to measure comes here
                node.ShortestPath = Dijkstra(node, factory);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
            }

            return 0;
        }

        private double Dijkstra(BasicNode source, BasicNode target)
        {
            // Initialize all distances (except source to source) to Infinity
            var nodes = BasicNodes.SelectMany(n => n).ToList();
            var queue = new PriorityQueue<BasicNode, double>();
            var distances = new Dictionary<BasicNode, double>();
            distances[source] = 0;
            //queue.Enqueue(source, 0);
            foreach(var node in nodes)
            {
                if(node != source)
                {
                    distances[node] = double.PositiveInfinity;
                }
                queue.Enqueue(node, distances[node]);
            }

            // Calculate shortest distances
            while(queue.Count > 0)
            {
                var node = queue.Dequeue();
                
                foreach(var edge in node.Edges)
                {
                    var alt = distances[node] + edge.HeatLoss;
                    if(alt < distances[edge.Target])
                    {
                        distances[edge.Target] = alt;
                        queue.Enqueue(edge.Target, alt);
                    }
                }
            }

            return distances[target];

        }          

        private void ParseInput(List<string> lines, bool part1 = false)
        {
            CreateBasicNodes(lines);
            if (part1)
            {
                CreateStateNodesPart1();
            }
            else
            {
                CreateStateNodesPart2();
            }
        }

        private void CreateStateNodesPart1()
        {
            var startNode = new Node(BasicNodes[0][0]);
            Nodes.Add(startNode);
            var nodes = new Queue<Node>();
            nodes.Enqueue(startNode);
 

            while(nodes.Any())
            {
                var node = nodes.Dequeue();
                var basicNode = BasicNodes.SelectMany(l => l).Where(x => x.ID == node.ID).First();

                //var furthestX = node.Path.Any() ? node.Path.Max(n => n.X) : 0;
               // var furthestY = node.Path.Any() ? node.Path.Max(n => n.Y) : 0;

                // Construct the northeren edge and state node, if possible
                if (node.Direction != Direction.South && !(node.Direction == Direction.North && node.StepsWithoutTurning == 2))
                {
                    var northNode = basicNode.Edges.Where(x => x.Direction == Direction.North).FirstOrDefault()?.Target;
                    if (northNode is not null)
                    {
                        var newNode = new Node(northNode);
                        newNode.Direction = Direction.North;
                        newNode.StepsWithoutTurning = node.Direction == Direction.North ? node.StepsWithoutTurning + 1 : 0;
                        node.CreateEdge(newNode, Direction.North);
                        if (!Hashmap.ContainsKey(newNode.Key))
                        {
                            nodes.Enqueue(newNode);
                            Nodes.Add(newNode);
                            Hashmap[newNode.Key] = newNode;
                            newNode.Path.AddRange(node.Path);
                            newNode.Path.Add(node);
                        }
                    }
                }

                // Construct the southern edge and state node, if possible
                if (node.Direction != Direction.North && !(node.Direction == Direction.South && node.StepsWithoutTurning == 2))
                {
                    var southNode = basicNode.Edges.Where(x => x.Direction == Direction.South).FirstOrDefault()?.Target;
                    if (southNode is not null)
                    {
                        var newNode = new Node(southNode);
                        newNode.Direction = Direction.South;
                        newNode.StepsWithoutTurning = node.Direction == Direction.South ? node.StepsWithoutTurning + 1 : 0;
                        node.CreateEdge(newNode, Direction.South);
                        if (!Hashmap.ContainsKey(newNode.Key))
                        {
                            nodes.Enqueue(newNode);
                            Nodes.Add(newNode);
                            Hashmap[newNode.Key] = newNode;
                            newNode.Path.AddRange(node.Path);
                            newNode.Path.Add(node);
                        }
                    }
                }

                // Construct the eastern edge and state node, if possible
                if (node.Direction != Direction.West && !(node.Direction == Direction.East && node.StepsWithoutTurning == 2))
                {
                    var eastNode = basicNode.Edges.Where(x => x.Direction == Direction.East).FirstOrDefault()?.Target;
                    if (eastNode is not null)
                    {
                        var newNode = new Node(eastNode);
                        newNode.Direction = Direction.East;
                        newNode.StepsWithoutTurning = node.Direction == Direction.East ? node.StepsWithoutTurning + 1 : 0;
                        node.CreateEdge(newNode, Direction.East);
                        if (!Hashmap.ContainsKey(newNode.Key))
                        {
                            nodes.Enqueue(newNode);
                            Nodes.Add(newNode);
                            Hashmap[newNode.Key] = newNode;
                            newNode.Path.AddRange(node.Path);
                            newNode.Path.Add(node);
                        }
                    }
                }

                // Construct the western edge and state node, if possible
                if (node.Direction != Direction.East && !(node.Direction == Direction.West && node.StepsWithoutTurning == 2))
                {
                    var westNode = basicNode.Edges.Where(x => x.Direction == Direction.West).FirstOrDefault()?.Target;
                    if (westNode is not null)
                    {
                        var newNode = new Node(westNode);
                        newNode.Direction = Direction.West;
                        newNode.StepsWithoutTurning = node.Direction == Direction.West ? node.StepsWithoutTurning + 1 : 0;
                        node.CreateEdge(newNode, Direction.West);
                        if (!Hashmap.ContainsKey(newNode.Key))
                        {
                            nodes.Enqueue(newNode);
                            Nodes.Add(newNode);
                            Hashmap[newNode.Key] = newNode;
                            newNode.Path.AddRange(node.Path);
                            newNode.Path.Add(node);
                        }
                    }
                }
            }
        }

        private void CreateStateNodesPart2()
        {
            var startNode = new Node(BasicNodes[0][0]);
            Nodes.Add(startNode);
            var nodes = new Queue<Node>();
            nodes.Enqueue(startNode);


            while (nodes.Any())
            {
                var node = nodes.Dequeue();
                var basicNode = BasicNodes.SelectMany(l => l).Where(x => x.ID == node.ID).First();

                // We're going to assume we  should never go North or West, because it would mean going at least 4 spaces

                // Construct the southern edge and state node, if possible
                if (node.Direction == Direction.None ||
                    (node.Direction == Direction.South && node.StepsWithoutTurning < 9) ||
                    (node.Direction == Direction.East && node.StepsWithoutTurning >= 3))
                {
                    var southNode = basicNode.Edges.Where(x => x.Direction == Direction.South).FirstOrDefault()?.Target;
                    if (southNode is not null &&
                        (node.Direction == Direction.None || node.Direction == Direction.South || (Direction.East == node.Direction && BasicNodes.Count > node.Y + 4)))
                    {     
                        var newNode = new Node(southNode);
                        newNode.Direction = Direction.South;
                        newNode.StepsWithoutTurning = node.Direction == Direction.South ? node.StepsWithoutTurning + 1 : 0;
                        node.CreateEdge(newNode, Direction.South);
                        if (!Hashmap.ContainsKey(newNode.Key))
                        {
                            nodes.Enqueue(newNode);
                            Nodes.Add(newNode);
                            Hashmap[newNode.Key] = newNode;
                            newNode.Path.AddRange(node.Path);
                            newNode.Path.Add(node);
                        }
                    }
                }

                // Construct the eastern edge and state node, if possible
                if (node.Direction == Direction.None ||  
                    (node.Direction == Direction.East && node.StepsWithoutTurning < 9) ||
                    (node.Direction == Direction.South && node.StepsWithoutTurning >= 3))
                {
                    var eastNode = basicNode.Edges.Where(x => x.Direction == Direction.East).FirstOrDefault()?.Target;
                    if (eastNode is not null &&
                        (node.Direction == Direction.None || node.Direction == Direction.East || (Direction.South == node.Direction && BasicNodes[node.Y].Count > node.X + 4)))
                    {
                        var newNode = new Node(eastNode);
                        newNode.Direction = Direction.East;
                        newNode.StepsWithoutTurning = node.Direction == Direction.East ? node.StepsWithoutTurning + 1 : 0;
                        node.CreateEdge(newNode, Direction.East);
                        if (!Hashmap.ContainsKey(newNode.Key))
                        {
                            nodes.Enqueue(newNode);
                            Nodes.Add(newNode);
                            Hashmap[newNode.Key] = newNode;
                            newNode.Path.AddRange(node.Path);
                            newNode.Path.Add(node);
                        }
                    }
                }
            }
        }

        private void CreateBasicNodes(List<string> lines)
        {
            // Populate Nodes
            var id = 0;
            var yx = 0;
            foreach(var line in lines)
            {
                var nodes = new List<BasicNode>();
                var xx = 0;
                foreach(var c in line.Trim())
                {
                    nodes.Add(new BasicNode(id, int.Parse(c.ToString()), xx, yx));
                    id++;
                    xx++;
                }
                yx++;
                BasicNodes.Add(nodes);
            }

            // Create edges
            for(var y = 0; y < BasicNodes.Count; y++)
            {
                for(var x = 0; x < BasicNodes[y].Count; x++)
                {
                    var node = BasicNodes[y][x];
                    if(y + 1 < BasicNodes.Count)
                    {
                        node.CreateEdge(BasicNodes[y + 1][x], Direction.South);
                    }
                    
                    if (y - 1 >= 0)
                    {
                        node.CreateEdge(BasicNodes[y - 1][x], Direction.North);
                    }

                    if (x + 1 < BasicNodes[y].Count)
                    {
                        node.CreateEdge(BasicNodes[y][x + 1], Direction.East);
                    }

                    if (x - 1 >= 0)
                    {
                        node.CreateEdge(BasicNodes[y][x - 1], Direction.West);
                    }
                }
            }
    
        }
    }
}

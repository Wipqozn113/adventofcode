using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace day16_2
{
    public class Path
    {
        public Path(List<Node> nodes, long pressure)
        {
            Nodes = nodes;
            PressureReleased = pressure;
        }

        public List<Node> Nodes { get; set; } = new List<Node>();
        public long PressureReleased { get; set; }
    }

    public class Graph
    {
        public Graph(Node root, long maxDepth = 30)
        {
            Root = root;
            MaxDepth = maxDepth - 1;
        }

        public Dictionary<string, Path> Paths { get; set; } = new Dictionary<string, Path>();

        public bool AddPath(List<Node> nodes, long pressureReleased)
        {
            var key = string.Join(":", nodes.Select(x => x.Name).ToList());
            if(Paths.ContainsKey(key))
            {
                var path = Paths[key];

                // Worse Path
                return false;
            }

            // Better path, or first time seeing this path
            Paths[key] = new Path(nodes.ToList(), pressureReleased);
            return true;
        }

        public long MaxDepth;

        public void ResetNodes()
        {
            foreach (var node in Nodes)
            {
                node.Reset();
            }
        }

        public long FindBestPressure(long crawlers = 1)
        {            
            Root.State = new State()
            {
                Depth = 0,
                MaxDepth = MaxDepth,
                Graph = this
            };

            var bestpressure = Root.Crawl();

            if(crawlers == 2)
            {                
                foreach(var path in Paths.Where(p => p.Value.PressureReleased > 0))
                {
                    if(path.Key == "JJ:HH:BB:EE:CC")
                    {
                        var yolo = "asdasd";
                    }
                    ResetNodes();
                    Root.State = new State()
                    {
                        Depth = 0,
                        UpdatePaths = false,
                        MaxDepth = MaxDepth,
                        Graph = this,
                        ValvesOn = path.Value.Nodes.ToList(),
                        FirstPath = path.Value.Nodes.ToList(),
                        PressureReleased = 0  // path.Value.PressureReleased
                    };
                    var pressure = Root.Crawl();
                    var pres2 = pressure + path.Value.PressureReleased;
                    if(pres2 >= 1327)
                    {
                        var what = "whadasd";
                    }
                    if (pres2 > bestpressure)
                        bestpressure = pres2;
                }
            }

            return bestpressure;
        }

        public void FindShortestPaths()
        {
            foreach (var node in Nodes)
            {
                foreach (var target in Nodes.Where(x => x.Name != node.Name))
                {
                    var distance = FindShortestPath(node, target);
                    if (distance > 0)
                        node.Distance[target.Name] = distance;
                    ResetNodes();
                }
            }
        }

        public long FindShortestPath(Node start, Node goal)
        {
            var node = BFS(start, goal);

            // Goal unreachable
            if (node is null)
                return 0;

            var distance = 0;
            var n = node;
            while (n.Parent is not null)
            {
                distance++;
                n = n.Parent;
            }

            return distance;
        }

        public Node? BFS(Node start, Node goal)
        {
            if (start.Name == "HH")
            {
                var ohmy = "lasd";
            }
            var queue = new Queue<Node>();
            start.Visited = true;
            queue.Enqueue(start);

            while (queue.Any())
            {
                var node = queue.Dequeue();
                if (node.Name == goal.Name)
                    return node;

                foreach (var child in node.Children)
                {
                    if (!child.Visited)
                    {
                        child.Visited = true;
                        child.Parent = node;
                        queue.Enqueue(child);
                    }
                }
            }

            // Unreachable
            return null;
        }

        public Node Root { get; set; }

        public List<Node> Nodes { get; set; } = new List<Node>();

        public List<Node> NonZeroFlowRateNodes { get; set; } = new List<Node>();

    }

    public class Node
    {
        public string Name
        {
            get
            {
                return Valve.Name;
            }
            set
            {
                Valve.Name = value;
            }
        }

        public void Reset()
        {
            Parent = null;
            Visited = false;
            State = new State();
        }

        public Graph Graph { get; set; }

        public Node? Parent { get; set; }
        public bool Visited { get; set; } = false;

        public Valve Valve { get; set; }
        public List<Node> Children { get; set; } = new List<Node>();
        public State State { get; set; } = new State();

        public Dictionary<string, long> Distance { get; set; } = new Dictionary<string, long>();

        public long Crawl(long distance = 0)
        {
            long bestPressure = State.PressureReleased;

            // Update pressure based on time taken to get here
            State.UpdatePressureReleased(distance);
            if (bestPressure == 480)
            {
                var aasd = "asdasd";
            }

            if (State.TimeRemaining == 0)
            {

                return State.PressureReleased;
            }

            // I'm the next Valve to turn on!
            if (Valve.Name == State.NextValve?.Name)
            {
                OpenValve();
            }

            var nodes = CreatePaths();

            // Either no nodes left to visit, or we've run out of time. Calculate final pressure.
            if (nodes.Count == 0 || State.TimeRemaining == 0)
            {
                State.UpdatePressureReleased(State.TimeRemaining);
                if (State.PressureReleased > bestPressure)
                    bestPressure = State.PressureReleased;
                if (bestPressure == 480)
                {
                    var aasd = "asdasd";
                }
                return bestPressure;
            }

            foreach (var node in nodes)
            {
                node.State = State.Copy();
                node.State.NextValve = node.Valve;
                node.State.Depth += Distance[node.Name];
                var pressure = node.Crawl(Distance[node.Name]);

                // If this path produced a better best pressure, update best pressure
                if (pressure > bestPressure)
                    bestPressure = pressure;
            }


            if (bestPressure == 480)
            {
                var aasd = "asdasd";
            }
            return bestPressure;
        }

        public List<Node> CreatePaths()
        {
            var nodes = new List<Node>();

            // Find all Nodes with unopened valves that we can reach within the time remaining
            // Give higher priority to nodes which will produce the most pressure 
            var unvisitedNodes = Graph.NonZeroFlowRateNodes
                .Where(node => !State.ValvesOn.Any(n => n.Name == node.Name))
                .Where(node => Distance.ContainsKey(node.Name) && State.TimeRemaining - Distance[node.Name] > 0)
                .OrderByDescending(node => node.Valve.FlowRate * (State.TimeRemaining - Distance[node.Name]))
                .ToList();

            // Already opened all valves, so just wait around
            if (unvisitedNodes.Count == 0)
                return nodes;

            // Determine which valves are worth visiting, and in what order
            return unvisitedNodes;
        }

        public void OpenValve()
        {
            State.ValvesOn.Add(this);
            State.UpdatePressureRate();
            State.Depth += 1;
            State.UpdatePressureReleased(1);
        }
    }

    public class State
    {
        public Graph Graph { get; set; }
        public List<Node> ValvesOn { get; set; } = new List<Node>();
        public List<Node> FirstPath { get; set; } = new List<Node>();
        public Valve? NextValve { get; set; }

        public bool UpdatePaths { get; set; } = true;

        public State Copy()
        {
            return new State()
            {
                ValvesOn = ValvesOn.ToList(),
                FirstPath = FirstPath.ToList(),
                PressureReleased = PressureReleased,
                _pressurePerTurn = PressurePerTurn,
                Depth = Depth,
                MaxDepth = MaxDepth,
                Graph = Graph,
                UpdatePaths = UpdatePaths
            };
        }

        public long PressureReleased { get; set; } = 0;

        public void UpdatePressureReleased(long time = 1)
        {
            // Can do multiple updates at once
            PressureReleased += PressurePerTurn * time;
            if (UpdatePaths)
            {
                Graph.AddPath(ValvesOn, PressureReleased);
            }
        }

        public void UpdatePressureRate()
        {
            _pressurePerTurn = ValvesOn
                .Where(node => !FirstPath.Any(p => node.Name == p.Name))
                .Sum(node => node.Valve.FlowRate);
        }

        public long PressurePerTurn
        {
            get
            {
                return _pressurePerTurn;
            }
        }

        private long _pressurePerTurn;

        public long Depth { get; set; }

        public long MaxDepth { get; set; }

        public long TimeRemaining
        {
            get
            {
                return MaxDepth - Depth;
            }
        }

    }
}

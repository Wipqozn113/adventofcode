using System.Collections;
using System.Diagnostics;
using System.Text;

namespace Day20
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            // Blueprint 1:  Each ore robot costs 4 ore.  Each clay robot costs 2 ore.  Each obsidian robot costs 3 ore and 14 clay.  Each geode robot costs 2 ore and 7 obsidian.
            var bp1 = new Blueprint(1,    
                    new Cost() { Ore = 4 }, 
                    new Cost() { Ore = 2 },
                    new Cost() { Ore = 3, Clay = 14 },
                    new Cost() { Ore = 2, Obs = 7}
            );
            // Blueprint 2:  Each ore robot costs 2 ore.Each clay robot costs 3 ore.Each obsidian robot costs 3 ore and 8 clay.Each geode robot costs 3 ore and 12 obsidian.
            var bp2 = new Blueprint(2,
                new Cost() { Ore = 2 },
                new Cost() { Ore = 3 },
                new Cost() { Ore = 3, Clay = 8 },
                new Cost() { Ore = 3, Obs = 12 }
            );*/

            //Blueprint 1: Each ore robot costs 2 ore.Each clay robot costs 4 ore.Each obsidian robot costs 4 ore and 17 clay.Each geode robot costs 3 ore and 11 obsidian.
            var bp1 = new Blueprint(1,
                    new Cost() { Ore = 2 },
                    new Cost() { Ore = 4 },
                    new Cost() { Ore = 4, Clay = 17 },
                    new Cost() { Ore = 3, Obs = 11 }
            );

            //Blueprint 2: Each ore robot costs 3 ore.Each clay robot costs 3 ore.Each obsidian robot costs 3 ore and 20 clay.Each geode robot costs 2 ore and 12 obsidian.
            var bp2 = new Blueprint(1,
                    new Cost() { Ore = 3 },
                    new Cost() { Ore = 3 },
                    new Cost() { Ore = 3, Clay = 20 },
                    new Cost() { Ore = 2, Obs = 12 }
            );

            //Blueprint 3: Each ore robot costs 4 ore.Each clay robot costs 4 ore.Each obsidian robot costs 4 ore and 12 clay.Each geode robot costs 3 ore and 8 obsidian.
            var bp3 = new Blueprint(1,
                    new Cost() { Ore = 4 },
                    new Cost() { Ore = 4 },
                    new Cost() { Ore = 4, Clay = 12 },
                    new Cost() { Ore = 3, Obs = 8 }
            );

            Console.WriteLine("Crawling Blueprint 1");
            var geo1 = Utils.RunBlueprint(bp1, 32);

            Console.WriteLine("Crawling Blueprint 2");
            var geo2 = Utils.RunBlueprint(bp2, 32);

            Console.WriteLine("Crawling Blueprint 3");
            var geo3 = Utils.RunBlueprint(bp3, 32);


            Console.WriteLine("==========================");
            Console.WriteLine((geo1 * geo2 * geo3).ToString());


        }

        public static class Utils
        {
            public static long RunBlueprint(Blueprint blueprint, int depth = 32)
            {
                var graph1 = new Graph(blueprint);
                graph1.CalcBestGeodes(depth);
                Console.WriteLine(graph1.MaxGeodes.ToString());
                var geo1 = graph1.MaxGeodes;
                graph1 = null; // Flag for garbage collection

                return geo1;
            }
        }


        public class Graph
        {
            public Graph(Blueprint blueprint)
            {
                Root = new Node(new State() { OreRobots = 1, Blueprint = blueprint }, this, 0);
                Blueprint = blueprint;
            }

            public Node Root { get; set;}
            public Blueprint Blueprint { get; set; }
            public long MaxGeodes
            {
                get
                {                    
                    return maxGeodes;
                }
                set
                {
                    maxGeodes = value;
                }
            }


            private long maxGeodes;
            public int MaxDepth { get; set; }

            public long CalcBestGeodes(int time = 32)
            {
                MaxDepth = time;

                var geodes = Root.Crawl();

                return MaxGeodes;
            }
        }

        public class Node
        {
            public Node(State state, Graph graph, int depth)
            {
                State = state;
                Graph = graph;
                Depth = depth;
            }

            public List<Node> Children { get; set; } = new List<Node>();
            public State State { get; set; }
            public Graph Graph { get; set; }
            public Blueprint Blueprint
            {
                get
                {
                    return Graph.Blueprint;
                }
            }
            public int Depth { get; set; }
            public int MaxDepth
            {
                get
                {
                    return Graph.MaxDepth;
                }
            }
            public int TimeLeft
            {
                get
                {
                    return MaxDepth - Depth;
                }
            }

            public int Crawl()
            {
                //Console.WriteLine($"Still working... {Depth}");

                /*if(Depth % 10 == 0)
                {
                    Console.WriteLine($"At Depth {Depth}");
                }*/

                // Max Depth. Leave.
                if(Depth == MaxDepth)
                {
                    //Console.WriteLine($"Reached bottom");
                    if (State.Geode > Graph.MaxGeodes)
                        Graph.MaxGeodes = State.Geode;

                    return State.Geode;
                }

                // Just keep looping until we can afford what we schedueld for building
                if(Saving())
                {
                    while(KeepSaving())
                    {
                        State.Mine();
                        Depth += 1;

                        // Max Depth. Leave.
                        if (Depth == MaxDepth)
                        {
                            //Console.WriteLine($"Reached bottom");
                            if (State.Geode > Graph.MaxGeodes)
                                Graph.MaxGeodes = State.Geode;

                            return State.Geode;
                        }
                    }

                    State.Build();
                }

                var max_geodes = State.Geode;
                CreateChildren();

                // Mine, Build, and then Crawl each Child
                foreach(var child in Children)
                {
                    child.State.Mine();
                    child.State.Build();
                    var geo = child.Crawl();
                    if(geo > max_geodes)
                    {
                        max_geodes = geo;
                    }
                }

                if (State.Geode > Graph.MaxGeodes)
                    Graph.MaxGeodes = State.Geode;

                return max_geodes;
            }

            public Node CreateChild()
            {
                return new Node(State.Copy(), Graph, Depth + 1);
            }

            public void CreateChildren()
            {
                // Always Build a Geode if you can afford it
                if(CanAffordGeode())
                {
                    var child = CreateChild();
                    child.State.BuildGeode = true;
                    child.State.GeodeRobotsBuilding = 1;
                    Children.Add(child);

                    return;
                }
                
                if(BuildGeode())
                {
                    var child = CreateChild();
                    child.State.BuildGeode = true;
                    Children.Add(child);
                }

                if (BuildObs())
                {
                    if (CanAffordObs())
                    {
                        var child = CreateChild();
                        child.State.BuildObs = true;
                        child.State.ObsRobotsBuilding = 1;
                        Children.Add(child);
                    }
                    else
                    {
                        var child = CreateChild();
                        child.State.BuildObs = true;
                        Children.Add(child);
                    }
                }

                if (BuildClay())
                {
                    if (CanAffordClay())
                    {
                        var child = CreateChild();
                        child.State.BuildClay = true;
                        child.State.ClayRobotsBuilding = 1;
                        Children.Add(child);
                    }
                    else
                    {
                        var child = CreateChild();
                        child.State.BuildClay = true;
                        Children.Add(child);
                    }
                }

                if (BuildOre())
                {
                    if (CanAffordOre())
                    {
                        var child = CreateChild();
                        child.State.BuildOre = true;
                        child.State.OreRobotsBuilding = 1;
                        Children.Add(child);
                    }
                    else
                    {
                        var child = CreateChild();
                        child.State.BuildOre = true;
                        Children.Add(child);
                    }
                }


            }

            public bool Saving()
            {
                return State.BuildOre || State.BuildClay || State.BuildObs || State.BuildGeode;
            }

            public bool KeepSaving()
            {
                return !CanAfford();
            }

            public bool CanAfford()
            {
                return (State.BuildOre && CanAffordOre()) ||
                    (State.BuildClay && CanAffordClay()) ||
                    (State.BuildObs && CanAffordObs()) ||
                    (State.BuildGeode && CanAffordGeode());
            }

            public bool CanAffordGeode()
            {
                return Blueprint.GeodeRobot.CanAfford(State);
            }

            public bool BuildGeode()
            {
                // Already building
                if(State.BuildGeode)
                  return true;

                // Saving up for something else
                if (Saving())
                    return false;

                // Don't have the robtos required to ever afford this
                if (State.ObsRobots == 0)
                    return false;

                // Can never afford anyways
                if (Blueprint.GeodeRobot.CanNeverAfford(State, TimeLeft))
                {
                    return false;
                }

                return true;
            }

            public bool BuildObs()
            {
                // Already building
                if (State.BuildObs)
                    return true;

                // Saving up for something else
                if (Saving())
                    return false;

                // Don't have the robtos required to ever afford this
                if (State.ClayRobots == 0)
                    return false;

                // Don't need anymore Obs robots
                if (State.ObsRobots > Blueprint.MaxObs)
                    return false;

                // Can never afford anyways
                if(Blueprint.ObsRobot.CanNeverAfford(State, TimeLeft))
                {
                    return false;
                }

                return true;
            }

            public bool BuildClay()
            {
                // Already building
                if (State.BuildClay)
                    return true;

                // Saving up for something else
                if (Saving())
                    return false;

                // Don't need anymore Obs robots
                if (State.ClayRobots > Blueprint.MaxClay)
                    return false;

                // Can never afford anyways
                if (Blueprint.ClayRobot.CanNeverAfford(State, TimeLeft))
                {
                    return false;
                }

                return true;
            }

            public bool BuildOre()
            {
                // Already building
                if (State.BuildOre)
                    return true;

                // Saving up for something else
                if (Saving())
                    return false;

                // Don't need anymore Obs robots
                if (State.OreRobots > Blueprint.MaxOre)
                    return false;

                return true;
            }

            public bool CanAffordObs()
            {
                return Blueprint.ObsRobot.CanAfford(State);
            }

            public bool CanAffordClay()
            {
                return Blueprint.ClayRobot.CanAfford(State);
            }

            public bool CanAffordOre()
            {
                return Blueprint.OreRobot.CanAfford(State);
            }


        }

        public class State
        {
            public Blueprint Blueprint { get; set; }

            public State Copy()
            {
                return new State()
                {
                    Ore = Ore,
                    Clay = Clay,
                    Obs = Obs,
                    Geode = Geode,
                    OreRobots = OreRobots,
                    ClayRobots = ClayRobots,
                    ObsRobots = ObsRobots,
                    GeodeRobots = GeodeRobots,
                    Blueprint = Blueprint
                };
            }

            public void Mine()
            {
                Ore += OreRobots;
                Clay += ClayRobots;
                Obs += ObsRobots;
                Geode += GeodeRobots;
            }

            public bool Build()
            {
                if (OreRobotsBuilding > 0)
                {
                    OreRobots += OreRobotsBuilding;
                    BuildOre = false;
                    OreRobotsBuilding = 0;
                    Blueprint.OreRobot.UpdateResource(this);

                    return true;
                }

                else if (ClayRobotsBuilding > 0)
                {
                    ClayRobots += ClayRobotsBuilding;
                    BuildClay = false;
                    ClayRobotsBuilding = 0;
                    Blueprint.ClayRobot.UpdateResource(this);

                    return true;
                }

                else if (ObsRobotsBuilding > 0)
                {
                    ObsRobots += ObsRobotsBuilding;
                    BuildObs = false;
                    ObsRobotsBuilding = 0;
                    Blueprint.ObsRobot.UpdateResource(this);

                    return true;
                }

                else if (GeodeRobotsBuilding > 0)
                {
                    GeodeRobots += GeodeRobotsBuilding;
                    BuildGeode = false;
                    GeodeRobotsBuilding = 0;
                    Blueprint.GeodeRobot.UpdateResource(this);

                    return true;
                }

                // Didn't build  anything
                return false;
            }

            public int Ore { get; set; }
            public int Clay { get; set; }
            public int Obs { get; set; }
            public int Geode
            {
                get
                {
                    return geode;
                }
                set
                {   
                    geode = value;
                }
            }
            private int geode;

            public int OreRobots { get; set; }
            public int ClayRobots { get; set; }
            public int ObsRobots { get; set; }
            public int GeodeRobots
            {
                get
                {
                    return _geodeRobots;
                }
                set
                {
                    _geodeRobots = value;
                }
            }
            private int _geodeRobots;

            public int OreRobotsBuilding { get; set; }
            public int ClayRobotsBuilding { get; set; }
            public int ObsRobotsBuilding { get; set; }
            public int GeodeRobotsBuilding { get; set; }

            public bool BuildOre { get; set; }
            public bool BuildClay { get; set; }
            public bool BuildObs { get; set; }
            public bool BuildGeode { get; set; }
        }

        public class Blueprint
        {
            public Blueprint(int id, Cost oreRobot, Cost clayRobot, Cost obsRobot, Cost geodeRobot)
            {
                Id = id;
                OreRobot = oreRobot;
                ClayRobot = clayRobot;
                ObsRobot = obsRobot;
                GeodeRobot = geodeRobot;

                int[] oreArray = { oreRobot.Ore, clayRobot.Ore, obsRobot.Ore, geodeRobot.Ore };
                MaxOre = oreArray.Max();

                int[] clayArray = { clayRobot.Clay, clayRobot.Clay, obsRobot.Clay, geodeRobot.Clay };
                MaxClay = clayArray.Max();

                int[] obsArray = { obsRobot.Obs, obsRobot.Obs, obsRobot.Obs, geodeRobot.Obs };
                MaxObs = obsArray.Max();
            }

            public int Id { get; set; }

            public int MaxOre { get; set; }
            public int MaxClay { get; set; }
            public int MaxObs { get; set; }

            public Cost OreRobot { get; set; }
            public Cost ClayRobot { get; set; }
            public Cost ObsRobot { get; set; }
            public Cost GeodeRobot { get; set; }
        }

        public class Cost
        {
            public int Ore { get; set; } = 0;
            public int Clay { get; set; } = 0;
            public int Obs { get; set; } = 0;

            public void UpdateResource(State state)
            {
                state.Ore -= Ore;
                state.Clay -= Clay;
                state.Obs -= Obs;
            }

            public bool CanAfford(State state)
            {
                return Ore <= state.Ore && Clay <= state.Clay && Obs <= state.Obs; 
            }

            public bool CanNeverAfford(State state, int TimeLeft)
            {
                var ore = (state.OreRobots * TimeLeft) + state.Ore;
                var clay = (state.ClayRobots * TimeLeft) + state.Clay;
                var obs = (state.ObsRobots * TimeLeft) + state.Obs;

                return (Ore > ore) && (Clay > clay) && (Obs > obs);
            }
        }
      
    }
}
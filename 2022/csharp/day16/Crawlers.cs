﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day16
{
    public class Crawlers
    {
        public Crawlers(Graph graph, long depth = 0)
        {
            Graph = graph;

            Human = new Crawler(depth, this, 1);
            Elephant = new Crawler(depth, this, 2);

        }

        public long StartCrawl()
        {
            var state = new CrawlerState() 
            { 
                MaxDepth = Graph.MaxDepth, 
                Graph = Graph, 
                Depth = 0,
                ElephantDepth = 0,
                HumanDepth = 0
            };

            state.CurrentElephantNode = Graph.Root;
            state.CurrentHumanNode = Graph.Root;
            
            // NEed to setup initial path....

            return Crawl(state);
        }

        public long Crawl(CrawlerState crawlerState)
        {
            crawlerState.UpdatePressureReleased();
            var maxPressure = crawlerState.PressureReleased;

            // At max depth
            if (crawlerState.AtMaxDepth)
            {
                return crawlerState.PressureReleased;
            }

            if (crawlerState.AllValvesOpen)
            {
                crawlerState.Depth += 1;
                return Crawl(crawlerState);
            }

            // Open Valves if ready
            /*if (crawlerState.HumanReady && crawlerState.NextHumanNode is not null)
            {
                crawlerState.NextHumanNode?.OpenValve(crawlerState);
                crawlerState.CurrentHumanNode = crawlerState.NextHumanNode;
                crawlerState.HumanOpeningValve = true;
            }*/

            if (crawlerState.ElephantReadyToOpenValve && crawlerState.NextElephantNode is not null)
            {
                crawlerState.ElephantOpeningValve = true;
                crawlerState.NextElephantNode?.OpenValve(crawlerState);
                crawlerState.CurrentElephantNode = crawlerState.NextElephantNode;
            }

            // Human ready
            if (crawlerState.HumanReadyForNextNode)
            {
                var humanNodes = crawlerState.CurrentHumanNode.NodesToCrawl(crawlerState);
                foreach (var humanNode in humanNodes)
                {
                    // Both ready                    
                    if (crawlerState.ElephantReadyForNextNode)
                    {
                        var elephantNodes = crawlerState.CurrentElephantNode.NodesToCrawl(crawlerState);
                        crawlerState.ElephantOpeningValve = false;
                        foreach (var elephantNode in elephantNodes.Where(node => node.Name != humanNode.Name))
                        {
                            var state = crawlerState.Copy();
                            state.Depth += 1;
                            state.NextHumanNode = humanNode;
                            state.UpdateHumanDepth();
                            state.NextElephantNode = elephantNode;
                            state.UpdateElephantDepth();

                            var pressure = Crawl(state);
                            if (pressure > maxPressure)
                                maxPressure = pressure;
                        }
                    }
                    // Only Human ready
                    else
                    {
                        var state = crawlerState.Copy();

                        state.Depth += 1;
                        state.NextHumanNode = humanNode;
                        state.UpdateHumanDepth();

                        var pressure = Crawl(state);
                        if (pressure > maxPressure)
                            maxPressure = pressure;
                    }
                }
            }
            // Only Elephant is ready
            else if (crawlerState.ElephantReadyForNextNode)
            {
                var elephantNodes = crawlerState.CurrentElephantNode.NodesToCrawl(crawlerState);
                foreach (var elephantNode in elephantNodes)
                {
                    var state = crawlerState.Copy();
                    state.NextElephantNode = elephantNode;
                    state.UpdateElephantDepth();
                    state.Depth += 1;

                    var pressure = Crawl(state);
                    if (pressure > maxPressure)
                        maxPressure = pressure;
                }
            }

            // At max depth
            if (crawlerState.AtMaxDepth)
            {
                return crawlerState.PressureReleased;
            }

            var cstate = crawlerState.Copy();
            cstate.Depth += 1;
            var pres = Crawl(cstate);
            if (pres > maxPressure)
                maxPressure = pres;

            return maxPressure;
        }

            /// <summary>
            ///  Need to make this recursive somehow
            /// </summary>
            /// <returns></returns>
        public long FindBestPressure()
        {
            long maxPressure = 0;
            var root = Graph.Root;
            var state = new CrawlerState();
            var humanNodes = root.NodesToCrawl(state);
            foreach (var humanNode in humanNodes)
            {
                var elephantNodes = root.NodesToCrawl(state);
                foreach(var elephantNode in elephantNodes)
                {
                    var depth = 0;
                    var crawlState = state.Copy();
                    while (depth <= Graph.MaxDepth)
                    { 
                        // Update Pressure
                        crawlState.UpdatePressureReleased();

                        // Is Human ready to move again?
                        if (Human.IsReady(depth))
                        {
                            var nextHumanNode = humanNode.MultiCrawl(state);
                        }

                        // Is Elephant ready to move again?
                        if (Elephant.IsReady(depth))
                        {
                            var nextElephantNode = elephantNode.MultiCrawl(state);
                        }
                    }
                    if (crawlState.PressureReleased > maxPressure)
                        maxPressure = crawlState.PressureReleased;
                       
                }
            }

            return maxPressure;
        }

        public Graph Graph { get; set; }

        public Crawler Human { get; set; }

        public Crawler Elephant { get; set; }

        public bool AllCrawlersReady()
        {
            return Human.Depth == Elephant.Depth;
        }
    }

    public class CrawlerState
    {
        public Graph Graph { get; set; }

        public List<Node> ValvesOn { get; set; } = new List<Node>();

        public bool AllValvesOpen
        {
            get
            {
                return ValvesOn.Count == Graph.NonZeroFlowRateNodes.Count;
            }
        }

        public CrawlerState Copy()
        {
            return new CrawlerState()
            {
                ValvesOn = ValvesOn.ToList(),
                PressureReleased = PressureReleased,
                _pressurePerTurn = PressurePerTurn,
                Depth = Depth,
                ElephantDepth = ElephantDepth,
                HumanDepth = HumanDepth,
                MaxDepth = MaxDepth,
                Graph = Graph,
                CurrentHumanNode = CurrentHumanNode,
                NextHumanNode = NextHumanNode,
                CurrentElephantNode = CurrentElephantNode,
                NextElephantNode= NextElephantNode
            };
        }

        public long JumpToDepth
        {
            get
            {
                return ElephantDepth > HumanDepth ? ElephantDepth : HumanDepth;
            }
        }

        public Node? CurrentElephantNode { get; set; }
        public Node? NextElephantNode { get; set; }
        public long ElephantDepth { get; set; } = 0;
        public bool ElephantReadyToOpenValve
        {
            get
            {
                return ElephantDepth == Depth;
            }
        }
  
        public bool ElephantReadyForNextNode
        {
            get
            {
                var ready =  (ElephantDepth <= Depth) && !ElephantOpeningValve;

                // IF ready, reset valve opening state
                if(ready)
                {
                    ElephantOpeningValve = false;
                }

                return ready;
            }
        }
        public bool ElephantOpeningValve { get; set; }
        public bool HumanOpeningValve { get; set; }
        public void UpdateElephantDepth()
        {
            ElephantDepth = Depth + CurrentElephantNode?.Distance[NextElephantNode?.Name ?? ""] ?? 0;
        }

        public Node? CurrentHumanNode { get; set; }
        public Node? NextHumanNode { get; set; }
        public long HumanDepth { get; set; } = 0;
        public bool HumanReadyToOpenValve
        {
            get
            {
                return false; // HumanDepth == Depth;
            }
        }
        public bool HumanReadyForNextNode
        {
            get
            {
                return false; // (HumanDepth == Depth) && !HumanOpeningValve;
            }
        }
        public void UpdateHumanDepth()
        {
            HumanDepth += CurrentHumanNode?.Distance[NextHumanNode?.Name ?? ""] ?? 0;
        }

        public long PressureReleased { get; set; } = 0;

        public void UpdatePressureReleased(long time = 1)
        {
            // Can do multiple updates at once
            PressureReleased += PressurePerTurn * time;
        }

        public void UpdatePressureRate()
        {
            _pressurePerTurn = ValvesOn.Sum(node => node.Valve.FlowRate);
        }

        public long PressurePerTurn
        {
            get
            {
                return _pressurePerTurn;
            }
        }

        private long _pressurePerTurn = 0;

        public long Depth { get; set; } = 0;

        public long MaxDepth { get; set; }

        public bool AtMaxDepth
        {
            get
            {
                var maxd = Depth == MaxDepth;
                if(Depth > MaxDepth)
                {
                    var what = "what";
                }
                return maxd;
            }
        }

        public long TimeRemaining
        {
            get
            {
                return MaxDepth - Depth;
            }
        }
    }

    public class Crawler
    {
        public Crawler(long depth, Crawlers crawlers, long id)
        {
            Depth = depth;
            Crawlers = crawlers;
            Id = id;
        }

        public bool IsReady(long depth)
        {
            return depth == Depth;
        }

        public long Id { get; set; }

        public long Depth { get; set; } = 0;

        public Node? CurrentNode { get; set; }

        public Node? NextNode { get; set; }

        public Crawlers Crawlers { get; set; }
    }
}

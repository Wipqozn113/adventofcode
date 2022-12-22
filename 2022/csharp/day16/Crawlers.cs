using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day16
{
    public class Crawlers
    {
        public Crawler(Graph graph, long depth = 0)
        {
            Graph = graph;

            Human = new Crawler(depth, this, 1));
            Elephant = new Crawler(depth, this, 2);

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
        public List<Node> ValvesOn { get; set; } = new List<Node>();

        public CrawlerState Copy()
        {
            return new CrawlerState()
            {
                ValvesOn = ValvesOn.ToList(),
                PressureReleased = PressureReleased,
                _pressurePerTurn = PressurePerTurn,
                Depth = Depth,
                MaxDepth = MaxDepth
            };
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

    public class Crawler
    {
        public Crawler(long depth, Crawlers crawlerStates, long id)
        {
            Depth = depth;
            CrawlerStates = crawlerStates;
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

        public Node Crawl(CrawlerState state)
        {

        }
    }
}

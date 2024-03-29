﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day16_2
{
    public static class Utils
    {
        public static List<Node> CreateNodes(bool test = false)
        {
            // Valve JC has flow rate=0; tunnels lead to valves XS, XK
            List<Node> nodes = new List<Node>();
            var data = new List<string>();
            if (test)
            {
                data = TestData();
            }
            else
            {
                data = Data();
            }

            foreach (var line in data)
            {
                var splitLine = line.Trim().Split(';');
                var valveInfo = splitLine[0].Split('=');
                var name = valveInfo[0].Split(" ")[1].Trim();
                var flowRate = long.Parse(valveInfo[1].Trim());
                var valves = splitLine[1].Trim()
                    .Replace("tunnels lead to valves", "").Replace("tunnel leads to valve", "")
                    .Trim().Split(",");

                var children = new List<string>();
                foreach (var valv in valves)
                {
                    children.Add(valv.Trim());
                }

                var valve = new Valve(name, flowRate, children);
                var node = new Node()
                {
                    Valve = valve,
                };
                nodes.Add(node);
            }

            return nodes;
        }

        public static void PopulateChildren(List<Node> nodes)
        {
            foreach (Node node in nodes)
            {
                node.Children = nodes.Where(n => node.Valve.Children.Contains(n.Name)).ToList();
            }
        }

        public static Graph CreateGraph(List<Node> nodes, long depth = 30, string rootName = "AA")
        {
            var root = nodes.Where(x => x.Name == rootName).Single();
            var graph = new Graph(root, depth);
            graph.Nodes = nodes.ToList();
            graph.NonZeroFlowRateNodes = nodes.Where(x => x.Valve.FlowRate > 0).ToList();
            graph.FindShortestPaths();
            graph.Nodes.ForEach(node => node.Graph = graph);

            return graph;
        }

        public static List<string> TestData()
        {
            var data = new List<string>()
            {
                "Valve AA has flow rate=0; tunnels lead to valves DD, II, BB",
                "Valve BB has flow rate = 13; tunnels lead to valves CC, AA",
                "Valve CC has flow rate = 2; tunnels lead to valves DD, BB",
                "Valve DD has flow rate = 20; tunnels lead to valves CC, AA, EE",
                "Valve EE has flow rate = 3; tunnels lead to valves FF, DD",
                "Valve FF has flow rate = 0; tunnels lead to valves EE, GG",
                "Valve GG has flow rate = 0; tunnels lead to valves FF, HH",
                "Valve HH has flow rate = 22; tunnel leads to valve GG",
                "Valve II has flow rate = 0; tunnels lead to valves AA, JJ",
                "Valve JJ has flow rate = 21; tunnel leads to valve II"
            };

            return data;
        }

        public static List<string> Data()
        {
            var data = new List<string>()
            {
                "Valve JC has flow rate=0; tunnels lead to valves XS, XK",
                "Valve TK has flow rate = 0; tunnels lead to valves AA, RA",
                "Valve PY has flow rate = 0; tunnels lead to valves UB, MW",
                "Valve XK has flow rate = 15; tunnels lead to valves CD, JC, TP, UE",
                "Valve EI has flow rate = 6; tunnels lead to valves UB, HD",
                "Valve OV has flow rate = 0; tunnels lead to valves QC, WK",
                "Valve CX has flow rate = 3; tunnels lead to valves ZN, AM, OE, YS, QE",
                "Valve YS has flow rate = 0; tunnels lead to valves QC, CX",
                "Valve DC has flow rate = 0; tunnels lead to valves UE, NM",
                "Valve EA has flow rate = 5; tunnels lead to valves QE, XO, GX",
                "Valve VE has flow rate = 0; tunnels lead to valves YH, NM",
                "Valve RN has flow rate = 0; tunnels lead to valves WK, NU",
                "Valve VJ has flow rate = 0; tunnels lead to valves QC, CS",
                "Valve HD has flow rate = 0; tunnels lead to valves JI, EI",
                "Valve UB has flow rate = 0; tunnels lead to valves EI, PY",
                "Valve XS has flow rate = 17; tunnels lead to valves JC, CE",
                "Valve AM has flow rate = 0; tunnels lead to valves NU, CX",
                "Valve GX has flow rate = 0; tunnels lead to valves EA, RA",
                "Valve UI has flow rate = 0; tunnels lead to valves NC, ZG",
                "Valve NM has flow rate = 22; tunnels lead to valves DC, VE, DX",
                "Valve CE has flow rate = 0; tunnels lead to valves XS, WD",
                "Valve NC has flow rate = 25; tunnels lead to valves UI, VQ",
                "Valve TP has flow rate = 0; tunnels lead to valves XK, RA",
                "Valve ZN has flow rate = 0; tunnels lead to valves CX, XI",
                "Valve CS has flow rate = 0; tunnels lead to valves AA, VJ",
                "Valve MW has flow rate = 23; tunnel leads to valve PY",
                "Valve AA has flow rate = 0; tunnels lead to valves TK, WC, CS, AL, MS",
                "Valve RA has flow rate = 4; tunnels lead to valves WD, TP, TK, GX, JI",
                "Valve NU has flow rate = 10; tunnels lead to valves DU, AM, RN, HS, AL",
                "Valve QE has flow rate = 0; tunnels lead to valves CX, EA",
                "Valve AH has flow rate = 0; tunnels lead to valves WK, MS",
                "Valve YH has flow rate = 20; tunnels lead to valves VE, CD",
                "Valve SH has flow rate = 0; tunnels lead to valves DU, ZG",
                "Valve XO has flow rate = 0; tunnels lead to valves EA, ZG",
                "Valve JI has flow rate = 0; tunnels lead to valves RA, HD",
                "Valve XI has flow rate = 0; tunnels lead to valves WK, ZN",
                "Valve HS has flow rate = 0; tunnels lead to valves QC, NU",
                "Valve VQ has flow rate = 0; tunnels lead to valves WK, NC",
                "Valve UE has flow rate = 0; tunnels lead to valves XK, DC",
                "Valve YP has flow rate = 19; tunnel leads to valve DX",
                "Valve WD has flow rate = 0; tunnels lead to valves CE, RA",
                "Valve DX has flow rate = 0; tunnels lead to valves NM, YP",
                "Valve ZG has flow rate = 11; tunnels lead to valves UI, SH, XO",
                "Valve MS has flow rate = 0; tunnels lead to valves AA, AH",
                "Valve QC has flow rate = 9; tunnels lead to valves HS, VJ, OV, YS",
                "Valve DU has flow rate = 0; tunnels lead to valves NU, SH",
                "Valve WK has flow rate = 12; tunnels lead to valves RN, XI, VQ, OV, AH",
                "Valve CD has flow rate = 0; tunnels lead to valves YH, XK",
                "Valve AL has flow rate = 0; tunnels lead to valves AA, NU",
                "Valve WC has flow rate = 0; tunnels lead to valves OE, AA"
            };

            return data;
        }
    }
}

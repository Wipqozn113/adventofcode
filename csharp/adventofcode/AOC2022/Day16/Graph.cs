using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2022.Day16
{
    public class Valve
    {
        public Valve(string name, int flowrate, List<string> childrenNames)
        {
            Name = name;
            Flowrate = flowrate;
            ChildrenNames = childrenNames;
        }

        public string Name { get; set; }

        public int Flowrate { get; set; }

        public List<string> ChildrenNames { get; set; }

        public List<Valve> Children { get; set; } = new List<Valve>();

        public Dictionary<Valve, double> DistanceTo { get; set; } = new Dictionary<Valve, double>();
    }

    public class Graph
    {
        public Graph(Valve root)
        {
            Root = root;
        }

        private Valve Root { get; set; }

        private List<Valve> Valves { get; set; } = new List<Valve>();

        public void AddValve(Valve valve)
        {
            Valves.Add(valve);
        }

        public void PopulateChildren()
        {
            foreach(var valve in Valves)
            {
                var children = Valves.Where(x => valve.ChildrenNames.Contains(x.Name)).ToList();
                valve.Children = children;
            }
        }

        public void Dijkstra(Valve source)
        {
            
        }

        public void DijkstraAll()
        {
            foreach(var valve in Valves)
            {
                Dijkstra(valve);
            }
        }
    }
}

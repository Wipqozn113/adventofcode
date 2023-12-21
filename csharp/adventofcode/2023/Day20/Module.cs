using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day20
{
    public abstract class Module : IModule
    {
        public Module(string name, Graph graph, List<string> childrenNames)
        {
            Name = name;    
            Graph = graph;
            ChildrenNames = childrenNames;
        }

        public abstract void RecievePulse(Pulse pulse, IModule module);

        public abstract void SendPulse();

        public  void PopulateChildren(List<IModule> modules)
        {
            foreach (var childName in ChildrenNames)
            {
                var child = modules.Where(x => x.Name == childName).First();
                var edge = new Edge(this, child);
                Output.Add(edge);
                if(child is ConjunctionModule)
                {
                    var cj = (ConjunctionModule)child;
                    cj.InitMemory(Name);
                }
            }
        }

        public List<string> ChildrenNames { get; set; }

        public List<Edge> Input { get; set; } = new List<Edge>();

        public List<Edge> Output { get; set; } = new List<Edge>();

        public Graph Graph { get; set; }

        public string Name { get; set; }
    }

    public enum Pulse
    {
        Low,
        High
    }

    public class Edge
    {
        public Edge(IModule parent, IModule child)
        {
            Parent = parent;
            Child = child;
        }

        public IModule Parent { get; set; }

        public IModule Child { get; set; }
    }
}

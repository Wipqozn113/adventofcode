using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCUtils.Graphs
{
    public class Node
    {
        public Node(string name, List<string> childNames) 
        {
            Name = name;
            ChildNames = childNames;
        }

        public string Name { get; set; }

        public List<string> ChildNames { get; set; }

        public List<Node> Children { get; set; } = new List<Node>();

        public void AddChildren(List<Node> children)
        {
            Children.AddRange(children);
        }

        public void AddChild(Node child)
        {
            Children.Add(child);
        }
    }
}

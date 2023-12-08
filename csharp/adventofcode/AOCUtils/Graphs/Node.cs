using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCUtils.Graphs
{
    public class Node<T> where T : IComparable
    {
        public Node(string name, List<string> childNames) 
        {
            Name = name;
            ChildNames = childNames;
        }

        public string Name { get; set; }

        public List<string> ChildNames { get; set; }

        public List<Node<T>> Children { get; set; } = new List<Node<T>>();

        public void AddChildren(List<Node<T>> children)
        {
            Children.AddRange(children);
        }

        public void AddChild(Node<T> child)
        {
            Children.Add(child);
        }
    }
}

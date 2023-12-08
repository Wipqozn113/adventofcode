using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCUtils.Graphs
{
    public class Graph<T, V> where T : Node<V> where V : IComparable
    {
        public Graph(T head)
        {
            Nodes = new List<Node<V>>();
            Head = head;
            Nodes.Add(head);
        }

        public List<Node<V>> Nodes { get; set; }

        public Node<V> Head { get; set; }

        public void PopulateChildren()
        {
            foreach(var node in Nodes)
            {
                var children = Nodes.Where(x => node.ChildNames.Contains(x.Name)).ToList();
                node.AddChildren(children);
            }
        }
    }
}

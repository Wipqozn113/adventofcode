using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCUtils.Graphs
{
    public class Graph<T> where T : Node
    {
        public Graph(T head)
        {
            Nodes = new List<T>();
            Head = head;
            Nodes.Add(head);
        }

        public List<T> Nodes { get; set; }

        public Node Head { get; set; }

        public void PopulateChildren()
        {
            foreach(var node in Nodes)
            {
                
            }
        }
    }
}

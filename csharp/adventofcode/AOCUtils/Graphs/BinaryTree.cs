using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCUtils.Graphs
{
    public class BinaryTree
    {
        public BinaryTree(string headName, long headValue)
        {
            Head = new Node(headName, headValue);
        }

        private Node Head { get; set; }

        private List<Node> Nodes = new List<Node>();

        public void Insert(string name, long value)
        {
            if(ContainsNode(name))
                throw new ArgumentException($"Tree already contains node with name {name}. Names must be unique.")

            var node = new Node(name, value);
            Head.AddChild(node);
        }

        public bool ContainsNode(long value)
        {
            return Nodes.Where(x => x.Value == value).Any();    
        }

        public bool ContainsNode(string name)
        {
            return Nodes.Where(x => x.Name == name).Any();
        }

        public long DistanceToNode(string name)
        {
            return DistanceBetweenNodes(Head.Name, name);
        }

        /// <summary>
        /// Finds the distance between two nodes. 
        /// Returns -1 if either of the nodes isn't in the tree, 
        /// or if END is unreachable from START.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public long DistanceBetweenNodes(string start, string end)
        {
            // Looking at itself
            if (start == end)
                return 0;

            var startNode = Nodes.Where(x => x.Name == start).FirstOrDefault();
            var endNode = Nodes.Where(x => x.Name == end).FirstOrDefault();

            // One of these nodes isn't in the tree
            if (startNode is null || endNode is null)
                return -1;

            long distance = 0;
            var node = startNode;
            while(node is not null)
            {
                if (node == endNode)
                    return distance;

                distance++;

                if (endNode.Value > node.Value)
                    node = node.RightChild;
                else
                    node = node.LeftChild;
            }

            return -1;

        }

        private class Node
        {
            public Node(string name, long value)
            {
                Name = name;
                Value = value;
            }

            public string Name { get; set; }

            public long Value { get; set; }

            public Node? LeftChild { get; set; }

            public Node? RightChild { get; set; }

            public void AddChild(Node node)
            {
                if(node.Value <= Value)
                {
                    if(LeftChild is null)
                        LeftChild = node;
                    else
                        LeftChild.AddChild(node);
                }
                else
                {
                    if(RightChild is null)
                        RightChild = node;
                    else
                        RightChild.AddChild(node);
                }
            }
        }
    }
}

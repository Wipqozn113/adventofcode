using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day8
{
    public class LRNode
    {
        public LRNode(string name, string leftChild, string rightChild)
        {
            Name = name;    
            LeftChildName = leftChild;
            RightChildName = rightChild;
        }

        public string Name { get; private set; }

        public string LeftChildName { get; private set; }
        public string RightChildName { get; private set; }

        public LRNode? LeftChild { get; set; }
        public LRNode? RightChild { get; set;}

    }
}

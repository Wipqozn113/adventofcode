using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AOCUtils.MathUtils;

namespace AOC2023.Day8
{
    public class LRGraph
    {
        public LRGraph(LRNode head, string instructions) 
        {
            Head = head;
            Instructions = instructions;
            Nodes = new List<LRNode>()
            {
                Head
            };
        }

        public LRNode Head;

        public List<LRNode> Nodes;
        public string Instructions;

        public void PopulateChildren()
        {
            foreach(LRNode node in Nodes)
            {
                node.LeftChild = Nodes.Where(x => x.Name == node.LeftChildName).SingleOrDefault();
                node.RightChild = Nodes.Where(x => x.Name == node.RightChildName).SingleOrDefault();
            }
        }

        public int FindSteps(string start, string end)
        {
            var startNode = Nodes.Where(x => x.Name == start).SingleOrDefault();
            var endNode = Nodes.Where(x => x.Name ==  end).SingleOrDefault();
            var steps = 0;
            var currentNode = startNode;
            while (currentNode != endNode)
            {
                currentNode = Instructions[steps % Instructions.Length] == 'L' ? currentNode?.LeftChild : currentNode?.RightChild;
                steps++;
            }

            return steps;
        }

        public long FindGhostSteps(char start, char end)
        {
            var currentNodes = Nodes.Where(x => x.Name.EndsWith(start)).ToList();
            var firstEnds = new List<int>();
            int steps = 0;
            while(currentNodes.Any())
            {
                for(var i = 0; i < currentNodes.Count; i++)
                {
                    var ins = steps % Instructions.Length;
                    currentNodes[i] = Instructions[ins] == 'L' ? currentNodes[i].LeftChild : currentNodes[i].RightChild;
                }

                steps++;

                var foundEndNode = currentNodes.Where(x => x.Name.EndsWith(end)).ToList();
                foreach(var node in foundEndNode)
                {
                    firstEnds.Add(steps);
                    currentNodes.Remove(node);
                    //Console.WriteLine(steps.ToString());
                }

            }

            foreach(var step in firstEnds)
            {
                Console.WriteLine(step.ToString());
            }
            return Core.LCM(firstEnds);
        }

        public long FindGhostStepsBruteForce(char start, char end)
        {
            var currentNodes = Nodes.Where(x => x.Name.EndsWith(start)).ToList();
            long steps = 0;
            while (true)
            {
                for (var i = 0; i < currentNodes.Count; i++)
                {
                    var ins = steps % (long)Instructions.Length;
                    currentNodes[i] = Instructions[(int)ins] == 'L' ? currentNodes[i].LeftChild : currentNodes[i].RightChild;
                }

                steps++;

                if (currentNodes.All(x => x.Name.EndsWith(end)))
                    break;

            }

            return steps;
        }
    }
}

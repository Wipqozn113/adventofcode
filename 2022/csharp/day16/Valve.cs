using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day16
{
    public class Valve
    {
        public Valve(string name, long flowRate, List<string> children)
        {
            Name = name;
            FlowRate = flowRate;
            Children = children;
        }

        public string Name { get; set; } = "";
        public long FlowRate { get; set; }
        public List<string> Children { get; set; }
    }
}

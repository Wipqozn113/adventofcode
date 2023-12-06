using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day5
{
    abstract public class FarmItem
    {
        public FarmItem(int startNum, int range) 
        {
            StartNum = startNum;
            EndNum = startNum + range;
        }

        public int StartNum { get; set; }
        public int EndNum { get; set; }

    }

    public class Seed : FarmItem
    {
        public Seed(int startNum, int range) : base(startNum, range) { }
    }

    public class Soil : FarmItem
    {
        public Soil(int startNum, int range) : base(startNum, range) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day5
{
    abstract public class FarmItem
    {
        public FarmItem() { }

        public FarmItem(int startNum, int range) 
        {
            StartNum = startNum;
            EndNum = startNum + range;
        }

        public int StartNum { get; set; }
        public int EndNum { get; set; }

        public bool CanConvert<T>(int startNum, int endNum) where T : FarmItem, new()
        {
            return (StartNum >= startNum && (StartNum <= endNum || EndNum <= endNum)) ||
               (StartNum < startNum && EndNum >= startNum);
        }

        public List<T> Split<T>(int startNum, int endNum) where T : FarmItem, new()
        {
            var newitems = new List<T>();

            if (StartNum < startNum && EndNum >= startNum)
            {
                newitems.Add(new T() { StartNum = StartNum, EndNum = startNum - 1 });
                StartNum = startNum;
            }

            if (EndNum > endNum && startNum <= EndNum)
            {
                newitems.Add(new T() { StartNum = endNum + 1, EndNum = EndNum });
                EndNum = endNum;
            }

            return newitems;
        }

        public T Convert<T>() where T: FarmItem, new()
        {
            return new T()
            {
                StartNum = StartNum,
                EndNum = EndNum
            };
        }

        public T Convert<T>(int sourceStart, int destStart) where T : FarmItem, new() 
        {
            var diff = sourceStart - destStart;
            var t = new T()
            {
                StartNum = StartNum - diff,
                EndNum = EndNum - diff
            };

            return t;
        }

    }

    public class Seed : FarmItem
    {
        public Seed() : base() { }

        public Seed(int startNum, int range) : base(startNum, range) { }

        public bool CanConvert(int startNum, int endNum)
        {
            return CanConvert<Soil>(startNum, endNum);
        }

        public List<Seed> Split(int startNum, int endNum)
        {
            return Split<Seed>(startNum, endNum);
        }

        public Soil Convert()
        {
            return Convert<Soil>();
        }

        public Soil Convert(int sourceStart, int destStart)
        {
            return Convert<Soil>(sourceStart, destStart);
        }
    }

    public class Soil : FarmItem
    {
        public Soil() : base() { }

        public Soil(int startNum, int range) : base(startNum, range) { }
    }
}

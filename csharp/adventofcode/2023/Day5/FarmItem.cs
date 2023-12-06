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

        public FarmItem(long startNum, long range) 
        {
            StartNum = startNum;
            EndNum = startNum + range;
        }

        public long StartNum { get; set; }
        public long EndNum { get; set; }

        public bool CanConvert<T>(long startNum, long endNum) where T : FarmItem, new()
        {
            return (StartNum >= startNum && (StartNum <= endNum || EndNum <= endNum)) ||
               (StartNum < startNum && EndNum >= startNum);
        }

        public List<T> Split<T>(long startNum, long endNum) where T : FarmItem, new()
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

        public T Convert<T>(long sourceStart, long destStart) where T : FarmItem, new() 
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

        public Seed(long startNum, long range) : base(startNum, range) { }
    }

    public class Soil : FarmItem
    {
        public Soil() : base() { }

        public Soil(long startNum, long range) : base(startNum, range) { }
    }

    public class Fertilizer : FarmItem
    {
        public Fertilizer() : base() { }

        public Fertilizer(long startNum, long range) : base(startNum, range) { }
    }

    public class Water : FarmItem
    {
        public Water() : base() { }

        public Water(long startNum, long range) : base(startNum, range) { }
    }

    public class Light : FarmItem
    {
        public Light() : base() { }

        public Light(long startNum, long range) : base(startNum, range) { }
    }

    public class Temperature : FarmItem
    {
        public Temperature() : base() { }

        public Temperature(long startNum, long range) : base(startNum, range) { }
    }

    public class Humdity : FarmItem
    {
        public Humdity() : base() { }

        public Humdity(long startNum, long range) : base(startNum, range) { }
    }

    public class Location : FarmItem
    {
        public Location() : base() { }

        public Location(long startNum, long range) : base(startNum, range) { }
    }
}

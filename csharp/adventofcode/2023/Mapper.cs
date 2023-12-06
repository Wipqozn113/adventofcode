using AOC2023;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day5
{
    public class Mapping
    {
        public Mapping(int sourceStart, int destStart, int range) 
        { 
            SourceStart = sourceStart;
            SourceEnd = sourceStart + Range; 
            DestStart = destStart;
            DestEnd = DestStart + Range;
            Range = range;
        }

        public int SourceStart { get; set; }
        public int SourceEnd { get; set; }
        
        public int DestStart { get; set; }
        public int DestEnd { get; set; }
        public int Range { get; set; }
    } 

    public class Mapper
    {
        public List<Mapping> SeedToSoilMappings { get; private set; } = new List<Mapping>();

        public List<Soil> Map(Seed seed)
        {
            var soils = new List<Soil>();
            var seeds = new Queue<Seed>();
            seeds.Enqueue(seed);
            while(seeds.Count > 0) 
            {
                var sd = seeds.Dequeue();
                foreach(var map in SeedToSoilMappings)
                {
                    var newseeds = sd.Split(map.SourceStart, map.SourceEnd);
                    newseeds.ForEach(s => seeds.Enqueue(s));
                    soils.Add(sd.Convert<Soil>(map.SourceStart, map.SourceEnd));
                }
            }


            return soils;
        }
    }
}

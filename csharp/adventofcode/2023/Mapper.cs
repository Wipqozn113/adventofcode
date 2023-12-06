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
            SourceEnd = sourceStart + range; 
            DestStart = destStart;
            DestEnd = DestStart + range;
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
        public List<Mapping> SoilToFertilizerMappings { get; private set; } = new List<Mapping>();

        public void CreateMapping(string line)
        {
            return;
        }

        public void CreateMapping(int sourceStart, int destStart, int range, string type) 
        {
            if(type == "seedtosoil")
            {
                SeedToSoilMappings.Add(new Mapping(sourceStart, destStart, range));
            }
        }

        public List<Soil> Map(Seed seed)
        {
            return Map<Soil, Seed>(seed, SeedToSoilMappings);
        }

        public List<TD> Map<TD, TS>(TS source, List<Mapping> mappings) where TS : FarmItem, new () where TD : FarmItem, new()
        {
            var td = new List<TD>();
            var ts = new Queue<TS>();
            ts.Enqueue(source);
            while (ts.Count > 0)
            {
                var sd = ts.Dequeue();
                foreach (var map in mappings)
                {
                    // Test if this can be converted
                    if (sd is not null && sd.CanConvert<TS>(map.SourceStart, map.SourceEnd))
                    {
                        var newts = sd.Split<TS>(map.SourceStart, map.SourceEnd);
                        newts.ForEach(t => ts.Enqueue(t));
                        td.Add(sd.Convert<TD>(map.SourceStart, map.DestStart));
                        // Converted, so nullify, since we don't need it anymore
                        sd = null;
                        break;
                    }
                }

                // If not null, then it was never converted
                if (sd is not null)
                    td.Add(sd.Convert<TD>());

            }


            return td;
        }

        /*
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
                    // Test if this can be converted
                    if (sd is not null && sd.CanConvert(map.SourceStart, map.SourceEnd))
                    {
                        var newseeds = sd.Split(map.SourceStart, map.SourceEnd);
                        newseeds.ForEach(s => seeds.Enqueue(s));
                        soils.Add(sd.Convert(map.SourceStart, map.DestStart));
                        // Converted, so nullify, since we don't need it anymore
                        sd = null;
                        break;
                    }
                }

                // If not null, then it was never converted
                if (sd is not null)
                    soils.Add(sd.Convert());

            }


            return soils;
        }
        */
    }
}

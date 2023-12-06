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
        public Mapping(long sourceStart, long destStart, long range) 
        { 
            SourceStart = sourceStart;
            SourceEnd = sourceStart + range; 
            DestStart = destStart;
            DestEnd = DestStart + range;
            Range = range;
        }

        public long SourceStart { get; set; }
        public long SourceEnd { get; set; }  
        
        public long DestStart { get; set; }
        public long DestEnd { get; set; }
        public long Range { get; set; }
    } 

    public class Mapper
    {
        public List<Mapping> SeedToSoilMappings { get; private set; } = new List<Mapping>(); 
        public List<Mapping> SoilToFertilizerMappings { get; private set; } = new List<Mapping>();
        public List<Mapping> FertilizerToWaterMappings { get; private set; } = new List<Mapping>();
        public List<Mapping> WaterLightToMappings { get; private set; } = new List<Mapping>();
        public List<Mapping> LightToTemperatureMappings { get; private set; } = new List<Mapping>();
        public List<Mapping> TemperatureToHumdityMappings { get; private set; } = new List<Mapping>();
        public List<Mapping> HumdityToLocationMappings { get; private set; } = new List<Mapping>();

        public void CreateMapping(string line)
        {
            return;
        }

        public void CreateMapping(long sourceStart, long destStart, long range, string type) 
        {
            switch (type)
            {
                case "seed-to-soil":
                    SeedToSoilMappings.Add(new Mapping(sourceStart, destStart, range));
                    break;
                case "soil-to-fertilizer":
                    SoilToFertilizerMappings.Add(new Mapping(sourceStart, destStart, range));
                    break;
                case "fertilizer-to-water":
                    FertilizerToWaterMappings.Add(new Mapping(sourceStart, destStart, range));
                    break;
                case "water-to-light":
                    WaterLightToMappings.Add(new Mapping(sourceStart, destStart, range));
                    break;
                case "light-to-temperature":
                    LightToTemperatureMappings.Add(new Mapping(sourceStart, destStart, range));
                    break;
                case "temperature-to-humidity":
                    TemperatureToHumdityMappings.Add(new Mapping(sourceStart, destStart, range));
                    break;
                case "humidity-to-location":
                    HumdityToLocationMappings.Add(new Mapping(sourceStart, destStart, range));
                    break;
            }
        }

        public List<Soil> Map(Seed seed)
        {
            return Map<Soil, Seed>(seed, SeedToSoilMappings);
        }

        public List<Fertilizer> Map(Soil soil)
        {
            return Map<Fertilizer, Soil>(soil, SoilToFertilizerMappings);
        }

        public List<Water> Map(Fertilizer fertilizer)
        {
            return Map<Water, Fertilizer>(fertilizer, FertilizerToWaterMappings);
        }

        public List<Light> Map(Water water)
        {
            return Map<Light, Water>(water, WaterLightToMappings);
        }
        public List<Temperature> Map(Light light)
        {
            return Map<Temperature, Light>(light, LightToTemperatureMappings);
        }

        public List<Humdity> Map(Temperature temperature)
        {
            return Map<Humdity, Temperature>(temperature, TemperatureToHumdityMappings);
        }

        public List<Location> Map(Humdity humdity)
        {
            return Map<Location, Humdity>(humdity, HumdityToLocationMappings);
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

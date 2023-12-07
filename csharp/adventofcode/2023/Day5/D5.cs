using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day5
{
    public static class D5
    {
        public static void Part2()
        {
            var mapper = new Mapper();
            var seeds = new List<Seed>();
            var filename = "test.txt";

            PopulateFromFile(filename, mapper, seeds);
            var locations = ConvertSeedsToLocations(mapper, seeds);
            var smallest = GetSmallestLocation(locations);
            Console.WriteLine(smallest.ToString());

        }

        private static long GetSmallestLocation(List<Location> locations)
        {
            return locations.Select(x => x.StartNum).Min();
        }

        private static List<Location> ConvertSeedsToLocations(Mapper mapper, List<Seed> seeds)
        {
            var soils = new List<Soil>();
            foreach(var seed in seeds)
            {
                soils.AddRange(mapper.Map(seed));
            }

            var ferts = new List<Fertilizer>();
            foreach (var soil in soils)
            {
                ferts.AddRange(mapper.Map(soil));
            }

            var waters = new List<Water>();
            foreach(var fert in ferts)
            {
                waters.AddRange(mapper.Map(fert));
            }    

            var lights = new List<Light>(); 
            foreach(var water in waters)
            {
                lights.AddRange(mapper.Map(water));
            }

            var temps = new List<Temperature>();
            foreach(var light in lights)
            {
                temps.AddRange(mapper.Map(light));
            }

            var humids = new List<Humdity>();
            foreach(var temp in temps)
            {
                humids.AddRange(mapper.Map(temp));
            }

            var locations = new List<Location>();
            foreach(var humidity in humids)
            {
                locations.AddRange(mapper.Map(humidity));
            }

            return locations;
        }

        private static void PopulateFromFile(string filename, Mapper mapper, List<Seed> seeds)
        {
            // Too lazy to get it to work properly
            string path = "C:\\Users\\Owner\\Development\\adventofcode\\csharp\\adventofcode\\2023\\Day5\\test.txt";
            var lines = File.ReadLines(path);
            var mapType = "";
            foreach (var line in lines)
            {
                if (line.StartsWith("seeds:"))
                {
                    seeds.AddRange(CreateSeeds(line));
                }
                else if (line.Contains("map"))
                {
                    mapType = line.Split(" ").First();
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    UpdateMapper(mapper, mapType, line);
                }
            }
        }

        private static List<Seed> CreateSeeds(string line)
        {
            var seeds = new List<Seed>();

            var ln = line.Split(":")[1];
            var sds = ln.Trim().Split(" ").ToList();

            while(sds.Any())
            {
                var seed = long.Parse(sds[0]);
                var range = long.Parse(sds[1]);
                seeds.Add(new Seed(seed, range));
                sds.RemoveRange(0, 2);
            }

            return seeds;
        }

        private static void UpdateMapper(Mapper mapper, string mapType, string line)
        {
            var vals  = line.Trim().Split(' ');
            var source = long.Parse(vals[1]);
            var dest = long.Parse(vals[0]);
            var range = long.Parse(vals[2]);
            mapper.CreateMapping(source, dest, range, mapType);
        }


    }
}

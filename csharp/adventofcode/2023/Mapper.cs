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
            DestStart = destStart;
            Range = range;
        }

        public int SourceStart { get; private set; }
        public int SourceEnd
        {
            get
            {
                return SourceStart + Range;
            }
        }
        public int DestStart { get; private set; }
        public int DestEnd
        {
            get
            {
                return DestStart + Range;
            }
        }
        public int Range { get; private set; }
    } 

    public class Mapper
    {
        public List<Mapping> SeedToSoilMappings { get; private set; } = new List<Mapping>();

        public List<Soil> Map(Seed seed)
        {
            var soils = new List<Soil>();           



            return soils;
        }
    }
}

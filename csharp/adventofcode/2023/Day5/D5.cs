using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day5
{
    public static class D5
    {
        public static void Part2()
        {
            var mapper = new Mapper();
            var seed1 = new Seed(79, 14);
            var seed2 = new Seed(55, 13);
            var seed3 = new Seed(50, 60);
            mapper.CreateMapping(98, 50, 2, "seedtosoil");
            mapper.CreateMapping(50, 52, 48, "seedtosoil");
            var soils = mapper.Map(seed1);
            soils = mapper.Map(seed3);
            
            Console.WriteLine("Test");
        }
    }
}

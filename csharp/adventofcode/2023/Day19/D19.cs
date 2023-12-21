using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day19
{
    public static class D19
    {
        public static void Part1()
        {
            var factory = CreateFactory(true);
            var val = factory.SumOfAccepted();
            Console.WriteLine("Part 1: " +  val.ToString());
        }

        public static void Part2() 
        {
            var factory = CreateFactory(false);
            factory.CreateTestParts();
            var val = factory.SumOfAccepted();
            Console.WriteLine("Part 2: " + val.ToString());
        }

        public static Factory CreateFactory(bool parseParts)
        {
            var factory = new Factory();
            string path = "C:\\Users\\Owner\\Development\\adventofcode\\csharp\\adventofcode\\2023\\Day19\\test.txt";
            var lines = File.ReadLines(path).ToList();
            var parsingParts = false;
            foreach(var line in lines)
            {
                if(parsingParts && parseParts)
                {
                    factory.AddPart(line.Trim());
                }
                else if(string.IsNullOrWhiteSpace(line))
                {
                    parsingParts = true;
                }
                else
                {
                    factory.AddWorkflow(line.Trim());
                }
            }

            return factory;
        }
    }
}

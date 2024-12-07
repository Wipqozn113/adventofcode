using AOTC2024.Day1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AOTC2024.Day7
{
    public class D7
    {
        public static void Part1()
        {
            var equations = ParseInputFromFile("input.txt");
            long total = 0;
            foreach (var equation in equations)
            {
                if (equation.IsValid())
                    total += equation.Sum;
            }
            Console.WriteLine($"Part 1: {total} ");
        }

        public static void Part2()
        {
            var equations = ParseInputFromFile("input.txt");
            long total = 0;
            foreach (var equation in equations)
            {
                if (equation.IsValid(true))
                    total += equation.Sum;
            }
            Console.WriteLine($"Part 2: {total} ");
        }

        public static List<Equation> ParseInputFromFile(string filename)
        {
            string path = $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}\\Day7\\{filename}";
            var lines = File.ReadLines(path);
            var equations = lines.Select(x =>  new Equation(x)).ToList();
            return equations;
            
        }
    }
}

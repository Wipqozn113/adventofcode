using AOTC2024.Day1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AOTC2024.Day3
{
    public class D3
    {
        public static void Part1()
        {
            var computer = ParseInputFromFile("input.txt");
            var total = computer.Calc();
            Console.WriteLine($"Part 1: {total}");
        }

        public static void Part2()
        {
            var computer = ParseInputFromFile("input.txt");
            var total = computer.CalcFull();
            Console.WriteLine($"Part 2: {total}");
        }

        public static Computer ParseInputFromFile(string filename)
        {
            string path = $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}\\Day3\\{filename}";
            var lines = File.ReadLines(path);
            return new Computer(lines.ToList());
        }
    }
}

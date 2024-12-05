using AOTC2024.Day1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOTC2024.Day5
{
    public class D5
    {
        public static void Part1()
        {
            var printer = ParseInputFromFile("input.txt");
            var total = printer.Calc();
            Console.WriteLine($"Part 1: {total}");
        }

        public static void Part2()
        {
            var printer = ParseInputFromFile("input.txt");
            var total = printer.CalcIncorrectPages();
            Console.WriteLine($"Part 2: {total}");
        }

        public static Printer ParseInputFromFile(string filename)
        {
            string path = $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}\\Day5\\{filename}";
            var lines = File.ReadLines(path).ToList();
            return new Printer(lines);
        }
    }
}

using AOTC2024.Day1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOTC2024.Day4
{
    public class D4
    {
        public static void Part1()
        {
            var wordsearch = ParseInputFromFile("test.txt");
            var count = wordsearch.CountInstancesOfWord("XMAS");
            Console.WriteLine($"Part 1: {count}");
        }

        public static void Part2()
        {
            Console.WriteLine("Part 2: NOT IMPLEMENTED");
        }

        public static Wordsearch ParseInputFromFile(string filename)
        {
            string path = $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}\\Day4\\{filename}";
            var lines = File.ReadLines(path).ToList();
            return new Wordsearch(lines);
        }
    }
}

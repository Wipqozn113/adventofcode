using AOTC2024.Day1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOTC2024.Day6
{
    public class D6
    {
        public static void Part1()
        {
            Console.WriteLine("Part 1: NOT IMPLEMENTED");
        }

        public static void Part2()
        {
            Console.WriteLine("Part 2: NOT IMPLEMENTED");
        }

        public static void ParseInputFromFile(string filename)
        {
            string path = $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}\\Day6\\{filename}";
            var lines = File.ReadLines(path);
        }
    }
}

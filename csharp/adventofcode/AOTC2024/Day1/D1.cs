using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;

namespace AOTC2024.Day1
{
    public class D1
    {
        public static void Part1()
        {
            var filename = "input.txt";
            var left = new List<int>();
            var right = new List<int>();
            PopulateFromFile(filename, left, right);
            left.Sort();
            right.Sort();
            var total = 0;
            for(int i  = 0; i < left.Count; i++)
            {
                total += Math.Abs(left[i] - right[i]);
            }

            Console.WriteLine("Part 1: " + total);
        }

        public static void Part2()
        {
            var filename = "input.txt";
            var left = new List<int>();
            var right = new List<int>();
            PopulateFromFile(filename, left, right);
            var total = 0;
            foreach(var l in left)
            {
                total += l * right.Count(x => x == l);
            }

            Console.WriteLine("Part 2: " + total);
        }

        public static void PopulateFromFile(string filename, List<int> left, List<int> right)
        {
            string path = $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}\\Day1\\{filename}";
            var lines = File.ReadLines(path);
            foreach (var line in lines)
            {
                var ln = line.Trim().Split("   ");
                left.Add(int.Parse(ln[0]));
                right.Add(int.Parse(ln[1]));
            }
        }
    }
}

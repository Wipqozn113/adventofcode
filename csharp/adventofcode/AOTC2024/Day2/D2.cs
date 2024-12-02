using AOTC2024.Day1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOTC2024.Day2
{
    public class D2
    {
        public static void Part1()
        {
            var reports = new List<Report>();
            ParseInputFromFile("input.txt", reports);
            var safeCount = 0;
            foreach (var report in reports)
            {
                if(report.IsSafeWithoutDampener())
                {
                    safeCount++;
                }
            }

            Console.WriteLine($"Part 1: {safeCount}");
        }

        public static void Part2()
        {
            var reports = new List<Report>();
            ParseInputFromFile("input.txt", reports);
            var safeCount = 0;
            foreach (var report in reports)
            {
                if (report.IsSafeWithDampener())
                {
                    safeCount++;
                }
            }

            Console.WriteLine($"Part 2: {safeCount}");
        }

        public static void ParseInputFromFile(string filename, List<Report> reports)
        {
            string path = $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}\\Day2\\{filename}";
            var lines = File.ReadLines(path);
            foreach(var line in lines)
            {
                reports.Add(new Report(line.Trim().Split(' ').Select(x => int.Parse(x)).ToList()));
            }
        }
    }
}

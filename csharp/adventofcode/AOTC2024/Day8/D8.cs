﻿using AOTC2024.Day1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOTC2024.Day8
{
    public class D8
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
            string path = $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}\\Day8\\{filename}";
            var lines = File.ReadLines(path);
        }
    }
}

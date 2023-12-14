using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day14
{
    public static class D14
    {
        public static void Part1()
        {
            var dish = CreateReflectorDish();
            dish.TiltNorth();
            Console.WriteLine("Part 1: " + dish.TotalLoad());
            //dish.PrintMe();
        }

        public static void Part2()
        {
            var dish = CreateReflectorDish();
            dish.SpinCycle();
            Console.WriteLine("Part 2: " + dish.TotalLoad());
        }

        public static ReflectorDish CreateReflectorDish()
        {
            string path = "C:\\Users\\Owner\\Development\\adventofcode\\csharp\\adventofcode\\2023\\Day14\\test.txt";
            var lines = File.ReadLines(path).ToList();
            var dish = new ReflectorDish(lines);
            return dish;
        }
    }
}

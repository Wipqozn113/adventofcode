using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOCUtils.MathUtils;

namespace AOC2023.Day18
{
    public static class D18
    {
        public static void Part1()
        {
            var polygon = CreatePart1Polygon();
            Console.WriteLine("Part 1: " + polygon.CalculateInclusiveArea().ToString());
        }

        public static void Part2()
        {
            var polygon = CreatePart2Polygon();
            Console.WriteLine("Part 2: " + polygon.CalculateInclusiveArea().ToString());
        }

        public static Polygon CreatePart1Polygon()
        {
            string path = "C:\\Users\\Owner\\Development\\adventofcode\\csharp\\adventofcode\\2023\\Day18\\input.txt";
            var lines = File.ReadLines(path).ToList();
            Polygon polygon = new Polygon();
            long x = 0;
            long y = 0;

            foreach(var line in lines)
            {
                var ln = line.Trim().Split(' ');
                var direction = ln[0];
                var distance = int.Parse(ln[1]);
                if(direction == "R")
                {
                    x += distance;
                }
                else if(direction == "L") 
                {
                    x -= distance;
                }
                else if(direction == "U")
                {
                    y += distance;
                }
                else if( direction == "D") 
                {
                    y -= distance;
                }

                polygon.AddPoint(x, y);
            }

            return polygon;
        }

        public static Polygon CreatePart2Polygon()
        {
            string path = "C:\\Users\\Owner\\Development\\adventofcode\\csharp\\adventofcode\\2023\\Day18\\input.txt";
            var lines = File.ReadLines(path).ToList();
            Polygon polygon = new Polygon();
            long x = 0;
            long y = 0;

            foreach (var line in lines)
            {
                // (#70c710)
                var ln = line.Trim().Split(' ');

                var direction = int.Parse(ln[2][7].ToString());
                var distanceHex = ln[2].Substring(2, 5);
                var distance = Convert.ToInt32(distanceHex, 16);

                // 0 means R, 1 means D, 2 means L, and 3 means U.
                if (direction == 0)
                {
                    x += distance;
                }
                else if (direction == 2)
                {
                    x -= distance;
                }
                else if (direction == 3)
                {
                    y += distance;
                }
                else if (direction == 1)
                {
                    y -= distance;
                }

                polygon.AddPoint(x, y);
            }

            return polygon;
        }
    }
}

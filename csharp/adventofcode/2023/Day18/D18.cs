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
            var polygon = CreatePolygon();
            Console.WriteLine("Part 1: " + polygon.CalculateInclusiveArea().ToString());
        }

        public static void Part2()
        {

        }

        public static Polygon CreatePolygon()
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
    }
}

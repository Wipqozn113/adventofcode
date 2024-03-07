using AOCUtils.MathUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day22
{
    public class Point
    {
        public Point(long x, long y, long z)
        {
            Coord = new Coordinate3D(x, y, z);
        }

        public Coordinate3D Coord { get; set; }

        public Brick? Brick { get; set; }
    }

    public class Tower
    {
        public Tower()
        {
            Map = new Point[999, 999, 999];
            for (var x = 0; x < 999; x++)
            {
                for(var y = 0; y < 999; y++)
                {
                    for(var z = 0; z < 999; z++)
                    {
                        Map[x, y, z] = new Point(x, y, z);
                    }
                }
            }
        }

        Point[,,] Map;

        public List<Brick> Bricks { get; set; } = new List<Brick>();

        public void ParseLine(string line)
        {
            Bricks.Add(new Brick(line));
        }

        public void AddBricks()
        {
            foreach(var brick in Bricks)
            {
                var coordinates = brick.GetCoordinates();
                foreach(var coord in coordinates)
                {
                    Map[coord.X, coord.Y, coord.Z].Brick = brick; 
                }
            }

            foreach(var brick in Bricks)
            {
                brick.PopulateSupporting(this);
            }
        }
    }
}

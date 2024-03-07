using AOCUtils.MathUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day22
{
    public class Brick
    {
        public Brick(string line)
        {
            var ln = line.Split('~');
            var start = ln[0].Trim().Split(',');
            var end = ln[1].Trim().Split(',');
            Start = new Coordinate3D(long.Parse(start[0]), long.Parse(start[1]), long.Parse(start[2]));
            End = new Coordinate3D(long.Parse(end[0]), long.Parse(end[1]), long.Parse(end[2]));
        }

        public Coordinate3D Start { get; set; }

        public Coordinate3D End { get; set; }

        public List<Brick> Supporting { get; set; } = new List<Brick>();

        public List<Brick> SupportedBy { get; set; } = new List<Brick>();

        public List<Coordinate3D> GetCoordinates()
        {
            var current = Start; 
            return new List<Coordinate3D>();
        }

        public void PopulateSupporting(Tower tower)
        {
            
        }

        public bool CanBeRemoved()
        {
            // A brick can be removed if all the bricks it's  
            // supporting are supported by another brick
            foreach (var brick in Supporting)
            {
                if (brick.SupportedBy.Count == 1)
                    return false;
            }

            return true;
        }

    }
}

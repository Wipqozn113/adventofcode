using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOCUtils.MathUtils;

namespace AOC2023.Day11
{
    public class Tile
    {
        public Tile() { }

        public Tile(char tile)
        {
            HasGalaxy = tile == '#';
        }

        public bool HasGalaxy { get; set; } = false;

        public long X { get; set; }

        public long Y { get; set; }

        public override string ToString()
        {
            return HasGalaxy ? "#" : ".";
        }

        public long Distance(Tile other)
        {
            return Core.ManhattenDistance(X, Y, other.X, other.Y);
        }
    }
}

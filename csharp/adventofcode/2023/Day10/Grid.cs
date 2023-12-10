using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day10
{
    public class Grid
    {
        public Tile? Start { get; set; }

        public List<List<Tile>> Tiles { get; set; } = new List<List<Tile>>();

        public void ParseLine(string line)
        {            
            var tiles = new List<Tile>();
            var x = 0;
            var y = Tiles.Count;
         
            foreach(var symbol in line.Trim())
            {
                var tile = new Tile(x, y, symbol);
                tiles.Add(tile);
                if(tile.IsStart)
                    Start = tile; 
                x++;
            }
            Tiles.Add(tiles);
        }

        public int FindLoop()
        {
            if (Start is null)
                return 0;

            var explorer = new Explorer();

            var count = Tiles.SelectMany(list => list).Distinct().Count();
            var symbols = new List<char> { '|', '-', 'L', 'J', '7', 'F' };
            foreach(var symbol in symbols)
            {
                Start.Symbol = symbol;
                explorer.Reset(Start);
                var tile = Start;
                var steps = 0;

                while(steps < count)
                {
                    explorer.Move(tile);
                    steps++;
                    tile = Tiles[explorer.Y][explorer.X];
                    if (tile == Start)
                    {
                        var val = (double)steps / 2;
                        var furthest = Math.Ceiling(val);
                        return (int)furthest;
                    }
                }
            }

            return 0;
        }

    }
}



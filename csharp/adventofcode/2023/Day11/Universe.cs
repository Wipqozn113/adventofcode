using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AOC2023.Day11
{
    public class Universe
    {
        public List<List<Tile>> Space { get; set; } = new List<List<Tile>>();

        public void PrintMe()
        {
            foreach(var space in Space)
            {
                var line = "";
                foreach(var tile in space)
                {
                    line += tile.ToString();
                }
                Console.WriteLine(line);
            }
        }

        public void ParseLine(string line)
        {
            var tiles = new List<Tile>();
            foreach(var tile in line)
            {
                tiles.Add(new Tile(tile));
            }
            Space.Add(tiles);
        }

        public void Expand(long expansion_size = 1)
        {
            // Find empty space that needs expanding
            var emptyList = new List<Tile>();
            for(long i = 0; i < Space[0].Count; i++)
            {
                emptyList.Add(new Tile());
            }

            var rows = new List<long>();
            for(var i  = 0; i < Space.Count; i++)
            {
                if(!Space[i].Where(x => x.HasGalaxy).Any())
                    rows.Add(i);
            }

            var cols = new List<long>();
            for(var i = 0; i < Space[0].Count; i++)
            {
                if(!Space.Where(x => x[i].HasGalaxy).Any())
                    cols.Add(i);
            }

            // Expand empty space

            /*
            var r = 0;
            foreach(var row in rows)
            {
                Space.Insert(row + r, emptyList.ToList());
                r++;                
            }

            var c = 0;
            foreach(var col in cols)
            {
                foreach (var row in Space)
                {
                    row.Insert(col + c, new Tile());
                }
                c++;
            }
            */

            SetTilesXY(expansion_size, rows, cols);
        }

        private void SetTilesXY(long expansion_size, List<long> rows, List<long> columns)
        {
            for(var y = 0; y < Space.Count; y++) 
            {   
                for(var x = 0; x < Space[0].Count; x++)
                {
                    var ymod = rows.Where(r => r <= y).Count() * expansion_size;
                    var xmod = columns.Where(c => c <= x).Count() * expansion_size;
                    Space[y][x].X = x + xmod;
                    Space[y][x].Y = y + ymod;
                }
            }
        }

        public long FindDistanceSum()
        {
            var galaxies = new Queue<Tile>();
            Space.SelectMany(l => l).Where(x => x.HasGalaxy).ToList().ForEach(g =>  galaxies.Enqueue(g));
            long total = 0;
            
            while(galaxies.Count > 0)
            {
                var galaxy = galaxies.Dequeue();
                foreach(var gal in galaxies)
                {
                    total += galaxy.Distance(gal);
                }
            }

            return total;

        }
    }
}

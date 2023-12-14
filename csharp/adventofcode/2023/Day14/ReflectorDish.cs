using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day14
{
    public class ReflectorDish
    {
        public ReflectorDish(List<string> lines)
        {
            PopulateTiles(lines);
        }

        public List<List<Tile>> Tiles { get; set; } = new List<List<Tile>>();

        private List<string> CycleOrder { get; set; } = new List<string>();

        private Dictionary<string, long> Map = new Dictionary<string, long>();

        private long StoreList(int count)
        {
            var line = "";
            foreach(var tiles in Tiles)
            {
                foreach(var tile in tiles)
                {
                    line += tile.Occupant;
                }
            }
            
            if(CycleOrder.Contains(line))
            {
                var start = CycleOrder.IndexOf(line);
                var cycleLength = CycleOrder.Count - start;
                var cycle = (count - start) % cycleLength;
                var pattern = CycleOrder[cycle + start - 1];
                return Map[pattern];
            }
            else
            {
                CycleOrder.Add(line);
                Map[line] = TotalLoad();
            }

            return -1;
        }

        private void PopulateTiles(List<string> lines)
        {
            var y = 0;
            foreach(var line in lines)
            {
                var x = 0;
                var tiles = new List<Tile>();
                foreach(var t in line)
                {
                    var tile = new Tile(t, x, y, lines.Count - y);
                    tiles.Add(tile);
                    x += 1;
                }
                Tiles.Add(tiles);
                y += 1;
            }
        }

        public void PrintMe()
        {
            foreach(var tiles in Tiles)
            {
                var line = "";
                foreach(var tile in tiles)
                {
                    line += tile.Occupant;
                }
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }

        public long TotalLoad()
        {
            long total = 0;
            foreach(var tile in Tiles.SelectMany(t => t))
            {
                total += tile.Load();
            }

            return total;
        }  

        public long SpinCycle(int count = 1000000000)
        {
            for (long i = 0; i < count; i++)
            {
                TiltNorth();               
                TiltWest();
                TiltSouth();
                TiltEast();
                
                var val = StoreList(count);    
                if(val >= 0)
                {
                    return val;
                }
            }

            return 0;
        }

        public void TiltNorth()
        {
            foreach(var tiles in Tiles)
            {
                foreach (var tile in tiles)
                {
                    if (tile.IsRoundRock)
                    {
                        var newy = tile.Y;
                        while (newy - 1 >= 0)
                        {
                            if (Tiles[newy - 1][tile.X].IsEmpty)
                            {
                                newy--;
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (newy != tile.Y && newy >= 0)
                        {
                            tile.Occupant = '.';
                            Tiles[newy][tile.X].Occupant = 'O';
                        }
                    }
                }
            }
        }

        public void TiltWest()
        {
            foreach (var tiles in Tiles)
            {
                foreach (var tile in tiles)
                {
                    if (tile.IsRoundRock)
                    {
                        var newx = tile.X;
                        while (newx - 1 >= 0)
                        {
                            if (Tiles[tile.Y][newx - 1].IsEmpty)
                            {
                                newx--;
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (newx != tile.X && newx >= 0)
                        {
                            tile.Occupant = '.';
                            Tiles[tile.Y][newx].Occupant = 'O';
                        }
                    }
                }
            }
        }

        public void TiltSouth()
        {
            var reversedTiles = Tiles.ToList();
            reversedTiles.Reverse();
            foreach (var tiles in reversedTiles)
            {
                foreach (var tile in tiles)
                {
                    if (tile.IsRoundRock)
                    {
                        var newy = tile.Y;
                        while (newy + 1 < Tiles.Count)
                        {
                            if (Tiles[newy + 1][tile.X].IsEmpty)
                            {
                                newy++;
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (newy != tile.Y && newy < Tiles.Count)
                        {
                            tile.Occupant = '.';
                            Tiles[newy][tile.X].Occupant = 'O';
                        }
                    }
                }
            }
        }
        public void TiltEast()
        {
            foreach (var tiles in Tiles)
            {
                var reversedTiles = tiles.ToList();
                reversedTiles.Reverse();
                foreach (var tile in reversedTiles)
                {
                    if (tile.IsRoundRock)
                    {
                        var newx = tile.X;
                        while (newx + 1 < tiles.Count)
                        {
                            if (Tiles[tile.Y][newx + 1].IsEmpty)
                            {
                                newx++;
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (newx != tile.X && newx < tiles.Count)
                        {
                            tile.Occupant = '.';
                            Tiles[tile.Y][newx].Occupant = 'O';
                        }
                    }
                }
            }
        }


    }
}

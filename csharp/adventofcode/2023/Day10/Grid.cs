using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
                var loop = new List<Tile>();

                try
                {
                    while (steps < count)
                    {
                        loop.Add(tile);
                        if (explorer.Move(tile))
                        {
                            steps++;

                            tile = Tiles[explorer.Y][explorer.X];
                            if (tile == Start)
                            {
                                if (!explorer.Move(tile))
                                {
                                    break;
                                }
                                loop.ForEach(t => t.PartOfLoop = true);
                                var val = (double)steps / 2;
                                var furthest = Math.Ceiling(val);
                                return (int)furthest;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            return 0;
        }

        private void ClearJunk()
        {
            var junk = Tiles.SelectMany(l => l).Distinct().Where(t => t.PartOfLoop == false).ToList();
            junk.ForEach(x => x.Symbol = '.');
        }

        public void PrintMe()
        {            
            foreach(var tiles in Tiles)
            {
                var line = "";
                foreach(var tile in tiles)
                {
                    if (tile.PartOfLoop)
                        line += tile.Symbol;
                    else if (tile.NestedInsideLoop)
                        line += 'I';
                    else if (!tile.NestedInsideLoop)
                        line += 'O';
                }
                Console.WriteLine(line);
            }
        }

        public int CountNestedTiles()
        {
            ClearJunk();

            /*
                1: | (flip)
                2: F-7 (no change)
                3: F-J (flip)
                4: L-7 (flip)
                5: L-J (no change)
                https://www.reddit.com/r/adventofcode/comments/18ey1s7/2023_day_10_part_2_stumped_on_how_to_approach_this/kcr1jga/
            */
            foreach (var tiles in Tiles)
            {
                var insideLoop = false;
                var last = ' ';
                foreach(var tile in tiles)
                {
                    if(tile.Symbol == '|')
                    {
                        insideLoop = !insideLoop;
                    }
                    else if (tile.Symbol == 'F' || tile.Symbol == 'L')
                    {
                        last = tile.Symbol;
                    }
                    else if (tile.Symbol == 'J' && last == 'F')
                    {
                        insideLoop = !insideLoop;
                    }
                    else if (tile.Symbol == '7' && last == 'L')
                    {
                        insideLoop = !insideLoop;
                    }
                    else if(tile.Symbol == '.')
                    {
                        tile.NestedInsideLoop = insideLoop;
                    }
                }
            }

            return Tiles.SelectMany(l => l).Distinct().Where(t => !t.PartOfLoop && t.NestedInsideLoop).Count();
        }


    }
}



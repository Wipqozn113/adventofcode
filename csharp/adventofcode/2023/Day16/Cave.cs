using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day16
{
    public class Cave
    {
        public Cave(List<string> input)
        {
            ParseInput(input);
            History = new List<LaserHistory>();
        }

        public List<List<Tile>> Tiles { get; set; } = new List<List<Tile>>();

        public List<Laser> Lasers { get; set; } = new List<Laser>();

        public List<LaserHistory> History { get; set; }    

        private void ParseInput(List<string> input)
        {
            var y = 0;
            foreach (var line in input)
            {
                var x = 0;
                var tiles = new List<Tile>();
                foreach(var tile in line) 
                {
                    tiles.Add(new Tile(tile, x, y));
                    x++;
                }
                Tiles.Add(tiles);
                y++;
            }
        }

        public long FireLasers()
        {
            long energizedTiles = 0;

            Console.WriteLine("Doing top tiles...");
            foreach (var tile in Tiles[0])
            {
                var result = FireLaser(new Laser(tile, Direction.South));
                if (result > energizedTiles)
                    energizedTiles = result;
            }

            Console.WriteLine("Doing bottom tiles...");
            foreach (var tile in Tiles.Last())
            {
                var result = FireLaser(new Laser(tile, Direction.North));
                if (result > energizedTiles)
                    energizedTiles = result;
            }

            Console.WriteLine("Doing left tiles...");
            foreach (var tile in Tiles.Select(x => x.First()).ToList())
            {
                var result = FireLaser(new Laser(tile, Direction.East));
                if (result > energizedTiles)
                    energizedTiles = result;
            }

            Console.WriteLine("Doing right tiles...");
            foreach (var tile in Tiles.Select(x => x.Last()).ToList())
            {
                var result = FireLaser(new Laser(tile, Direction.West));
                if (result > energizedTiles)
                    energizedTiles = result;
            }

            return energizedTiles;

        }

        public long FireLaser(Laser? startingLaser = null)
        {
            if (startingLaser is null)
            {
                Lasers = new List<Laser>()
                {
                    new Laser(Tiles[0][0], Direction.East)
                };
            }
            else
            {
                Lasers = new List<Laser> { startingLaser };
            }

            // Reset history and energization of tiles
            Tiles.SelectMany(t => t).ToList().ForEach(x => x.Energized = false);
            History.Clear();

            // Continue moving lasers until they exit the cave
            while (Lasers.Any())
            {
                // Move all lasers 1 tile
                foreach (var laser in Lasers.ToList())
                {
                    // Move laser, and then remove it if it exited the cave
                    if(laser.MoveLaser(this))
                    {
                        Lasers.Remove(laser);
                    }
                }
            }

            return Tiles.SelectMany(l => l).Where(t => t.Energized).Count();
        }
    }

    public struct LaserHistory
    {
        public LaserHistory(Tile tile, Direction facing)
        {
            Tile = tile;
            Facing = facing;
        }

        public Tile Tile { get; }

        public Direction Facing { get; }
    }
}

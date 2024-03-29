﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day23
{
    public static class Utils
    {
        public static Elves CreateElves(bool test = false)
        {
            var data = test ? TestData() : Data();
            return ParseElves(data);
        }

        public static Elves ParseElves(string elvesMap)
        {
            var elfs = new List<Elf>();
            var x = 0;
            var y = 0;
            var lines = elvesMap.Split(Environment.NewLine);
            foreach (var line in lines)
            {
                if (line.Count() == 0)
                    continue;

                var l = line.Trim();
                foreach (char c in l)
                {
                    if(c == '#')
                    {
                        var coord = new Coordinate(x, y);
                        var elf = new Elf(coord, elfs.Count);
                        elfs.Add(elf);
                    }
                    x++;
                }
                y++;
                x = 0;
            }

            var elves = new Elves(elfs);
            return elves;
        }

        public static string TestData()
        {
var map = @"....#..
..###.#
#...#.#
.#...##
#.###..
##.#.##
.#..#..";

            return map;
        }

        public static string Data()
        {
            var map = @".##.#.#.#..#.#.##.##..#.###.#.####..#.#.##.##..##.#....##.#..####.##..###
#..##.####.####.##.#.#..#.#..#.##.###..#...##.#.#...#...#..#.##.#.###.#..
#.#..##....#.##.#.#...###....##.#...#..#.#.#####.##..#.##..###.####.#.###
.#.....#.####..#.....#....#...#...###.###.#.#..##..####....####...#..#..#
##.#.##.#.#.......##...###.##.#.###...#.#.#.....#.#...##.#..#...#.##.###.
#..##.#####.###.####.###..##.##...#..##.#..###.#####.####..#......###..#.
#...###.#.###.#..#####.#.#.#.####...###.#..#....#....#...#..##.#...#.#.#.
.###.##.##..#.........#.###.#.#..#.##.#.#.##.....#..###..#..##.##.####..#
#.##.#..#####..###.#.......#####.#...###..###.#.#...####.#.###.##..##...#
.#..#....#.##..#...##.###.##....#...##...#.....##.###...#.##.###..###...#
.###.###....#.######.###..####.#..##..#.#.#####.####.#...##.##.....##..#.
#..#.##..##.#.#.##.##..#######.#.########...##.#..##.##..#..##.#.###.####
#.##...##.##..####.....#####..##.#...#...#.#.#.#..#.######.....###.####.#
###.....#...#.########..########..#..###..##.#.#.#.##.#.##..##.####.##.#.
##.....#..##.#...#.#.##.#...#..##.#.######..##..##...#..##..#....##.#..##
#.####.#.#..#...#.........###...........#.....#.#...##......#.###..##....
.####.####....#...#.#.##.######......#..##...##..#.#.###...####.##..#..#.
.#.##..#.##.#.##....#..#####....###..#.##..#.##.#...#.#.##.#.......##.#..
#..#.#.#.#..##...#..##...#####....#...######.#......#######.....#..#.####
#...####..#.#.#.#.##....#.##.#..##...##..#.#..#.##.##.##.##....#.##.###..
..#.###.####..##.##.#...#.#......#.##..###.....##.###......####.....#...#
.##..####...##.#...#.##..##.##..#....###.#..####.######.#..#...#.#....##.
..#....#.#...##.#.###..##.##...#.#..#.##...##...#....#.##.#.#.#...#..####
##..###...#.##.##..###.##..####.#.###.##.###.....###.####.##..#..#..#.##.
.#.#.##.#.#..##..##.##.#.##.##.#...##.#....###.#..#...#..#.#.###.#..##..#
##...........###.#.###..#######....##.#.###.#...###.##.######.##..#..##..
###....#...##....#......#...#...##...#.##.#.#.#.###....#...##...##..#.#..
#.###...#..#.#.###.#.##.....###..#..##...#.###...##.#####.#...#.....###.#
......##...#.#..#.####.##.###..#.#..#####.####.....#.........#....##.#.##
.....#.#.#.#.#.#..##.#..##.#....###.#.#..####..##..##...#.##.#####..#..#.
##..#......###.......#.#.###..#..###..###.##..#..#.#.....#....##.###..###
...#.##.##.######....##...###..##.#......#.##....#....#..#.######.##...#.
......#.#.#.#..##.#.##...##.#....#.#.#####...#..####..#.##.#...###...##.#
.....####....#.#####.##...##...........##.##...####..###.####.#.##...###.
#....##..........##..#...#...##..#.#.###.##..#.###.#...#..#..#####....#..
.#####.###...#.##.#...####.#.##....##.###....#..#..#.#..######...###.#.#.
..##.###....#..#.#....#.##..###...#.##..#..######.##.#..####..##.#.#.####
#.#..#.#..##..#.#.####..#.##..####...##.#..#.###.##..#..###.##....###.#.#
..#.#...##..##....###.##.#..#.#..###.#..#.#.##..#.#.###..#.#.###...##.#..
.##.#....#......##..#.#..#.##.#...##.######....#.....##.##.#..#####.#.#..
#..#.#..#.#.###.##.#...#.....##.##.#..###.#..#....#.##.#####.#.#..#..#...
#.###..###...###..###.....####.##..#####.#....####.#.#..####....##.###.##
..#.##..#.....##..#######.#.#..##..##..##...#..........##.#.#.#..#....#.#
....####.##.#..###.....##..##..#.####....#...#...##...#.#....#..#.#..###.
#....##...###.#####.#.###.#...#.#.#.#....#..##...#.#...#..#..#.....#..#.#
###....##.##..#.##..#######.##.#.#.......#.##....###..#.#.......#.#####..
#.##.#.####..##.#.#...######..###..##.##......###.##.#.##.##..#.....#.##.
.###..##..###.#.##.#...#......#####.#.#..#......#.##.#.#.##..#..###.##.#.
.##.##.##..#.###.#...##.####.#..#.#..#.##...###.#.....#.#...#####.#.#####
##.#..#...#.#....#.#.#.#..###...####...#......##...##.#..###..##..##.###.
#....#.##.....#..###..##.###.##.#.###....#.#.###.#.....#..#..#..#..#####.
.##.######.#..####..#.##.###..#...#####.##....##..##.#.#.##..#####.####.#
..#..#####.##.#..#.##.##.#...##.#..###.#.##.#..#..#.##.##.###.##....#.#..
..##.#.#.##.##...##..#.#....##.#..#.#.#..#####..##....#.#.#.###.....#..##
#.#.##..#...####.##..#....#.#..##.#..#.####.##.#.###..#.#...#.##..####..#
##.......#.#.##.##....#.##...###..#.#..###.#..#.#..#.##...##...##..##...#
####.##..#..#..#..##.#.....#.#.###..##..#...#....#.##...#...##......##...
##..#....#..####.##.#.#....#.#.###...#.##...##.....##....#.###.#..#.####.
###.#...#.....#.#.##..#....##...####..#..#.....#....####..##.#.#....#####
#..###...####..#.##.#..###.#...##.#.#.###.###.#.#...#..##.##..##.##.#..##
#####...##....#.#.###......##.##.###.#..#.##.##.###...#..##.##...#######.
.#.#.#########..###...#..#..######.......##.#....#.##.###..#...##...##..#
...###..#.##.#.#..#.........####.#.##...####..####..####.#.#.....#.#.#..#
#####..#.##..###..###.#.##.#.##.###......#..#..#..###...##..#.###.###..##
.##.#.#.#.....###.##.#..###...#....#..##..#....#.##.#.....####..##..#..##
##..#...#.....##.##.#...#.#.##.###..##.#....####....#####...#...#...####.
.####....#...#.#..##..###...#.#..#.....#.#.##..#####........##.##....####
##.#####...#.####.#....#.#..##.##.###...##...#.##.##......#.##.......##..
.##...##..#........####..#...#.#...###.......###.#.#..#.##.###...#..#....
....#.#.#............##.#..####...#...##.##..#....#..#.#..#.#.#.#.#.#.###
..#.#..#.#####.#.##.##.##..#.##.#..###..###..###..####.#.#.##..######.#..
.#.#...#.##..##..#.####...#..##..####.#######.####.#...##....###.##.#.###
###.#..###.##.......##..#.....#.#.##.##.#.###.#..#####...##.##.#.....#...";

            return map;
        }
    }
}

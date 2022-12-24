using System.Collections;
using System.Diagnostics;
using System.Text;

namespace day23
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Part1();
            Part2(false);
        }

        static void Part1(bool test = false)
        {
            var elves = Utils.CreateElves(test);
            var emptySquares = elves.Run(false, 10);
            Console.WriteLine($"Part 1: {emptySquares}");
        }

        static void Part2(bool test = false)
        {
            var elves = Utils.CreateElves(test);
            var round = elves.Run(true);
            Console.WriteLine($"Part 2: {round}");
        }
    }
}
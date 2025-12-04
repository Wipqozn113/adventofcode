using System.Diagnostics;

namespace AOTC2024.Day6
{
    public class D6
    {
        public static void Part1()
        {
            var guard = ParseInputFromFile("input.txt");
            var count = guard.CountUniqueSquaresVisited();
            Console.WriteLine($"Part 1: {count}");
        }

        public static void Part2()
        {
            var timer = new Stopwatch();
            timer.Start();
            var guard = ParseInputFromFile("input.txt");
            var count = guard.CountPossibleLoops();
            timer.Stop();
            Console.WriteLine($"Part 2: {count} {timer.Elapsed.TotalSeconds }");
        }

        public static Guard ParseInputFromFile(string filename)
        {
            string path = $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}\\Day6\\{filename}";
            var lines = File.ReadLines(path).ToList();
            var map = new Map(lines);
            var guard = new Guard(map);
            return guard;
        }
    }
}

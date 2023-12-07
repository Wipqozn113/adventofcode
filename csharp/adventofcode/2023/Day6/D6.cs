using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day6
{
    public static class D6
    {
        public static void Part1()
        {
            var hands = PopulateFromFile();
            hands.Sort();
            var total = 0;
            for (int i = 0; i < hands.Count; i++)
            {
                total += hands[i].Bid * (i + 1);
            }
            Console.WriteLine("Part 1: " + total);
        }

        public static void Part2()
        {
            var hands = PopulateFromFile(true);
            hands.Sort();
            var total = 0;
            for (int i = 0; i < hands.Count; i++)
            {
                total += hands[i].Bid * (i + 1);
            }
            Console.WriteLine("Part 2: " + total);
        }

        private static List<Hand> PopulateFromFile(bool jokers = false)
        {
            var hands = new List<Hand>();
            string path = "C:\\Users\\Owner\\Development\\adventofcode\\csharp\\adventofcode\\2023\\Day6\\input.txt";
            var lines = File.ReadLines(path);

            foreach(var line in lines)
            {
                hands.Add(new Hand(line, jokers));
            }

            return hands;
        }
    }
}

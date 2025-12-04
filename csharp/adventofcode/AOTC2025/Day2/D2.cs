using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOTC2025.Day2
{
    public class D2
    {
        public static void Part1()
        {
            var ranges = PopulateFromFile("input.txt");
            long invalidSum = 0;
            foreach (var range in ranges)
            {
                for(long i = range.Start; i <= range.End; i++)
                {
                    if (IsInvalid(i))
                    {
                       //Console.WriteLine(i);
                        invalidSum += i;
                    }
                }
            }
            Console.WriteLine(invalidSum);
        }

        public static void Part2()
        {
            var ranges = PopulateFromFile("input.txt");
            long invalidSum = 0;
            foreach (var range in ranges)
            {
                for (long i = range.Start; i <= range.End; i++)
                {
                    if (IsInvalidAlt(i))
                    {
                        //Console.WriteLine(i);
                        invalidSum += i;
                    }
                }
            }
            Console.WriteLine("\n" + invalidSum);
        }

        private static bool IsInvalid(long id)
        {
            var scannableId = id.ToString();

            if (scannableId.Length % 2 == 0)
            {
                var half = scannableId.Length / 2;
                var firstHalf = scannableId.Substring(0, half);
                var secondHalf = scannableId.Substring(half);
                if (firstHalf == secondHalf) return true;
            }

            return false;
        }

        private static bool IsInvalidAlt(long id)
        {
            var scannableId = id.ToString();

            if (scannableId.Length == 1) return false;

            for(int i = 1; i <= (int)Math.Ceiling((decimal)scannableId.Length / 2); i++)
            {
                if(scannableId.Length % i == 0)
                {
                    var substring = scannableId.Substring(0, i);
                    var matches = Regex.Matches(scannableId, substring).Count;
                    if (matches == scannableId.Length / i) return true;
                }
            }

            return false;
        }

        private static List<IdRange> PopulateFromFile(string filename)
        {
            string path = $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}\\Day2\\{filename}";
            var lines = File.ReadLines(path);
            var ranges = new List<IdRange>();

            foreach (var line in lines)
            {
                var ln = line.Trim().Split(",");
                foreach(var range in ln)
                {
                    var rng = range.Split("-");
                    var idRange = new IdRange();
                    idRange.Start = long.Parse(rng[0]);
                    idRange.End = long.Parse(rng[1]);
                    ranges.Add(idRange);
                }
            }

            return ranges;
        }       
        
        private struct IdRange
        {
            public long Start;
            public long End;
        }
    }
}

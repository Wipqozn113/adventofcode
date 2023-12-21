using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AOC2023.Day19
{
    public class Part
    {
        public Part(int x, int m, int a, int s)
        {
            X = x;
            M = m;
            A = a;
            S = s;
        }

        public Part(string line)
        {
            var rx = new Regex(@"\d+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var matches = rx.Matches(line);
            X = int.Parse(matches[0].Value);
            M = int.Parse(matches[1].Value);
            A = int.Parse(matches[2].Value);
            S = int.Parse(matches[3].Value);
        }

        public int X { get; set; }

        public int M { get; set; }

        public int A { get; set; }

        public int S { get; set; }  

        public long SumOfValues()
        {
            return X + M + A + S;
        }

        public bool Accepted { get; set; } = false;
    }
}

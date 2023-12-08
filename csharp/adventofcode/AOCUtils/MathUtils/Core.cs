using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCUtils.MathUtils
{
    public static class Core
    {
        // https://stackoverflow.com/a/29717490/608314
        public static int LCM(IEnumerable<int> numbers)
        {
            return numbers.Aggregate(lcm);
        }
        public static int lcm(int a, int b)
        {
            return Math.Abs(a * b) / GCD(a, b);
        }
        public static int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }
    }
}

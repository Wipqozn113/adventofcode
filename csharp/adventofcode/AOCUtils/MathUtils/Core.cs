﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCUtils.MathUtils
{
    public static class Core
    {
        public static long ManhattenDistance(long x1, long y1, long x2, long y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        public static int ManhattenDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        public static long LCM(IEnumerable<int> numbers)
        {
            var convertedList = new List<long>();
            
            foreach(var number in numbers)
            {
                convertedList.Add(Convert.ToInt64(number));
            }

            return LCM(convertedList);
        }

        public static long LCM(IEnumerable<long> numbers) 
        {
            return numbers.Aggregate(LCM);
        }

        // https://stackoverflow.com/a/20824923/608314
        public static long GCF(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // https://stackoverflow.com/a/20824923/608314
        public static long LCM(long a, long b)
        {
            return (a / GCF(a, b)) * b;
        }
    }
}

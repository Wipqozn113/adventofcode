using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AOTC2024.Day2
{
    public class Report
    {
        private int MinIncrease { get; set; } = 1;

        private int MaxIncrease { get; set; } = 3;

        public Report(List<int> levels) 
        {
            Levels = levels.ToList();
            IsIncreasing = Levels[0] < Levels[1];
            IsDecreasing = Levels[0] > Levels[1];
        }

        public List<int> Levels { get; private set; }

        public bool IsIncreasing { get; private set; }

        public bool IsDecreasing { get; private set; }

        public string PrintMe
        {
            get
            {
                return string.Join(' ', Levels);
            }
        }

        public bool IsSafeWithoutDampener()
        {
            var isSafe = IsSafe(Levels, IsIncreasing, IsDecreasing);
            return isSafe;
        }

        public bool IsSafeWithDampener()
        {
            for(int i = 0; i < Levels.Count; i++)
            {
                var levels = Levels.ToList();
                levels.RemoveAt(i);
                var isIncreasing = levels[0] < levels[1];
                var isDecreasing = levels[0] > levels[1];
                if (IsSafe(levels, isIncreasing, isDecreasing))
                    return true;
            }

            return false;
        }


        private bool IsSafe(List<int> levels, bool isIncreasing, bool isDecreasing)
        {
            if (isIncreasing && isDecreasing)
            {                
                return false;
            }

            for (int i = 0; i < levels.Count - 1; i++)
            {
                if (levels[i] == levels[i + 1]
                    || isIncreasing && levels[i] > levels[i + 1]
                    || isDecreasing && levels[i] < levels[i + 1])
                {    
                    return false;
                }

                var diff = Math.Abs(levels[i] - levels[i + 1]);
                if (diff < MinIncrease || diff > MaxIncrease)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

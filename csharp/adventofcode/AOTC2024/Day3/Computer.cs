using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOTC2024.Day3
{
    public class Computer
    {
        public Computer(List<string> memory)
        {
            Memory = memory.ToList();
        }

        public List<string> Memory {  get; set; }

        public int Calc()
        {
            var total = 0;
            var pattern = "mul\\(\\d{1,3},\\d{1,3}\\)";

            foreach (var item in Memory)
            {
                foreach (Match match in Regex.Matches(item, pattern, RegexOptions.IgnoreCase))
                {
                    var matches = Regex.Matches(match.Value, "\\d{1,3}", RegexOptions.IgnoreCase);
                    var num1 = int.Parse(matches[0].Value);
                    var num2 = int.Parse(matches[1].Value);
                    total += num1 * num2;
                }
            }

            return total;
        }

        public int CalcFull()
        {
            var total = 0;            
            var pattern = "mul\\(\\d{1,3},\\d{1,3}\\)";

            foreach (var item in Memory)
            {
                var dos = Regex.Matches(item, "do\\(\\)", RegexOptions.IgnoreCase).ToList();
                var donts = Regex.Matches(item, "don't\\(\\)", RegexOptions.IgnoreCase).ToList();

                foreach (Match match in Regex.Matches(item, pattern, RegexOptions.IgnoreCase))
                {
                    if (IsEnabled(match, dos, donts))
                    {
                        var matches = Regex.Matches(match.Value, "\\d{1,3}", RegexOptions.IgnoreCase);
                        var num1 = int.Parse(matches[0].Value);
                        var num2 = int.Parse(matches[1].Value);
                        total += num1 * num2;
                    }                 
                }
            }

            return total;
        }

        private bool IsEnabled(Match mul, List<Match> dos, List<Match> donts)
        {
            int? closestDo = null;
            foreach(var d in dos)
            {
                if (d.Index < mul.Index)
                    closestDo = d.Index;
                else
                    break;
            }

            int? closestDont = null;
            foreach (var dt in donts)
            {
                if (dt.Index < mul.Index)
                    closestDont = dt.Index;
                else
                    break;
            }

            if (closestDont is null)
                return true;

            if (closestDo is null && closestDont is not null)
                return false;

            return closestDo > closestDont;
        }
    }
}

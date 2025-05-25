using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// Calculates the sum of all all mul() operations while ignoring do and dont operations.
        /// </summary>
        /// <returns>Sum of all mul() operations (do and dont ignored)</returns>
        public int Calc()
        {
            var total = 0;
            var pattern = "mul\\(\\d{1,3},\\d{1,3}\\)";

            foreach (var item in Memory)
            {
                foreach (Match match in Regex.Matches(item, pattern, RegexOptions.IgnoreCase))
                {
                    total += Mul(match);
                }
            }

            return total;
        }

        /// <summary>
        /// Calculates the sum of all mul() operations while accounting for do and dont operations.
        /// </summary>
        /// <returns>Sum of all mul() operations (do and dont accounted for)</returns>
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
                        total += Mul(match);
                    }                 
                }
            }

            return total;
        }

        /// <summary>
        /// Perform the mul() operation
        /// </summary>
        /// <param name="mulMatch">The regex match for the mul operation</param>
        /// <returns>mul(x,y) = x * y</returns>
        private int Mul(Match mulMatch)
        {
            var matches = Regex.Matches(mulMatch.Value, "\\d{1,3}", RegexOptions.IgnoreCase);
            var num1 = int.Parse(matches[0].Value);
            var num2 = int.Parse(matches[1].Value);
            return num1 * num2;
        }

        /// <summary>
        /// Determines if the provided mul() operation is enabled
        /// </summary>
        /// <param name="mul">Regex match for the mul operation to check</param>
        /// <param name="dos">List of do operations</param>
        /// <param name="donts">List of donts operations</param>
        /// <returns>TRUE if this mul() operation is enabled. False otherwise.</returns>
        private bool IsEnabled(Match mul, List<Match> dos, List<Match> donts)
        {
            // Find the closest do operation that occurs before the mul operation
            int? closestDo = null;
            foreach(var d in dos)
            {
                if (d.Index < mul.Index)
                    closestDo = d.Index;
                else
                    break;
            }

            // Find the closest dont operation that occurs before the mul operation
            int? closestDont = null;
            foreach (var dt in donts)
            {
                if (dt.Index < mul.Index)
                    closestDont = dt.Index;
                else
                    break;
            }

            // mul is enabled by default
            if (closestDont is null)
                return true;

            // Disabled if there's a dont before but no dos before
            if (closestDo is null && closestDont is not null)
                return false;

            // Enabled if the closest do is closer than the closest dont
            return closestDo > closestDont;
        }
    }
}

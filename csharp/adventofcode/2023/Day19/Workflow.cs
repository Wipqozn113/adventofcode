using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AOC2023.Day19
{
    public class Workflow
    {
        public Workflow(string line)
        {
            var wf = line.Split("{");
            Id = wf[0];
            Instructions = wf[1].Split("}")[0];
            Checks = Instructions.Split(",").ToList();
        }

        public string Id { get; set; }

        public string Instructions { get; set; }

        public List<string> Checks { get; set; }

        public string DetermineLocation(Part part)
        {
            var location = Checks.Last();

            foreach(var check in Checks)
            {                
                if(check.Contains(':'))
                {
                    var ck = check.Split(':');
                    if(PerformCheck(part, ck[0]))
                    {
                        location = ck[1];
                        break;
                    }
                }
            }

            return location;
        }

        private bool PerformCheck(Part part, String check)
        {
            var val = part.X;
            if (check[0] == 'a')
                val = part.A;
            else if (check[0] == 'm')
                val = part.M;
            else if (check[0] == 's')
                val = part.S;

            var c = int.Parse(check.Substring(2));

            if (check[1] == '>')
            {
                return val > c;
            }
            else if (check[1] == '<')
            {
                return val < c;
            }

            return false;
        }
    }
}

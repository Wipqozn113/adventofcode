using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day19
{
    public class Factory
    {

        public Dictionary<string, Workflow> Workflows { get; set; } = new Dictionary<string, Workflow>();

        public Workflow StartingWorkflow
        {
            get
            {
                return Workflows["in"];
            }
        }

        public List<Part> Parts { get; set; } = new List<Part>();

        public void AddWorkflow(string line)
        {
            var workflow = new Workflow(line);
            Workflows[workflow.Id] = workflow;
        }

        public void AddPart(string line) 
        {
            var part = new Part(line);  
            Parts.Add(part);
        }

        public void CreateTestParts()
        {
            for(int x = 1; x <= 4000; x++)
            {
                for (int m = 1; m <= 4000; m++)
                {
                    for (int a = 1; a <= 4000; a++)
                    {
                        for (int s = 1; s <= 4000; s++)
                        {
                            Parts.Add(new Part(x, m, a, s));
                        }
                    }
                }
            }
        }

        public long SumOfAccepted()
        {
            long val = 0;

            foreach(var part in Parts)
            {
                var workflow = StartingWorkflow;
                while(true)
                {
                    var location = workflow.DetermineLocation(part);
                    if(location == "R")
                    {
                        part.Accepted = false;
                        break;
                    }
                    else if(location == "A")
                    {
                        part.Accepted = true;
                        val += part.SumOfValues();
                        break;
                    }
                    else
                    {
                        workflow = Workflows[location];
                    }
                }
            }

            return val;
        }

    }
}

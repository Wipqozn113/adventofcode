using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AOC2023.Day20
{
    public class FlipFlopModule : Module
    {
        public FlipFlopModule(string name, Graph graph, List<string> childrenNames) : base(name, graph, childrenNames) { }

        public bool IsOn { get; set; } = false;

        private Queue<Pulse> PulseQueue { get; set; } = new Queue<Pulse>();

        public override void RecievePulse(Pulse pulse, IModule module)
        { 
            PulseQueue.Enqueue(pulse);
            Graph.ModulesQueue.Enqueue(this);

            if (pulse == Pulse.Low)
            {
                Graph.LowPulses++;
            }
            else
            {
                Graph.HighPulses++;
            }
        }

        public override void SendPulse()
        {
            var pulse = PulseQueue.Dequeue();
            if(pulse == Pulse.Low)
            {
                if(IsOn)
                {
                    IsOn = false;
                    foreach(var module in Output)
                    {
                        module.Child.RecievePulse(Pulse.Low, this);
                    }
                }
                else
                {
                    IsOn = true;
                    foreach (var module in Output)
                    {
                        module.Child.RecievePulse(Pulse.High, this);
                    }
                }
            }
        }
    }
}

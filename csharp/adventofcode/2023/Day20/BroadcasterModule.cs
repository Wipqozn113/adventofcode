using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day20
{
    public class BroadcasterModule : Module
    {

        public BroadcasterModule(string name, Graph graph, List<string> childrenNames) : base(name, graph, childrenNames) { }

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
            foreach (var module in Output)
            {
                module.Child.RecievePulse(pulse, this);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day20
{
    public class SinkModule : Module
    {
        public SinkModule(string name, Graph graph, List<string> childrenNames) : base(name, graph, childrenNames) { }

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

        public override bool SendPulse()
        {
            // Does nothing, just a sink
            var pulse = PulseQueue.Dequeue();
            return (pulse == Pulse.Low);
        }
    }
}

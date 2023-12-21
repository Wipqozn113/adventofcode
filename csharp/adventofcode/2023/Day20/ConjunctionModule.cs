using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day20
{
    public class ConjunctionModule : Module
    {
        public ConjunctionModule(string name, Graph graph, List<string> childrenNames) : base(name, graph, childrenNames) { }

        private Dictionary<string, Pulse> PulseMemory = new Dictionary<string, Pulse>();

        private Queue<(Pulse, string)> PulseQueue = new Queue<(Pulse, string)>();

        public void InitMemory(string name)
        {
            PulseMemory[name] = Pulse.Low;
        }

        public override void RecievePulse(Pulse pulse, IModule module)
        {
            PulseQueue.Enqueue((pulse, module.Name));
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
            (var pulse, var moduleName) = PulseQueue.Dequeue();
            PulseMemory[moduleName] = pulse;

            var newPulse = Pulse.Low;
            if(PulseMemory.Where(x => x.Value == Pulse.Low).Any())
            {
                newPulse = Pulse.High;
            }

            foreach (var module in Output)
            {
                module.Child.RecievePulse(newPulse, this);
            }

            return false;
        }
    }
}

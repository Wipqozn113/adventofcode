using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AOC2023.Day20
{
    public class Graph
    {
        public Graph()
        {
            var rx = new SinkModule("rx", this, new List<string>());
            Modules.Add(rx);
        }

        public List<IModule> Modules { get; set; } = new List<IModule>();

        public Queue<IModule> ModulesQueue { get; set; } = new Queue<IModule>();

        public BroadcasterModule BroadcasterModule
        {
            get
            {
                var module = Modules.Where(x => x.Name == "broadcaster").First();
                return (BroadcasterModule)module;
            }
        }

        public long LowPulses { get; set; }

        public long HighPulses { get; set; }

        public void AddModule(string line)
        {
            var ln = line.Split("->");
            var name = "";
            var moduleType = 'b';
            if (ln[0].Trim() == "broadcaster")
            {
                name = "broadcaster";
                moduleType = 'b';
            }
            else
            {
                name = ln[0].Trim().Substring(1);
                moduleType = ln[0][0];
            }
            var names = ln[1].Split(",").ToList();
            var childrenNames = new List<string>();
            foreach (var childName in names)
            {
                childrenNames.Add(childName.Trim());
            }

            if(moduleType == 'b')
            {
                Modules.Add(new BroadcasterModule(name, this, childrenNames)); 
            }
            else if (moduleType == '%')
            {
                Modules.Add(new FlipFlopModule(name, this, childrenNames));
            }
            else if (moduleType == '&')
            {
                Modules.Add(new ConjunctionModule(name, this, childrenNames));
            }
        }

        public void PopulateChildren()
        {            
            foreach (IModule module in Modules)
            {
                module.PopulateChildren(Modules);
            }
            
        }

        public long PressButton(long pressCount)
        {
            for (int i = 0; i < pressCount; i++)
            {
                BroadcasterModule.RecievePulse(Pulse.Low, BroadcasterModule);
                while(ModulesQueue.Any())
                {
                    var module = ModulesQueue.Dequeue();
                    module.SendPulse();
                }
            }

            return LowPulses * HighPulses;
        }
        public long PressButton()
        {
            var i = 0;
            while(true)
            {
                i++;
                BroadcasterModule.RecievePulse(Pulse.Low, BroadcasterModule);
                while (ModulesQueue.Any())
                {
                    var module = ModulesQueue.Dequeue();
                    if(module.SendPulse())
                    {
                        return i;
                    }
                }
            }
        }
    }
}

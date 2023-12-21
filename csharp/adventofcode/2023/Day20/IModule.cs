﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day20
{
    public interface IModule
    {
        public string Name { get; set; }

        public void RecievePulse(Pulse pulse, IModule module);

        public void SendPulse();

        public void PopulateChildren(List<IModule> modules);

    }
}

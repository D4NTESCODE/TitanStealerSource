using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titan.NF.Client.Hardware.Contracts
{
    internal interface IHardwareCollector
    {
        string[] Collect();
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titan.Socket.Machine
{
    internal class MachineObject
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        public Dictionary<string, string> Properties { get; set; } = new();

        public void DropToDisk()
        {

        }
    }
}

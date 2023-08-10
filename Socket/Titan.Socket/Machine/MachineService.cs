using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titan.Socket.Machine
{
    internal class MachineService
    {
        private static ConcurrentBag<MachineObject> _machines = new();

        public static MachineObject CreateMachine(Guid guid)
        {
            MachineObject machine = new MachineObject
            {
                Guid = guid
            };

            _machines.Add(machine);

            return machine;
        }

        public static void DropAll()
        {
            
        }

        public static MachineObject? Get(Guid guid)
            => _machines.FirstOrDefault((x) => x.Guid == guid);


        public static MachineObject GetOrCreate(Guid guid)
        {
            var machine = Get(guid);

            if(machine == null)
            {
                return CreateMachine(guid);
            }

            return machine;
        }
    }
}

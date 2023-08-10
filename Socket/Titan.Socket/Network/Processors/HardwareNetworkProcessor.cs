using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Socket.Network.Contracts;

namespace Titan.Socket.Network.Processors
{
    internal class HardwareNetworkProcessor : INetworkProcessor
    {
        public bool Can(Dictionary<string, object> metadata)
        {
            return metadata["type"].ToString() == "Hardware";
        }

        public async Task<(Dictionary<string, object> metadata, byte[] payload)> Process(byte[] data, Dictionary<string, object> metadata)
        {
            var responseMetaData = new Dictionary<string, object>()
            {
                ["code"] = 0,
                ["data.received"] = data.Length
            };

            return await Task.Run(() =>
            {
                return (responseMetaData, new byte[8]);
            });
        }
    }
}

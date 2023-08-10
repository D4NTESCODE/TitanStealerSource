using System;
using System.Management;
using System.Runtime.Versioning;
using Titan.NF.Client.Hardware.Contracts;

namespace Titan.NF.Client.Hardware.Collectors
{
    //[SupportedOSPlatform("windows")]
    internal class GPUHardwareCollector : IHardwareCollector
    {
        public string[] Collect()
        {
            string query = "SELECT * FROM Win32_VideoController";
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                ManagementObjectCollection results = searcher.Get();

                foreach (ManagementObject obj in results)
                {
                    string gpuName = obj["Name"].ToString();

                    return new string[] { "GraphicCard: " + gpuName  };
                }
                return null;
            }
        }

        private static string EncodedTag()
        {
            return "R3JhcGhpY0NhcmQ=";
        }
    }
}

using System.Diagnostics;
using System;
using System.Runtime.Versioning;
using Titan.NF.Client.Hardware.Contracts;
using System.Management;

namespace Titan.NF.Client.Hardware.Collectors
{
    //[SupportedOSPlatform("windows")]
    internal class RAMHardwareCollector : IHardwareCollector
    {
        public string[] Collect()
        {
            ManagementObjectSearcher ramMonitor = 
            new ManagementObjectSearcher("SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem");

            foreach (ManagementObject objram in ramMonitor.Get())
            {
                ulong totalRam = Convert.ToUInt64(objram["TotalVisibleMemorySize"]);
                return new string[] { $"RAM: {totalRam / 1024} MB" };
            }
            return null;
        }

        private static string EncodedTag()
        {
            return "";
            //return ChromiumDumpHelper.FromBase64Short("UkFN");
        }
    }
}

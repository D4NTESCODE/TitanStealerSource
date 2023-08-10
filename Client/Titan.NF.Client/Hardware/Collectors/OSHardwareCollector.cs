using Microsoft.Win32;
using System;
using System.Runtime.Versioning;
using Titan.NF.Client.Hardware.Contracts;

namespace Titan.NF.Client.Hardware.Collectors
{
    //[SupportedOSPlatform("windows")]
    internal class OSHardwareCollector : IHardwareCollector
    {
        public string[] Collect()
        {

            string name = string.Empty;
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
            {
                if (key != null)
                {
                    name = key.GetValue("ProductName").ToString();
                }
            }
            return new string[] { $"OS: {name}" };
        }

        private static string EncodedTag()
        {
            return "T1M=";
        }
    }
}

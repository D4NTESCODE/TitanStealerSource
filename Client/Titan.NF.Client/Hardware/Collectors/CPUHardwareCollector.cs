using System;
using Titan.NF.Client.Hardware.Contracts;

namespace Titan.NF.Client.Hardware.Collectors
{
    internal class CPUHardwareCollector : IHardwareCollector
    {
        public string[] Collect()
        {
            string processorName = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER");
            return new string[] { "CPU: " + processorName };
        }

        private static string EncodedTag()
        {
            return "Q1BV";
        }
    }
}

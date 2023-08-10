using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Titan.NF.Client.Hardware.Collectors;
using Titan.NF.Client.Hardware.Contracts;

namespace Titan.NF.Client.Hardware
{
    internal class HardwareService
    {
        [StructLayout(LayoutKind.Sequential)]
        class HWProfile
        {
            public Int32 dwDockInfo;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 39)]
            public string szHwProfileGuid;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szHwProfileName;
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool GetCurrentHwProfile(IntPtr fProfile);



        //p/ublic static readonly IHardwareInfo HardwareAccessor = new HardwareInfo();

        private static List<IHardwareCollector> _collectors = new List<IHardwareCollector>()
        {
            new OSHardwareCollector(),
            new CPUHardwareCollector(),
            new GPUHardwareCollector(),
            new RAMHardwareCollector(),
            new MonitorHardwareCollector(),
            new EPathHardwareCollector(),
        };

        private static string CreateMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return hashBytes.ToHexWithoutDashes();
            }
        }

        public static string GetHardwareId()
        {
            string lText = "";
            IntPtr lHWInfoPtr = Marshal.AllocHGlobal(123);
            HWProfile lProfile = new HWProfile();
            Marshal.StructureToPtr(lProfile, lHWInfoPtr, false);

            if (GetCurrentHwProfile(lHWInfoPtr))
            {
                Marshal.PtrToStructure(lHWInfoPtr, lProfile);
                lText = lProfile.szHwProfileGuid.ToString();
            }
            Marshal.FreeHGlobal(lHWInfoPtr);

            return CreateMD5(lText);
        }

        public static List<string[]> CollectHardware()
        {
            //HardwareAccessor.RefreshAll();

            List<string[]> buffer = new List<string[]>();

            _collectors.ForEach((element) =>
            {
                buffer.Add(element.Collect());
            });

            return buffer;
        }
    }
}

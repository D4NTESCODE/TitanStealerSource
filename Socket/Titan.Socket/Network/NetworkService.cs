using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Socket.Network.Contracts;
using Titan.Socket.Network.Processors;
using Titan.Socket.Terminal;
using WatsonTcp;

namespace Titan.Socket.Network
{
    internal class NetworkService
    {
        public static WatsonTcpServer? _server;

        public static ConcurrentBag<INetworkProcessor> _processors = new();

        public static void Listen(string ip, int port)
        {
            ApplyDefaultProcessors();
            
            _server ??= new(ip, port);
            _server.Events.ClientConnected += OnConnected;
            _server.Events.MessageReceived += OnDataReceived;
            //_server.Settings.DebugMessages = true;
            //_server.Settings.Logger += OnLog;

            _server.Keepalive.EnableTcpKeepAlives = true;
            _server.Keepalive.TcpKeepAliveInterval = 5;      // seconds to wait before sending subsequent keepalive
            _server.Keepalive.TcpKeepAliveTime = 5;          // seconds to wait before sending a keepalive
            _server.Keepalive.TcpKeepAliveRetryCount = 5;    // number of failed keepalive probes before terminating connection

            _server.Start();

            TerminalService.Trace($"Server was listen on: {ip}:{port}");
        }

        private static async void OnDataReceived(object? sender, MessageReceivedEventArgs e)
        {
            if(_server != null)
            {
                foreach (var processor in _processors)
                {
                    if (processor.Can(e.Metadata))
                    {
                        var (metadata, payload) = await processor.Process(e.Data, e.Metadata);
                        //await _server.SendAsync(e.Client.Guid, payload, metadata);
                    }
                }
            }
        }

        private static void OnLog(Severity sev, string msg)
        {
            Console.WriteLine(sev.ToString() + ": " + msg);
        }

        private static void OnConnected(object? sender, ConnectionEventArgs e)
        {
            TerminalService.Success($"Machine: {e.Client} was connected");
        }

        private static void ApplyDefaultProcessors()
        {
            _processors.Clear();
            _processors.Add(new DebugNetworkProcessor());
        }
    }
}

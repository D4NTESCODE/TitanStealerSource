using Titan.Socket.Configuration;
using Titan.Socket.Network;

NetworkService.Listen(Env.Get<string>("network.address") ?? "127.0.0.1", Env.Get<int>("network.port"));

while (true)
{
    var cmd = Console.ReadLine() ?? "";
    
    if(cmd.Equals("exit"))
    {
        break;
    }
}

Environment.Exit(0);
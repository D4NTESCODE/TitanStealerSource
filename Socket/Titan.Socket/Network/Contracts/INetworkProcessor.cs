namespace Titan.Socket.Network.Contracts
{
    internal interface INetworkProcessor
    {
        public bool Can(Dictionary<string, object> metadata);
        public Task<(Dictionary<string, object> metadata, byte[] payload)> Process(byte[] data, Dictionary<string, object> metadata);
    }
}

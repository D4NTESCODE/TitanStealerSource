namespace Titan.Socket.Configuration
{
    internal class EnvStorage
    {
        public static Dictionary<string, string> GetStructure()
        {
            return new()
            {
                // Network
                ["network.address"] = "127.0.0.1",
                ["network.port"] = "27140",

                // Database 
                ["database.host"] = "127.0.0.1",
                ["database.port"] = "3306",
                ["database.user"] = "root",
                ["database.pass"] = "password",
                ["database.target"] = "titan-main-api"
            };
        }

        private static Dictionary<string, string> m_ValuesRegistry = new();

        public static void Setup(Dictionary<string, string> registry)
            => m_ValuesRegistry = registry;

        public static Dictionary<string, string> GetValues()
            => m_ValuesRegistry;

        public static bool WasLoaded()
            => GetValues().Count > 0;

        public static bool Has(string key)
            => GetValues().ContainsKey(key);

        public static object? Get(string key, object? defaultValue = default)
        {
            if (Has(key))
            {
                return GetValues()[key];
            }

            Terminal.TerminalService.Warning($"Trying to take not defined env variable: {key}");
            return defaultValue;
        }
    }
}

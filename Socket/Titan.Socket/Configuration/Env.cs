using Titan.Socket.Terminal;

namespace Titan.Socket.Configuration
{
    internal class Env
    {
        static Env()
        {
            TerminalService.Trace("Loading environment");
            EnvFile.Reload();
        }

        ~Env()
        {
            TerminalService.Trace("Environment unload");
            EnvFile.Unload();
        }

        public static bool Has(string key)
            => EnvStorage.Has(key);

        public static T? Get<T>(string key, T? defaultValue = default)
        {
            if (!EnvStorage.WasLoaded())
            {
                EnvFile.Reload();
            }

            var data = EnvStorage.Get(key, defaultValue);

            if(data == null)
            {
                return defaultValue;
            }

            if(typeof(T) == typeof(int))
            {
                return (T)(object)int.Parse((string)data);
            }

            return (T)data;
        }
    }
}

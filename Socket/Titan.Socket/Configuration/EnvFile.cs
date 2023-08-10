namespace Titan.Socket.Configuration
{
    internal class EnvFile
    {
        public static void Load()
        {
            if (IsCorrupted())
            {
                CreateDefault();
                Load();
            }

            var lines = File.ReadAllLines(".env");
            var dictionary = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                var split = line.Split('=');

                if (split != null && split.Length == 2)
                {
                    dictionary.Add(split[0], split[1]);
                }
            }

            EnvStorage.Setup(dictionary);
        }

        public static void Unload()
        {
            var values = EnvStorage.GetValues();

            if (values.Count > 0)
            {
                Drop(values);
            }
        }

        public static void Reload()
        {
            Unload();
            Load();
        }

        private static void Drop(Dictionary<string, string> values)
        {
            string[] buffer = new string[values.Count];

            for (var i = 0; i < values.Count; i++)
            {
                var pair = values.ElementAt(i);
                buffer[i] = $"{pair.Key}={pair.Value}";
            }

            File.WriteAllLines(".env", buffer);
        }

        private static bool IsCorrupted()
            => !File.Exists(".env");

        private static void CreateDefault()
            => Drop(EnvStorage.GetStructure());
    }
}

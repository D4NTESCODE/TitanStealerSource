namespace Titan.Socket.Analyzer.Records
{
    internal record CookieRecord
    {
        public string Host { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Path { get; set; }

        public CookieRecord(string host, string name, string value, string path)
        {
            Host = host;
            Name = name;
            Value = value;
            Path = path;
        }

        public override string ToString()
        {
            return $"-- Host: {Host} *#* Name: {Name} *#* Value: {Value} *#* Path: {Path}";
        }
    }
}

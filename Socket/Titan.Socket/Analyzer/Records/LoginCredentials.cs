namespace Titan.Socket.Analyzer.Records
{
    internal record LoginCredentials
    {
        public string Url { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public LoginCredentials(string url, string user, string password)
        {
            Url = url;
            User = user;
            Password = password;
        }

        public override string ToString()
        {
            return $"-- URL: {Url} *#* " + $"Login: {User} *#* " + $"Password: {Password}";
        }
    }
}

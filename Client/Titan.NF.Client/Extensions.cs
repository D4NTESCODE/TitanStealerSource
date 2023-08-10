using System;
using System.Text;

namespace Titan.NF.Client
{
    public static class Extensions
    {
        public static string FromBase64(this string value)
        {
            return Encoding.ASCII.GetString(Convert.FromBase64String(value));
        }

        public static string ToHexWithoutDashes(this byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", string.Empty);
        }
    }
}

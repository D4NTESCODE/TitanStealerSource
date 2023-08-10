namespace Titan.Socket.Terminal
{
    internal class TerminalService
    {
        public static void Error(string message, bool newLine = true)
            => WriteStylized($"[!] {message}", ConsoleColor.Red, newLine);

        public static void Warning(string message, bool newLine = true)
            => WriteStylized($"[*] {message}", ConsoleColor.Yellow, newLine);

        public static void Success(string message, bool newLine = true)
            => WriteStylized($"[+] {message}", ConsoleColor.Green, newLine);

        public static void Trace(string message, bool newLine = true)
            => WriteStylized($"[.] {message}", ConsoleColor.Gray, newLine);

        private static void WriteStylized(string message, ConsoleColor color, bool newLine = true)
        {
            Console.ForegroundColor = color;

            if (newLine)
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.Write(message);
            }

            Console.ResetColor();
        }
    }
}

namespace OuchDL
{
    internal class Program
    {
        static DateTime LastActionTime;

        static Program()
        {
            LastActionTime = DateTime.Now;
        }

        static void Main(string[] args)
        {
            Console.WriteLine($"OuchDL {typeof(Program).Assembly.GetName().Version}");
        }

        public static void Log(string message)
        {
            DateTime dateTime = DateTime.Now;
            Console.WriteLine($"[{dateTime:hh.mm.ss.fff}]{message}[+{(int)(dateTime - LastActionTime).TotalMilliseconds}ms]");
            LastActionTime = dateTime;
        }
    }
}

namespace Ouch
{
    internal static class Program
    {
        static DateTime LastActionTime = DateTime.Now;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Log("Initializing Application...");
            ApplicationConfiguration.Initialize();
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.Run(new Form1());
        }

        public static void Log(string message)
        {
            DateTime dateTime = DateTime.Now;
            Console.WriteLine($"[{dateTime:hh.mm.ss.fff}]{message}[+{(int)(dateTime - LastActionTime).TotalMilliseconds}ms]");
            LastActionTime = dateTime;
        }
    }
}
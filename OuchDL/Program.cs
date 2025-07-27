using System.Text.Json;

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
            if(args.Length != 3)
            {
                PrintHelp();
                return;
            }
            string jsonFile = args[0];
            string language = args[1];
            string outputFolder = args[2];
            Log("Initializing...");
            if (!File.Exists(jsonFile))
            {
                Console.WriteLine($"Error: JSON file '{jsonFile}' does not exist.");
                return;
            }
            if(!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }
            Log("Parsing JSON file...");
            var jsonContent = File.ReadAllText(jsonFile);
            var jsonDoc = JsonDocument.Parse(jsonContent);
            List<JsonElement> FileList = [];
            Log("Searching for files...");
            foreach (var element in jsonDoc.RootElement.EnumerateArray())
            {
                var lang = element.GetProperty("newsletters").EnumerateArray().ToList();
                foreach (var file in lang)
                {
                    if (file.GetProperty("language").GetString().Contains(language)) {
                        FileList = [.. FileList, .. file.GetProperty("file").EnumerateArray()];
                    }
                }
            }
            Log($"Found {FileList.Count} files for language '{language}'.");

        }

        public static void Log(string message)
        {
            DateTime dateTime = DateTime.Now;
            Console.WriteLine($"[{dateTime:hh.mm.ss.fff}]{message}[+{(int)(dateTime - LastActionTime).TotalMilliseconds}ms]");
            LastActionTime = dateTime;
        }

        static void PrintHelp()
        {
            Console.WriteLine("Usage: OuchDL JsonFile Language OutputFolder");
            Console.WriteLine("e.g. OuchDL ouch.json English output");
        }

    }
}

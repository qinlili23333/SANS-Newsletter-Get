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
            foreach (var file in FileList)
            {
                string fileName = file.GetProperty("filename").GetString().Replace(".pdf",$"_{file.GetProperty("uid").GetString()}.pdf");
                string fileUrl = file.GetProperty("url").GetString();
                if(File.Exists(Path.Combine(outputFolder, fileName)))
                {
                    Log($"File {fileName} already exists, skipping download.");
                    continue;
                }
                Log($"Downloading {fileName}...");
                try
                {
                    using (var client = new HttpClient())
                    {
                        var response = client.GetAsync(fileUrl).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var content = response.Content.ReadAsByteArrayAsync().Result;
                            string outputPath = Path.Combine(outputFolder, fileName);
                            File.WriteAllBytes(outputPath, content);
                            Log($"Downloaded and saved to {outputPath}");
                        }
                        else
                        {
                            Log($"Failed to download {fileName}: {response.ReasonPhrase}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log($"Error downloading {fileName}: {ex.Message}");
                }
            }
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

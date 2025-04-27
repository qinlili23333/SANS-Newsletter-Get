namespace Ouch
{
    public class WV2Bridge(Form1 Form)
    {
        Form1 Form1 { get; set; } = Form;

        public void Log(string message)
        {
            Form1.Log(message);
        }

        public void WriteToDisk(string filename, string content)
        {
            if (filename.Contains('/') || filename.Contains('\\'))
            {
                throw new Exception("Want path travesal? Fuck you idiot!");
            }
            Directory.CreateDirectory("output");
            File.WriteAllText("output\\" + filename, content);
        }

        public int Format()
        {
            if (Environment.GetCommandLineArgs().Length>1 && Int32.Parse(Environment.GetCommandLineArgs()[1])>0)
            {
                return Int32.Parse(Environment.GetCommandLineArgs()[1]);
            }
            return 0;
        }

        public void Exit()
        {
            Form1.Close();
        }
    }
}

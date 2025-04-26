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

        public void Exit()
        {
            Form1.Close();
        }
    }
}

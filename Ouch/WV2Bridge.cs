namespace Ouch
{
    public class WV2Bridge(Form1 Form)
    {
        Form1 Form1 { get; set; } = Form;

        public void Log(string message)
        {
            Form1.Log(message);
        }
    }
}

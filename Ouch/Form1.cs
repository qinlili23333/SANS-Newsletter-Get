using Microsoft.Web.WebView2.Core;

namespace Ouch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            // I want it to run without GPU
            var WebviewArgu = "--disable-features=msSmartScreenProtection --disable-gpu --disable-gpu-compositing --renderer-process-limit=1";
            CoreWebView2EnvironmentOptions options = new()
            {
                AdditionalBrowserArguments = WebviewArgu
            };
            Directory.CreateDirectory(Environment.CurrentDirectory + @"\QinliliWebview2\");
            var webView2Environment = await CoreWebView2Environment.CreateAsync(null, Environment.CurrentDirectory + @"\QinliliWebview2\", options);
            await WebView.EnsureCoreWebView2Async(webView2Environment);
            WebView.Enabled = true;
            WebView.CoreWebView2.Settings.IsStatusBarEnabled = false;
            WebView.CoreWebView2.Settings.IsBuiltInErrorPageEnabled = false;
            WebView.CoreWebView2.Navigate("https://www.sans.org/newsletters/ouch/");
            // Image is useless for me, disable to save data
            WebView.CoreWebView2.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.Image);
            WebView.CoreWebView2.WebResourceRequested += delegate (
               object? sender, CoreWebView2WebResourceRequestedEventArgs args)
            {
                CoreWebView2WebResourceResponse response = WebView.CoreWebView2.Environment.CreateWebResourceResponse(null, 200, "OK", "Content-Type: image/png");
                args.Response = response;
            };
        }
    }
}

using CefSharp.Wpf;
using System;
using System.IO;
using System.Security.Policy;

namespace WebClientRuntime
{

    internal class Program
    {
        static ChromiumWebBrowser browser;

        [STAThread]
        static void Main(string[] args)
        {

            browser = new ChromiumWebBrowser();
            _d_event(browser);
            if (args.Length == 0)
            {

               
                while (true)
                {
                    browser.Address = Console.ReadLine();
                    
                    Console.WriteLine("ViewSource");
                }
            }
            else
            {
                browser.Address = args[0];
            }


            void _d_event( ChromiumWebBrowser browser)
            {
                

                browser.FrameLoadEnd += (o, e) =>
                {
                    if (e.Frame.IsMain)
                    {
                        Console.WriteLine("LoadEnd");
                        browser.GetBrowser().MainFrame.GetSourceAsync().ContinueWith(taskHtml =>
                        {
                            var html = taskHtml.Result;
                            File.WriteAllText("index.html", html);
                           
                        });
                    }
                };
            }
        }
        
    }
}

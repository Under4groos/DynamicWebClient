using CefSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string[] args_ = Environment.GetCommandLineArgs();
            if(args_.Length > 1)
            {
                browser.Address = args_[1];
                browser.LoadingStateChanged += (o, e) =>
                {
                    if (e.IsLoading == false)
                        browser.GetBrowser().MainFrame.GetSourceAsync().ContinueWith(taskHtml =>
                        {
                            var html = taskHtml.Result;
                            File.WriteAllText("index.html", html);

                        });


                };
                if (args_[3] != string.Empty && Directory.Exists(args_[3]))
                {
                    browser.ExecuteScriptAsync(File.ReadAllText(args_[3]));

                }
                
            }
            
        } 
    }  
}

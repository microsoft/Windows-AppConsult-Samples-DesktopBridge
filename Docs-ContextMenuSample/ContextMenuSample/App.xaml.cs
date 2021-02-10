using System;
using System.Windows;

namespace ContextMenuSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string FilePath { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Length > 0)
            {
                FilePath = e.Args[0];

            }

            this.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
        }
    }
}

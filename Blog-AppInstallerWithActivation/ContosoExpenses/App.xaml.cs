using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace ContosoExpenses
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string Arguments { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}//AppInstaller.txt";


            if (e.Args.Length > 0)
            {
                UriBuilder builder = new UriBuilder(e.Args[0]);
                var result = HttpUtility.ParseQueryString(builder.Query);
                var value = result["source"];

                Arguments = $"Source: {value}";

                System.IO.File.WriteAllText(path, $"Source: {value}");
            }
            else
            {
                Arguments = "No arguments available";

                System.IO.File.WriteAllText(path, "No arguments available");
            }
        }
    }
}

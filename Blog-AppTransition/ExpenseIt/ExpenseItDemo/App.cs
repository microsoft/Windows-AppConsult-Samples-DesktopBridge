// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ExpenseItDemo.Data.Model;
using System.Windows;

namespace ExpenseItDemo
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
        public Employee SelectedEmployee { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Length > 0)
            {
                string path = e.Args[0];
                ViewReport viewReport = new ViewReport() { ReportPath = path };
                viewReport.ShowDialog();
            }  
          
        }
    }
}
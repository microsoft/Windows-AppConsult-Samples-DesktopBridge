using System.IO;
using System.Windows;

namespace ExpenseItDemo
{
    /// <summary>
    /// Interaction logic for ViewReport.xaml
    /// </summary>
    public partial class ViewReport : Window
    {
        public string ReportPath { get; set; }

        public ViewReport()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string report = File.ReadAllText(ReportPath);
            EmployeeReport.Text = report;
        }
    }
}

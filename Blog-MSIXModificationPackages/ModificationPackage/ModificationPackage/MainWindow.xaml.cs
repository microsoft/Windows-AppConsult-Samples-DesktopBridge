using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace ModificationPackage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnReadTextFile(object sender, RoutedEventArgs e)
        {
            string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)}\\ModificationPackage\\test.txt";
            FilePath.Text = path;
            if (File.Exists(path))
            {
                string content = File.ReadAllText(path);
                FileContent.Text = content;
            }
            else
            {
                FileContent.Text = "File is missing";
            }
        }
    }
}

using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;

namespace PSFDemo
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

        private void OnReadFile(object sender, RoutedEventArgs e)
        {
            string filePath = $"{Environment.CurrentDirectory}/app.json";
            string json = File.ReadAllText(filePath);
            Config config = JsonConvert.DeserializeObject<Config>(json);
            AppName.Text = config.AppName;
            Version.Text = config.Version;

            MessageBox.Show("Configuration has been read successfully", "PSFDemo");
        }

        private void OnWriteFile(object sender, RoutedEventArgs e)
        {
            Config config = new Config
            {
                AppName = AppName.Text,
                Version = Version.Text
            };

            string json = JsonConvert.SerializeObject(config);

            string filePath = $"{Environment.CurrentDirectory}/app.json";
            File.WriteAllText(filePath, json);
        }
    }
}

using MyEmployees.Entities;
using MyEmployees.PluginInterface;
using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ExportDataLibrary
{
    public partial class Form1 : Form
    {
        IPlugin plugin;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadPlugins();
            LoadData();
        }


        public void LoadPlugins() 
        {
            try
            {
                string dllPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\Contoso\\MyEmployees\\Plugins\\ExportDataLibrary.dll";
                plugin = LoadAssembly(dllPath);
                exportAsCsvButton.Visible = true;
            }
            catch (Exception)
            {
                exportMenu.Visible = false;
            }
        }

        private void LoadData()
        {
            string result = Assembly.GetExecutingAssembly().Location;
            int index = result.LastIndexOf("\\");
            string dbPath = $"{result.Substring(0, index)}\\Employees.db";

            SQLiteConnection connection = new SQLiteConnection($"Data Source= {dbPath}");
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT * FROM Employees";
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            EmployeeId = int.Parse(reader[0].ToString()),
                            FirstName = reader[1].ToString(),
                            LastName = reader[2].ToString(),
                            Email = reader[3].ToString()
                        };

                        employeeBindingSource.Add(employee);
                    }
                }
            }

            dataGridView1.DataSource = employeeBindingSource;
        }

        private void exportAsCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV|*.csv";
            saveFileDialog.Title = "Save a CSV file";
            saveFileDialog.ShowDialog();

            bool isFileSaved = plugin.Execute(employeeBindingSource.List, saveFileDialog.FileName);
            if (isFileSaved)
            {
                MessageBox.Show("The CSV file has been exported with success");
            }
            else
            {
                MessageBox.Show("The export operation has failed");
            }
        }

        private IPlugin LoadAssembly(string assemblyPath)
        {
            string assembly = Path.GetFullPath(assemblyPath);
            Assembly ptrAssembly = Assembly.LoadFile(assembly);
            foreach (Type item in ptrAssembly.GetTypes())
            {
                if (!item.IsClass) continue;
                if (item.GetInterfaces().Contains(typeof(IPlugin)))
                {
                    return (IPlugin)Activator.CreateInstance(item);
                }
            }
            throw new Exception("Invalid DLL, Interface not found!");
        }

        //private async Task GetOptionalPackages()
        //{
        //    Debug.WriteLine($"Number of dependencies: {Package.Current.Dependencies.Count}");
        //    foreach (var package in Package.Current.Dependencies)
        //    {
        //        Debug.WriteLine(package.Id.FullName);
        //        var folder = await package.InstalledLocation.GetFolderAsync("VFS\\ProgramFilesX86\\Contoso\\MyEmployees\\Plugins\\");
        //        var file = await folder.GetFileAsync("ExportDataLibrary.dll");
        //        plugin = LoadAssembly(file.Path);
        //        exportAsCsvButton.Visible = true;
        //    }
        //}
    }
}

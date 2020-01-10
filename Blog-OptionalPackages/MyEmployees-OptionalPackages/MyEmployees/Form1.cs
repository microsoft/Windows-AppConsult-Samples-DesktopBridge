using MyEmployees.Entities;
using MyEmployees.PluginInterface;
using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.ApplicationModel;

namespace ExportDataLibrary
{
    public partial class Form1 : Form
    {
        IPlugin plugin;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadOptionalPackagesAsync();
            LoadData();

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

        private async Task LoadOptionalPackagesAsync()
        {
            if (Package.Current.Dependencies.Count > 0)
            {
                foreach (var package in Package.Current.Dependencies)
                {
                    if (package.Id.Name.Contains("ExportPlugin"))
                    {
                        var file = await package.InstalledLocation.GetFileAsync("VFS\\ProgramFilesX86\\Contoso\\MyEmployees\\Plugins\\ExportDataLibrary.dll");
                        plugin = LoadAssembly(file.Path);
                        exportAsCsvButton.Visible = true;
                    }
                }
            }
            else
            {
                exportAsCsvButton.Visible = false;
            }
        }
    }
}

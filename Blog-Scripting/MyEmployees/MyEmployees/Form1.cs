using Microsoft.Win32;
using MyEmployees.Entities;
using Newtonsoft.Json;
using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportDataLibrary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Uncomment this line if you want to enable the database copy within the app
            //CopyDatabase();
            LoadData();        
        }

        private void CopyDatabase()
        {
            string result = Assembly.GetExecutingAssembly().Location;
            int index = result.LastIndexOf("\\");
            string dbPath = $"{result.Substring(0, index)}\\Employees.db";

            string destinationPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\\MyEmployees\\Employees.db";
            string destinationFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\\MyEmployees\\";

            if (!File.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationFolder);
                File.Copy(dbPath, destinationPath, true);
            }
        }

        private void LoadData()
        {
            string dbPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\\MyEmployees\\Employees.db";
            if (File.Exists(dbPath))
            {

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
        }
    }
}

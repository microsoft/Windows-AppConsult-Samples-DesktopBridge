using MyEmployees.Entities;
using System;
using System.Data.SQLite;
using System.Reflection;
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
    }
}

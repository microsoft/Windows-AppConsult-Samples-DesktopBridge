using ExpenseItDemo.Data.Model;
using System.Collections.Generic;
using System.Data.OleDb;

namespace ExpenseItDemo.Data.Services
{
    public class DatabaseService
    {
        private OleDbConnection connection;
        public DatabaseService()
        {
            connection = new OleDbConnection();
            string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            int index = location.LastIndexOf("\\");
            string path = $"{location.Substring(0, index)}\\Expenses.mdb";
            connection.ConnectionString = $"Provider = Microsoft.Jet.Oledb.4.0; Data Source = {path}";
        }

        public List<Employee> GetEmployees()
        {
            OleDbCommand command = new OleDbCommand();
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            List<Employee> employees = new List<Employee>();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM Employees";
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Employee employee = new Employee
                    {
                        EmployeeId = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Type = reader.GetString(3),
                        Email = reader.GetString(4)
                    };

                    employees.Add(employee);
                }
            }

            return employees;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            OleDbCommand command = new OleDbCommand();
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            Employee employee = null;

            command.Connection = connection;
            command.CommandText = "SELECT * FROM Employees WHERE Id=@EmployeeId";
            command.Parameters.AddWithValue("EmployeeId", employeeId);
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    employee = new Employee
                    {
                        EmployeeId = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Type = reader.GetString(3),
                        Email = reader.GetString(4),
                        CostCenterId = reader.GetInt32(5)
                    };
                }
            }

            return employee;
        }

        public List<Employee> GetEmployeesByType(string type)
        {
            OleDbCommand command = new OleDbCommand();
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            List<Employee> employees = new List<Employee>();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM Employees WHERE Type=@Type";
            command.Parameters.AddWithValue("Type", type);
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Employee employee = new Employee
                    {
                        EmployeeId = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Type = reader.GetString(3),
                        Email = reader.GetString(4),
                        CostCenterId = reader.GetInt32(5)
                    };

                    employees.Add(employee);
                }
            }

            return employees;
        }

        public List<Expense> GetExpensesByEmployee(int employeeId)
        {
            OleDbCommand command = new OleDbCommand();
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            List<Expense> expenses = new List<Expense>();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM Expenses WHERE EmployeeId=@EmployeeId";
            command.Parameters.AddWithValue("EmployeeId", employeeId);
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Expense expense = new Expense();
                    expense.ExpenseId = reader.GetInt32(0);
                    expense.Type = reader.GetString(1);
                    expense.Description = reader.GetString(2);
                    expense.Cost = reader.GetDouble(3);
                    expense.EmployeeId = employeeId;

                    expenses.Add(expense);
                }
            }

            return expenses;
        }
    }
}

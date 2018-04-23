// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ExpenseItDemo.Data.Model;
using ExpenseItDemo.Data.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

// EventHandler
// ObservableCollection

// INotifyPropertyChanged, PropertyChangedEventArgs

namespace ExpenseItDemo
{
    public class ExpenseReport : INotifyPropertyChanged
    {
        private string _email;
        private int _costCenterId;
        private string _employeeId;
        private double _totalExpenses;

        public ExpenseReport(int employeeId)
        {
            DatabaseService dbService = new DatabaseService();
            Employee employee = dbService.GetEmployeeById(employeeId);

            Email = employee.Email;
            CostCenterId = employee.CostCenterId;
            EmployeeId = employeeId.ToString();

            List<Expense> expenses = dbService.GetExpensesByEmployee(employeeId);
            Expenses = new ExpensesCollection(expenses);
            Expenses.ExpenseCostChanged += OnExpenseCostChanged;
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        public int CostCenterId
        {
            get { return _costCenterId; }
            set
            {
                _costCenterId = value;
                OnPropertyChanged("CostCenterId");
            }
        }

        public string EmployeeId
        {
            get { return _employeeId; }
            set
            {
                _employeeId = value;
                OnPropertyChanged("EmployeeId");
            }
        }

        public double TotalExpenses
        {
            // calculated property, no setter
            get
            {
                RecalculateTotalExpense();
                return _totalExpenses;
            }
        }

        public ExpensesCollection Expenses { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnExpenseCostChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("TotalExpenses");
        }

        private void RecalculateTotalExpense()
        {
            _totalExpenses = 0;
            foreach (var item in Expenses)
                _totalExpenses += item.Cost;
        }

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
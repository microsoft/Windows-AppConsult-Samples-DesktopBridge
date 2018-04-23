// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ExpenseItDemo.Data.Model;
using ExpenseItDemo.Data.Services;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace ExpenseItDemo
{
    /// <summary>
    ///     Interaction logic for CreateExpenseReportDialogBox.xaml
    /// </summary>
    public partial class CreateExpenseReportDialogBox : Window
    {
        private Employee CurrentEmployee;
        private DatabaseService dbService;
        public ExpenseReport Report { get; set; }

        public CreateExpenseReportDialogBox()
        {
            InitializeComponent();
            dbService = new DatabaseService();
        }

        private void addExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            var app = Application.Current;
        }

        private void viewChartButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ViewChartWindow {Owner = this};
            dlg.Show();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Reports|*.rep";
            saveFileDialog.Title = "Save the report";
            saveFileDialog.ShowDialog();

            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                Stream fileStream = saveFileDialog.OpenFile();
                using (TextWriter writer = new StreamWriter(fileStream))
                {
                    writer.WriteLine($"Employee id: {CurrentEmployee.EmployeeId}");
                    writer.WriteLine($"Employee name: {CurrentEmployee.FirstName} {CurrentEmployee.LastName}");
                    writer.WriteLine($"Employee mail: {CurrentEmployee.Email}");
                    writer.WriteLine();
                    writer.WriteLine("Expenses:");
                    writer.WriteLine();
                    foreach (var expense in Report.Expenses)
                    {
                        writer.WriteLine($"{expense.Description} - Price: {expense.Cost} $");
                    }

                    writer.WriteLine($"Total: {Report.TotalExpenses} $");
                    writer.Flush(); 
                }

                MessageBox.Show(
                "Expense Report Created!",
                "ExpenseIt Standalone",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

                DialogResult = true;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            CurrentEmployee = (Application.Current as App).SelectedEmployee;
            Report = new ExpenseReport(CurrentEmployee.EmployeeId);
            this.DataContext = Report;
        }
    }
}
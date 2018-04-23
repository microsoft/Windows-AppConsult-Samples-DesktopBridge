// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ExpenseItDemo.Data.Model;
using System.Windows;

namespace ExpenseItDemo
{
    /// <summary>
    ///     Interaction logic for ViewChartWindow.xaml
    /// </summary>
    public partial class ViewChartWindow : Window
    {
        private Employee CurrentEmployee;

        public ViewChartWindow()
        {
            InitializeComponent();
            CurrentEmployee = (Application.Current as App).SelectedEmployee;
            ExpenseReport report = new ExpenseReport(CurrentEmployee.EmployeeId);
            this.DataContext = report;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
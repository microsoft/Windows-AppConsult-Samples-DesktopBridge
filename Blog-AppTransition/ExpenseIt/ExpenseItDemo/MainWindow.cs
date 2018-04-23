// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ExpenseItDemo.Data.Model;
using ExpenseItDemo.Data.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ExpenseItDemo
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static MainWindow()
        {
            // Define CreateExpenseReportCommand
            CreateExpenseReportCommand = new RoutedUICommand("_Create Expense Report...", "CreateExpenseReport",
                typeof(MainWindow));
            CreateExpenseReportCommand.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Control | ModifierKeys.Shift));

            // Define ExitCommand
            ExitCommand = new RoutedUICommand("E_xit", "Exit", typeof(MainWindow));

            // Define AboutCommand
            AboutCommand = new RoutedUICommand("_About ExpenseIt Standalone", "About", typeof(MainWindow));
        }

        public MainWindow()
        {
            Initialized += MainWindow_Initialized;

            InitializeComponent();

            employeeTypeRadioButtons.SelectionChanged += employeeTypeRadioButtons_SelectionChanged;

            // Bind CreateExpenseReportCommand
            var commandBindingCreateExpenseReport = new CommandBinding(CreateExpenseReportCommand);
            commandBindingCreateExpenseReport.Executed += commandBindingCreateExpenseReport_Executed;
            CommandBindings.Add(commandBindingCreateExpenseReport);

            // Bind ExitCommand
            var commandBindingExitCommand = new CommandBinding(ExitCommand);
            commandBindingExitCommand.Executed += commandBindingExitCommand_Executed;
            CommandBindings.Add(commandBindingExitCommand);

            // Bind AboutCommand
            var commandBindingAboutCommand = new CommandBinding(AboutCommand);
            commandBindingAboutCommand.Executed += commandBindingAboutCommand_Executed;
            CommandBindings.Add(commandBindingAboutCommand);
        }

        private void MainWindow_Initialized(object sender, EventArgs e)
        {
            DesktopBridge.Helpers helpers = new DesktopBridge.Helpers();
            if (helpers.IsRunningAsUwp())
            {
                this.Title = "ExpenseIt - Desktop Bridge";
            }

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AppConsult\ExpenseIt", true);
            if (key == null)
            {
                key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\AppConsult\ExpenseIt", RegistryKeyPermissionCheck.ReadWriteSubTree);
            }

            var count = key.GetValue("LaunchesCount");
            if (count != null)
            {
                int total = (int)count;
                key.SetValue("LaunchesCount", total + 1);
            }
            else
            {
                key.SetValue("LaunchesCount", 1);
            }

            // Select the first employee type radio button
            employeeTypeRadioButtons.SelectedIndex = 0;
            RefreshEmployeeList();
        }

        private void commandBindingCreateExpenseReport_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (EmployeesList.SelectedItem == null)
            {
                MessageBox.Show(
               "You have to choose an employee to generate an expense report!",
               "No employee selected!",
               MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
            else
            {
                (Application.Current as App).SelectedEmployee = EmployeesList.SelectedItem as Employee;
                var dlg = new CreateExpenseReportDialogBox { Owner = this };
                dlg.ShowDialog();
            }
        }

        private void commandBindingExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void commandBindingAboutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AppConsult\ExpenseIt", true);
            string version = string.Empty;
            if (key != null)
            {
                version = $" - Version: {key.GetValue("Version").ToString()}";
            }

            MessageBox.Show(
                $"ExpenseIt Standalone Sample Application {version}, by the WPF SDK",
                "ExpenseIt Standalone",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void employeeTypeRadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshEmployeeList();
        }

        /// <summary>
        ///     Select the employees who have the employment type that is specified
        ///     by the currently checked employee type radio button
        /// </summary>
        private void RefreshEmployeeList()
        {
            var selectedItem = employeeTypeRadioButtons.SelectedItem as ListBoxItem;

            DatabaseService dbService = new DatabaseService();
            List<Employee> employees = dbService.GetEmployeesByType(selectedItem.Content.ToString());
            EmployeesList.ItemsSource = employees;
        }

        #region Commands

        public static RoutedUICommand CreateExpenseReportCommand;
        public static RoutedUICommand ExitCommand;
        public static RoutedUICommand AboutCommand;

        #endregion
    }
}
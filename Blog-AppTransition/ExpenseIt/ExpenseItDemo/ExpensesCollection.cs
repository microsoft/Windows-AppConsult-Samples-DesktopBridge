// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ExpenseItDemo.Data.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ExpenseItDemo
{
    public class ExpensesCollection : ObservableCollection<Expense>
    {
        public ExpensesCollection(IEnumerable<Expense> expenses)
        {
            foreach (Expense expense in expenses)
            {
                Add(expense);
            }
        }

        public event EventHandler ExpenseCostChanged;

        public new void Add(Expense item)
        {
            if (item != null)
            {
                item.PropertyChanged += ExpensePropertyChanged;
            }
            base.Add(item);
        }

        private void ExpensePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Cost")
            {
                OnExpenseCostChanged(this, new EventArgs());
            }
        }

        private void OnExpenseCostChanged(object sender, EventArgs args)
        {
            ExpenseCostChanged?.Invoke(sender, args);
        }
    }
}
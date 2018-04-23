using System.ComponentModel;

namespace ExpenseItDemo.Data.Model
{
    public class Expense: INotifyPropertyChanged
    {
        private double _cost;
        private string _description;
        private string _type;
        private int _expenseId;
        private int _employeeId;

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public double Cost
        {
            get { return _cost; }
            set
            {
                _cost = value;
                OnPropertyChanged("Cost");
            }
        }

        
        public int ExpenseId
        {
            get { return _expenseId; }
            set
            {
                _expenseId = value;
                OnPropertyChanged("ExpenseId");
            }
        }

        public int EmployeeId
        {
            get { return _employeeId; }
            set
            {
                _employeeId = value;
                OnPropertyChanged("EmployeeId");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

       

    }
}

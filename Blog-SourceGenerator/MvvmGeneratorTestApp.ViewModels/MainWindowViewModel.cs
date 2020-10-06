using MvvmGenerator;

namespace MvvmGeneratorTestApp.VIewModels
{
    public partial class MainWindowViewModel
    {
        [AutoNotify]
        private string _firstName;
        [AutoNotify]
        private string _lastName;
        [AutoNotify]
        public string FullName => $"{FirstName} {LastName}";
    }
}

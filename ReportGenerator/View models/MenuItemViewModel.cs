namespace ReportGenerator
{
    using System.Windows.Input;

    public class MenuItemViewModel : MenuItemViewModelBase
    {
        private bool enabled = true;
        private bool selected = false;

        public bool Enabled
        {
            get => enabled;
            set
            {
                enabled = value;
                OnPropertyChanged();
            }
        }

        public bool Selected
        {
            get => selected;
            set
            {
                selected = value;
                OnPropertyChanged();
            }
        }

        public string HeaderText { get; set; }

        public ICommand ExecuteCommand { get; set; }

        public object Tag { get; set; }

        public string IconName { get; set; }
    }
}

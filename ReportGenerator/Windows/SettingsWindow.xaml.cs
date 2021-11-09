namespace ReportGenerator
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow(ISettingsViewModel settingsViewModel)
        {
            this.DataContext = settingsViewModel;
            InitializeComponent();
        }
    }
}

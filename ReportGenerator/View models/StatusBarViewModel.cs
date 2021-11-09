namespace ReportGenerator
{
    using System.ComponentModel.Composition;

    [Export(typeof(IStatusBarViewModel))]
    public class StatusBarViewModel : ViewModelBase, IStatusBarViewModel
    {
        private readonly IReportPartProviderViewModel reportPartProvider;
        private readonly IThemesViewModel themesViewModel;
        private string selectedTheme;
        private string selectedSetting;

        [ImportingConstructor]
        public StatusBarViewModel(IReportPartProviderViewModel reportPartProvider, IThemesViewModel themesViewModel)
        {
            reportPartProvider.PropertyChanged += ReportPartProvider_PropertyChanged;
            themesViewModel.SelectedThemeChanged += ThemesViewModel_SelectedThemeChanged;

            this.reportPartProvider = reportPartProvider;
            this.themesViewModel = themesViewModel;

            SetSelectedTheme();
            SetSelectedSetting();
        }

        public string SelectedTheme
        {
            get => selectedTheme;
            set
            {
                selectedTheme = value;
                OnPropertyChanged();
            }
        }

        public string SelectedSetting
        {
            get => selectedSetting;
            set
            {
                selectedSetting = value;
                OnPropertyChanged();
            }
        }

        private void SetSelectedTheme()
        {
            SelectedTheme = themesViewModel.SelectedTheme.Name;
        }

        private void SetSelectedSetting()
        {
            SelectedSetting = reportPartProvider.SelectedSettings;
        }

        private void ThemesViewModel_SelectedThemeChanged(object sender, System.Collections.Generic.List<ThemeColour> e)
        {
            SetSelectedTheme();
        }

        private void ReportPartProvider_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IReportPartProviderViewModel.SelectedSettings))
            {
                SetSelectedSetting();
            }
        }
    }
}

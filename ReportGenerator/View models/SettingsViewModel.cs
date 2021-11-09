namespace ReportGenerator
{
    using System.ComponentModel.Composition;

    [Export(typeof(ISettingsViewModel))]
    [Export(typeof(INavigateAware))]
    public class SettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        private bool ready;

        [ImportingConstructor] // IReportPartProviderViewModel this should be the exact view model !
        public SettingsViewModel(
            IStatusBarViewModel statusBarViewModel,
            ISettingsMenuViewModel settingsMenuViewModel,
            ISelectableReportPartsViewModel selectableReportPartsViewModel)
        {
            StatusBarViewModel = statusBarViewModel;
            SettingsMenuViewModel = settingsMenuViewModel;
            SelectableReportPartsViewModel = selectableReportPartsViewModel;
        }

        public bool Ready
        {
            get => ready;
            set
            {
                ready = value;
                OnPropertyChanged();
            }
        }

        public IStatusBarViewModel StatusBarViewModel { get; }

        public ISettingsMenuViewModel SettingsMenuViewModel { get; }

        public ISelectableReportPartsViewModel SelectableReportPartsViewModel { get; }

        public void Navigated()
        {
            Ready = true;
        }
    }
}

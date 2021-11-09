namespace ReportGenerator
{
    public class DesignTimeSettingsViewModel : ISettingsViewModel
    {
        public bool Ready => true;

        public IStatusBarViewModel StatusBarViewModel => new DesignTimeStatusBarViewModel();

        public ISettingsMenuViewModel SettingsMenuViewModel => new DesignTimeSettingsMenuViewModel();

        public ISelectableReportPartsViewModel SelectableReportPartsViewModel => new DesignTimeSelectableReportPartsViewModel();

        public void Navigated()
        {
        }
    }
}

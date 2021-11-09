namespace ReportGenerator
{
    public interface ISettingsViewModel : INavigateAware
    {
        IStatusBarViewModel StatusBarViewModel { get; }

        ISettingsMenuViewModel SettingsMenuViewModel { get; }

        ISelectableReportPartsViewModel SelectableReportPartsViewModel { get; }

        bool Ready { get; }
    }
}

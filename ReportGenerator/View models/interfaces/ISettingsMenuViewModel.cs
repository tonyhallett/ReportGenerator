namespace ReportGenerator
{
    using System.Collections.ObjectModel;

    public interface ISettingsMenuViewModel
    {
        ObservableCollection<RootMenuItemViewModel> MenuItems { get; }
    }
}

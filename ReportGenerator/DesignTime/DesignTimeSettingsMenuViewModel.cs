namespace ReportGenerator
{
    using System.Collections.ObjectModel;

    public class DesignTimeSettingsMenuViewModel : ISettingsMenuViewModel
    {
        public ObservableCollection<RootMenuItemViewModel> MenuItems => new ObservableCollection<RootMenuItemViewModel>
        {
            new RootMenuItemViewModel { HeaderText = "Menu1" },
            new RootMenuItemViewModel { HeaderText = "Menu2" },
        };
    }
}

namespace ReportGenerator
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class RootMenuItemViewModel
    {
        public string HeaderText { get; set; }

        public ObservableCollection<MenuItemViewModelBase> Children { get; } = new ObservableCollection<MenuItemViewModelBase>();

        public IEnumerable<MenuItemViewModel> MenuItemViewModels => Children.OfType<MenuItemViewModel>();
    }
}

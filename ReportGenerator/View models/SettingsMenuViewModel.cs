namespace ReportGenerator
{
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition;
    using System.Linq;

    [Export(typeof(ISettingsMenuViewModel))]
    public class SettingsMenuViewModel : ISettingsMenuViewModel
    {
        private readonly RootMenuItemViewModel themesRootMenuItemViewModel;
        private readonly IShortcutKeysProvider shortcutKeysProvider;
        private RootMenuItemViewModel settingsRootMenuItemViewModel;
        private IReportPartProviderViewModel reportPartProviderViewModel;

        [ImportingConstructor]
        public SettingsMenuViewModel(
            IReportPartProviderViewModel reportPartProvider,
            IThemesViewModel themesViewModel,
            IProcessedReportSaverViewModel processedReportSaver,
            IReportProviderViewModel reportProviderViewModel,
            IShortcutKeysProvider shortcutKeysProvider)
        {
            this.shortcutKeysProvider = shortcutKeysProvider;
            var reportRootMenuItemViewModel = SetupReportMenu(reportProviderViewModel, processedReportSaver);
            SetupSettingsMenu(reportPartProvider);
            themesRootMenuItemViewModel = SetupThemesMenu(themesViewModel);

            MenuItems.Add(themesRootMenuItemViewModel);
            MenuItems.Add(settingsRootMenuItemViewModel);
            MenuItems.Add(reportRootMenuItemViewModel);
        }

        public ObservableCollection<RootMenuItemViewModel> MenuItems { get; } = new ObservableCollection<RootMenuItemViewModel>();

        private RootMenuItemViewModel SetupReportMenu(IReportProviderViewModel reportProviderViewModel, IProcessedReportSaverViewModel processedReportSaver)
        {
            var reportRootMenuItemViewModel = new RootMenuItemViewModel { HeaderText = "_Report" };
            reportRootMenuItemViewModel.Children.Add(new MenuItemViewModel { HeaderText = "Open", IconName = "OpenFile", ExecuteCommand = reportProviderViewModel.ChooseReportCommand });
            var useDefaultReportMenuItemViewModel = new MenuItemViewModel { HeaderText = "Use _Default", ExecuteCommand = reportProviderViewModel.UseDefaultReportCommand, Enabled = reportProviderViewModel.CanUseDefaultReport };
            reportProviderViewModel.PropertyChanged += (_, args) =>
            {
                if (args.PropertyName == nameof(IReportProviderViewModel.CanUseDefaultReport))
                {
                    useDefaultReportMenuItemViewModel.Enabled = reportProviderViewModel.CanUseDefaultReport;
                }
            };
            reportRootMenuItemViewModel.Children.Add(useDefaultReportMenuItemViewModel);
            reportRootMenuItemViewModel.Children.Add(new SeparatorViewModel());
            reportRootMenuItemViewModel.Children.Add(new MenuItemViewModel { HeaderText = "Save", IconName = "Save", ExecuteCommand = processedReportSaver.SaveCommand, Enabled = processedReportSaver.CanSaveProcessed });
            return reportRootMenuItemViewModel;
        }

        private void SetupSettingsMenu(IReportPartProviderViewModel reportPartProviderViewModel)
        {
            this.reportPartProviderViewModel = reportPartProviderViewModel;
            this.reportPartProviderViewModel.PropertyChanged += ReportPartProvider_PropertyChanged;
            reportPartProviderViewModel.Settings.CollectionChanged += Settings_CollectionChanged;

            settingsRootMenuItemViewModel = new RootMenuItemViewModel { HeaderText = "_Settings" };
            settingsRootMenuItemViewModel.Children.Add(new MenuItemViewModel { HeaderText = "Save", IconName = "Save", ExecuteCommand = reportPartProviderViewModel.SaveCommand });
            settingsRootMenuItemViewModel.Children.Add(new MenuItemViewModel { HeaderText = "Save As", IconName = "SaveAs", ExecuteCommand = reportPartProviderViewModel.SaveAsCommand });
            settingsRootMenuItemViewModel.Children.Add(new SeparatorViewModel());

            foreach (var setting in reportPartProviderViewModel.Settings)
            {
                AddSettingMenuItem(setting);
            }
        }

        private RootMenuItemViewModel SetupThemesMenu(IThemesViewModel themesViewModel)
        {
            var themesRootMenuItemViewModel = new RootMenuItemViewModel { HeaderText = "_Themes" };
            foreach (var theme in themesViewModel.Themes)
            {
                var themeMenuItemViewModel = new MenuItemViewModel { HeaderText = shortcutKeysProvider.Shortcut(theme.Name), Selected = theme == themesViewModel.SelectedTheme };
                var command = new RelayCommand(_ =>
                {
                    themesViewModel.SelectedTheme = theme;
                    UpdateSelectedTheme(themeMenuItemViewModel);
                });
                themeMenuItemViewModel.ExecuteCommand = command;
                themesRootMenuItemViewModel.Children.Add(themeMenuItemViewModel);
            }

            return themesRootMenuItemViewModel;
        }

        private void ReportPartProvider_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IReportPartProviderViewModel.SelectedSettings))
            {
                UpdateSelectedSettingMenuItemViewModels();
            }
        }

        private void UpdateSelectedSettingMenuItemViewModels()
        {
            foreach (var menuItemViewModel in settingsRootMenuItemViewModel.MenuItemViewModels)
            {
                menuItemViewModel.Selected = menuItemViewModel.HeaderText == reportPartProviderViewModel.SelectedSettings;
            }
        }

        private void AddSettingMenuItem(string setting)
        {
            var settingMenuItemViewModel = new MenuItemViewModel { HeaderText = setting, Selected = setting == reportPartProviderViewModel.SelectedSettings };
            var command = new RelayCommand(_ =>
            {
                reportPartProviderViewModel.SelectedSettings = setting;
            });
            settingMenuItemViewModel.ExecuteCommand = command;
            settingsRootMenuItemViewModel.Children.Add(settingMenuItemViewModel);
        }

        private void UpdateSelectedTheme(MenuItemViewModel themeMenuItemViewModel)
        {
            var currentSelected = themesRootMenuItemViewModel.MenuItemViewModels.SingleOrDefault(m => m.Selected);
            if (currentSelected != null)
            {
                currentSelected.Selected = false;
            }

            themeMenuItemViewModel.Selected = true;
        }

        private void Settings_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var newSettings = e.NewItems.Cast<string>();
            foreach (var newSetting in newSettings)
            {
                AddSettingMenuItem(newSetting);
            }
        }
    }
}

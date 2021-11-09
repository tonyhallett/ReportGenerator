namespace ReportGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Windows.Input;

    [Export(typeof(IReportPartProviderViewModel))]
    public class ReportPartProviderViewModel : ViewModelBase, IReportPartProviderViewModel
    {
        private readonly IThemesViewModel themesViewModel;
        private readonly ISettingsProvider settingsProvider;
        private readonly IDialogService dialogService;
        private string selectedSettings;
        private IReportPartViewModel selectedReportPart;

        [ImportingConstructor]
        public ReportPartProviderViewModel(IThemesViewModel themesViewModel, ISettingsProvider settingsProvider, IDialogService dialogService)
        {
            this.themesViewModel = themesViewModel;
            this.settingsProvider = settingsProvider;
            this.dialogService = dialogService;
            Settings = new ObservableCollection<string>(settingsProvider.Settings);
            selectedSettings = Settings[0];
            SetReportParts();
        }

        public event EventHandler ColoursChanged;

        public ObservableCollection<string> Settings { get; }

        public string SelectedSettings
        {
            get => selectedSettings;
            set
            {
                selectedSettings = value;
                UpdateReportParts();
                OnPropertyChanged();
            }
        }

        public List<IReportPartViewModel> ReportPartViewModels { get; private set; }

        public ICommand SaveCommand => new RelayCommand((_) => Save());

        public ICommand SaveAsCommand => new RelayCommand((_) => SaveAs());

        public IReportPartViewModel SelectedReportPart
        {
            get
            {
                return selectedReportPart;
            }

            set
            {
                if (SelectedReportPart != null)
                {
                    SelectedReportPart.Selected = false;
                }

                selectedReportPart = value;
                selectedReportPart.Selected = true;
            }
        }

        public void SetReportParts()
        {
            ReportPartViewModels = settingsProvider.LoadSetting(SelectedSettings).Select(rp =>
            {
                var reportPart = new ReportPartViewModel(themesViewModel, rp.SelectedThemeColourName, rp.Name);
                reportPart.SelectedThemeColourChanged += ReportPart_SelectedThemeColourChanged;
                return reportPart as IReportPartViewModel;
            }).ToList();
            SelectedReportPart = ReportPartViewModels[0];
        }

        private void UpdateReportParts()
        {
            var selectedSettings = settingsProvider.LoadSetting(SelectedSettings);
            foreach (var reportPart in selectedSettings)
            {
                var reportPartViewModel = ReportPartViewModels.Single(rp => rp.Name == reportPart.Name);
                reportPartViewModel.ThemeColourNameChanged(reportPart.SelectedThemeColourName);
            }
        }

        private void ReportPart_SelectedThemeColourChanged(object sender, EventArgs e)
        {
            ColoursChanged?.Invoke(this, EventArgs.Empty);
        }

        private void Save()
        {
            SaveAs(SelectedSettings);
        }

        private void SaveAs()
        {
            var saveAsViewModel = new SaveAsViewModel();
            var result = dialogService.Show(saveAsViewModel);
            if (result.HasValue && result.Value)
            {
                var newSettingsName = saveAsViewModel.NewSettingsName;
                SaveAs(newSettingsName);
                Settings.Add(newSettingsName);
                SelectedSettings = newSettingsName;
            }
        }

        private void SaveAs(string name)
        {
            settingsProvider.Save(name, ReportPartViewModels.Select(rp => new ReportPart { Name = rp.Name, SelectedThemeColourName = rp.SelectedThemeColour.Name }).ToList());
        }
    }
}

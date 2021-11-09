namespace ReportGenerator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    [Export(typeof(IReportViewModel))]
    [Export(typeof(INavigateAware))]
    public class ReportViewModel : ViewModelBase, IReportViewModel, INavigateAware
    {
        private readonly IReportProviderViewModel reportProviderViewModel;
        private readonly IReport report;
        private readonly IProcessedReportSaverViewModel processedReportSaver;
        private IReportPartViewModel selectedReportPart;
        private System.Windows.Media.Color mainWindowColour;
        private bool showLoadingIndicator = true;
        private bool showReport;

        [ImportingConstructor]
        public ReportViewModel(
            IReportProviderViewModel reportProviderViewModel,
            IThemesViewModel themesViewModel,
            IReportPartProviderViewModel reportPartProvider,
            IReport report,
            IProcessedReportSaverViewModel processedReportSaver)
        {
            this.processedReportSaver = processedReportSaver;
            reportProviderViewModel.ReportChanged += ReportProvider_ReportChanged;
            this.reportProviderViewModel = reportProviderViewModel;
            this.ThemesViewModel = themesViewModel;
            this.report = report;
            SetMainWindowColour();
            ThemesViewModel.SelectedThemeChanged += ThemesViewModel_Changed;

            ReportParts = reportPartProvider.ReportPartViewModels;
            report.ReportColours = new ReportColours(ReportParts);
            reportPartProvider.ColoursChanged += ReportPartProvider_ColoursChanged;
        }

        public IThemesViewModel ThemesViewModel { get; set; }

        public List<IReportPartViewModel> ReportParts { get; set; }

        public System.Windows.Media.Color MainWindowColour
        {
            get
            {
                return mainWindowColour;
            }

            set
            {
                mainWindowColour = value;
                OnPropertyChanged();
            }
        }

        public IReportPartViewModel SelectedReportPart
        {
            get => selectedReportPart;
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

        public bool ShowLoadingIndicator
        {
            get => showLoadingIndicator;
            set
            {
                showLoadingIndicator = value;
                OnPropertyChanged();
            }
        }

        public bool ShowReport
        {
            get => showReport;
            set
            {
                showReport = value;
                OnPropertyChanged();
            }
        }

        public void Navigated()
        {
            ShowLoadingIndicator = false;
            ShowReport = true;
        }

        public void Initialized()
        {
            ProcessAndNavigate(reportProviderViewModel.DefaultLightReport);
        }

        private void ReportProvider_ReportChanged(object sender, string e)
        {
            ProcessAndNavigate(e);
        }

        private void ThemesViewModel_Changed(object sender, List<ThemeColour> e)
        {
            SetMainWindowColour();
        }

        private void SetMainWindowColour()
        {
            MainWindowColour = ThemesViewModel.SelectedTheme.Colours.First(tc => tc.Name == "EnvironmentColors.EnvironmentBackgroundColorKey").Color;
        }

        private void ReportPartProvider_ColoursChanged(object sender, EventArgs e)
        {
            report.ColoursChanged();
        }

        private void ProcessAndNavigate(string htmlForProcessingPath)
        {
            var processed = report.ProcessAndNavigate(htmlForProcessingPath);
            processedReportSaver.SetProcessed(processed);
        }
    }
}

namespace ReportGenerator
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Reflection;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [Import(typeof(IReportViewModel))]
        public IReportViewModel ReportViewModel { get; set; }

        [Import(typeof(ISettingsViewModel))]
        public ISettingsViewModel SettingsViewModel { get; set; }

        [Import(typeof(IReport))]
        public IReport Report { get; set; }

        [ImportMany(typeof(INavigateAware))]
        public List<INavigateAware> NavigateAware { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var container = new CompositionContainer(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            container.ComposeParts(this);

            var reportWindow = new MainWindow
            {
                DataContext = ReportViewModel,
                NavigateAwares = NavigateAware,
            };
            reportWindow.Show();
            Report.Initialize(reportWindow);

            new SettingsWindow(SettingsViewModel);

            ReportViewModel.Initialized();
        }
    }
}

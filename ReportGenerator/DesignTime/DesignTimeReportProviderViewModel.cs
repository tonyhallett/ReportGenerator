namespace ReportGenerator
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;

    public class DesignTimeReportProviderViewModel : IReportProviderViewModel
    {
#pragma warning disable 67
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<string> ReportChanged;
#pragma warning restore 67

        public bool CanUseDefaultReport { get; set; }

        public string DefaultLightReport => "default-light";

        public ICommand ChooseReportCommand => new RelayCommand(_ => { });

        public ICommand UseDefaultReportCommand => new RelayCommand(_ => { });
    }
}

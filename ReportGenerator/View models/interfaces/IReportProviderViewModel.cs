namespace ReportGenerator
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;

    public interface IReportProviderViewModel : INotifyPropertyChanged
    {
        event EventHandler<string> ReportChanged;

        bool CanUseDefaultReport { get; set; }

        string DefaultLightReport { get; }

        ICommand ChooseReportCommand { get; }

        ICommand UseDefaultReportCommand { get; }
    }
}

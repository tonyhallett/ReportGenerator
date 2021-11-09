namespace ReportGenerator
{
    using System.ComponentModel;
    using System.Windows.Input;

    public interface IProcessedReportSaverViewModel : INotifyPropertyChanged
    {
        bool CanSaveProcessed { get; }

        ICommand SaveCommand { get; }

        void SetProcessed(string processed);
    }
}

namespace ReportGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;

    public interface IReportPartProviderViewModel : INotifyPropertyChanged
    {
        event EventHandler ColoursChanged;

        List<IReportPartViewModel> ReportPartViewModels { get; }

        IReportPartViewModel SelectedReportPart { get; set; }

        ObservableCollection<string> Settings { get; }

        string SelectedSettings { get; set; }

        ICommand SaveCommand { get; }

        ICommand SaveAsCommand { get; }
    }
}

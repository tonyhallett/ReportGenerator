namespace ReportGenerator
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Media;

    public interface IReportViewModel : INotifyPropertyChanged
    {
        Color MainWindowColour { get; set; }

        List<IReportPartViewModel> ReportParts { get; set; }

        IReportPartViewModel SelectedReportPart { get; set; }

        IThemesViewModel ThemesViewModel { get; set; }

        bool ShowLoadingIndicator { get; }

        bool ShowReport { get; }

        void Initialized();
    }
}

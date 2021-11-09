namespace ReportGenerator
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    [Export(typeof(ISelectableReportPartsViewModel))]
    public class SelectableReportPartsViewModel : ViewModelBase, ISelectableReportPartsViewModel
    {
        private readonly IReportPartProviderViewModel reportPartProviderViewModel;

        [ImportingConstructor]
        public SelectableReportPartsViewModel(IReportPartProviderViewModel reportPartProviderViewModel)
        {
            this.reportPartProviderViewModel = reportPartProviderViewModel;
            ReportPartViewModels = reportPartProviderViewModel.ReportPartViewModels;
        }

        public List<IReportPartViewModel> ReportPartViewModels { get; }

        public IReportPartViewModel SelectedReportPart
        {
            get => reportPartProviderViewModel.SelectedReportPart;
            set
            {
                reportPartProviderViewModel.SelectedReportPart = value;
                OnPropertyChanged();
            }
        }
    }
}

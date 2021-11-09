namespace ReportGenerator
{
    using System.Collections.Generic;

    public class DesignTimeSelectableReportPartsViewModel : ISelectableReportPartsViewModel
    {
        public DesignTimeSelectableReportPartsViewModel()
        {
            ReportPartViewModels = new List<IReportPartViewModel>
            {
                new DesignTimeReportPartViewModel { Name = "First", Selected = true },
            };
            SelectedReportPart = ReportPartViewModels[0];
        }

        public List<IReportPartViewModel> ReportPartViewModels { get; }

        public IReportPartViewModel SelectedReportPart { get; }
    }
}

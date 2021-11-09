namespace ReportGenerator
{
    using System.Collections.Generic;

    public interface ISelectableReportPartsViewModel
    {
        List<IReportPartViewModel> ReportPartViewModels { get; }

        IReportPartViewModel SelectedReportPart { get; }
    }
}

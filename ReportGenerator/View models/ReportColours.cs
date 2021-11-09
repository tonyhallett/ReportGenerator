namespace ReportGenerator
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ReportColours : IReportColours
    {
        private readonly List<IReportPartViewModel> reportParts;

        public ReportColours(List<IReportPartViewModel> reportParts)
        {
            this.reportParts = reportParts;
        }

        public string ScrollBarArrowColour => GetColour();

        public string ScrollBarTrackColour => GetColour();

        public string ScrollBarThumbColour => GetColour();

        public string ComboBoxColour => GetColour();

        public string ComboBoxBorderColour => GetColour();

        public string ComboBoxTextColour => GetColour();

        public string GrayCoverage => GetColour();

        public string BackgroundColour => GetColour();

        public string FontColour => GetColour();

        public string TableBorderColour => GetColour();

        public string LinkColour => GetColour();

        public string CoverageTableHeaderFontColour => GetColour();

        public string CoverageTableActiveSortColour => GetColour();

        public string CoverageTableInactiveSortColour => GetColour();

        public string CoverageTableExpandCollapseIconColour => GetColour();

        public string CoverageTableRowHoverBackgroundColour => GetColour();

        public string TabBackgroundColour => GetColour();

        public string DivHeaderBackgroundColour => GetColour();

        public string HeaderFontColour => GetColour();

        public string HeaderBorderColour => GetColour();

        public string TextBoxColour => GetColour();

        public string TextBoxTextColour => GetColour();

        public string TextBoxBorderColour => GetColour();

        private string GetColour([CallerMemberName] string name = "")
        {
            var colour = reportParts.Single(rp => rp.Name == name).SelectedThemeColour.Color;
            return $"rgba({colour.R},{colour.G},{colour.B},{colour.A})";
        }
    }
}

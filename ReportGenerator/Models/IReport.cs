namespace ReportGenerator
{
    public interface IReport
    {
        IReportColours ReportColours { set; }

        void Initialize(IWebBrowser webBrowser);

        void ColoursChanged();

        string ProcessAndNavigate(string htmlForProcessingPath);

        string ProcessUnifiedHtml(string htmlForProcessing);
    }
}

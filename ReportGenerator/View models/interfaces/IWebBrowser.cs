namespace ReportGenerator
{
    public interface IWebBrowser
    {
        object InvokeScript(string scriptName, params object[] args);

        void NavigateToString(string contents);
    }
}

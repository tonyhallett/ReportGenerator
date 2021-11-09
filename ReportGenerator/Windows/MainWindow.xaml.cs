namespace ReportGenerator
{
    using System.Collections.Generic;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IWebBrowser
    {
        public MainWindow()
        {
            InitializeComponent();
            webBrowser.LoadCompleted += WebBrowser_LoadCompleted;
        }

        public List<INavigateAware> NavigateAwares { get; set; }

        public object InvokeScript(string scriptName, params object[] args)
        {
            return webBrowser.InvokeScript(scriptName, args);
        }

        public void NavigateToString(string text)
        {
            webBrowser.NavigateToString(text);
        }

        private void WebBrowser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            foreach (var navigateAware in NavigateAwares)
            {
                navigateAware.Navigated();
            }
        }
    }
}

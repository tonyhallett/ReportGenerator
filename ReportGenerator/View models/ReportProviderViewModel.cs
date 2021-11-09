namespace ReportGenerator
{
    using System;
    using System.ComponentModel.Composition;
    using System.Windows.Forms;
    using System.Windows.Input;

    [Export(typeof(IReportProviderViewModel))]
    public class ReportProviderViewModel : ViewModelBase, IReportProviderViewModel
    {
        private bool canUseDefaultReport = false;

        [ImportingConstructor]
        public ReportProviderViewModel(IAssetPath assetPath)
        {
            DefaultLightReport = assetPath.Combine("FCCLight.html");
        }

        public event EventHandler<string> ReportChanged;

        public string DefaultLightReport { get; }

        public bool CanUseDefaultReport
        {
            get => canUseDefaultReport;
            set
            {
                canUseDefaultReport = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChooseReportCommand => new RelayCommand(_ => ChooseReport());

        public ICommand UseDefaultReportCommand => new RelayCommand(_ => UseDefaultReport());

        private void ChooseReport()
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Chooses processed report file path";
                openFileDialog.Filter = "html files|*.html";
                var dialogResult = openFileDialog.ShowDialog();
                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    CanUseDefaultReport = true;
                    ReportChanged?.Invoke(this, openFileDialog.FileName);
                }
            }
        }

        private void UseDefaultReport()
        {
            canUseDefaultReport = false;
            ReportChanged?.Invoke(this, DefaultLightReport);
        }
    }
}

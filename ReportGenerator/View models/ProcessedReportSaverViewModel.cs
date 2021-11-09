namespace ReportGenerator
{
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Windows.Forms;
    using System.Windows.Input;

    [Export(typeof(IProcessedReportSaverViewModel))]
    public class ProcessedReportSaverViewModel : ViewModelBase, IProcessedReportSaverViewModel
    {
        private string processed;
        private bool canSaveProcessed;

        public bool CanSaveProcessed
        {
            get => canSaveProcessed;
            set
            {
                canSaveProcessed = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand => new RelayCommand((_) => this.Save());

        public void SetProcessed(string processed)
        {
            this.processed = processed;
            CanSaveProcessed = true;
        }

        private void Save()
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Chooses processed report file path";
                var dialogResult = saveFileDialog.ShowDialog();
                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, this.processed);
                }
            }
        }
    }
}

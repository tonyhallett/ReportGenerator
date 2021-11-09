namespace ReportGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Windows.Input;

    public class SaveAsViewModel : ViewModelBase, IDialogViewModel
    {
        private string newSettingsName;

        public string NewSettingsName
        {
            get => newSettingsName;
            set
            {
                newSettingsName = value;
                OnPropertyChanged();
            }
        }

        public string Title => "Save As";
    }
}

namespace ReportGenerator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public interface IReportPartViewModel : INotifyPropertyChanged
    {
        event EventHandler SelectedThemeColourChanged;

        string Name { get; set; }

        bool Selected { get; set; }

        ThemeColour SelectedThemeColour { get; set; }

        List<ThemeColour> ThemeColours { get; set; }

        void ThemeColourNameChanged(string selectedThemeColourName);
    }
}

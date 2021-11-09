namespace ReportGenerator
{
    using System;
    using System.Collections.Generic;

    public class DesignTimeReportPartViewModel : ViewModelBase, IReportPartViewModel
    {
        public DesignTimeReportPartViewModel()
        {
            ThemeColours = DesignTimeThemeColours.Colours;
            SelectedThemeColour = ThemeColours[0];
        }

#pragma warning disable 67
        public event EventHandler SelectedThemeColourChanged;
#pragma warning restore 67

        public string Name { get; set; }

        public bool Selected { get; set; }

        public ThemeColour SelectedThemeColour { get; set; }

        public List<ThemeColour> ThemeColours { get; set; }

        public void ThemeColourNameChanged(string selectedThemeColourName)
        {
            throw new NotImplementedException();
        }
    }
}

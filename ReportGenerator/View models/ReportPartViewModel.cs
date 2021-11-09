namespace ReportGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReportPartViewModel : ViewModelBase, IReportPartViewModel
    {
        private List<ThemeColour> themeColours;
        private ThemeColour selectedThemeColour;
        private bool selected;

        public ReportPartViewModel(IThemesViewModel themesViewModel, string selectedThemeColourName, string name)
        {
            this.Name = name;
            themesViewModel.SelectedThemeChanged += ThemeColoursProvider_Changed;
            ThemeColours = themesViewModel.SelectedTheme.Colours;
            SetSelectedThemeColour(selectedThemeColourName);
        }

        public event EventHandler SelectedThemeColourChanged;

        public List<ThemeColour> ThemeColours
        {
            get => themeColours; set
            {
                themeColours = value;
                OnPropertyChanged();
            }
        }

        public string Name { get; set; } // e.g ScrollBarArrow

        public ThemeColour SelectedThemeColour
        {
            get
            {
                return selectedThemeColour;
            }

            set
            {
                if (value != selectedThemeColour)
                {
                    selectedThemeColour = value;
                    OnPropertyChanged();
                    if (value != null)
                    {
                        SelectedThemeColourChanged?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        public bool Selected
        {
            get => selected;
            set
            {
                selected = value;
                OnPropertyChanged();
            }
        }

        public void ThemeColourNameChanged(string selectedThemeColourName)
        {
            SelectedThemeColour = ThemeColours.First(tc => tc.Name == selectedThemeColourName);
        }

        private void ThemeColoursProvider_Changed(object sender, List<ThemeColour> themeColours)
        {
            var currentSelected = SelectedThemeColour.Name;

            ThemeColours = themeColours;
            SetSelectedThemeColour(currentSelected);
        }

        private void SetSelectedThemeColour(string name)
        {
            SelectedThemeColour = ThemeColours.First(tc => tc.Name == name);
        }
    }
}

namespace ReportGenerator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    [Export(typeof(IThemesViewModel))]
    public class ThemesViewModel : ViewModelBase, IThemesViewModel
    {
        private ITheme selectedTheme;

        [ImportingConstructor]
        public ThemesViewModel(IThemesProvider themesProvider)
        {
            Themes = themesProvider.Themes;
            SelectedTheme = Themes[0];
        }

        public event EventHandler<List<ThemeColour>> SelectedThemeChanged;

        public List<ITheme> Themes { get; }

        public ITheme SelectedTheme
        {
            get
            {
                return selectedTheme;
            }

            set
            {
                selectedTheme = value;
                SelectedThemeChanged?.Invoke(this, selectedTheme.Colours);
                OnPropertyChanged();
            }
        }
    }
}

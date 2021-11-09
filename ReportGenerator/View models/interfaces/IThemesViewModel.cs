namespace ReportGenerator
{
    using System;
    using System.Collections.Generic;

    public interface IThemesViewModel
    {
        event EventHandler<List<ThemeColour>> SelectedThemeChanged;

        List<ITheme> Themes { get; }

        ITheme SelectedTheme { get; set; }
    }
}

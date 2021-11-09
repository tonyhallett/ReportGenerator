namespace ReportGenerator
{
    using System.Collections.Generic;

    public static class DesignTimeThemeColours
    {
        public static List<ThemeColour> Colours => new List<ThemeColour>
        {
            new ThemeColour { Name = "Colour1", Color = System.Windows.Media.Color.FromRgb(122, 1, 1) },
            new ThemeColour { Name = "Colour2", Color = System.Windows.Media.Color.FromRgb(0, 1, 1) },
        };
    }
}

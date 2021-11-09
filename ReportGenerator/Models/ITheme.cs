namespace ReportGenerator
{
    using System.Collections.Generic;

    public interface ITheme
    {
        List<ThemeColour> Colours { get; }

        string Name { get; }
    }
}

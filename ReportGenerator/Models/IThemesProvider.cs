namespace ReportGenerator
{
    using System.Collections.Generic;

    public interface IThemesProvider
    {
        List<ITheme> Themes { get; }
    }
}

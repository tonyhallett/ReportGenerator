namespace ReportGenerator
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;

    [Export(typeof(IThemesProvider))]
    public class ThemesProvider : IThemesProvider
    {
        [ImportingConstructor]
        public ThemesProvider(IAssetPath assetPath)
        {
            var path = assetPath.Combine("Theme Values");
            Themes = Directory.GetFiles(path).Select(t => new Theme(t) as ITheme).ToList();
        }

        public List<ITheme> Themes { get; }
    }
}

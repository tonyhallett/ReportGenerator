namespace ReportGenerator
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class Theme : ITheme
    {
        public Theme(string path)
        {
            Name = System.IO.Path.GetFileNameWithoutExtension(path);
            var xDoc = XDocument.Load(path);
            Colours = xDoc.Root.Descendants("Colour").Select(el =>
            {
                var c = System.Drawing.ColorTranslator.FromHtml(el.Attribute("Colour").Value);
                var colour = System.Windows.Media.Color.FromArgb(c.A, c.R, c.G, c.B);
                return new ThemeColour { Color = colour, Name = el.Attribute("Name").Value };
            }).ToList();
        }

        public string Name { get; private set; }

        public List<ThemeColour> Colours { get; private set; }
    }
}

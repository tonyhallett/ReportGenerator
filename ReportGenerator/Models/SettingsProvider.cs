namespace ReportGenerator
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    [Export(typeof(ISettingsProvider))]
    public class SettingsProvider : ISettingsProvider
    {
        private const string DefaultSettingsName = "default";
        private readonly string reportSettingsFolderPath;
        private readonly List<string> reportSettingsFiles;

        [ImportingConstructor]
        public SettingsProvider(IAssetPath assetPath, IThemesProvider themesProvider)
        {
            reportSettingsFolderPath = assetPath.Combine("Report Settings");
            reportSettingsFiles = Directory.GetFiles(reportSettingsFolderPath).ToList();
            if (reportSettingsFiles.Count == 0)
            {
                var theme = themesProvider.Themes[0];
                var colours = theme.Colours;
                var defaultName = colours[0].Name;
                var defaultSettingsPath = CreateDefaultSettings(defaultName);
                reportSettingsFiles.Add(defaultSettingsPath);
            }

            Settings = reportSettingsFiles.Select(f => Path.GetFileNameWithoutExtension(f)).ToList();
        }

        public List<string> Settings { get; }

        public List<ReportPart> LoadSetting(string name)
        {
            var path = SettingsPath(name);
            var xDoc = XDocument.Load(path);
            var root = xDoc.Root;
            return root.Elements().Select(el =>
            {
                return new ReportPart { SelectedThemeColourName = el.Attribute("SelectedThemeColourName").Value, Name = el.Attribute("Name").Value };
            }).ToList();
        }

        public void Save(string name, List<ReportPart> reportParts)
        {
            var reportPartsElement = new XElement("ReportParts");
            reportParts.ForEach(rp =>
            {
                reportPartsElement.Add(new XElement("ReportPart", new XAttribute("Name", rp.Name), new XAttribute("SelectedThemeColourName", rp.SelectedThemeColourName)));
            });
            reportPartsElement.Save(SettingsPath(name));
        }

        private string CreateDefaultSettings(string defaultName)
        {
            var reportPartsElement = new XElement(
                "ReportParts",
                typeof(ReportColours).GetProperties().Select(p =>
                {
                    return new XElement("ReportPart", new XAttribute("Name", p.Name), new XAttribute("SelectedThemeColourName", defaultName));
                }).ToList());
            var path = SettingsPath(DefaultSettingsName);
            reportPartsElement.Save(SettingsPath(DefaultSettingsName));
            return path;
        }

        private string SettingsPath(string name)
        {
            return Path.Combine(reportSettingsFolderPath, $"{name}.xml");
        }
    }
}

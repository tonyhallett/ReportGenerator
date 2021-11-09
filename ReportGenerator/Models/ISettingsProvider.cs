namespace ReportGenerator
{
    using System.Collections.Generic;

    public interface ISettingsProvider
    {
        List<string> Settings { get; }

        List<ReportPart> LoadSetting(string name);

        void Save(string name, List<ReportPart> reportParts);
    }
}

namespace ReportGenerator
{
    public interface IAssetPath
    {
        string Combine(params string[] paths);
    }
}

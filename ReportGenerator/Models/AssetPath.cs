namespace ReportGenerator
{
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    [Export(typeof(IAssetPath))]
    public class AssetPath : IAssetPath
    {
        private readonly string rootPath;

        public AssetPath()
        {
            this.rootPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets");
        }

        public string Combine(params string[] paths)
        {
            paths = new string[] { this.rootPath }.Concat(paths).ToArray();
            return Path.Combine(paths);
        }
    }
}

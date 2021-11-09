namespace ReportGenerator
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Text;

    [Export(typeof(IShortcutKeysProvider))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ShortcutKeysProvider : IShortcutKeysProvider
    {
        private readonly List<char> shortcutKeys = new List<char>();

        public string Shortcut(string name)
        {
            StringBuilder sb = new StringBuilder();
            var addedShortcut = false;
            for (var i = 0; i < name.Length; i++)
            {
                var character = name[i];
                if (!addedShortcut && !shortcutKeys.Contains(character))
                {
                    shortcutKeys.Add(character);
                    addedShortcut = true;
                    sb.Append($"_{character}");
                }
                else
                {
                    sb.Append(character);
                }
            }

            return sb.ToString();
        }
    }
}

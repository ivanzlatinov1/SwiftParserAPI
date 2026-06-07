using System.Text.RegularExpressions;

namespace SwiftParser.Shared;

internal static class Utilities
{
    extension(string content)
    {
        public string GetTag(string tag)
        {
            string pattern = $@":{tag}:(.*?)(?=\r?\n:[0-9A-Z]+:|\r?\n-\}}|$)";

            Match match = Regex.Match(content, pattern, RegexOptions.Singleline);

            return match.Success ? match.Groups[1].Value.Trim() : string.Empty;
        }
    }
}
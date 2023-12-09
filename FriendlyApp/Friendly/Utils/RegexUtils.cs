using System.Text.RegularExpressions;

namespace Friendly.WebAPI.Utils
{
    public static class RegexUtils
    {
        public static string RemoveHtmlTags(string input)
        {
            return Regex.Replace(input, @"<.*?>", string.Empty);
        }
    }
}
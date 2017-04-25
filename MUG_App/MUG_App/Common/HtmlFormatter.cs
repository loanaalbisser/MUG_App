using System.Text.RegularExpressions;

namespace MUG_App.Common
{
    public static class HtmlFormatter
    {
        public static string RemoveHtmlTags(string source)
        {
            string output;

            output = Regex.Replace(source, "<[^>]*>", string.Empty);

            return output;
        }
    }
}

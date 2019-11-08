


namespace Org.Joey.Common
{
    using System.Text.RegularExpressions;
    using System.Linq;
    public static class StringExtension
    {       
        public static string GetPlainText(this string html)
        {
            if (html == null) return string.Empty;
            html= Regex.Replace(html, "<[^>]*>", string.Empty, RegexOptions.IgnoreCase);
            html=html.Replace("&nbsp;", " ");
            html = html.Replace("-", " ");         
            var lines = html.Split(new char[] { '\r','\n'});
            html = string.Join(" ", lines);           
            return html;
        }
    }
}

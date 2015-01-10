using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Downloader
{
    /// <summary>
    /// 解析Html中的anchor元素
    /// </summary>
    public class TextAnalyzer
    {
        private static string aPattern = "<a\\s{1,}([A-Za-z0-9#:\\.\\s=\"'/\\-_&\\?\\%\u4e00-\u9fa5]*)>";
        public static string hrefPattern = "href\\s*=\\s*\"[A-Za-z0-9\\.\\s=\\-_&\\?\\%/:]*\"";
        
        private static List<string> Analyze(string text,string pattern)
        {
            Regex regex = new Regex(pattern);

            var matches = regex.Matches(text);
            List<string> result = new List<string>();
            foreach (Match match in matches)
            {
                result.Add(match.Value);
            }

            return result;
        }

        public static List<string> GetAllAnchors(string text)
        {
            return Analyze(text, aPattern);
        }

        public static string GetHref(string text)
        {
            var result = Analyze(text, hrefPattern);
            if(result !=null && result.Count>0)
            {
                return result[0];
            }

            return null;
        }

        private static string GetUrlFromHref (string href)
        {
            int startIndex = href.IndexOf("\"");
            int lastIndex = href.LastIndexOf("\"");
            return href.Substring(startIndex + 1, lastIndex - startIndex - 1);
        }

        //public static string[] Split(string url)
        //{
        //    string str = url.Trim().ToLower().Trim('/') ;
        //    if (str.StartsWith("http://"))
        //    {
        //        str = str.Substring(7);
        //    }
        //    return str.Split('/');
        //}
    }
}

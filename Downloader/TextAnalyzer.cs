using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Downloader
{
    public class TextAnalyzer
    {
        private static string aPattern = "<a\\s{1,}([A-Za-z0-9#:\\.\\s=\"'/\\-_&\\?\\%\u4e00-\u9fa5]*)>";
        public static string hrefPattern = "href\\s*=\\s*\"[A-Za-z0-9\\.\\s=\\-_&\\?\\%/:]*\"";
        public static List<string> Analyzer(string text)
        {
            
            Regex regex = new Regex(aPattern);
            Regex regex2 = new Regex(hrefPattern);
            var matches = regex.Matches(text);
            List<string> result = new List<string>();
            foreach (Match match in matches)
            {
                //result.Add(match.Value);
                var ms = regex2.Matches(match.Value);
                foreach(Match m in ms)
                {
                    result.Add(m.Value);
                }
            }

            return result;
        }
    }
}

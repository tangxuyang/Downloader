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
        public static List<string> Analyze(string text,string domain)
        {
            
            Regex regex = new Regex(aPattern);
            Regex regex2 = new Regex(hrefPattern);
            var matches = regex.Matches(text);
            List<string> result = new List<string>();
            foreach (Match match in matches)
            {
                //result.Add(match.Value);
                var ms = regex2.Matches(match.Value);
                Console.WriteLine(match.Value);
                foreach(Match m in ms)
                {
                    string s = GetUrl(m.Value);
                    Console.WriteLine(m.Value);
                    if(s.ToLower().StartsWith("http://"))
                    {
                        result.Add(s);
                    }
                    else
                    {
                        s = domain + "/" + s.Trim('/');
                        result.Add(s);
                    }
                }
            }
            //foreach(var v in result)
            //{
            //    Console.WriteLine(v);
            //}
            return result;
        }

        private static string GetUrl (string str)
        {
            int startIndex = str.IndexOf("\"");
            int lastIndex = str.LastIndexOf("\"");
            return str.Substring(startIndex + 1, lastIndex - startIndex - 1);
        }

        public static string[] Split(string url)
        {
            string str = url.Trim().ToLower();
            if (str.StartsWith("http://"))
            {
                str = str.Substring(7);
            }
            return str.Split('/');
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Downloader
{
    public class UrlHelper
    {
        public static string GetDomain(string url)
        {
            string temp = url;
            if(url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                temp = url.Substring(7);
            }
            int index = temp.IndexOf('/');
            if (index < 0)
            {
                return temp;
            }
            else
            {
                return temp.Substring(0, index);
            }
        }

        public static string[] SplitUrl(string url)
        {
            string str = url.Trim().ToLower().Trim('/');
            if (str.StartsWith("http://"))
            {
                str = str.Substring(7);
            }
            return str.Split('/');
        }
    }
}

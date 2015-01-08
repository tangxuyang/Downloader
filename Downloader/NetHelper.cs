using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Downloader
{
    public class NetHelper
    {
        public static string Request(string url)
        {
            HttpWebRequest request = WebRequest.Create(url.TrimEnd('/')) as HttpWebRequest;
            StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream());
            return sr.ReadToEnd();
        }

        public static void Download(string rootFolder,string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            var stream = request.GetResponse().GetResponseStream();
            var tags = TextAnalyzer.Split(url);
            string fileName =Path.Combine(rootFolder, tags[tags.Length-1]);
            FileStream fs = new FileStream(fileName, FileMode.Create);
            byte[] buffer = new byte[10240];
            int n;
            while((n = stream.Read(buffer,0,buffer.Length))>0)
            {
                fs.Write(buffer, 0, n);
            }
            fs.Close();
        }
    }
}

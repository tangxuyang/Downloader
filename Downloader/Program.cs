using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Downloader
{
    class Program
    {
        static void Main(string[] args)
        {
            //FileStream fs = new FileStream("..\\..\\text.txt", FileMode.Open);
            //StreamReader sr = new StreamReader(fs);
            //string str = sr.ReadToEnd();
            //var strs = TextAnalyzer.Analyzer(str);
            
            //strs.ForEach(o => Console.WriteLine(o));
            //Console.WriteLine(strs.Count);
            //Console.ReadLine();

            Console.WriteLine("Please input a url:");
            string url = Console.ReadLine();              
            Thread thread = new Thread(Start);
            thread.IsBackground = true;
            thread.Start(url);
            //DateTime now = DateTime.Now;
            //TimeSpan ts = new TimeSpan(0, 0, 30);
            //while(DateTime.Now - now<ts )
            //{
            //    Thread.Sleep(2000);
            //}

            Console.WriteLine("over..");
            Console.ReadLine();
        }

        public static void Start(object data)
        {
            string url = data as string;
            //Create tree
            PageNode root = PageNode.CreateTree(url);

            //
            if (root == null)
            {
                Console.WriteLine("url is invalid.");
                return;
            }
            string rootFolder = "f:\\Down";
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(url);
            while (queue.Count > 0)
            {
                url = queue.Dequeue();
                if (!PageNode.IsVisited(root, url))
                {
                    if (CanDownloadable(url))
                    {
                        NetHelper.Download(rootFolder, url);
                    }
                    else
                    {
                        var tempNode = PageNode.AddToTree(root, url);
                        string text = NetHelper.Request(url);
                        var urls = TextAnalyzer.Analyze(text,GetDomain(tempNode));
                        foreach (var v in urls)
                        {
                            if (!PageNode.IsVisited(root, v))
                            {
                                queue.Enqueue(v);
                            }
                        }
                    }
                }
            }
        }

        public static string GetDomain(PageNode node)
        {
            string result = node.Tag;
              
            PageNode parent = node.Parent;
            while (parent!=null)
            {
                result = parent.Tag + "/" + result;
                parent = parent.Parent;
            }

            return "http://" + result;
        }

        static bool CanDownloadable(string url)
        {
            string[] suffixex = {".mp3",".pdf",".wav",".wma" };
            
            foreach(var s in suffixex)
            {
                if (url.ToLower().EndsWith(s))
                    return true;
            }
            return false;
        }
    }

    class Person
    {
        public int? Name { get; set; }
    }
}

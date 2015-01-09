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

            Console.WriteLine("Please input a url:");
            string url = Console.ReadLine();
            Logger.Instance.WriteLog("The url you input is:"+url);
            Thread thread = new Thread(Start);
            thread.IsBackground = true;
            thread.Start(url);
            Logger.Instance.WriteLog("Thread is started.");

            //DateTime now = DateTime.Now;
            //TimeSpan ts = new TimeSpan(0, 0, 30);
            //while(DateTime.Now - now<ts )
            //{
            //    Thread.Sleep(2000);
            //}

            //print over...
            Console.WriteLine("over..");
            Console.ReadLine();
        }

        public static void Start(object data)
        {
            string url = data as string;
            //Create tree
            PageNode root = PageNode.CreateTree(url);
            string domain = GetDomain(root) ;
            //
            if (root == null)
            {
                Console.WriteLine("url is invalid.");
                Logger.Instance.WriteLog("Failed to create tree");
                return;
            }
            string rootFolder = "f:\\Down";
            Logger.Instance.WriteLog("Download folder is "+rootFolder);
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(url);
            while (queue.Count > 0)
            {
                url = queue.Dequeue();
                Logger.Instance.WriteLog("Begin to deal with "+url);
                if (!PageNode.IsVisited(root, url))
                {
                    Logger.Instance.WriteLog("Begin AddToTree " + url);
                    var tempNode = PageNode.AddToTree(root, url);
                    Logger.Instance.WriteLog("End AddToTree " + url);
                    if (CanDownloadable(url))
                    {
                        Logger.Instance.WriteLog("Begin to download "+url);
                        NetHelper.Download(rootFolder, url);
                        tempNode.Visited = true;
                        Logger.Instance.WriteLog("End to download " + url);
                    }
                    else
                    {                       
                        string text = NetHelper.Request(url);
                        Logger.Instance.WriteLog("Begin to analyze content of "+url);
                        var urls = TextAnalyzer.Analyze(text,GetDomain(tempNode),domain);
                        Logger.Instance.WriteLog("Result of analyzing " + url+Environment.NewLine+string.Join(";",urls));
                        Logger.Instance.WriteLog("End to analyze content of " + url);
                        foreach (var v in urls)
                        {
                            if (!PageNode.IsVisited(root, v))
                            {
                                queue.Enqueue(v);
                                Logger.Instance.WriteLog("Enqueue "+v);
                            }
                        }
                        tempNode.Visited = true;
                    }
                }
                else
                {
                    Logger.Instance.WriteLog(url+" is visited.");
                }

                Logger.Instance.WriteLog("End to deal with "+url);
            }
        }

        public static string GetDomain(PageNode node)
        {
            if (node.Parent == null)
                return "http://" + node.Tag;
            string result = "";// node.Tag;
              
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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Downloader
{
    public class PageNode
    {
        public PageNode Parent
        {
            get;
            set;
        }

        public List<PageNode> Children
        {
            get;
            set;
        }

        public string Tag
        {
            get;
            set;
        }

        public PageNode()
        {
            Children = new List<PageNode>();
        }

        public static PageNode CreateTree(string url)
        {
            string[] tags = TextAnalyzer.Split(url);
            PageNode root = null;
            if(tags.Length>0)
            {
                root = new PageNode() { Tag = tags[0], Parent = null };
            }
            //if (tags.Length == 1)
            //{
            //    root = new PageNode();
            //    root.Parent = null;
            //    root.Tag = tags[0];
            //}
            //else if (tags.Length > 1)
            //{
            //    root = new PageNode();
            //    root.Parent = null;
            //    PageNode node = null;
            //    for (int i = 0; i < tags.Length; i++)
            //    {
            //        if (i == 0)
            //        {
            //            root.Tag = tags[i];
            //            node = root;
            //        }
            //        else
            //        {
            //            node.Children.Add(new PageNode() { Parent = node, Tag = tags[i] });
            //            node = node.Children[0];
            //        }
            //    }
            //}

            return root;
        }

        public static bool IsVisited(PageNode root, string url)
        {
            string[] tags = TextAnalyzer.Split(url);
            if(tags.Length == 0 || tags.Length== 1|| tags[0]!=root.Tag)
            {
                return false;
            }
            PageNode node = root;
            for(int i =1; i<tags.Length; i++)
            {
                node = FindNode(node, tags[i]);
                if (node == null)
                    return false;
            }
            return true;
        }

        public static PageNode FindNode(PageNode root, string tag)
        {
            foreach(PageNode node in root.Children)
            {
                if(node.Tag == tag)
                {
                    return node;
                }
            }

            return null;
        }

        public static PageNode AddToTree(PageNode root, string url)
        {
            string[] tags = TextAnalyzer.Split(url);
            if (tags.Length == 0 || tags[0] != root.Tag)
            {
                return null ;
            }
            PageNode node = root;
            PageNode tempNode;
            for(int i=1;i<tags.Length; i++)
            {
                tempNode = FindNode(node, tags[i]);
                if(tempNode==null)
                {
                    PageNode n = new PageNode() { Parent = node, Tag = tags[i] };
                    node.Children.Add(n);
                    node = n;
                }
                else
                {
                    node = tempNode;
                }
            }
            return node;
        }
    }
}

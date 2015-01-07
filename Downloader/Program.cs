using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Downloader
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream("..\\..\\text.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string str = sr.ReadToEnd();
            var strs = TextAnalyzer.Analyzer(str);
            
            strs.ForEach(o => Console.WriteLine(o));
            Console.WriteLine(strs.Count);
            Console.ReadLine();
        }
    }

    class Person
    {
        public int? Name { get; set; }
    }
}

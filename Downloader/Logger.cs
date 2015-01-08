using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Downloader
{
    public class Logger
    {
        private StreamWriter sw;
        private Logger() 
        {
            string fileName = DateTime.Now.Ticks.ToString()+".txt";
            sw = new StreamWriter(fileName);
        }
        private static Logger instance;
        public static Logger Instance
        {
            get
            {
                if(instance==null)
                {
                    instance = new Logger();
                }
                return instance;
            }
        }

        public void WriteLog(string text)
        {
            sw.WriteLine(text+"["+DateTime.Now.ToString()+"]");
            sw.Flush();
        }
    }
}

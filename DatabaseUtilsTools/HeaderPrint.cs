using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryKeyFinder
{
    class HeaderPrint : IConsoleRunnable
    {
        public string GetCode()
        {
            return "header";
        }

        public void Run(Queue<string> parameters)
        {
            string[] header = Database.HeaderAndData.Item1;
            int[] all = Enumerable.Range(1, header.Length).ToArray();
            Console.WriteLine(Utils.JoinColumns(header, all));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseUtilsTools
{
    class PrintTable : IConsoleRunnable
    {
        public string GetCode()
        {
            return "print";
        }

        public void Run(Queue<string> parameters)
        {
            string[] header = Database.HeaderAndData.Item1;
            List<string[]> entries = Database.HeaderAndData.Item2;
            List<int> selection = new List<int>();
            int min = int.Parse(parameters.Dequeue());
            int max = int.Parse(parameters.Dequeue());
            while (parameters.Count > 0)
            {
                selection.Add(int.Parse(parameters.Dequeue()) + 1);
            }
            for(int i = min; i <= max; i++)
            { 
                Console.Write(i + ") ");
                Console.WriteLine(Utils.JoinColumns(entries[i], selection.ToArray()));
            }
        }
    }
}

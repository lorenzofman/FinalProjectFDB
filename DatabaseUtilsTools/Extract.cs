using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseUtilsTools
{
    class Extract : IConsoleRunnable
    {
        public string GetCode()
        {
            return "extract";
        }

        public void Run(Queue<string> parameters)
        {
            string newTableName = parameters.Dequeue();
            int[] indexes = new int[parameters.Count];
            int i = 0;
            while (parameters.Count > 0)
            {
                indexes[i++] = int.Parse(parameters.Dequeue()) + 1;
            }

            string[] header = Database.HeaderAndData.Item1;
            List<string[]> data = Database.HeaderAndData.Item2;
            string extractedHeader = Utils.JoinColumns(header, indexes);
            List<string> extractedData = new List<string>();
            foreach(string[] entry in data)
            {
                extractedData.Add(Utils.JoinColumns(entry, indexes));
            }
            Utils.WriteHeaderAndData(extractedHeader, extractedData, newTableName);

        }
    }
}

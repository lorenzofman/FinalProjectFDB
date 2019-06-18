using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryKeyFinder
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
            while(parameters.Count > 0)
            {
                indexes[i++] = int.Parse(parameters.Dequeue()) + 1;
            }
            StreamWriter streamWriter = new StreamWriter(Directory.GetCurrentDirectory() + "\\" + newTableName, append: false, Encoding.Default);

            string[] header = Database.HeaderAndData.Item1;
            List<string[]> data = Database.HeaderAndData.Item2;

            Utils.WriteRegisterLine(streamWriter, Utils.JoinColumns(header, indexes));
            foreach (string[] entry in data)
            {
                Utils.WriteRegisterLine(streamWriter, Utils.JoinColumns(entry, indexes));
            }
            streamWriter.Close();

        }
    }
}

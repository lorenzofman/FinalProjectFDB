using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DatabaseUtilsTools
{
    public class DuplicateRemover : IConsoleRunnable
    {
        public string GetCode()
        {
            return "removeDuplicates";
        }

        public void Run(Queue<string> parameters)
        {
            string[] header = Database.HeaderAndData.Item1;
            List<string[]> data = Database.HeaderAndData.Item2;
            int[] all = Enumerable.Range(1, header.Length).ToArray();
            string joinedHeader = Utils.JoinColumns(header, all);
            List<string> dataUnique = new List<string>();
            var groupings = data
                .GroupBy(x => new { y = Utils.JoinColumns(x, all) });
            foreach(var group in groupings)
            {
                dataUnique.Add(group.Key.y);
            }
            Utils.WriteHeaderAndData(joinedHeader, dataUnique, "[NoDuplicates]" + Database.File);
            Console.WriteLine(string.Format("Removed {0} duplicated entries from {1} entries", (data.Count - dataUnique.Count), data.Count));
        }

        
    }
}

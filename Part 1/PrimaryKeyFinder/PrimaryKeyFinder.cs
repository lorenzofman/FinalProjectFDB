using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PrimaryKeyFinder
{
    public class PrimaryKeyFinder : IConsoleRunnable
    {
        #region Interface Implementations

        string IConsoleRunnable.GetCode()
        {
            return "find";
        }

        void IConsoleRunnable.Run(Queue<string> parameters)
        {
            StreamReader input = new StreamReader(Database.File, Encoding.Default);
            int maxKeys = int.Parse(parameters.Dequeue());
            Utils.ReadHeaderAndData(input, out string[] header, out List<string[]> data);
            CheckAllCombinationsUntilMaxKeys(header, data, maxKeys);
        }

        #endregion
  
        #region CheckingMethods

        private void CheckAllCombinationsUntilMaxKeys(string[] header, List<string[]> items, int maxKeys)
        {
            for (int i = 1; i <= maxKeys; i++)
            {
                IEnumerable<int[]> combinations = Combinations.Calculate(i, header.Length);
                foreach (int[] combination in combinations)
                {
                    CheckUniqueness(header, items, combination);
                }
            }
        }

        private void CheckUniqueness(string[] header, List<string[]> items, int[] combination)
        {
            bool unique = items
                .GroupBy(x => Utils.JoinColumns(x, combination))
                .All(group => group.Count() == 1);
            if (unique)
            {
                string headerSelection = Utils.JoinColumns(header, combination);
                Console.WriteLine(headerSelection.PadRight(50));
            }
        }

        #endregion
    }
}

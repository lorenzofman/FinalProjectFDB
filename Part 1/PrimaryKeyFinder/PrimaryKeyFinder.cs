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
            int maxKeys = int.Parse(parameters.Dequeue());
            bool verbose = false;
            if (parameters.Count > 0)
            {
                verbose = bool.Parse(parameters.Dequeue());
            }
            CheckAllCombinationsUntilMaxKeys(Database.HeaderAndData.Item1, Database.HeaderAndData.Item2, maxKeys, verbose);
        }

        #endregion
  
        #region CheckingMethods

        private void CheckAllCombinationsUntilMaxKeys(string[] header, List<string[]> items, int maxKeys, bool verbose)
        {
            for (int i = 1; i <= maxKeys; i++)
            {
                IEnumerable<int[]> combinations = Combinations.Calculate(i, header.Length);
                foreach (int[] combination in combinations)
                {
                    CheckUniqueness(header, items, combination, verbose);
                }
            }
        }

        private void CheckUniqueness(string[] header, List<string[]> items, int[] combination, bool verbose)
        {
            string headerSelection = Utils.JoinColumns(header, combination);
            if(verbose)
                Console.WriteLine("Checking uniqueness of " + headerSelection);
            IEnumerable<IGrouping<string, string[]>> groups = items
                .GroupBy(x => Utils.JoinColumns(x, combination));
            bool unique = groups.All(group => group.Count() == 1);
            if (unique)
            {
                Console.WriteLine(headerSelection.PadRight(50));
            }
            else
            {
                if (verbose)
                {
                    IGrouping<string, string[]> grouping = groups.First(x => x.Count() > 1);
                    Console.WriteLine(grouping.Key);
                }
            }
        }

        #endregion
    }
}

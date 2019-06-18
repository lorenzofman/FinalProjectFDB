using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimaryKeyFinder
{
    public class PartialDependancyFinder : DependencyFinder, IConsoleRunnable
    {
        public string GetCode()
        {
            return "fn2";
        }

        public void Run(Queue<string> parameters)
        {
            List<IEnumerable<int[]>> allCombinations = new List<IEnumerable<int[]>>();
            for (int i = 1; i < Database.PrimaryKeys.Length; i++)
            {
                IEnumerable<int[]> x = Combinations.Calculate(i, Database.PrimaryKeys.Length);
                allCombinations.Add(x);
            }
            foreach(IEnumerable<int[]> groupOfCombinations in allCombinations)
            {
                foreach (int[] comb in groupOfCombinations)
                {
                    int[] keys = new int[comb.Length];
                    for(int i = 0; i < comb.Length; i++)
                    {
                        keys[i] = Database.PrimaryKeys[comb[i] - 1] + 1;
                    }
                    int[] otherColumns = NonPrimaryKeys(Database.HeaderAndData.Item1.Length);
                    foreach (int otherColumn in otherColumns)
                    {
                        CheckDependency(Database.HeaderAndData, keys, otherColumn);
                    }
                }
            }
        }        
    }
}

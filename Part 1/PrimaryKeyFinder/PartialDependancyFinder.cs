using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimaryKeyFinder
{
    public class PartialDependancyFinder : IConsoleRunnable
    {
        public string GetCode()
        {
            return "fn2";
        }

        public void Run(Queue<string> parameters)
        {
            if(parameters.Count > 0)
            {
                throw new Exception(GetCode() + " command requires zero parameters");
            }

            List<IEnumerable<int[]>> allCombinations = new List<IEnumerable<int[]>>();
            for (int i = 1; i < Database.PrimaryKey.Length; i++)
            {
                IEnumerable<int[]> x = Combinations.Calculate(i, Database.PrimaryKey.Length);
                allCombinations.Add(x);
            }

            foreach(IEnumerable<int[]> groupOfCombinations in allCombinations)
            {
                foreach (int[] comb in groupOfCombinations)
                {
                    int[] keys = new int[comb.Length];
                    for(int i = 0; i < comb.Length; i++)
                    {
                        keys[i] = Database.PrimaryKey[comb[i] - 1] + 1;
                    }
                    int[] otherColumns = InvertCombination(Database.HeaderAndData.Item1.Length);
                    foreach (int otherColumn in otherColumns)
                    {
                        CheckDependency(Database.HeaderAndData, keys, otherColumn);
                    }
                }
            }

        }

        private int[] InvertCombination(int length)
        {
            List<int> oppositeComb = new List<int>();
            oppositeComb.AddRange(Enumerable.Range(0, length));
            foreach(int comb in Database.PrimaryKey)
            {
                oppositeComb.Remove(comb);
            }
            return oppositeComb.ToArray();
        }

        /*
        private void CheckDependency((string[], List<string[]>) headerAndData, int[] comb, int desiredColumn)
        {
            Console.WriteLine("Checking dependency: " + Utils.JoinColumns(headerAndData.Item1, comb) + " -> " + headerAndData.Item1[desiredColumn]);
            int[] extraComb = new int[comb.Length + 1];
            comb.CopyTo(extraComb,0);
            extraComb[comb.Length] = desiredColumn;
            IEnumerable<IGrouping<string, string[]>> groupings = headerAndData.Item2
                .GroupBy(x => Utils.JoinColumns(x, extraComb));
            IEnumerable<string> selection = groupings.Select(x => x.Key);
            bool depends = selection.GroupBy(x => x.Split('\t')[0]).All(group => group.Count() == 1);
            if (depends)
            {
                Console.WriteLine(headerAndData.Item1[desiredColumn] + "is dependent from: " + Utils.JoinColumns(headerAndData.Item1, comb));
            }
        }
        */
        /*
        private void CheckDependency((string[], List<string[]>) headerAndData, int[] comb, int desiredColumn)
        {
            Console.WriteLine("Checking dependency: " + Utils.JoinColumns(headerAndData.Item1, comb) + " -> " + headerAndData.Item1[desiredColumn]);
            int[] extraComb = new int[comb.Length + 1];
            comb.CopyTo(extraComb, 0);
            extraComb[comb.Length] = desiredColumn + 1;
            var groupings = headerAndData.Item2
                .GroupBy(x => new { y = Utils.JoinColumns(x, comb), z = Utils.JoinColumns(x, extraComb) });
            var ab = groupings.
                GroupBy(x => x.Key.y);
            bool depends = ab.All(group => group.All(o => o == group.First()));
            if (depends)
            {
                Console.WriteLine(headerAndData.Item1[desiredColumn] + "is dependent from: " + Utils.JoinColumns(headerAndData.Item1, comb));
            }
            else
            {
                var abc = ab.First(group => !group.All(o => o == group.First()));
                Console.WriteLine(abc.First().Key);
            }
        }
        */

        private void CheckDependency((string[], List<string[]>) headerAndData, int[] comb, int desiredColumn)
        {
            string[] header = headerAndData.Item1;
            List<string[]> entries = headerAndData.Item2;
            Console.Write("Checking dependency: " + Utils.JoinColumns(header, comb) + " -> " + header[desiredColumn] + "is: ");
            foreach (string[] entry in entries)
            {
                string key = Utils.JoinColumns(entry, comb);
                string value = Utils.JoinColumns(entry, new int[] { desiredColumn + 1 });
                foreach (string[] otherEntry in entries)
                {
                    string otherKey = Utils.JoinColumns(otherEntry, comb);
                    string otherValue = Utils.JoinColumns(otherEntry, new int[] { desiredColumn + 1 });
                    if (key == otherKey)
                    {
                        if(value != otherValue)
                        {
                            Console.WriteLine(string.Format("key = {0}, value = {1}; otherKey = {2}, otherValue = {2}", key, value, otherKey, otherValue));
                            Console.WriteLine("not dependent");
                            Console.WriteLine("\n\n\n\n\n");
                            return;
                        }
                    }
                }
            }
            Console.WriteLine("dependent");
        }
    }
}

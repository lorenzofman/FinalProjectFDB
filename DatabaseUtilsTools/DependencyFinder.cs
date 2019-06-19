using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseUtilsTools
{
    public class DependencyFinder
    {
        protected void CheckDependency((string[], List<string[]>) headerAndData, int[] comb, int desiredColumn)
        {
            int[] extraComb = new int[comb.Length + 1];
            comb.CopyTo(extraComb, 0);
            extraComb[comb.Length] = desiredColumn + 1;
            var groupings = headerAndData.Item2.GroupBy(x => new { y = Utils.JoinColumns(x, comb), z = Utils.JoinColumns(x, extraComb) });
            var ab = groupings.
                GroupBy(x => x.Key.y);
            bool depends = ab.All(group => group.All(o => o == group.First()));
            if (depends)
            {
                Console.WriteLine(headerAndData.Item1[desiredColumn] + "is dependent from: " + Utils.JoinColumns(headerAndData.Item1, comb));
            }
        }


        protected int[] NonPrimaryKeys(int length)
        {
            List<int> oppositeComb = new List<int>();
            oppositeComb.AddRange(Enumerable.Range(0, length));
            foreach (int comb in Database.PrimaryKeys)
            {
                oppositeComb.Remove(comb);
            }
            return oppositeComb.ToArray();
        }
    }
}
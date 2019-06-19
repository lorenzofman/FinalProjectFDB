using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseUtilsTools
{
    class TransitiveDependencyFinder : DependencyFinder, IConsoleRunnable
    {
        public string GetCode()
        {
            return "fn3";
        }

        public void Run(Queue<string> parameters)
        {
            string[] header = Database.HeaderAndData.Item1;
            int[] nonPrimary = NonPrimaryKeys(header.Length);
            foreach(int i in nonPrimary)
            {
                int k = i + 1;
                foreach(int j in nonPrimary)
                {
                    if(i != j)
                    {
                        CheckDependency(Database.HeaderAndData, new int[] {k}, j);
                    }
                }
            }
        }
    }
}

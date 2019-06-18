using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryKeyFinder
{
    public class SetWorkingFile : IConsoleRunnable
    {
        public string GetCode()
        {
            return "file";
        }

        public void Run(Queue<string> parameters)
        {
            Database.File = parameters.Dequeue();
        }
    }
}

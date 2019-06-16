using System;
using System.Collections.Generic;

namespace PrimaryKeyFinder
{
    public class PartialDependancyFounder : IConsoleRunnable
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


        }
    }
}

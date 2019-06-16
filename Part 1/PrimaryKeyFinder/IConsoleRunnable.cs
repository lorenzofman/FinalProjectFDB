using System;
using System.Collections.Generic;

namespace PrimaryKeyFinder
{
    interface IConsoleRunnable
    {
        void Run(Queue<string> parameters);
        string GetCode();
    }
}

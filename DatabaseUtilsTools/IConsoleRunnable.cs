using System;
using System.Collections.Generic;

namespace DatabaseUtilsTools
{
    interface IConsoleRunnable
    {
        void Run(Queue<string> parameters);
        string GetCode();
    }
}

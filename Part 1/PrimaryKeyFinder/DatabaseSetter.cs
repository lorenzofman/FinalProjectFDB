﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryKeyFinder
{
    public class DatabasePrimaryKeySetter : IConsoleRunnable
    {
        string IConsoleRunnable.GetCode()
        {
            return "set";
        }

        void IConsoleRunnable.Run(Queue<string> parameters)
        {
            Database.PrimaryKey = new int[parameters.Count];
            int i = 0;
            while(parameters.Count > 0)
            {
                Database.PrimaryKey[i++] = int.Parse(parameters.Dequeue());
            }
        }
    }
}

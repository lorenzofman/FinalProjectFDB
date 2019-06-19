using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseUtilsTools
{
    // Hardcoded for licitacoes.csv
    public class SQLConverter : IConsoleRunnable
    {
        public string GetCode()
        {
            return "sqlize";
        }

        public void Run(Queue<string> parameters)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
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
            StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + "\\" + Database.File + ".sql", false,  Encoding.Default);
            List<string> sqlInsertions = new List<string>();
            List<string[]> csvData = Database.HeaderAndData.Item2;
            foreach(string[] entry in csvData)
            {
                sqlInsertions.Add(ExtractInsertionFromEntry(entry));
            }
            Utils.WriteHeaderAndData(null, sqlInsertions, writer);
            writer.Close();
        }

        private string ExtractInsertionFromEntry(string[] entry)
        {
            RasterizeData(entry);
            string biddingId            = entry[0];
            string processId            = entry[1];
            string objectName           = entry[2];
            string bidType              = entry[3];
            string bidState             = entry[4];
            string superiorAgencyCode   = entry[5];
            string superiorAgencyName   = entry[6];
            string agencyCode           = entry[7];
            string agencyName           = entry[8];
            string managementUnitCode   = entry[9];
            string managementUnitName   = entry[10];
            string city                 = entry[11];
            string publicationDate      = entry[12];
            string openingDate          = entry[13];
            string value                = entry[14];

            return string.Format(@"insert into Bidding values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14});",
                int.Parse(biddingId),
                ToSqlVarCharField(processId),
                ToSqlVarCharField(objectName),
                ToSqlVarCharField(bidType),
                ToSqlVarCharField(bidState),
                superiorAgencyCode,
                ToSqlVarCharField(superiorAgencyName),
                agencyCode,
                ToSqlVarCharField(agencyName),
                managementUnitCode,
                ToSqlVarCharField(managementUnitName),
                ToSqlVarCharField(city),
                ToSqlVarCharField(ConvertDateToSQLFormat(publicationDate)),
                ToSqlVarCharField(ConvertDateToSQLFormat(openingDate)),
                value.Replace(",","."));
        }

        private string ToSqlVarCharField(string str)
        {
            str = str.Replace("'", "`");
            if (str != "null")
            {
                str = str.Insert(0, @"'");
                str = str.Insert(str.Length, @"'");
            }
            return str;
        }
        private void RasterizeData(string[] entry)
        {
            for (int i = 0; i < entry.Length; i++)
            {
                entry[i] = entry[i].Replace("\"", string.Empty);
                if (entry[i].Length == 0)
                {
                    entry[i] = "null";
                }
            }
        }

        private string ConvertDateToSQLFormat(string date)
        {
            if(date == "null")
            {
                return date;
            }
            string[] splittedDate = date.Split('/');
            string day = splittedDate[0];
            string month = splittedDate[1];
            string year = splittedDate[2];
            return string.Format("{0}-{1}-{2}", year, month, day);
        }
    }
}

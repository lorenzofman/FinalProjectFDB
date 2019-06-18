using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PrimaryKeyFinder
{
    public static class Utils
    {
        public static void Assert(bool condition, string message, bool fatal)
        {
            if (condition)
            {
                return;
            }

            if (fatal)
            {
                throw new Exception(message);
            }

            Console.WriteLine(message);
        }

        public static IEnumerable<Type> FindAssignableClasses<T>(bool includeInterfaces = false) where T : class
        {
            Type type = typeof(T);
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsInterface == includeInterfaces);
            return types;
        }

        public static string JoinColumns(string[] item, int[] combination)
        {
            string selectedItems = "";
            for (int i = 0; i < combination.Length; i++)
            {
                selectedItems += item[combination[i] - 1] + ((i + 1 == combination.Length) ? "" : ";");
            }
            return selectedItems;
        }

        public static string ReadCommand()
        {
            Console.Write("\n" + Directory.GetCurrentDirectory() + "\n$ ");
            return Console.ReadLine();
        }

        #region Read File Methods

        public static void ReadHeaderAndData(StreamReader input, out string[] header, out List<string[]> data)
        {
            header = ReadRegisterLine(input);
            data = new List<string[]>();
            string[] row;
            while ((row = ReadRegisterLine(input)) != null)
            {
                data.Add(row);
            }

        }

        public static string[] ReadRegisterLine(StreamReader sr)
        {
            if (sr.EndOfStream)
            {
                return null;
            }
            string header = sr.ReadLine();
            return Regex.Split(header, ";(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
        }

        #endregion

        #region Write File Methods

        public static void WriteHeaderAndData(string header, List<string> data, string newTableName)
        {
            StreamWriter streamWriter = new StreamWriter(Directory.GetCurrentDirectory() + "\\" + newTableName, append: false, Encoding.Default);
            Utils.WriteRegisterLine(streamWriter, header);
            foreach (string entry in data)
            {
                Utils.WriteRegisterLine(streamWriter, entry);
            }
            streamWriter.Close();
        }

        public static void WriteRegisterLine(StreamWriter sw, string information)
        {
            sw.WriteLine(information);
        }

        #endregion
    }
}

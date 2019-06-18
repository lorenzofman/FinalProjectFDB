using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PrimaryKeyFinder
{
    public static class Database
    {
        #region File

        private static bool fileSet;
        private static string file;
        public static string File
        {
            get
            {
                if(fileSet == false)
                {
                    throw new Exception("Missing working file");
                }
                return file;
            }
            set
            {
                file = value;
                fileSet = true;
                primaryKeySet = false;
                cachedHeaderAndData = false;
            }
        }

        #endregion

        #region PrimaryKey

        private static bool primaryKeySet;
        private static int[] primaryKey;
        public static int[] PrimaryKey
        {
            get
            {
                if (primaryKeySet == false)
                {
                    throw new Exception("Missing primary key");
                }
                return primaryKey;
            }
            set
            {
                primaryKey = value;
                primaryKeySet = true;
            }
        }

        #endregion

        #region CustomName



        #endregion

        #region HeaderAndData

        private static bool cachedHeaderAndData;
        private static (string[], List<string[]>) headerAndData;
        public static (string[], List<string[]>) HeaderAndData
        {
            get
            {
                if (cachedHeaderAndData == false)
                {
                    StreamReader sr = new StreamReader(File, Encoding.Default);
                    Utils.ReadHeaderAndData(sr, out headerAndData.Item1, out headerAndData.Item2);
                    cachedHeaderAndData = true;
                    sr.Close();
                }
                return headerAndData;
            }
        }

        #endregion
    }
}

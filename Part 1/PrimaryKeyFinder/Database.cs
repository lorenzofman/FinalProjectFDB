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
                primaryKeysSet = false;
                cachedHeaderAndData = false;
            }
        }

        #endregion

        #region PrimaryKey

        private static bool primaryKeysSet;
        private static int[] primaryKeys;
        public static int[] PrimaryKeys
        {
            get
            {
                if (primaryKeysSet == false)
                {
                    throw new Exception("Missing primary key");
                }
                return primaryKeys;
            }
            set
            {
                primaryKeys = value;
                primaryKeysSet = true;
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

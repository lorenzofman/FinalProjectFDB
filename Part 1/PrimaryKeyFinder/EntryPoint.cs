using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
namespace Project1
{
    public class EntryPoint
    {
        private static int testedCases = 0;
        private static int successCases = 0;

        public static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Please specify the filename as an argument (include the extension) and the number for max primary keys");
                return;
            }

            if(args.Length != 2)
            {
                Console.WriteLine("Invalid parameters");
                return;
            }

            string filename = args[0];
            string currentDir = Directory.GetCurrentDirectory();
            string path = currentDir + "\\" + filename;
            if(!int.TryParse(args[1], out int result))
            {
                Console.WriteLine("Invalid N value");
                return;
            }
            StreamReader sr = new StreamReader(path, Encoding.Default);
            FindPrimaryKey(sr, result);
        }
        private static void FindPrimaryKey(StreamReader sr, int maxKeys)
        {
            string[] header = ReadColumn(sr);
            if (header.Length < maxKeys)
            {
                Console.WriteLine("N should be less than the size of the array");
                return;
            }
            List<string[]> data = new List<string[]>();
            string[] row;
            while ((row = ReadColumn(sr)) != null)
            {
                data.Add(row);
            }
            BruteForce(header, data, maxKeys);
        }

        private static void BruteForce(string[] header, List<string[]> items, int maxKeys)
        {
            for (int i = 1; i <= maxKeys; i++)
            {
                IEnumerable<int[]> combinations = Combinations(i, header.Length);
                foreach (int[] combination in combinations)
                {
                    CheckUniqueness(header, items, combination);
                }
            }
            Console.WriteLine(("Tested combinations of primary keys:" + testedCases).PadRight(50));
            Console.WriteLine(("Possible combinations of primary keys:" + successCases).PadRight(50));
            Console.WriteLine(("Duplicate keys:" + (testedCases - successCases)).PadRight(50));
        }

        private static IEnumerable<int[]> Combinations(int k, int n)
        {
            int[] result = new int[k];
            Stack<int> stack = new Stack<int>();
            stack.Push(1);

            while (stack.Count > 0)
            {
                int index = stack.Count - 1;
                int value = stack.Pop();

                while (value <= n)
                {
                    result[index++] = value++;
                    stack.Push(value);
                    if (index == k)
                    {
                        yield return result;
                        break;
                    }
                }
            }
        }
        private static void CheckUniqueness(string[] header, List<string[]> items, int[] combination)
        {
            bool unique = items.GroupBy(x => Join(x, combination)).All(group => group.Count() == 1);
            string headerSelection = Join(header,combination);
            testedCases++;
            if (unique)
            {
                Console.WriteLine(Join(header, combination).PadRight(50));
                successCases++;
            }
        }
        private static string Join(string[] item, int[] combination)
        {
            string selectedItems = "";
            for(int i = 0; i < combination.Length; i++)
            {
                selectedItems += item[combination[i] - 1] + "\t";
            }
            return selectedItems;
        }


        
        private static string[] ReadColumn(StreamReader sr)
        {
            if (sr.EndOfStream)
            {
                return null;
            }
            string header = sr.ReadLine();
            return header.Split(';');
        }
        static string RemoveDiacritics(string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }


}

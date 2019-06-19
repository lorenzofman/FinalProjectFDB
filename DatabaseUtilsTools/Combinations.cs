using System.Collections.Generic;

namespace DatabaseUtilsTools
{
    public static class Combinations
    {
        public static IEnumerable<int[]> Calculate(int k, int n)
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
    }
}

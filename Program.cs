using System;

namespace skip_list_example
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] arr = new int[] { 0, 8, 12, 2, 5 };

            SkipListConsoleOutput skipList = new SkipListConsoleOutput(5);

            for (int i = 0; i < arr.Length; i++)
            {
                skipList.Insert(arr[i]);
            }

            skipList.Search(5);

            skipList.Delete(8);

        }
    }
}

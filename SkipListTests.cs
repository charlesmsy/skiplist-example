using System;
namespace skip_list_example
{
    public class SkipListTest
    {
        public static void Run()
        {
            SkipList skipList = new SkipList(10);

            // testing the methods
            Console.WriteLine("Testing SkipList methods:\n");
            Random r = new Random();

            for (int i = 0; i < 10; i++)
            {
                int insertValue = r.Next(0, 10);

                Console.WriteLine("Inserting {0}...", insertValue);
                skipList.Insert(insertValue);

                int searchValue = r.Next(0, 10);

                Console.WriteLine("Search {0} returned: {1}", searchValue, skipList.Search(searchValue));

                int deleteValue = r.Next(0, 10);

                Console.WriteLine("Deleting {0}...", deleteValue);
                skipList.Delete(deleteValue);

                Console.WriteLine();
            }

            // printing the list out
            skipList.PrintListContents();
        }
    }
}

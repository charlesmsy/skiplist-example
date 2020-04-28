using System;

namespace skip_list_example
{
    class Program
    {
        static void Main(string[] args)
        {

            //SkipListTest.Run();

            RunConsoleDemo();

        }

        static void RunConsoleDemo()
        {
            Random r = new Random();
            SkipListConsoleOutput demoList = new SkipListConsoleOutput(16);

            for (int i = 0; i < 50; i++)
            {
                demoList.Insert(r.Next(0, 100));
            }

            demoList.PrintGraphicalList();
        }

    }
}

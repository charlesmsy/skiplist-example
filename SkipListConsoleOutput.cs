using System;
using System.Text;

namespace skip_list_example
{
    class SkipListConsoleOutput
    {
        private int maxLevel;
        private SLNode header;
        private SLNode sentinel;
        private int count = 0;

        class SLNode
        {
            public SLNode[] forward;
            public int key;
            public int height;

            public SLNode(int key, int height)
            {
                if (key == -1)
                {
                    Console.WriteLine("Created header with height {0}\n", height);
                }
                else if (key == 1000)
                {
                    Console.WriteLine("Created sentinel with height {0}\n", height);
                }
                else
                {
                    Console.WriteLine("Created node {0} with height {1}\n", key, height);
                }

                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                this.key = key;
                this.height = height;
                forward = new SLNode[height];
            }
        }

        public SkipListConsoleOutput(int maxLevel)
        {
            Console.WriteLine("SkipList with max height" +
                " {0} created\n", maxLevel);

            this.maxLevel = maxLevel;

            // SL will contain positive values between 0 and 100
            header = new SLNode(-1, maxLevel);
            sentinel = new SLNode(1000, maxLevel);

            for (int i = 0; i < maxLevel; i++)
            {
                header.forward[i] = sentinel;
            }

            PrintGraphicalList();
        }

        public bool Search(int searchKey)
        {
            Console.WriteLine("Searching for key {0}...\n", searchKey);
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            SLNode cur = header;

            for (int i = maxLevel - 1; i >= 0; i--)
            {
                Console.WriteLine("On level {0}\nOn header", i);

                while (cur.forward[i].key < searchKey)
                {
                    Console.WriteLine("On node  {0}", cur.forward[i].key);
                    cur = cur.forward[i];
                }
            }

            Console.WriteLine("\nLoop complete, all levels checked");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }

            cur = cur.forward[0];

            Console.WriteLine("Seeing if next item {0} matches searchKey", cur.key);

            if (cur.key == searchKey)
            {
                Console.WriteLine("Found match, Search() complete\n");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                return true;
            }
            else
            {
                Console.WriteLine("Match not found, Search() complete\n");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                return false;
            }
        }

        public void Insert(int searchKey)
        {
            Console.WriteLine("Inserting key {0}...\n", searchKey);
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            SLNode[] update = new SLNode[maxLevel];
            SLNode sLNode = new SLNode(searchKey, GenerateLevel());
            SLNode cur = header;

            for (int i = maxLevel - 1; i >= 0; i--)
            {
                Console.WriteLine("On level {0}", i);

                while (cur.forward[i].key < searchKey)
                {
                    cur = cur.forward[i];
                    Console.WriteLine("cur set to {0}", cur.key);
                }

                update[i] = cur;

                if (cur.key == -1)
                {
                    Console.WriteLine("Set update[{0}] to header\n", i);
                }
                else
                {
                    Console.WriteLine("Set update[{0}] to node {1}\n", i, cur.key);
                }

                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            }

            // stitch it in
            Console.WriteLine("Splicing in new node...\n");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            for (int i = 0; i < sLNode.height; i++)
            {
                if (update[i].forward[i].key == 1000)
                {
                    Console.WriteLine("Set new node's forward[{0}] to sentinel", i);
                }
                else
                {
                    Console.WriteLine("Set new node's forward[{0}] to node {1}", i, update[i].forward[i].key);
                }
                
                sLNode.forward[i] = update[i].forward[i];

                if (update[i].key == -1)
                {
                    Console.WriteLine("Set header's forward[{0}] to new node\n", i);
                }
                else
                {
                    Console.WriteLine("Set node {0}'s forward[{1}] to new node\n", update[i].key, i);
                }

                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                update[i].forward[i] = sLNode;
            }
            count++;
            Console.WriteLine("Insert() complete\n");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            PrintGraphicalList();
        }

        public void Delete(int target)
        {
            Console.WriteLine("Attempting to delete {0}...\n", target);
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            SLNode targetNode = null;
            SLNode cur = header;
            bool deleted = false;

            for (int i = maxLevel - 1; i >= 0; i--)
            {
                Console.WriteLine("On level {0}\nOn header", i);
                while (cur.forward[i].key < target)
                {
                    Console.WriteLine("On node  {0}", cur.forward[i].key);
                    cur = cur.forward[i];
                }

                if (cur.forward[i].key == target)
                {
                    Console.WriteLine("Target found at level {0}", i);
                    targetNode = cur.forward[i];
                    cur.forward[i] = targetNode.forward[i];
                    Console.WriteLine("Spliced out target at level {0}\n", i);
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    deleted = true;
                }

                if (!deleted)
                {
                    Console.WriteLine("Target not found on level {0}\n", i);
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                }
            }

            Console.WriteLine("\nLoop complete, all levels checked");

            if (deleted)
            {
                Console.WriteLine("target deleted, Delete() complete\n");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                count--;
            }
            else
            {
                Console.WriteLine("target not found, Delete() complete\n");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            }

            PrintGraphicalList();
        }

        public int GenerateLevel()
        {
            Random random = new Random();
            int newlevel = 1;

            while (random.Next(2) != 0 && newlevel < maxLevel)
            {
                newlevel += 1;
            }

            return newlevel;
        }

        public void PrintListContents()
        {
            SLNode cur = header;

            while (cur.forward[0].forward[0] != null)
            {
                Console.WriteLine("key: {0} height: {1}", cur.forward[0].key, cur.forward[0].height);
                cur = cur.forward[0];
            }
        }

        public void PrintGraphicalList()
        {
            Console.WriteLine("SkipList visual:\n");
            SLNode cur = header;
            int[] items = new int[count];
            int[] lengths = new int[count];
            int[] heights = new int[count];
            int counter = 0;
            StringBuilder stringBuilder = new StringBuilder();

            while (cur.forward[0].forward[0] != null)
            {
                items[counter] = cur.forward[0].key;

                if (cur.forward[0].key < 10)
                {
                    lengths[counter] = 1;
                }
                else if (cur.forward[0].key < 100)
                {
                    lengths[counter] = 2;
                }
                else
                {
                    lengths[counter] = 3;
                }

                heights[counter] = cur.forward[0].height;

                counter++;
                cur = cur.forward[0];
            }

            for (int i = maxLevel; i > 0; i--)
            {
                stringBuilder.Append("[]");

                for (int j = 0; j < count; j++)
                {
                    if (heights[j] >= i)
                    {
                        stringBuilder.Append("-" + items[j] + "-");
                    }
                    else
                    {
                        stringBuilder.Append(new String('-', lengths[j] + 2));
                    }
                }
                stringBuilder.Append("[]\n");
            }

            stringBuilder.Append("\n");
            Console.Write(stringBuilder);

        }

    }

}

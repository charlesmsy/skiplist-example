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
                // maybe check for negative values...
                this.key = key;
                this.height = height;
                forward = new SLNode[height];
            }
        }

        public SkipListConsoleOutput(int maxLevel)
        {
            this.maxLevel = maxLevel;

            // assume SL has positive values only.
            // use -1 to denote header
            // sentinel needs to be higher than all values to remain at the end
            // is there a better way to handle this?
            header = new SLNode(-1, maxLevel);
            sentinel = new SLNode(1000, maxLevel);

            for (int i = 0; i < maxLevel; i++)
                header.forward[i] = sentinel;
        }

        public bool Search(int searchKey)
        {
            SLNode cur = header;

            for (int i = maxLevel - 1; i >= 0; i--)
            {
                while (cur.forward[i] != null && cur.forward[i].key < searchKey)
                {
                    cur = cur.forward[i];
                }
            }

            cur = cur.forward[0];

            if (cur.key == searchKey)
                return true;
            else
                return false;
        }

        public void Insert(int searchKey)
        {
            SLNode[] update = new SLNode[maxLevel];
            SLNode sLNode = new SLNode(searchKey, GenerateLevel());
            SLNode cur = header;

            for (int i = maxLevel - 1; i >= 0; i--)
            {
                // somehow didn't need "cur.forward[i] != null &&"
                while (cur.forward[i].key < searchKey)
                {
                    cur = cur.forward[i];
                }

                update[i] = cur;
            }

            // stitch it in
            for (int i = 0; i < sLNode.height; i++)
            {
                sLNode.forward[i] = update[i].forward[i];
                update[i].forward[i] = sLNode;
            }

            // update the count of items in the list
            // this is a nice to have and not necessary
            count++;
        }

        public void Delete(int target)
        {
            SLNode targetNode = null;
            SLNode cur = header;

            for (int i = maxLevel - 1; i >= 0; i--)
            {
                while (cur.forward[i].key < target)
                {
                    cur = cur.forward[i];
                }

                if (cur.forward[i].key == target)
                {
                    targetNode = cur.forward[i];
                    cur.forward[i] = targetNode.forward[i];
                    count--;
                }
            }
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


        public void PrintGraphicalList()
        {
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
                //Console.WriteLine(cur.key);
            }

            for (int i = maxLevel; i > 0; i--)
            {
                stringBuilder.Append("[]");

                for (int j = 0; j < count; j++)
                {
                    if (heights[j] >= i)
                    {
                        stringBuilder.Append("-" + items[j].ToString() + "-");
                    }
                    else
                    {
                        stringBuilder.Append(new String('-', lengths[j] + 2));
                    }
                }
                stringBuilder.Append("[]\n");
            }

            Console.Write(stringBuilder);

        }
    }

}

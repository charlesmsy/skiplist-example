﻿using System;

namespace skip_list_example
{
    class SkipList
    {
        private int maxLevel;
        private SLNode header;
        private SLNode sentinel;
        
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

        public SkipList(int maxLevel)
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

        public void PrintListContents()
        {
            SLNode cur = header;

            while (cur.forward[0].forward[0] != null)
            {
                Console.WriteLine("key: {0} height: {1}", cur.forward[0].key, cur.forward[0].height);
                cur = cur.forward[0];
            }
        }
    }

}

using System;

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
                this.key = key;
                this.height = height;
                forward = new SLNode[height];
            }
        }

        public SkipList(int maxLevel)
        {
            this.maxLevel = maxLevel;

            // SL will contain values between 0 and 999
            // Warning - nothing has been done to ensure this
            header = new SLNode(-1, maxLevel);
            sentinel = new SLNode(1000, maxLevel);

            for (int i = 0; i < maxLevel; i++)
            {
                header.forward[i] = sentinel;
            }
        }

        public bool Search(int searchKey)
        {
            SLNode cur = header;

            for (int i = maxLevel - 1; i >= 0; i--)
            {
                while (cur.forward[i].key < searchKey)
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
            SLNode sLNode = new SLNode(searchKey, GenerateLevel());
            SLNode[] update = new SLNode[maxLevel];
            SLNode cur = header;

            for (int i = maxLevel - 1; i >= 0; i--)
            {
                
                while (cur.forward[i].key < searchKey)
                {
                    cur = cur.forward[i];
                }

                update[i] = cur;
            }

            // stitch in
            for (int i = 0; i < sLNode.height; i++)
            {
                sLNode.forward[i] = update[i].forward[i];
                update[i].forward[i] = sLNode;
            }
        }

        public void Delete(int target)
        {
            SLNode targetNode;
            SLNode cur = header;

            for (int i = maxLevel - 1; i >= 0; i--)
            {
                while (cur.forward[i].key < target)
                {
                    cur = cur.forward[i];
                }

                // splice out
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
    }

}

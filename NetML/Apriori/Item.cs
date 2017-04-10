using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetML.Apriori
{
    public class Item
    {
        private List<string> itemKeys = new List<string>();

        public List<string> ItemKeys
        {
            get { return itemKeys; }
            set { itemKeys = value; }
        }

        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj is Item)
            {                
                var yItem = obj as Item;
                var xItemKeys = ItemKeys;
                foreach (var key in xItemKeys)
                {
                    if (!yItem.ItemKeys.Contains(key))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var key in itemKeys)
            {
                stringBuilder.Append(key);                
            }
            return stringBuilder.ToString();
        }

        public int GetHashCode(object obj)
        {
            return 0;
        }
    }
}

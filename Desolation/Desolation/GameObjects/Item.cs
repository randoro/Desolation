using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Desolation
{
    public class Item
    {
        public int itemID { set; get; }
        public ItemType itemType { set; get; }
        public Item(int itemID, ItemType itemType)
        {
            this.itemID = itemID;
            this.itemType = itemType;
        }
        

        
    }
}

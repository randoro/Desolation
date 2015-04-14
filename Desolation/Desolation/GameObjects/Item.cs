using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Desolation
{
    public class Item
    {
        int itemID;
        ItemType itemType;
        public Item(int itemID, ItemType itemType)
        {
            this.itemID = itemID;
            this.itemType = itemType;
        }
        

        
    }
}

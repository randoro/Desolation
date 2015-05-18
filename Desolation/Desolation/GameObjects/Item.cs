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

        public int range { set; get; }
        public int damge { set; get; }
        public int attackspeed { set; get; }

        public Item(int itemID)
        {
            this.itemID = itemID;
            this.itemType = itemType;
            this.range = range;
            this.damge = damge;
            this.attackspeed = attackspeed;
            switch (itemID)
            {
                case 0 :
                     range=0;
                    damge=0;
                    attackspeed = 0;
                    itemType= ItemType.Effect;
                    break;
                case 1:
                    range=200;
                    damge=5;
                    attackspeed = 3;
                    itemType= ItemType.Ranged;

                    break;

                case 2:
                    range=150;
                    damge=1;
                    attackspeed = 1;
                    itemType= ItemType.Ranged;

                    break;
                case 3:
                    range=10;
                    damge=7;
                    attackspeed = 7;
                    itemType= ItemType.Melee;

                    break;
                case 4:
                    range = 15;
                    damge = 2;
                    attackspeed = 4;
                    itemType = ItemType.Melee;

                    break;


            }
        }

    


    }
}

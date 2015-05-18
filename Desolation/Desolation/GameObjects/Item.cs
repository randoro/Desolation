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
        public bool twohandded { set; get; }

        public Item(int itemID)
        {
            this.itemID = itemID;
            this.itemType = itemType;
            this.range = range;
            this.damge = damge;
            this.attackspeed = attackspeed;
            this.twohandded = twohandded;
            switch (itemID)
            {
                case 0 :
                     range=0;
                    damge=0;
                    attackspeed = 0;
                    itemType= ItemType.Effect;
                    break;
                case 1:
                    range=15;
                    damge=5;
                    attackspeed = 7;
                    itemType= ItemType.Melee;
                    twohandded = false;


                    break;

                case 2:
                    range=20;
                    damge=7;
                    attackspeed = 5;
                    itemType= ItemType.Melee;
                    twohandded = false;

                    break;
                case 3:
                    range=200;
                    damge=7;
                    attackspeed = 7;
                    itemType= ItemType.Ranged;
                    twohandded = false;

                    break;
                case 4:
                    range = 150;
                    damge = 6;
                    attackspeed = 4;
                    itemType = ItemType.Ranged;
                    twohandded = true;

                    break;


            }
        }

    


    }
}

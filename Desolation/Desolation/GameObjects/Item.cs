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
        public float skadearea { set; get; }

        public Item(int itemID)
        {
            this.itemID = itemID;
            this.itemType = itemType;
            this.range = range;
            this.damge = damge;
            this.attackspeed = attackspeed;
            this.twohandded = twohandded;
            this.skadearea = skadearea;
           
            ItemID tempid = (ItemID)itemID;
            switch (tempid)
            {
                case ItemID.Sword:
                    range = 15;
                    damge = 5;
                    attackspeed = 6;
                    itemType = ItemType.Melee;
                    twohandded = false;
                    skadearea = 0.3f;
                    break;
                case ItemID.Spear:
                      range = 20;
                    damge = 7;
                    attackspeed = 5;
                    itemType = ItemType.Melee;
                    skadearea = 0.1f;
                    twohandded = true;

                    break;
                case ItemID.Knife:
                        range = 10;
                    damge = 3;
                    attackspeed = 1;
                    itemType = ItemType.Melee;
                    skadearea = 0.15f;
                    twohandded = false;

                    break;
                case ItemID.Halberd:
                    range = 25;
                    damge = 8;
                    attackspeed = 7;
                    itemType = ItemType.Melee;
                    twohandded = true;
                    skadearea = 0.2f;
                    break;
                case ItemID.Bow:
                    range = 200;
                    damge = 7;
                    attackspeed = 4;
                    itemType = ItemType.Ranged;
                    twohandded = true;
                    skadearea = 0.2f;
                    break;
                case ItemID.Crossbow:
                    range = 175;
                    damge = 9;
                    attackspeed = 7;
                    itemType = ItemType.Ranged;
                    twohandded = true;
                    skadearea = 0.15f;
                    break;
                case ItemID.Pistol:
                    range = 150;
                    damge = 3;
                    attackspeed = 4;
                    itemType = ItemType.Ranged;
                    twohandded = false;
                    skadearea = 0.1f;
                    break;
                case ItemID.Smg:
                    range = 180;
                    damge = 1;
                    attackspeed = 1;
                    itemType = ItemType.Ranged;
                    twohandded = false;
                    skadearea = 0.075f;
                    break;
                case ItemID.Shotgun:
                    range = 150;
                    damge = 9;
                    attackspeed = 6;
                    itemType = ItemType.Ranged;
                    twohandded = true;
                    skadearea = 0.2f;
                    break;
                case ItemID.Sniperrifle:
                    range = 300;
                    damge = 8;
                    attackspeed = 7;
                    itemType = ItemType.Ranged;
                    twohandded = true;
                    skadearea = 0.33f;
                    break;
                case ItemID.Asultrifle:
                    range = 250;
                    damge = 6;
                    attackspeed = 3;
                    itemType = ItemType.Ranged;
                    twohandded = true;
                    skadearea = 0.17f;
                    break;
                default:
                     range=0;
                   damge=0;
                   attackspeed = 0;
                   itemType= ItemType.Effect;
                   skadearea = 0.0f;
                    break;
            }
        }

    


    }
}

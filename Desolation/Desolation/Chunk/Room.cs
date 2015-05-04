using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Desolation
{
    class Room
    {
        public Rectangle area;
        public int structureID { set; get; }

        public Room(int innerPositionX, int innerPositionY, int width, int height)
        {
            area = new Rectangle(innerPositionX, innerPositionY, width, height);

        }
    }
}

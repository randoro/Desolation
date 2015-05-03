using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Desolation
{
    class Structure
    {
        public int structureCenterX { set; get; }
        public int structureCenterY { set; get; }
        public List<Room> roomList;

        public Structure(int structureCenterX, int structureCenterY)
        {
            this.structureCenterX = structureCenterX;
            this.structureCenterY = structureCenterY;
            roomList = new List<Room>();
        }
    }
}

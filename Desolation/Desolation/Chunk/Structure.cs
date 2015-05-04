using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Desolation
{
    public class Structure
    {
        public int structureCenterX { set; get; }
        public int structureCenterY { set; get; }
        public int structureID { set; get; }

        public Structure(int structureCenterX, int structureCenterY)
        {
            this.structureCenterX = structureCenterX;
            this.structureCenterY = structureCenterY;

            structureID = Globals.getUniqueNumber(structureCenterX, structureCenterY);
            
        }

        public void generateRooms()
        {
            ChunkManager.roomList.Add(new Room(structureCenterX - 64, structureCenterY - 32, 128, 64, structureID));

            ChunkManager.roomList.Add(new Room(structureCenterX - 64 + 200, structureCenterY - 32 + 200, 128, 64, structureID));
        }
    }
}

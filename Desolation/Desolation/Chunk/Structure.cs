using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Desolation
{
    public class Structure
    {
        public int structureCenterBlockX { set; get; }
        public int structureCenterBlockY { set; get; }
        public uint structureID { set; get; }

        public Structure(int structureCenterBlockX, int structureCenterBlockY)
        {
            this.structureCenterBlockX = structureCenterBlockX;
            this.structureCenterBlockY = structureCenterBlockY;


            uint posStructureCenterX = Globals.getUniquePositiveFromAny(structureCenterBlockX);
            uint posStructureCenterY = Globals.getUniquePositiveFromAny(structureCenterBlockY);
            structureID = Globals.getUniqueNumber(posStructureCenterX, posStructureCenterY);
            
        }

        public void generateRooms()
        {
            ChunkManager.roomList.Add(new Room(structureCenterBlockX*16 - 64, structureCenterBlockY*16 - 32, 128, 128, structureID));

            ChunkManager.roomList.Add(new Room(structureCenterBlockX*16 - 64 + 200, structureCenterBlockY*16 - 32 + 200, 128, 128, structureID));
        }
    }
}

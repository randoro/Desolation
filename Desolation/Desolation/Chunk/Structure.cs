using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Desolation
{
    public class Structure
    {
        public int structureCenterPositionX { set; get; }
        public int structureCenterPositionY { set; get; }
        public uint structureID { set; get; }

        public Structure(int structureCenterBlockX, int structureCenterBlockY)
        {
            this.structureCenterPositionX = structureCenterPositionX;
            this.structureCenterPositionY = structureCenterPositionY;


            uint posStructureCenterX = Globals.getUniquePositiveFromAny(structureCenterBlockX);
            uint posStructureCenterY = Globals.getUniquePositiveFromAny(structureCenterBlockY);
            structureID = Globals.getUniqueNumber(posStructureCenterX, posStructureCenterY);
            
        }

        public void generateRooms()
        {
            Room newRoom = new Room((int)structureCenterPositionX, (int)structureCenterPositionY, 128, 128, structureID);
            newRoom.generateRoom();
            ChunkManager.roomList.Add(newRoom);

            //Room newRoom2 = new Room(structureCenterBlockX * 16 - 64 + 192, structureCenterBlockY * 16 - 32 + 192, 1280, 1280, structureID);
            //newRoom2.generateRoom();
            //ChunkManager.roomList.Add(newRoom2);

            
        }
    }
}

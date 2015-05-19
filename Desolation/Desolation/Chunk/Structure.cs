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

        public Structure(int structureCenterPositionX, int structureCenterPositionY)
        {
            this.structureCenterPositionX = structureCenterPositionX;
            this.structureCenterPositionY = structureCenterPositionY;


            uint posStructureCenterX = Globals.getUniquePositiveFromAny(structureCenterPositionX);
            uint posStructureCenterY = Globals.getUniquePositiveFromAny(structureCenterPositionY);
            structureID = Globals.getUniqueNumber(posStructureCenterX, posStructureCenterY);
            
        }

        public void generateRooms(Random generator)
        {
            int chance = generator.Next(0, 4);
            //mainroom
            switch (chance)
            {
                case 0:
                    Room newRoom = new Room((int)structureCenterPositionX, (int)structureCenterPositionY, 256, 128, structureID);
                    newRoom.generateRoom();
                    ChunkManager.roomList.Add(newRoom);

                    Room newRoom2 = new Room((int)structureCenterPositionX + 64, (int)structureCenterPositionY + 64, 128, 128, structureID);
                    newRoom2.generateRoom();
                    ChunkManager.roomList.Add(newRoom2);
                    break;
                case 1:
                    Room newRoom3 = new Room((int)structureCenterPositionX, (int)structureCenterPositionY, 128, 128, structureID);
                    newRoom3.generateRoom();
                    ChunkManager.roomList.Add(newRoom3);

                    Room newRoom4 = new Room((int)structureCenterPositionX + 56, (int)structureCenterPositionY + 64, 128, 256, structureID);
                    newRoom4.generateRoom();
                    ChunkManager.roomList.Add(newRoom4);

                    Room newRoom5 = new Room((int)structureCenterPositionX + 112, (int)structureCenterPositionY + 128, 128, 128, structureID);
                    newRoom5.generateRoom();
                    ChunkManager.roomList.Add(newRoom5);
                    break;
                case 2:
                    Room newRoom6 = new Room((int)structureCenterPositionX, (int)structureCenterPositionY, 256, 256, structureID);
                    newRoom6.generateRoom();
                    ChunkManager.roomList.Add(newRoom6);

                    Room newRoom7 = new Room((int)structureCenterPositionX + 112, (int)structureCenterPositionY + 64, 256, 256, structureID);
                    newRoom7.generateRoom();
                    ChunkManager.roomList.Add(newRoom7);

                    Room newRoom8 = new Room((int)structureCenterPositionX - 48, (int)structureCenterPositionY - 16, 128, 128, structureID);
                    newRoom8.generateRoom();
                    ChunkManager.roomList.Add(newRoom8);
                    break;
                case 3:
                    Room newRoom9 = new Room((int)structureCenterPositionX, (int)structureCenterPositionY, 128, 128, structureID);
                    newRoom9.generateRoom();
                    ChunkManager.roomList.Add(newRoom9);

                    
                    break;
                default:
                    break;
            }
            

            //Room newRoom2 = new Room(structureCenterBlockX * 16 - 64 + 192, structureCenterBlockY * 16 - 32 + 192, 1280, 1280, structureID);
            //newRoom2.generateRoom();
            //ChunkManager.roomList.Add(newRoom2);

            
        }
    }
}

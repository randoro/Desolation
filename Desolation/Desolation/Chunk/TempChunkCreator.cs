using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Desolation
{
    static class TempChunkCreator
    {

        public static Chunk createChunk(Region region)
        {

            bool[] chunksLoaded = region.chunksLoaded;

            bool allChunksLoaded = !Array.Exists(chunksLoaded, delegate(bool x) { return !x; }); //checks if all chunks are loaded

            if(!allChunksLoaded) 
            {

            int valueIndex = -1;
            for (int i = 0; i < chunksLoaded.Length; i++)
			{
			    if(!chunksLoaded[i]) 
                {
                    valueIndex = i;
                    break;
                }
			}

            Chunk chunk = new Chunk();


            int XPos = region.xPosRegion * 4 + valueIndex % 4;

            int YPos = region.yPosRegion * 4 + valueIndex / 4;


            int localxPos;
            int localyPos;
            if (XPos >= 0)
            {
                localxPos = XPos % 4;
            }
            else
            {
                localxPos = 3 + (XPos + 1) % 4;
            }
            if (YPos >= 0)
            {
                localyPos = YPos % 4;
            }
            else
            {
                localyPos = 3 + (YPos + 1) % 4;
            }

            chunk.innerIndex = (byte)(localxPos + localyPos * 4);
            region.chunksLoaded[localxPos + localyPos * 4] = true;
            

            chunk.XPos = XPos;
            chunk.YPos = YPos;

            chunk.terrainPopulated = 1;

            chunk.lastUpdate = 123;
            chunk.inhabitedTime = 456;          
                               
                             
            chunk.biomes = new byte[256];

            byte[] blocks = new byte[256];
            for (int j = 0; j < blocks.Length; j++)
            {
                int chance = Globals.rand.Next(0, 2);
                if (chance <= 0)
                {
                    blocks[j] = (byte)0;
                }
                else
                {

                    blocks[j] = (byte)2;
                }
            }
            blocks[0] = (byte)1;
            blocks[15] = (byte)1;
            blocks[240] = (byte)1;
            blocks[255] = (byte)1;

            chunk.blocks = blocks;

            byte[] objects = new byte[256];
            for (int j = 0; j < objects.Length; j++)
            {
                int chance = Globals.rand.Next(0, 20);
                if (chance <= 0)
                {
                    objects[j] = (byte)0;
                }
                else
                {

                    objects[j] = (byte)0;
                }
            }

            chunk.objects = objects;


            List<List<Tag>> entities = new List<List<Tag>>();
            chunk.entities = entities;

            List<List<Tag>> rooms = new List<List<Tag>>();
            chunk.rooms = rooms;

            //List<List<Tag>> tileEntities = new List<List<Tag>>();
            //chunk.tileEntities = tileEntities;

            return chunk;
            
            }
            return null;
        }

        public static void makeRandomChunk(Region region)
        {
            
            bool writing = true;
            int chunks = 16;

            if (writing)
            {
                FileStream fileStream = region.fileStream;
                makeCompound("region", fileStream);




                for (int i = 0; i < chunks; i++)
                {

                    makeCompound("chunk", fileStream);

                    int XPos = region.xPosRegion * 4 + i % 4;
                    makeInt("XPos", XPos, fileStream);

                    int YPos = region.yPosRegion * 4 + i / 4;
                    makeInt("YPos", YPos, fileStream);

                    makeLong("LastUpdate", 123456, fileStream);

                    makeByte("TerrainPopulated", 0, fileStream);

                    makeLong("InhabitedTime", 1337, fileStream);

                    byte[] biomes = new byte[256];
                    makeByteArray("Biomes", biomes, fileStream);

                    byte[] blocks = new byte[256];
                    for (int j = 0; j < blocks.Length; j++)
                    {
                        blocks[j] = (byte)Globals.rand.Next(0, 2);
                    }
                    makeByteArray("Blocks", blocks, fileStream);

                    byte[] objects = new byte[256];
                    makeByteArray("Objects", objects, fileStream);


                    makeEnd(fileStream);

                }

                makeEnd(fileStream);

            }
        }

        public static void makeEmptyChunk(Region region)
        {

            bool writing = true;
            int chunks = 16;

            if (writing)
            {
                FileStream fileStream = region.fileStream;
                makeCompound("region", fileStream);




                for (int i = 0; i < chunks; i++)
                {

                    makeCompound("chunk", fileStream);

                    int XPos = region.xPosRegion * 4 + i % 4;
                    makeInt("XPos", XPos, fileStream);

                    int YPos = region.yPosRegion * 4 + i / 4;
                    makeInt("YPos", YPos, fileStream);

                    //makeLong("LastUpdate", 123456, fileStream);

                    //makeByte("TerrainPopulated", 0, fileStream);

                    //makeLong("InhabitedTime", 1337, fileStream);

                    byte[] biomes = new byte[256];
                    makeByteArray("Biomes", biomes, fileStream);

                    byte[] blocks = new byte[256];
                    for (int j = 0; j < blocks.Length; j++)
                    {
                        blocks[j] = (byte)0;
                    }
                    blocks[0] = (byte)1;
                    blocks[15] = (byte)1;
                    blocks[240] = (byte)1;
                    blocks[255] = (byte)1;
                    makeByteArray("Blocks", blocks, fileStream);

                    byte[] objects = new byte[256];
                    for (int j = 0; j < objects.Length; j++)
                    {
                        int chance = Globals.rand.Next(0, 20);
                        if(chance <= 0) 
                        {
                            objects[j] = (byte)1;
                        } else 
                        {

                            objects[j] = (byte)0;
                        }
                    }
                    makeByteArray("Objects", objects, fileStream);


                    makeEnd(fileStream);

                }

                makeEnd(fileStream);

            }
        }

        public static void makeCompound(String TagNamn, FileStream fileStream)
        {
            //temporär filskrivare
            TagID ID = TagID.Compound;
            byte[] byteArray = BitConverter.GetBytes(TagNamn.Length);
            byte[] buffer = Encoding.UTF8.GetBytes(TagNamn);
            //byte[] array = { 4, 3, 2, 1, 5, 3, 4, 2, 1, 4, 2 };
            //int value = array.Length;
            //byte[] length = BitConverter.GetBytes(value);
            //byte[] payload = new byte[length.Length + array.Length];
            //length.CopyTo(payload, 0);
            //array.CopyTo(payload, length.Length);

            //TagNamn.Length

            fileStream.WriteByte((byte)ID);
            fileStream.WriteByte(byteArray[0]);
            fileStream.WriteByte(byteArray[1]);
            fileStream.Write(buffer, 0, TagNamn.Length);
        }

        public static void makeByte(String TagNamn, sbyte number, FileStream fileStream)
        {
            TagID ID3 = TagID.Byte;
            byte[] byteArray3 = BitConverter.GetBytes(TagNamn.Length);
            byte[] buffer3 = Encoding.UTF8.GetBytes(TagNamn);
            //byte[] array = { 4, 3, 2, 1, 5, 3, 4, 2, 1, 4, 2 };
            //int XPos = i % 4;
            //byte[] payload = BitConverter.GetBytes(number);
            //byte[] payload = new byte[length.Length + array.Length];
            //length.CopyTo(payload, 0);
            //array.CopyTo(payload, length.Length);

            //TagNamn.Length

            fileStream.WriteByte((byte)ID3);
            fileStream.WriteByte(byteArray3[0]);
            fileStream.WriteByte(byteArray3[1]);
            fileStream.Write(buffer3, 0, TagNamn.Length);
            fileStream.WriteByte((byte)number);
        }

        public static void makeInt(String TagNamn, int number, FileStream fileStream)
        {
            TagID ID3 = TagID.Int;
            byte[] byteArray3 = BitConverter.GetBytes(TagNamn.Length);
            byte[] buffer3 = Encoding.UTF8.GetBytes(TagNamn);
            //byte[] array = { 4, 3, 2, 1, 5, 3, 4, 2, 1, 4, 2 };
            //int XPos = i % 4;
            byte[] payload = BitConverter.GetBytes(number);
            //byte[] payload = new byte[length.Length + array.Length];
            //length.CopyTo(payload, 0);
            //array.CopyTo(payload, length.Length);

            //TagNamn.Length

            fileStream.WriteByte((byte)ID3);
            fileStream.WriteByte(byteArray3[0]);
            fileStream.WriteByte(byteArray3[1]);
            fileStream.Write(buffer3, 0, TagNamn.Length);
            fileStream.Write(payload, 0, 4);
        }



        public static void makeByteArray(String TagNamn, byte[] numbers, FileStream fileStream)
        {
            TagID ID3 = TagID.ByteArray;
            byte[] byteArray3 = BitConverter.GetBytes(TagNamn.Length);
            byte[] buffer3 = Encoding.UTF8.GetBytes(TagNamn);
            //byte[] array = { 4, 3, 2, 1, 5, 3, 4, 2, 1, 4, 2 };
            //int XPos = i % 4;
            int length = numbers.Length;
            byte[] arraylength = BitConverter.GetBytes(length);
            //byte[] payload = new byte[length.Length + array.Length];
            //length.CopyTo(payload, 0);
            //array.CopyTo(payload, length.Length);

            //TagNamn.Length

            fileStream.WriteByte((byte)ID3);
            fileStream.WriteByte(byteArray3[0]);
            fileStream.WriteByte(byteArray3[1]);
            fileStream.Write(buffer3, 0, TagNamn.Length);
            fileStream.Write(arraylength, 0, 4);
            fileStream.Write(numbers, 0, length);
        }


        public static void makeLong(String TagNamn, long number, FileStream fileStream)
        {
            TagID ID3 = TagID.Long;
            byte[] byteArray3 = BitConverter.GetBytes(TagNamn.Length);
            byte[] buffer3 = Encoding.UTF8.GetBytes(TagNamn);
            //byte[] array = { 4, 3, 2, 1, 5, 3, 4, 2, 1, 4, 2 };
            //int XPos = i % 4;
            byte[] payload = BitConverter.GetBytes(number);
            //byte[] payload = new byte[length.Length + array.Length];
            //length.CopyTo(payload, 0);
            //array.CopyTo(payload, length.Length);

            //TagNamn.Length

            fileStream.WriteByte((byte)ID3);
            fileStream.WriteByte(byteArray3[0]);
            fileStream.WriteByte(byteArray3[1]);
            fileStream.Write(buffer3, 0, TagNamn.Length);
            fileStream.Write(payload, 0, 8);
        }

        public static void makeEnd(FileStream fileStream)
        {
            TagID end = TagID.End;
            fileStream.WriteByte((byte)end);
        }
    }
}

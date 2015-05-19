using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Desolation
{
    static class Generator
    {
        static float[,] values = new float[320, 320];
        static int width = 10;

        static int height = 10;

        static int featuresize = 1;

        public static Chunk createForrestChunk(Region region)
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

            uint uniquePos = Globals.getUniqueNumber(Globals.getUniquePositiveFromAny(XPos), Globals.getUniquePositiveFromAny(YPos));
            uint uniqueseed = Globals.getUniqueNumber(uniquePos, (uint)Globals.seed);
            Random generator = new Random((int)uniqueseed);

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

            chunk.structurePopulated = 0;

            chunk.lastUpdate = 123;
            chunk.inhabitedTime = 456;          
                               
                             
            chunk.biomes = new byte[256];
               // (int)Globals.getUniqueNumber(Globals.getUniquePositiveFromAny(region.xPosRegion), Globals.getUniquePositiveFromAny(region.yPosRegion)
            //float[,] noise = createNoise(XPos, YPos, 16, 16);

            //for (int i = 0; i < 16; i++)
            //{
            //    for (int j = 0; j < 16; j++)
            //    {
            //        float f = noise[j, i];
            //        float f2 = (float)Math.Max(0.0, Math.Min(1.0, (double)(f)));
            //        byte b = (byte)Math.Floor((double)(f2 == 1.0 ? 255 : f2 * 256.0));
            //        chunk.biomes[j + i*16] = b;
            //    }
            //}
            




            byte[] blocks = new byte[256];
            for (int j = 0; j < blocks.Length; j++)
            {
                int chance = generator.Next(0, 8);
                if (chance == 0)
                {
                    blocks[j] = (byte)BlockID.DarkGrass;
                }
                else if (chance > 0 && chance < 4)
                {
                    blocks[j] = (byte)BlockID.Grass;
                }
                else if (chance > 3 && chance < 8)
                {
                    blocks[j] = (byte)BlockID.SwampGrass;
                }
            }
            //blocks[0] = (byte)1;
            //blocks[15] = (byte)1;
            //blocks[240] = (byte)1;
            //blocks[255] = (byte)1;

            chunk.blocks = blocks;

            
            byte[] objects = new byte[256];
            for (int j = 0; j < objects.Length; j++)
            {
                int chance = generator.Next(0, 400);
                if (chance == 0)
                {
                    objects[j] = (byte)ObjectID.Oak;
                }
                  else if (chance > 0 && chance < 8)
                {
                    objects[j] = (byte)ObjectID.Pine;
                }

                else
                {

                    objects[j] = (byte)ObjectID.Air;
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

        public static Chunk createDesertChunk(Region region)
        {

            bool[] chunksLoaded = region.chunksLoaded;

            bool allChunksLoaded = !Array.Exists(chunksLoaded, delegate(bool x) { return !x; }); //checks if all chunks are loaded

            if (!allChunksLoaded)
            {

                int valueIndex = -1;
                for (int i = 0; i < chunksLoaded.Length; i++)
                {
                    if (!chunksLoaded[i])
                    {
                        valueIndex = i;
                        break;
                    }
                }

                Chunk chunk = new Chunk();


                int XPos = region.xPosRegion * 4 + valueIndex % 4;

                int YPos = region.yPosRegion * 4 + valueIndex / 4;

                uint uniquePos = Globals.getUniqueNumber(Globals.getUniquePositiveFromAny(XPos), Globals.getUniquePositiveFromAny(YPos));
                uint uniqueseed = Globals.getUniqueNumber(uniquePos, (uint)Globals.seed);
                Random generator = new Random((int)uniqueseed);

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

                chunk.structurePopulated = 0;

                chunk.lastUpdate = 123;
                chunk.inhabitedTime = 456;


                chunk.biomes = new byte[256];
                // (int)Globals.getUniqueNumber(Globals.getUniquePositiveFromAny(region.xPosRegion), Globals.getUniquePositiveFromAny(region.yPosRegion)
                //float[,] noise = createNoise(XPos, YPos, 16, 16);

                //for (int i = 0; i < 16; i++)
                //{
                //    for (int j = 0; j < 16; j++)
                //    {
                //        float f = noise[j, i];
                //        float f2 = (float)Math.Max(0.0, Math.Min(1.0, (double)(f)));
                //        byte b = (byte)Math.Floor((double)(f2 == 1.0 ? 255 : f2 * 256.0));
                //        chunk.biomes[j + i*16] = b;
                //    }
                //}





                byte[] blocks = new byte[256];
                for (int j = 0; j < blocks.Length; j++)
                {
                    int chance = generator.Next(0, 5);
                    if (chance == 0)
                    {
                        blocks[j] = (byte)BlockID.RoughSand;
                    }
                    else if (chance > 0)
                    {
                        blocks[j] = (byte)BlockID.Sand;
                    }
                }
                //blocks[0] = (byte)1;
                //blocks[15] = (byte)1;
                //blocks[240] = (byte)1;
                //blocks[255] = (byte)1;

                chunk.blocks = blocks;


                byte[] objects = new byte[256];
                for (int j = 0; j < objects.Length; j++)
                {
                    int chance = generator.Next(0, 400);
                    if (chance == 0)
                    {
                        objects[j] = (byte)ObjectID.DarkCactus;
                    }
                    else if (chance == 1)
                    {
                        objects[j] = (byte)ObjectID.LightCactus;
                    }
                    else
                    {

                        objects[j] = (byte)ObjectID.Air;
                    }

                    int chance2 = generator.Next(0, 1000);
                    if (chance2 == 0)
                    {
                        objects[j] = (byte)ObjectID.Skull;
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

        public static Chunk createTundraChunk(Region region)
        {

            bool[] chunksLoaded = region.chunksLoaded;

            bool allChunksLoaded = !Array.Exists(chunksLoaded, delegate(bool x) { return !x; }); //checks if all chunks are loaded

            if (!allChunksLoaded)
            {

                int valueIndex = -1;
                for (int i = 0; i < chunksLoaded.Length; i++)
                {
                    if (!chunksLoaded[i])
                    {
                        valueIndex = i;
                        break;
                    }
                }

                Chunk chunk = new Chunk();


                int XPos = region.xPosRegion * 4 + valueIndex % 4;

                int YPos = region.yPosRegion * 4 + valueIndex / 4;

                uint uniquePos = Globals.getUniqueNumber(Globals.getUniquePositiveFromAny(XPos), Globals.getUniquePositiveFromAny(YPos));
                uint uniqueseed = Globals.getUniqueNumber(uniquePos, (uint)Globals.seed);
                Random generator = new Random((int)uniqueseed);

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

                chunk.structurePopulated = 0;

                chunk.lastUpdate = 123;
                chunk.inhabitedTime = 456;


                chunk.biomes = new byte[256];
                // (int)Globals.getUniqueNumber(Globals.getUniquePositiveFromAny(region.xPosRegion), Globals.getUniquePositiveFromAny(region.yPosRegion)
                //float[,] noise = createNoise(XPos, YPos, 16, 16);

                //for (int i = 0; i < 16; i++)
                //{
                //    for (int j = 0; j < 16; j++)
                //    {
                //        float f = noise[j, i];
                //        float f2 = (float)Math.Max(0.0, Math.Min(1.0, (double)(f)));
                //        byte b = (byte)Math.Floor((double)(f2 == 1.0 ? 255 : f2 * 256.0));
                //        chunk.biomes[j + i*16] = b;
                //    }
                //}





                byte[] blocks = new byte[256];
                for (int j = 0; j < blocks.Length; j++)
                {
                    int chance = generator.Next(0, 2);
                    if (chance <= 0)
                    {
                        blocks[j] = (byte)BlockID.Sand;
                    }
                    else
                    {

                        blocks[j] = (byte)BlockID.RoughSand;
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
                    int chance = generator.Next(0, 200);
                    if (chance == 3)
                    {
                        objects[j] = (byte)ObjectID.Oak;
                    }
                    else if (chance == 4)
                    {
                        objects[j] = (byte)ObjectID.Pine;
                    }
                    else if (chance == 5)
                    {
                        objects[j] = (byte)ObjectID.LeafLessTree;
                    }
                    else if (chance == 6)
                    {
                        objects[j] = (byte)ObjectID.Snowpine;
                    }

                    else
                    {

                        objects[j] = (byte)ObjectID.Air;
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




        public static void tryToGenerateStructure(Chunk chunk)
        {
            chunk.structurePopulated = 1;
            if(chunk.innerIndex == 5) 
            {

            uint uniquePos = Globals.getUniqueNumber(Globals.getUniquePositiveFromAny(chunk.XPos), Globals.getUniquePositiveFromAny(chunk.YPos));
            uint uniqueseed = Globals.getUniqueNumber(uniquePos, (uint)Globals.seed);
            Random generator = new Random((int)uniqueseed);

            int chance = generator.Next(0, 10);
            if (chance == 0)
            {
                Structure tempStruct = new Structure(chunk.XPos * 16 * 16, chunk.YPos * 16 * 16);
                tempStruct.generateRooms(generator);
                ChunkManager.structureList.Add(tempStruct);
            }

            }
            
        }
        
        

        
        
        /*
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

        */
    }
}

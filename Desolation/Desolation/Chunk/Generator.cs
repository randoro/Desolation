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
        static float[,] values = new float[257, 257];
        static int width = 10;

        static int height = 10;

        static int featuresize = 1;

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
                int chance = Globals.rand.Next(0, 200);
                if (chance <= 0)
                {
                    objects[j] = (byte)3;
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





        public static float[,] createNoise(int xPos, int yPos, int width, int height)
        {
            Console.WriteLine("Noice:");


            float[,] testNoise = GenerateWhiteNoise(xPos, yPos, width, height);
            float[,] testNoise2 = GenerateWhiteNoise(xPos, yPos, width, height);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    testNoise2[j, i] = testNoise[width + j - 16, i];
                }
            }
            float[,] perlinNoise = GeneratePerlinNoise(testNoise, 4);

            float[,] perlinNoise2 = GeneratePerlinNoise(testNoise2, 4);


            float[,] testNoise3 = new float[width * 2, height];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    testNoise3[j, i] = perlinNoise[j, i];
                    testNoise3[j + width, i] = perlinNoise2[j, i];
                }
            }

            //float[,] perlinNoise3 = GeneratePerlinNoise(testNoise3, 6);
            //float[,] perlinNoise = GeneratePerlinNoise(testNoise, 6);

            //for (int i = 0; i < width; i++)
            //{
            //    for (int j = 0; j < height; j++)
            //    {
            //        Console.Write("[" + perlinNoise[i, j] + "] ");
            //    }
            //    Console.WriteLine("");
            //}



            //int[,] oGridCells = { { 1, 2 }, { 3, 4 } };
            //float[] oResult = new float[width * height];
            //System.Buffer.BlockCopy(perlinNoise, 0, oResult, 0, width * height * 4);
            return testNoise3;
        }



        private static float[,] GenerateWhiteNoise(int xPos, int yPos,int width, int height)
        {
            
            float[,] noise = new float[width, height];
            uint seed = Globals.getUniqueNumber(Globals.getUniquePositiveFromAny(xPos * 16), Globals.getUniquePositiveFromAny(yPos * 16));
            Random random = new Random((int)seed);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    noise[j, i] = (float)random.NextDouble() % 1f;
                }
            }

            return noise;
        }



        private static float[,] GenerateSmoothNoise(float[,] baseNoise, int octave)
        {
            int width = baseNoise.GetLength(0);
            int height = baseNoise.GetLength(1);

            float[,] smoothNoise = new float[width, height];

            int samplePeriod = 1 << octave; // calculates 2 ^ k
            float sampleFrequency = 1.0f / samplePeriod;

            for (int i = 0; i < width; i++)
            {
                //calculate the horizontal sampling indices
                int sample_i0 = (i / samplePeriod) * samplePeriod;
                int sample_i1 = (sample_i0 + samplePeriod) % width; //wrap around
                float horizontal_blend = (i - sample_i0) * sampleFrequency;

                for (int j = 0; j < height; j++)
                {
                    //calculate the vertical sampling indices
                    int sample_j0 = (j / samplePeriod) * samplePeriod;
                    int sample_j1 = (sample_j0 + samplePeriod) % height; //wrap around
                    float vertical_blend = (j - sample_j0) * sampleFrequency;

                    //blend the top two corners
                    float top = Interpolate(baseNoise[sample_i0, sample_j0],
                       baseNoise[sample_i1, sample_j0], horizontal_blend);

                    //blend the bottom two corners
                    float bottom = Interpolate(baseNoise[sample_i0, sample_j1],
                       baseNoise[sample_i1, sample_j1], horizontal_blend);

                    //final blend
                    smoothNoise[i, j] = Interpolate(top, bottom, vertical_blend);
                }
            }

            return smoothNoise;
        }

        private static float Interpolate(float x0, float x1, float alpha)
        {
            return x0 * (1 - alpha) + alpha * x1;
        }


        private static float[,] GeneratePerlinNoise(float[,] baseNoise, int octaveCount)
        {
            int width = baseNoise.GetLength(0);
            int height = baseNoise.GetLength(1);

            float[][,] smoothNoise = new float[octaveCount][,]; //an array of 2D arrays containing

            float persistance = 0.5f;

            //generate smooth noise
            for (int i = 0; i < octaveCount; i++)
            {
                smoothNoise[i] = GenerateSmoothNoise(baseNoise, i);
            }

            float[,] perlinNoise = new float[width, height];
            float amplitude = 1.0f;
            float totalAmplitude = 0.0f;

            //blend noise together
            for (int octave = octaveCount - 1; octave >= 0; octave--)
            {
                amplitude *= persistance;
                totalAmplitude += amplitude;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        perlinNoise[i, j] += smoothNoise[octave][i, j] * amplitude;
                    }
                }
            }

            //normalisation
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    perlinNoise[i, j] /= totalAmplitude;
                }
            }

            return perlinNoise;
        }

        public static Color GetColor(Color gradientStart, Color gradientEnd, byte t)
        {
            float t2 = (float)t / 256f;
            float mod = t2 % 0.2f;
            float r = t2 - mod;
            float u = 1 - r;

            Color color = new Color(
               (int)(gradientStart.R * u + gradientEnd.R * r),
               (int)(gradientStart.G * u + gradientEnd.G * r),
               (int)(gradientStart.B * u + gradientEnd.B * r));

            return color;
        }

        public static float[]noise(float freq, int length) {
            Random rand = new Random();
        float phase =  (float)(rand.NextDouble() * 2*Math.PI);
        float[] returnValue = new float[length];
            for (int i = 0; i < length; i++)
			{
                returnValue[i] = (float)(Math.Sin(2 * Math.PI * freq * i / length + phase));
			}

            return returnValue;
        }



        public static float[,] DiamondSquare(int x1, int y1, int x2, int y2, float range, int level)
        {
            if (level == 8)
            {
                for (int i = 0; i < x2 - x1 + 1; i++)
                {
                    for (int j = 0; j < y2 - y1 + 1; j++)
                {
                    values[i, j] = 0.5f;
                }
                }
            }
            if (level < 1)
            {
                return values;
            }
            

            Random rand = new Random(0);
            // diamonds
            for (int i = x1 + level; i < x2; i += level)
                for (int j = y1 + level; j < y2; j += level)
                {
                    float a = values[i - level,j - level];
                    float b = values[i,j - level];
                    float c = values[i - level,j];
                    float d = values[i,j];
                    float e = values[i - level / 2,j - level / 2] = (a + b + c + d) / 4 + (float)rand.NextDouble() * range;
                }

            // squares
            for (int i = x1 + 2 * level; i < x2; i += level)
                for (int j = y1 + 2 * level; j < y2; j += level)
                {
                    float a = values[i - level,j - level];
                    float b = values[i,j - level];
                    float c = values[i - level,j];
                    float d = values[i,j];
                    float e = values[i - level / 2,j - level / 2];

                    float f = values[i - level,j - level / 2] = (a + c + e + values[i - 3 * level / 2,j - level / 2]) / 4 + (float)rand.NextDouble() * range;
                    float g = values[i - level / 2,j - level] = (a + b + e + values[i - level / 2,j - 3 * level / 2]) / 4 + (float)rand.NextDouble() * range;
                }

            return DiamondSquare(x1, y1, x2, y2, range / 2, level / 2);
            //return null;
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

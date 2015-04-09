using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Desolation
{
    public enum RenderType { blocks, objects };
    public enum TagID { End, Byte, Short, Int, Long, Float, Double, ByteArray, String, List, Compound, IntArray };

    public enum EntityID { Player, Goblin, Zombie, Deer };
    public enum Direction { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest, None};
    static class Globals
    {
        public static readonly String gamePath = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly int ticksPerChunkLoad = 1000000; //10 miljoner = 1 sekund
        public static readonly byte[] dataTypeSizes = { 0, 1, 2, 4, 8, 4, 8, 0, 0, 0, 0, 0 }; //datatypes in order of TagID's sizes
        public static Texture2D tempsheet;
        public static Random rand;
        public static Vector2 playerPos;
        public static readonly byte blockSize = 16;
        public static readonly int screenX = 1920;
        public static readonly int screenY = 1080;
        public static readonly int entitiesPerTick = 100;
        public static readonly int chunkSavePerTick = 10;
        public static long ticksLastChunkLoad = 0;


        public static void shiftChunksRight(ref Chunk[] chunkArray) //chunks moving right and left side getting null
        {
            for (int i = 0; i < 12; i++) //ycoords
            {
                for (int j = 0; j < 8; j++) //xcoords
                {
                    chunkArray[i * 12 + 11 - j] = chunkArray[i * 12 + 7 - j];
                }

                Array.Clear(chunkArray, i*12, 4);
            }
        }



        public static void shiftChunksLeft(ref Chunk[] chunkArray) //chunks moving left and right side getting null
        {
            for (int i = 0; i < 12; i++) //ycoords
            {
                for (int j = 0; j < 8; j++) //xcoords
                {
                    chunkArray[i * 12 + j] = chunkArray[i * 12 + j + 4];
                }

                Array.Clear(chunkArray, i * 12 + 8, 4);
            }
        }

        public static void shiftChunksUp(ref Chunk[] chunkArray) //chunks moving up and down side getting null
        {
            for (int i = 0; i < 96; i++) //ycoords
            {
                 chunkArray[i] = chunkArray[i + 48];
            }
            Array.Clear(chunkArray, 96, 48);
        }

        public static void shiftChunksDown(ref Chunk[] chunkArray) //chunks moving up and down side getting null
        {
            for (int i = 96; i > 0; i--) //ycoords
            {
                chunkArray[i + 47] = chunkArray[i - 1];
            }
            Array.Clear(chunkArray, 0, 48);
        }

        public static void shiftRegionsRight(ref Region[] regionArray) //regions moving right and left side getting null
        {
            //if (regionArray[2] != null)
            //    regionArray[2].fileStream.Close();
            //if (regionArray[5] != null)
            //    regionArray[5].fileStream.Close();
            //if (regionArray[8] != null)
            //    regionArray[8].fileStream.Close();

            regionArray[2] = regionArray[1];
            regionArray[1] = regionArray[0];
            regionArray[5] = regionArray[4];
            regionArray[4] = regionArray[3]; 
            regionArray[8] = regionArray[7];
            regionArray[7] = regionArray[6];

            Array.Clear(regionArray, 0, 1);
            Array.Clear(regionArray, 3, 1);
            Array.Clear(regionArray, 6, 1);
        }



        public static void shiftRegionsLeft(ref Region[] regionArray) //regions moving left and right side getting null
        {
            //if (regionArray[0] != null)
            //    regionArray[0].fileStream.Close();
            //if (regionArray[3] != null)
            //    regionArray[3].fileStream.Close();
            //if (regionArray[6] != null)
            //    regionArray[6].fileStream.Close();

            regionArray[0] = regionArray[1];
            regionArray[1] = regionArray[2];
            regionArray[3] = regionArray[4];
            regionArray[4] = regionArray[5];
            regionArray[6] = regionArray[7];
            regionArray[7] = regionArray[8];

            Array.Clear(regionArray, 2, 1);
            Array.Clear(regionArray, 5, 1);
            Array.Clear(regionArray, 8, 1);
        }

        public static void shiftRegionsUp(ref Region[] regionArray) //chunks moving up and down side getting null
        {
            //if (regionArray[0] != null)
            //    regionArray[0].fileStream.Close();
            //if (regionArray[1] != null)
            //    regionArray[1].fileStream.Close();
            //if (regionArray[2] != null)
            //    regionArray[2].fileStream.Close();

            regionArray[0] = regionArray[3];
            regionArray[1] = regionArray[4];
            regionArray[2] = regionArray[5];
            regionArray[3] = regionArray[6];
            regionArray[4] = regionArray[7];
            regionArray[5] = regionArray[8];

            Array.Clear(regionArray, 6, 1);
            Array.Clear(regionArray, 7, 1);
            Array.Clear(regionArray, 8, 1);
        }

        public static void shiftRegionsDown(ref Region[] regionArray) //chunks moving up and down side getting null
        {
            //if (regionArray[6] != null)
            //    regionArray[6].fileStream.Close();
            //if (regionArray[7] != null)
            //    regionArray[7].fileStream.Close();
            //if (regionArray[8] != null)
            //    regionArray[8].fileStream.Close();

            regionArray[8] = regionArray[5];
            regionArray[7] = regionArray[4];
            regionArray[6] = regionArray[3];
            regionArray[5] = regionArray[2];
            regionArray[4] = regionArray[1];
            regionArray[3] = regionArray[0];

            Array.Clear(regionArray, 0, 1);
            Array.Clear(regionArray, 1, 1);
            Array.Clear(regionArray, 2, 1);
        }

        public static int getRegionValue(float input)
        {

            int returnInt;
            if (input >= 0)
            {
                return returnInt = (int)input / 1024;
            }
            else
            {
                return returnInt = (int)input / 1024 - 1;
            }
        }

        public static int getChunkValue(float input)
        {

            int returnInt;
            if (input >= 0)
            {
                return returnInt = (int)input / 256;
            }
            else
            {
                return returnInt = (int)input / 256 - 1;
            }
        }
    }
}

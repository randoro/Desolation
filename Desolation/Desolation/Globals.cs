using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Desolation
{

    public enum TagID { End, Byte, Short, Int, Long, Float, Double, ByteArray, String, List, Compound, IntArray };
    public enum Direction { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest }
    static class Globals
    {
        public static readonly String gamePath = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly int ticksPerChunkLoad = 10000000;
        public static readonly byte[] dataTypeSizes = { 0, 1, 2, 4, 8, 4, 8, 0, 0, 0, 0, 0 }; //datatypes in order of TagID's sizes
        public static Direction dir;
        public static Texture2D tempsheet;
        public static Random rand;


        public static void shiftRight(ref int[] chunkArray) //chunks moving right and left side getting null
        {
            for (int i = 0; i < 12; i++) //ycoords
            {
                for (int j = 0; j < 8; j++) //xcoords
                {
                    chunkArray[i * 12 + 11 - j] = chunkArray[i * 12 + 7 - j];
                }

                Array.Clear(chunkArray, i, 3);
            }
        }
    }
}

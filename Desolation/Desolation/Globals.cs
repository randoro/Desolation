using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Desolation
{

    public enum TagID { End, Byte, Short, Int, Long, Float, Double, ByteArray, String, List, Compound, IntArray };
    public enum Direction { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest, None}
    static class Globals
    {
        public static readonly String gamePath = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly int ticksPerChunkLoad = 10000000;
        public static readonly byte[] dataTypeSizes = { 0, 1, 2, 4, 8, 4, 8, 0, 0, 0, 0, 0 }; //datatypes in order of TagID's sizes
        public static Texture2D tempsheet;
        public static Random rand;
        public static Vector2 playerPos;
        public static readonly int screenX = 1920;
        public static readonly int screenY = 1080;


        public static void shiftRight(ref int[] chunkArray) //chunks moving right and left side getting null
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



        public static void shiftLeft(ref int[] chunkArray) //chunks moving left and right side getting null
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

        public static void shiftUp(ref int[] chunkArray) //chunks moving up and down side getting null
        {
            for (int i = 0; i < 96; i++) //ycoords
            {
                 chunkArray[i] = chunkArray[i + 48];
            }
            Array.Clear(chunkArray, 96, 48);
        }

        public static void shiftDown(ref int[] chunkArray) //chunks moving up and down side getting null
        {
            for (int i = 96; i > 0; i--) //ycoords
            {
                chunkArray[i + 47] = chunkArray[i - 1];
            }
            Array.Clear(chunkArray, 0, 48);
        }
    }
}

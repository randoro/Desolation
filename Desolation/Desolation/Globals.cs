using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Desolation
{

    //Layerdepth definition
    // 0.2-0.2*y Blocks
    // 0.3-0.3*y Objects
    // 0.3-0.3*y Entities
    // 0.4 particles
    // 0.5 roof
    //
    // 0.9 animations
    // 1.0 debug


    public enum RenderType { blocks, objects };
    public enum TagID { End, Byte, Short, Int, Long, Float, Double, ByteArray, String, List, Compound, IntArray };

    public enum BlockID { Grass, LightGrass, DarkGrass, DryGrass, SwampGrass, Sand, RoughSand, GreySand, WaveSand, QuickSand, Dirt, DarkDirt, LightDirt, Mud, Gravel, Snow, BlueSnow, YellowSnow, GreySnow, CrystalIce, HardIce, Water, SaltWater, FreshWater, VoidWater , WoodPlanks };
    public enum ObjectID {Air, Planks,Bricks ,Windows,Marmor,Oak,Snowpine,Palm,LeafLessTree,Pine };
    public enum ItemID { Sword, Spear, Knife, Halberd, Bow, Crossbow,Pistol,Smg,Shotgun,Sniperrifle,Asultrifle };
    public enum EntityID { Player, Goblin, Zombie, Deer };
    public enum Direction { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest, None};

    public enum AnimationType { FadeOutAndIn, Smoke }
    public enum ItemType { Melee, Ranged, Effect }

    public enum Lists { Entities, Rooms, TileEntities }
    static class Globals
    {
        public static readonly String gamePath = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly int ticksPerChunkLoad = 500000; //10 miljoner = 1 sekund //20 ticks per second = 500k
        public static readonly byte[] dataTypeSizes = { 0, 1, 2, 4, 8, 4, 8, 0, 0, 0, 0, 0 }; //datatypes in order of TagID's sizes
        public static Random rand;
        public static Vector2 playerPos;
        public static Vector2 cameraPos;
        public static int currentPlayerChunkXPos;
        public static int currentPlayerChunkYPos;
        public static Vector2 oldPlayerPos;
        public static readonly byte blockSize = 16;
        public static readonly int screenX = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width; //should be 1920
        public static readonly int screenY = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height; //should be 1080
        public static readonly int globalMeleeRange = 2;
        public static readonly int globalRangedRange = 50;
        public static long ticksLastChunkLoad = 0;
        public static uint currentStructureID; //structure player is inside
        public static SpriteFont font;
        public static readonly int seed = 123;


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


        public static int getBlockValue(float input)
        {

            int returnInt;
            if (input >= 0)
            {
                return returnInt = (int)input / 16 % 16;
            }
            else
            {
                return returnInt = 15 + ((int)input) / 16 % 16;
            }
        }


        public static bool checkRange(Vector2 firstPoint, Vector2 secondPoint, int radie)
        {
            if ((firstPoint.X - secondPoint.X) * (firstPoint.X - secondPoint.X) + (firstPoint.Y - secondPoint.Y) * (firstPoint.Y - secondPoint.Y) < (radie * radie))
            {
                return true;
            }
            return false;
        }

        public static Direction getOppositeDirection(Direction dir)
        {
            switch (dir)
            {
                case Direction.North:
                    return Direction.South;
                    break;
                case Direction.NorthEast:
                    return Direction.SouthWest;
                    break;
                case Direction.East:
                    return Direction.West;
                    break;
                case Direction.SouthEast:
                    return Direction.NorthWest;
                    break;
                case Direction.South:
                    return Direction.North;
                    break;
                case Direction.SouthWest:
                    return Direction.NorthEast;
                    break;
                case Direction.West:
                    return Direction.East;
                    break;
                case Direction.NorthWest:
                    return Direction.SouthEast;
                    break;
                case Direction.None:
                    return Direction.None;
                    break;
                default:
                    return Direction.None;
                    break;
            }
        }



        public static uint getUniqueNumber(uint firstNr, uint secondNr) //Cantor pairing function
        {
            return ((firstNr+secondNr)*(firstNr+secondNr+1)/2)+secondNr;
            //return 0;
            //π(a,b)=1/2(a+b)(a+b+1)+b
        }

        public static long[] getUniqueNumberReverse(long z)
        {
            long[] pair = new long[2];
            long t = (int)Math.Floor((-1D + Math.Sqrt(1D + 8 * z)) / 2D);
            long x = t * (t + 3) / 2 - z;
            long y = z - t * (t + 1) / 2;
            pair[0] = (long)x;
            pair[1] = (long)y;
            return pair;
        }

        public static uint getUniquePositiveFromAny(int number)
        {
            if(number >= 0) 
            {
                return (uint)(2 * number);
            } else 
            {
                return (uint)(-2*number - 1);
            }
        }
    }
}

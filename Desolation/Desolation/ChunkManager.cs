using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Desolation
{
    class ChunkManager
    {

        List<Region> regionFiles;
        List<Chunk> chunkList;
        FileLoader fileLoader;
        Chunk[] chunkArray;
        Region[] regionArray;
        long ticksLastChunkLoad;

        int lastRegionX;
        int lastRegionY;
        int lastChunkX;
        int lastChunkY;

        int newRegionX;
        int newRegionY;
        int newChunkX;
        int newChunkY;

        public ChunkManager()
        {
            regionFiles = new List<Region>();
            chunkList = new List<Chunk>();
            fileLoader = new FileLoader();
            ticksLastChunkLoad = DateTime.Now.Ticks;
            regionArray = new Region[9];


                Region tempRegion0 = fileLoader.loadRegionFile(-1, -1);
                TempChunkCreator tempChunkCreator0 = new TempChunkCreator(tempRegion0);
                regionArray[0] = tempRegion0;

                Region tempRegion1 = fileLoader.loadRegionFile(0, -1);
                TempChunkCreator tempChunkCreator1 = new TempChunkCreator(tempRegion1);
                regionArray[1] = tempRegion1;

                Region tempRegion2 = fileLoader.loadRegionFile(1, -1);
                TempChunkCreator tempChunkCreator2 = new TempChunkCreator(tempRegion2);
                regionArray[2] = tempRegion2;

                Region tempRegion3 = fileLoader.loadRegionFile(-1, 0);
                TempChunkCreator tempChunkCreator3 = new TempChunkCreator(tempRegion3);
                regionArray[3] = tempRegion3;

                Region tempRegion4 = fileLoader.loadRegionFile(0, 0);
                TempChunkCreator tempChunkCreator4 = new TempChunkCreator(tempRegion4);
                regionArray[4] = tempRegion4;

                Region tempRegion5 = fileLoader.loadRegionFile(1, 0);
                TempChunkCreator tempChunkCreator5 = new TempChunkCreator(tempRegion5);
                regionArray[5] = tempRegion5;

                Region tempRegion6 = fileLoader.loadRegionFile(-1, 1);
                TempChunkCreator tempChunkCreator6 = new TempChunkCreator(tempRegion6);
                regionArray[6] = tempRegion6;

                Region tempRegion7 = fileLoader.loadRegionFile(0, 1);
                TempChunkCreator tempChunkCreator7 = new TempChunkCreator(tempRegion7);
                regionArray[7] = tempRegion7;

                Region tempRegion8 = fileLoader.loadRegionFile(1, 1);
                TempChunkCreator tempChunkCreator8 = new TempChunkCreator(tempRegion8);
                regionArray[8] = tempRegion8;


            


            chunkArray = new Chunk[144];
            

            //for (int i = 0; i < 144; i++)
            //{
            //    chunkArray[i] = i;
            //}
            //Globals.shiftChunksDown(ref chunkArray);
            //int temp = 0;
        }

        public void addRegion(Region newRegion)
        {
            regionFiles.Add(newRegion);
        }

        public void update(GameTime gameTime, GameWindow window)
        {
            long now = DateTime.Now.Ticks;
            if (now > ticksLastChunkLoad + Globals.ticksPerChunkLoad)
            {


                //time for new chunkLoad
                //Console.WriteLine(now);
                ticksLastChunkLoad = DateTime.Now.Ticks;


                for (int i = 0; i < 9; i++)
                {
                    if (regionArray[i] != null)
                    {
                        Chunk newChunk = TagTranslator.getUnloadedChunk(regionArray[i]);
                        if (newChunk != null)
                        {
                            byte innerIndex = newChunk.innerIndex;
                            chunkArray[((innerIndex / 4) + (i / 3) * 4) * 12 + (innerIndex % 4) + (i % 3) * 4] = newChunk;
                        }
                    }
                }
                //check if chunks in regionfile needs loading
            }

            //set region file numbers (the region file the player is currently in)
            if (Globals.playerPos.X >= 0)
            {
                newRegionX = (int)Globals.playerPos.X / 1024;
                newChunkX = (int)Globals.playerPos.X / 256;
            }
            else
            {
                newRegionX = (int)Globals.playerPos.X / 1024 - 1;
                newChunkX = (int)Globals.playerPos.X / 256 - 1;
            }

            if (Globals.playerPos.Y >= 0)
            {
                newRegionY = (int)Globals.playerPos.Y / 1024;
                newChunkY = (int)Globals.playerPos.Y / 256;
            }
            else
            {
                newRegionY = (int)Globals.playerPos.Y / 1024 - 1;
                newChunkY = (int)Globals.playerPos.Y / 256 - 1;
            }

            window.Title = "newRegionX:" + newRegionX + "  newRegionY:" + newRegionY;

            if (newRegionX != lastRegionX || newRegionY != lastRegionY)
            {
                
                if (newRegionX < lastRegionX)
                {
                    
                    lastRegionX = newRegionX;
                    Globals.shiftRegionsRight(ref regionArray);
                    Globals.shiftChunksRight(ref chunkArray);

                    Region tempRegion1 = fileLoader.loadRegionFile(newRegionX - 1, newRegionY - 1);
                    if (tempRegion1 != null)
                    {
                        TempChunkCreator tempChunkCreator = new TempChunkCreator(tempRegion1);
                        regionArray[0] = tempRegion1;
                    }

                    Region tempRegion2 = fileLoader.loadRegionFile(newRegionX - 1, newRegionY);
                    if (tempRegion2 != null)
                    {
                        TempChunkCreator tempChunkCreator2 = new TempChunkCreator(tempRegion2);
                        regionArray[3] = tempRegion2;
                    }
                    Region tempRegion3 = fileLoader.loadRegionFile(newRegionX - 1, newRegionY + 1);
                    if (tempRegion3 != null)
                    {
                        TempChunkCreator tempChunkCreator3 = new TempChunkCreator(tempRegion3);
                        regionArray[6] = tempRegion3;
                    }

                }
                else if (newRegionX > lastRegionX)
                {
                    lastRegionX = newRegionX;
                    Globals.shiftRegionsLeft(ref regionArray);
                    Globals.shiftChunksLeft(ref chunkArray);

                    Region tempRegion1 = fileLoader.loadRegionFile(newRegionX + 1, newRegionY - 1);
                    if (tempRegion1 != null)
                    {
                        TempChunkCreator tempChunkCreator = new TempChunkCreator(tempRegion1);
                        regionArray[2] = tempRegion1;
                    }
                    Region tempRegion2 = fileLoader.loadRegionFile(newRegionX + 1, newRegionY);
                    if (tempRegion2 != null)
                    {
                        TempChunkCreator tempChunkCreator2 = new TempChunkCreator(tempRegion2);
                        regionArray[5] = tempRegion2;
                    }
                    Region tempRegion3 = fileLoader.loadRegionFile(newRegionX + 1, newRegionY + 1);
                    if (tempRegion3 != null)
                    {
                        TempChunkCreator tempChunkCreator3 = new TempChunkCreator(tempRegion3);
                        regionArray[8] = tempRegion3;
                    }

                    
                    
                }

                if (newRegionY < lastRegionY)
                {
                    lastRegionY = newRegionY;

                    Globals.shiftRegionsDown(ref regionArray);
                    Globals.shiftChunksDown(ref chunkArray);

                    Region tempRegion1 = fileLoader.loadRegionFile(newRegionX - 1, newRegionY - 1);
                    if (tempRegion1 != null)
                    {
                        TempChunkCreator tempChunkCreator = new TempChunkCreator(tempRegion1);
                        regionArray[0] = tempRegion1;
                    }
                    Region tempRegion2 = fileLoader.loadRegionFile(newRegionX, newRegionY - 1);
                    if (tempRegion2 != null)
                    {
                        TempChunkCreator tempChunkCreator2 = new TempChunkCreator(tempRegion2);
                        regionArray[1] = tempRegion2;
                    }
                    Region tempRegion3 = fileLoader.loadRegionFile(newRegionX + 1, newRegionY - 1);
                    if (tempRegion3 != null)
                    {
                        TempChunkCreator tempChunkCreator3 = new TempChunkCreator(tempRegion3);
                        regionArray[2] = tempRegion3;
                    }

                }
                else if (newRegionY > lastRegionY)
                {
                    lastRegionY = newRegionY;

                    Globals.shiftRegionsUp(ref regionArray);
                    Globals.shiftChunksUp(ref chunkArray);

                    Region tempRegion1 = fileLoader.loadRegionFile(newRegionX - 1, newRegionY + 1);
                    if (tempRegion1 != null)
                    {
                        TempChunkCreator tempChunkCreator = new TempChunkCreator(tempRegion1);
                        regionArray[6] = tempRegion1;
                    }
                    Region tempRegion2 = fileLoader.loadRegionFile(newRegionX, newRegionY + 1);
                    if (tempRegion2 != null)
                    {
                        TempChunkCreator tempChunkCreator2 = new TempChunkCreator(tempRegion2);
                        regionArray[7] = tempRegion2;
                    }
                    Region tempRegion3 = fileLoader.loadRegionFile(newRegionX + 1, newRegionY + 1);
                    if (tempRegion3 != null)
                    {
                        TempChunkCreator tempChunkCreator3 = new TempChunkCreator(tempRegion3);
                        regionArray[8] = tempRegion3;
                    }
                }

            }

            lastRegionX = newRegionX;
            lastRegionY = newRegionY;
            

                
                

            foreach (Chunk e in chunkList)
            {
                e.update(gameTime);
            }
            
        }

        public void draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 144; i++)
			{
                if(chunkArray[i] != null) 
                { 
                    chunkArray[i].draw(spriteBatch);
                }
            }
        }


    }
}

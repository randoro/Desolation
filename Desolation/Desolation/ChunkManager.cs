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
        int[] chunkArray;
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

            Region tempRegion = fileLoader.loadRegionFile(0, 0);
            TempChunkCreator tempChunkCreator = new TempChunkCreator(tempRegion);
            addRegion(tempRegion);
            


            chunkArray = new int[144];

            for (int i = 0; i < 144; i++)
            {
                chunkArray[i] = i;
            }
            Globals.shiftDown(ref chunkArray);
            int temp = 0;
        }

        public void addRegion(Region newRegion)
        {
            regionFiles.Add(newRegion);
        }

        public void update(GameTime gameTime)
        {
            //set region file numbers (the region file the player is currently in)
            newRegionX = (int)Globals.playerPos.X / 1024;
            newRegionY = (int)Globals.playerPos.Y / 1024;
            newChunkX = (int)Globals.playerPos.X / 256;
            newChunkY = (int)Globals.playerPos.Y / 256;

            if (newRegionX != lastRegionX || newRegionY != lastRegionY)
            {
                //time for new region file load
                lastRegionX = newRegionX;
                lastRegionY = newRegionY;

                Region tempRegion = fileLoader.loadRegionFile(newRegionX, newRegionY);
                TempChunkCreator tempChunkCreator = new TempChunkCreator(tempRegion);
                addRegion(tempRegion);
            }


            long now = DateTime.Now.Ticks;
            if (now > ticksLastChunkLoad + Globals.ticksPerChunkLoad)
            {

                if (regionFiles.Count > 1)
                {
                    int derp = 0;
                }

                //time for new chunkLoad
                //Console.WriteLine(now);
                ticksLastChunkLoad = DateTime.Now.Ticks;

                foreach (Region reg in regionFiles)
                {
                    Chunk newchunk = TagTranslator.getUnloadedChunk(reg);
                    if (newchunk != null)
                    {
                        chunkList.Add(newchunk);
                    }
                }
                //check if chunks in regionfile needs loading
            }

            foreach (Chunk e in chunkList)
            {
                e.update(gameTime);
            }
            
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (Chunk e in chunkList)
            {
                e.draw(spriteBatch);
            }
        }


    }
}

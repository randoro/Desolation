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
        Chunk[,] chunkArray;
        long ticksLastChunkLoad;

        public ChunkManager()
        {
            regionFiles = new List<Region>();
            chunkList = new List<Chunk>();
            ticksLastChunkLoad = DateTime.Now.Ticks;

            chunkArray = new Chunk[12, 12];
            Globals.shiftRight(ref chunkArray);
        }

        public void addRegion(Region newRegion)
        {
            regionFiles.Add(newRegion);
        }

        public void update(GameTime gameTime)
        {
            long now = DateTime.Now.Ticks;
            if (now > ticksLastChunkLoad + Globals.ticksPerChunkLoad)
            {
                //time for new chunkLoad
                //Console.WriteLine(now);
                ticksLastChunkLoad = DateTime.Now.Ticks;

                Chunk newchunk = TagTranslator.getUnloadedChunk(regionFiles[0]);
                if (newchunk != null)
                {
                    chunkList.Add(newchunk);
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

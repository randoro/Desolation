using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Desolation
{
    class ChunkManager
    {

        List<Region> regionFiles;
        long ticksLastChunkLoad;

        public ChunkManager()
        {
            regionFiles = new List<Region>();
            ticksLastChunkLoad = DateTime.Now.Ticks;
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

                TagTranslator.getUnloadedChunk(regionFiles[0]);
                //check if chunks in regionfile needs loading
            }
            
        }


    }
}

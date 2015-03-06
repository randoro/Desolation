using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Desolation
{
    class ChunkManager
    {

        List<Region> regionFiles;

        public ChunkManager()
        {
            regionFiles = new List<Region>();
        }

        public void addRegion(Region newRegion)
        {
            regionFiles.Add(newRegion);
        }


    }
}

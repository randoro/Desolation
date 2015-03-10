using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Desolation
{
    class Chunk
    {
        public int XPos { set; get; }
        public int YPos { set; get; }
        public long lastUpdate { set; get; }
        public byte terrainPopulated { set; get; }
        public long inhabitedTime { set; get; }

        public Chunk()
        {

        }

        
    }
}

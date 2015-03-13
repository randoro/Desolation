using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Desolation
{
    class Region
    {
        public FileStream fileStream;
        public int xPosRegion;
        public int yPosRegion;

        public bool[] chunksLoaded;

        public Region(FileStream fileStream, int xPosRegion, int yPosRegion)
        {
            this.fileStream = fileStream;
            this.xPosRegion = xPosRegion;
            this.yPosRegion = yPosRegion;

            chunksLoaded = new bool[16];

        }

    }
}

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
        int xPosRegion;
        int yPosRegion;

        public bool[,] chunksLoaded;

        public Region(FileStream fileStream, int xPosRegion, int yPosRegion)
        {
            this.fileStream = fileStream;
            this.xPosRegion = xPosRegion;
            this.yPosRegion = yPosRegion;

            chunksLoaded = new bool[4, 4];

            
            //fileStream.WriteByte(10);
            //fileStream.WriteByte(0);
            //fileStream.WriteByte(3);

            //Console.WriteLine(fileStream.ReadByte());
            //Console.WriteLine(fileStream.ReadByte());
            //testar lite
            //test test
            //hamdi testar
        }

    }
}

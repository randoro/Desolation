using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Desolation
{
    class Region
    {
        FileStream fileStream;
        int xPosRegion;
        int yPosRegion;

        bool[,] chunksLoaded;

        public Region(FileStream fileStream, int xPosRegion, int yPosRegion)
        {
            this.fileStream = fileStream;
            this.xPosRegion = xPosRegion;
            this.yPosRegion = yPosRegion;

            chunksLoaded = new bool[4, 4];

            
            //fileStream.WriteByte(123);
            //fileStream.WriteByte(32);

            Console.WriteLine(fileStream.ReadByte());
            Console.WriteLine(fileStream.ReadByte());
            //testar lite
            //test test
        }
    }
}

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

            ////temporär filskrivare
            //TagID ID = TagID.Byte;
            //String TagNamn = "asdasdddddddddddddddddddddddddddddddddddaaaaaaadasdasdasdasdasdasdasdddddddddddddddddddddddddddddddddddaaaaaaadasdasdasdasdasdasdasdddddddddddddddddddddddddddddddddddaaaaaaadasdasdasdasdasdasdasdddddddddddddddddddddddddddddddddddaaaaaaadasdasdasdasdasdasdasdasdasdasd";
            //byte[] byteArray = BitConverter.GetBytes(TagNamn.Length);
            //byte[] buffer = Encoding.UTF8.GetBytes(TagNamn);
            //byte value = 42;

            ////TagNamn.Length

            //fileStream.WriteByte((byte)ID);
            //fileStream.WriteByte(byteArray[0]);
            //fileStream.WriteByte(byteArray[1]);
            //fileStream.Write(buffer, 0, TagNamn.Length);
            //fileStream.WriteByte(value);
            //Console.WriteLine(fileStream.ReadByte());
            //Console.WriteLine(fileStream.ReadByte());
            //testar lite
            //test test
            //hamdi testar
        }

    }
}

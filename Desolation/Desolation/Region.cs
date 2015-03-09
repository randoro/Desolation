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

            bool writing = false;

            if (writing)
            {
                //temporär filskrivare
                TagID ID = TagID.Int;
                String TagNamn = "asdasdddddddddddddddddddddddddddddddddddaaaaaaadasdasdasdasdasdasdasdddddddddddddddddddddddddddddddddddaaaaaaadasdasdasdasdasdasdasdddddddddddddddddddddddddddddddddddaaaaaaadasdasdasdasdasdasdasdddddddddddddddddddddddddddddddddddaaaaaaadasdasdasdasdasdasdasdasdasdasd";
                byte[] byteArray = BitConverter.GetBytes(TagNamn.Length);
                byte[] buffer = Encoding.UTF8.GetBytes(TagNamn);
                int value = 42;

                byte[] payload =  BitConverter.GetBytes(value);

                //TagNamn.Length

                fileStream.WriteByte((byte)ID);
                fileStream.WriteByte(byteArray[0]);
                fileStream.WriteByte(byteArray[1]);
                fileStream.Write(buffer, 0, TagNamn.Length);
                fileStream.Write(payload, 0, payload.Length);
            }

        }

    }
}

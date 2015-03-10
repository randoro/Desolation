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

        public bool[] chunksLoaded;

        public Region(FileStream fileStream, int xPosRegion, int yPosRegion)
        {
            this.fileStream = fileStream;
            this.xPosRegion = xPosRegion;
            this.yPosRegion = yPosRegion;

            chunksLoaded = new bool[16];

            bool writing = false;
            int times = 20000;

            if (writing)
            {
                for (int i = 0; i < times; i++)
                    {
                
                    //temporär filskrivare
                    TagID ID = TagID.ByteArray;
                    String TagNamn = "asdasdddddddddddddddddddddddddddddddddddaaaaaaadasdasdasdasdasdasdasdddddddddddddddddddddddddddddddddddaaaaaaadasdasdasdasdasdasdasdddddddddddddddddddddddddddddddddddaaaaaaadasdasdasdasdasdasdasdddddddddddddddddddddddddddddddddddaaaaaaadasdasdasdasdasdasdasdasdasdasd";
                    byte[] byteArray = BitConverter.GetBytes(TagNamn.Length);
                    byte[] buffer = Encoding.UTF8.GetBytes(TagNamn);
                    byte[] array = { 4, 3, 2, 1, 5, 3, 4, 2, 1, 4, 2 };
                    int value = array.Length;
                    byte[] length = BitConverter.GetBytes(value);
                    byte[] payload = new byte[length.Length + array.Length];
                    length.CopyTo(payload, 0);
                    array.CopyTo(payload, length.Length);

                    //TagNamn.Length

                    fileStream.WriteByte((byte)ID);
                    fileStream.WriteByte(byteArray[0]);
                    fileStream.WriteByte(byteArray[1]);
                    fileStream.Write(buffer, 0, TagNamn.Length);
                    fileStream.Write(payload, 0, payload.Length);



                    //joakim testar
                }

            }

        }

    }
}

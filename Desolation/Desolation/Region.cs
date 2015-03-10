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

            chunksLoaded[5] = true;

            bool writing = false;
            int chunks = 16;

            if (writing)
            {

                //temporär filskrivare
                TagID ID = TagID.Compound;
                String TagNamn = "region";
                byte[] byteArray = BitConverter.GetBytes(TagNamn.Length);
                byte[] buffer = Encoding.UTF8.GetBytes(TagNamn);
                //byte[] array = { 4, 3, 2, 1, 5, 3, 4, 2, 1, 4, 2 };
                //int value = array.Length;
                //byte[] length = BitConverter.GetBytes(value);
                //byte[] payload = new byte[length.Length + array.Length];
                //length.CopyTo(payload, 0);
                //array.CopyTo(payload, length.Length);

                //TagNamn.Length

                fileStream.WriteByte((byte)ID);
                fileStream.WriteByte(byteArray[0]);
                fileStream.WriteByte(byteArray[1]);
                fileStream.Write(buffer, 0, TagNamn.Length);




                for (int i = 0; i < chunks; i++)
                    {
                
                    

                    TagID ID2 = TagID.Compound;
                    String TagNamn2 = "chunk";
                    byte[] byteArray2 = BitConverter.GetBytes(TagNamn2.Length);
                    byte[] buffer2 = Encoding.UTF8.GetBytes(TagNamn2);
                    //byte[] array = { 4, 3, 2, 1, 5, 3, 4, 2, 1, 4, 2 };
                    //int value = array.Length;
                    //byte[] length = BitConverter.GetBytes(value);
                    //byte[] payload = new byte[length.Length + array.Length];
                    //length.CopyTo(payload, 0);
                    //array.CopyTo(payload, length.Length);

                    //TagNamn.Length

                    fileStream.WriteByte((byte)ID2);
                    fileStream.WriteByte(byteArray2[0]);
                    fileStream.WriteByte(byteArray2[1]);
                    fileStream.Write(buffer2, 0, TagNamn2.Length);

                    TagID ID3 = TagID.Int;
                    String TagNamn3 = "XPos";
                    byte[] byteArray3 = BitConverter.GetBytes(TagNamn3.Length);
                    byte[] buffer3 = Encoding.UTF8.GetBytes(TagNamn3);
                    //byte[] array = { 4, 3, 2, 1, 5, 3, 4, 2, 1, 4, 2 };
                    int XPos = i % 4;
                    byte[] payload = BitConverter.GetBytes(XPos);
                    //byte[] payload = new byte[length.Length + array.Length];
                    //length.CopyTo(payload, 0);
                    //array.CopyTo(payload, length.Length);

                    //TagNamn.Length

                    fileStream.WriteByte((byte)ID3);
                    fileStream.WriteByte(byteArray3[0]);
                    fileStream.WriteByte(byteArray3[1]);
                    fileStream.Write(buffer3, 0, TagNamn3.Length);
                    fileStream.Write(payload, 0, 4);

                    TagID ID4 = TagID.Int;
                    String TagNamn4 = "YPos";
                    byte[] byteArray4 = BitConverter.GetBytes(TagNamn4.Length);
                    byte[] buffer4 = Encoding.UTF8.GetBytes(TagNamn4);
                    //byte[] array = { 4, 3, 2, 1, 5, 3, 4, 2, 1, 4, 2 };
                    int YPos = i / 4;
                    byte[] payload2 = BitConverter.GetBytes(YPos);
                    //byte[] payload = new byte[length.Length + array.Length];
                    //length.CopyTo(payload, 0);
                    //array.CopyTo(payload, length.Length);

                    //TagNamn.Length

                    fileStream.WriteByte((byte)ID4);
                    fileStream.WriteByte(byteArray4[0]);
                    fileStream.WriteByte(byteArray4[1]);
                    fileStream.Write(buffer4, 0, TagNamn4.Length);
                    fileStream.Write(payload2, 0, 4);

                    TagID ID5 = TagID.End;

                    fileStream.WriteByte((byte)ID5);

                    
                    //fileStream.Write(payload, 0, payload.Length);



                    //joakim testar
                }

                TagID ID6 = TagID.End;

                fileStream.WriteByte((byte)ID6);

            }

        }

    }
}

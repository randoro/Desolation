using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Desolation
{
    static class TagTranslator
    {

        public static Chunk getUnloadedChunk(Region regionFile)
        {
            return null;

            //regionFile.fileStream.ReadByte();
        }


        public static void readTag(FileStream fileStream)
        {
            TagID tagID = (TagID)fileStream.ReadByte();
            Tag returnTag;

            if (tagID.Equals(TagID.Byte))
            {
                byte[] byte2 = new byte[2];
                fileStream.Read(byte2, 0, 2);
                int stringLength = BitConverter.ToInt16(byte2, 0);
                byte[] byteString = new byte[stringLength];
                fileStream.Read(byteString, 0, stringLength);
                String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
                //Payload



                byte[] payload = new byte[1]; //changed for each tag
                payload[0] = (byte)fileStream.ReadByte();

                

                returnTag = new Tag(tagID, tagIdentifier, payload);
            }
            else if (tagID.Equals(TagID.Short))
            {
                byte[] byte2 = new byte[2];
                fileStream.Read(byte2, 0, 2);
                int stringLength = BitConverter.ToInt16(byte2, 0);
                byte[] byteString = new byte[stringLength];
                fileStream.Read(byteString, 0, stringLength);
                String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
                //Payload


                byte[] payload = new byte[2]; //changed for each tag
                fileStream.Read(payload, 0, 2);

                returnTag = new Tag(tagID, tagIdentifier, payload);
            }
            else if (tagID.Equals(TagID.Int))
            {
                byte[] byte2 = new byte[2];
                fileStream.Read(byte2, 0, 2);
                int stringLength = BitConverter.ToInt16(byte2, 0);
                byte[] byteString = new byte[stringLength];
                fileStream.Read(byteString, 0, stringLength);
                String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
                //Payload


                byte[] payload = new byte[4]; //changed for each tag
                fileStream.Read(payload, 0, 4);

                returnTag = new Tag(tagID, tagIdentifier, payload);
            }
            else if (tagID.Equals(TagID.Long))
            {
                returnTag = new Tag(tagID, null, null);
            }
            else if (tagID.Equals(TagID.Float))
            {
                returnTag = new Tag(tagID, null, null);
            }
            else if (tagID.Equals(TagID.Double))
            {
                returnTag = new Tag(tagID, null, null);
            }
            else if (tagID.Equals(TagID.ByteArray))
            {
                returnTag = new Tag(tagID, null, null);
            }
            else if (tagID.Equals(TagID.String))
            {
                returnTag = new Tag(tagID, null, null);
            }
            else if (tagID.Equals(TagID.List))
            {
                returnTag = new Tag(tagID, null, null);
            }
            else if (tagID.Equals(TagID.Compound))
            {
                returnTag = new Tag(tagID, null, null);
            }
            else if (tagID.Equals(TagID.IntArray))
            {
                returnTag = new Tag(tagID, null, null);
            }
            else
            {
                returnTag = new Tag(tagID, null, null);
            }

        }
    }
}

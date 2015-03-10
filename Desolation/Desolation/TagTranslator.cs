﻿using System;
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



            bool[] chunksLoaded = regionFile.chunksLoaded;

            bool allChunksLoaded = !Array.Exists(chunksLoaded, delegate(bool x) { return !x; }); //checks if all chunks are loaded

            if (!allChunksLoaded)
            {
                int layerDepth = 0;
                Tag regionTag = readTag(regionFile.fileStream); //should be compound

                while (layerDepth > 0)
                {
                    Tag currentTag = readTag(regionFile.fileStream);
                    TagID tagID = currentTag.getID();


                    switch (tagID)
                    {
                        case TagID.End:
                            layerDepth--;
                            break;
                        case TagID.Byte:
                            break;
                        case TagID.Short:
                            break;
                        case TagID.Int:
                            break;
                        case TagID.Long:
                            break;
                        case TagID.Float:
                            break;
                        case TagID.Double:
                            break;
                        case TagID.ByteArray:
                            break;
                        case TagID.String:
                            break;
                        case TagID.List:
                            break;
                        case TagID.Compound:
                            layerDepth++;
                            break;
                        case TagID.IntArray:
                            break;
                        default:
                            break;
                    }
                }
            }

            //regionFile.fileStream.ReadByte();
            return null;
        }


        public static Tag readTag(FileStream fileStream)
        {
            TagID tagID = (TagID)fileStream.ReadByte();
            Tag returnTag;

            if (tagID.Equals(TagID.Byte))
            {
                byte[] byte2 = new byte[2];
                fileStream.Read(byte2, 0, 2);
                short stringLength = BitConverter.ToInt16(byte2, 0);
                byte[] byteString = new byte[stringLength];
                fileStream.Read(byteString, 0, stringLength);
                String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
                //Payload



                byte[] payload = new byte[Globals.dataTypeSizes[(int)tagID]]; //changed for each tag
                payload[0] = (byte)fileStream.ReadByte();

                

                returnTag = new Tag(tagID, tagIdentifier, payload);
                return returnTag;
            }
            else if (tagID.Equals(TagID.Short))
            {
                byte[] byte2 = new byte[2];
                fileStream.Read(byte2, 0, 2);
                short stringLength = BitConverter.ToInt16(byte2, 0);
                byte[] byteString = new byte[stringLength];
                fileStream.Read(byteString, 0, stringLength);
                String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
                //Payload


                byte[] payload = new byte[Globals.dataTypeSizes[(int)tagID]]; //changed for each tag
                fileStream.Read(payload, 0, Globals.dataTypeSizes[(int)tagID]);

                returnTag = new Tag(tagID, tagIdentifier, payload);
                return returnTag;
            }
            else if (tagID.Equals(TagID.Int))
            {
                byte[] byte2 = new byte[2];
                fileStream.Read(byte2, 0, 2);
                short stringLength = BitConverter.ToInt16(byte2, 0);
                byte[] byteString = new byte[stringLength];
                fileStream.Read(byteString, 0, stringLength);
                String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
                //Payload


                byte[] payload = new byte[Globals.dataTypeSizes[(int)tagID]]; //changed for each tag
                fileStream.Read(payload, 0, Globals.dataTypeSizes[(int)tagID]);

                returnTag = new Tag(tagID, tagIdentifier, payload);
                return returnTag;
            }
            else if (tagID.Equals(TagID.Long))
            {
                byte[] byte2 = new byte[2];
                fileStream.Read(byte2, 0, 2);
                short stringLength = BitConverter.ToInt16(byte2, 0);
                byte[] byteString = new byte[stringLength];
                fileStream.Read(byteString, 0, stringLength);
                String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
                //Payload


                byte[] payload = new byte[Globals.dataTypeSizes[(int)tagID]]; //changed for each tag
                fileStream.Read(payload, 0, Globals.dataTypeSizes[(int)tagID]);

                returnTag = new Tag(tagID, tagIdentifier, payload);
                return returnTag;
            }
            else if (tagID.Equals(TagID.Float))
            {
                byte[] byte2 = new byte[2];
                fileStream.Read(byte2, 0, 2);
                short stringLength = BitConverter.ToInt16(byte2, 0);
                byte[] byteString = new byte[stringLength];
                fileStream.Read(byteString, 0, stringLength);
                String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
                //Payload


                byte[] payload = new byte[Globals.dataTypeSizes[(int)tagID]]; //changed for each tag
                fileStream.Read(payload, 0, Globals.dataTypeSizes[(int)tagID]);

                returnTag = new Tag(tagID, tagIdentifier, payload);
                return returnTag;
            }
            else if (tagID.Equals(TagID.Double))
            {
                byte[] byte2 = new byte[2];
                fileStream.Read(byte2, 0, 2);
                short stringLength = BitConverter.ToInt16(byte2, 0);
                byte[] byteString = new byte[stringLength];
                fileStream.Read(byteString, 0, stringLength);
                String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
                //Payload


                byte[] payload = new byte[Globals.dataTypeSizes[(int)tagID]]; //changed for each tag
                fileStream.Read(payload, 0, Globals.dataTypeSizes[(int)tagID]);

                returnTag = new Tag(tagID, tagIdentifier, payload);
                return returnTag;
            }
            else if (tagID.Equals(TagID.ByteArray))
            {
                byte[] byte2 = new byte[2];
                fileStream.Read(byte2, 0, 2);
                short stringLength = BitConverter.ToInt16(byte2, 0);
                byte[] byteString = new byte[stringLength];
                fileStream.Read(byteString, 0, stringLength);
                String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
                //Payload



                byte[] sizeArray = new byte[4]; //changed for each tag
                fileStream.Read(sizeArray, 0, 4);
                int arraySizeNumber = BitConverter.ToInt32(sizeArray, 0);

                byte[] payload = new byte[4 + arraySizeNumber]; //changed for each tag
                payload[0] = sizeArray[0];
                payload[1] = sizeArray[1];
                payload[2] = sizeArray[2];
                payload[3] = sizeArray[3];
                fileStream.Read(payload, 4, arraySizeNumber);


                returnTag = new Tag(tagID, tagIdentifier, payload);
                return returnTag;
            }
            else if (tagID.Equals(TagID.String))
            {
                byte[] byte2 = new byte[2];
                fileStream.Read(byte2, 0, 2);
                short stringLength = BitConverter.ToInt16(byte2, 0);
                byte[] byteString = new byte[stringLength];
                fileStream.Read(byteString, 0, stringLength);
                String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
                //Payload



                byte[] sizeArray = new byte[2]; //changed for each tag
                fileStream.Read(sizeArray, 0, 2);
                short stringSizeNumber = BitConverter.ToInt16(sizeArray, 0);

                byte[] payload = new byte[2 + stringSizeNumber]; //changed for each tag
                payload[0] = sizeArray[0];
                payload[1] = sizeArray[1];
                fileStream.Read(payload, 2, stringSizeNumber);


                returnTag = new Tag(tagID, tagIdentifier, payload);
                return returnTag;
            }
            else if (tagID.Equals(TagID.List))
            {
                byte[] byte2 = new byte[2];
                fileStream.Read(byte2, 0, 2);
                short stringLength = BitConverter.ToInt16(byte2, 0);
                byte[] byteString = new byte[stringLength];
                fileStream.Read(byteString, 0, stringLength);
                String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
                //Payload



                byte listTagID = (byte)fileStream.ReadByte();

                byte[] sizeArray = new byte[4];
                fileStream.Read(sizeArray, 0, 4);
                int arraySizeNumber = BitConverter.ToInt32(sizeArray, 0);

                byte payloadElementSize = Globals.dataTypeSizes[listTagID];

                byte[] payload = new byte[1 + 4 + arraySizeNumber * payloadElementSize]; //ID + length + payload

                payload[0] = listTagID;
                payload[1] = sizeArray[0];
                payload[2] = sizeArray[1];
                payload[3] = sizeArray[2];
                payload[4] = sizeArray[3];
                fileStream.Read(payload, 5, arraySizeNumber * payloadElementSize);


                returnTag = new Tag(tagID, tagIdentifier, payload);
                return returnTag;
            }
            else if (tagID.Equals(TagID.Compound))
            {
                byte[] byte2 = new byte[2];
                fileStream.Read(byte2, 0, 2);
                short stringLength = BitConverter.ToInt16(byte2, 0);
                byte[] byteString = new byte[stringLength];
                fileStream.Read(byteString, 0, stringLength);
                String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
                // no Payload

                returnTag = new Tag(tagID, tagIdentifier, null);
                return returnTag;


            }
            else if (tagID.Equals(TagID.IntArray))
            {
                byte[] byte2 = new byte[2];
                fileStream.Read(byte2, 0, 2);
                short stringLength = BitConverter.ToInt16(byte2, 0);
                byte[] byteString = new byte[stringLength];
                fileStream.Read(byteString, 0, stringLength);
                String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
                //Payload



                byte[] sizeArray = new byte[4]; //changed for each tag
                fileStream.Read(sizeArray, 0, 4);
                int arraySizeNumber = BitConverter.ToInt32(sizeArray, 0);

                byte[] payload = new byte[4 + arraySizeNumber * 4]; //changed for each tag
                payload[0] = sizeArray[0];
                payload[1] = sizeArray[1];
                payload[2] = sizeArray[2];
                payload[3] = sizeArray[3];
                fileStream.Read(payload, 4, arraySizeNumber * 4);


                returnTag = new Tag(tagID, tagIdentifier, payload);
                return returnTag;
            }
            else
            {
                returnTag = new Tag(tagID, null, null);
                return returnTag;
            }

        }
    }
}

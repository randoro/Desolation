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
            Chunk newChunk;
            regionFile.fileStream.Position = 0;

            bool[] chunksLoaded = regionFile.chunksLoaded;

            bool allChunksLoaded = !Array.Exists(chunksLoaded, delegate(bool x) { return !x; }); //checks if all chunks are loaded

            if (!allChunksLoaded)
            {
                newChunk = new Chunk();
                int chunkXPos = 0;
                int chunkYPos = 0;
                bool chunkIdentified = false;
                bool chunkChecked = false;
                bool chunkAlreadyLoaded = false;
                int layerDepth = 1;
                Tag regionTag = readTag(regionFile.fileStream); //should be compound
                
                
                while (layerDepth > 0)
                {
                    Tag currentTag = readTag(regionFile.fileStream);
                    String tagName = currentTag.getName();
                    TagID tagID = currentTag.getID();


                    if (chunkIdentified & !chunkChecked)
                    {

                        int localxPos;
                        int localyPos;
                        if (chunkXPos >= 0)
                        {
                            localxPos = chunkXPos % 4;
                        }
                        else
                        {
                            localxPos = 3 + (chunkXPos + 1) % 4;
                        }
                        if (chunkYPos >= 0)
                        {
                            localyPos = chunkYPos % 4;
                        }
                        else
                        {
                            localyPos = 3 + (chunkYPos + 1) % 4;
                        }

                        bool isThisChunkLoaded = regionFile.chunksLoaded[localxPos + localyPos * 4];
                        // nummer = xpos % 4 + (ypos % 4) * 4

                        if (isThisChunkLoaded)
                        {
                            chunkAlreadyLoaded = true;
                        }
                        else
                        {
                            //chunk is not loaded and will be now
                            newChunk.XPos = chunkXPos;
                            newChunk.YPos = chunkYPos;
                            regionFile.chunksLoaded[localxPos + localyPos * 4] = true;
                        }

                        chunkChecked = true;
                        
                    }

                    if (!chunkAlreadyLoaded)
                    {

                        switch (tagID)
                        {
                            case TagID.End:
                                layerDepth--;
                                if (layerDepth == 1)
                                {
                                    //end of unloaded chunk
                                    //this means its now added return new chunk
                                    Console.WriteLine("chunk now loaded");
                                    return newChunk;
                                }
                                break;
                            case TagID.Byte:
                                if (tagName.Equals("TerrainPopulated"))
                                {
                                    newChunk.terrainPopulated = (byte)currentTag.getData();
                                }
                                break;
                            case TagID.Short:
                                break;
                            case TagID.Int:
                                if (tagName.Equals("XPos"))
                                {
                                    chunkXPos = (int)currentTag.getData();
                                }
                                else if (tagName.Equals("YPos"))
                                {
                                    chunkYPos = (int)currentTag.getData();
                                    chunkIdentified = true;
                                }
                                break;
                            case TagID.Long:
                                if (tagName.Equals("LastUpdate"))
                                {
                                    newChunk.lastUpdate = (long)currentTag.getData();
                                }
                                else if (tagName.Equals("InhabitedTime"))
                                {
                                    newChunk.inhabitedTime = (long)currentTag.getData();
                                }
                                break;
                            case TagID.Float:
                                break;
                            case TagID.Double:
                                break;
                            case TagID.ByteArray:
                                if (tagName.Equals("Biomes"))
                                {
                                    newChunk.biomes = (byte[])currentTag.getData();
                                }
                                else if (tagName.Equals("Blocks"))
                                {
                                    newChunk.blocks = (byte[])currentTag.getData();
                                }
                                else if (tagName.Equals("Objects"))
                                {
                                    newChunk.objects = (byte[])currentTag.getData();
                                }
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
                                layerDepth = 0;
                                break;
                        }
                    }
                    else
                    {
                        switch (tagID)
                        {
                            case TagID.End:
                                layerDepth--;
                                if (layerDepth == 1)
                                {
                                    //end of already loaded chunk
                                    chunkAlreadyLoaded = false;
                                    chunkIdentified = false;
                                    chunkChecked = false;
                                }
                                break;
                            case TagID.Compound:
                                layerDepth++;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            Console.WriteLine("all chunks are loaded");
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
            else if (tagID.Equals(TagID.List)) //måste fixas så att compound tag sparar extra.   typ, om de e compund tag så körs readTag här inne i en whileloop som breakas genom end tag sen läggs datan mellan varje compund och end in i en bytearray som läggs i en lista av bytearrays.
            {
                byte[] byte2 = new byte[2];
                fileStream.Read(byte2, 0, 2);
                short stringLength = BitConverter.ToInt16(byte2, 0);
                byte[] byteString = new byte[stringLength];
                fileStream.Read(byteString, 0, stringLength);
                String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
                //Payload



                byte listTagID = (byte)fileStream.ReadByte();
                byte[] elementArray = new byte[4];
                fileStream.Read(elementArray, 0, 4);
                int elementsInList = BitConverter.ToInt32(elementArray, 0);

                if (elementsInList > 0)
                {
                    if (!listTagID.Equals(TagID.Compound))
                    {
                        byte payloadElementSize = Globals.dataTypeSizes[listTagID];

                    }
                    else
                    {

                    }
                }

                //if (!listTagID.Equals(TagID.Compound))
                //{

                //    byte[] sizeArray = new byte[4];
                //    fileStream.Read(sizeArray, 0, 4);
                //    int arraySizeNumber = BitConverter.ToInt32(sizeArray, 0);

                //    byte payloadElementSize = Globals.dataTypeSizes[listTagID];

                //    byte[] payload = new byte[1 + 4 + arraySizeNumber * payloadElementSize]; //ID + length + payload

                //    payload[0] = listTagID;
                //    payload[1] = sizeArray[0];
                //    payload[2] = sizeArray[1];
                //    payload[3] = sizeArray[2];
                //    payload[4] = sizeArray[3];
                //    fileStream.Read(payload, 5, arraySizeNumber * payloadElementSize);


                //    returnTag = new Tag(tagID, tagIdentifier, payload);
                //}
                //else
                //{
                    returnTag = new Tag(tagID, tagIdentifier, null);
                //}
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

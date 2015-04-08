using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Desolation
{
    static class TagTranslator
    {

        public static Chunk getUnloadedChunk(Region regionFile)
        {
            Chunk newChunk;
            regionFile.fileStream.Position = 0;

            //bool[] chunksLoaded = regionFile.chunksLoaded;

            //bool allChunksLoaded = !Array.Exists(chunksLoaded, delegate(bool x) { return !x; }); //checks if all chunks are loaded

            //if (!allChunksLoaded)
            //{
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
                            newChunk.innerIndex = (byte)(localxPos + localyPos * 4);
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
                                    newChunk.terrainPopulated = (sbyte)currentTag.getData();
                                }
                                break;
                            case TagID.Short:
                                break;
                            case TagID.Int:
                                if (tagName.Equals("XPos"))
                                {
                                    byte[] data = (byte[])currentTag.getRawData();
                                    int dataInt = BitConverter.ToInt32(data, 0);
                                    chunkXPos = dataInt;
                                }
                                else if (tagName.Equals("YPos"))
                                {
                                    byte[] data = (byte[])currentTag.getRawData();
                                    int dataInt = BitConverter.ToInt32(data, 0);
                                    chunkYPos = dataInt;
                                    chunkIdentified = true;
                                }
                                break;
                            case TagID.Long:
                                if (tagName.Equals("LastUpdate"))
                                {
                                    byte[] data = (byte[])currentTag.getRawData();
                                    long dataLong = BitConverter.ToInt64(data, 0);
                                    newChunk.lastUpdate = dataLong;
                                }
                                else if (tagName.Equals("InhabitedTime"))
                                {
                                    byte[] data = (byte[])currentTag.getRawData();
                                    long dataLong = BitConverter.ToInt64(data, 0);
                                    newChunk.inhabitedTime = dataLong;
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
                                if (tagName.Equals("Entities"))
                                {
                                    newChunk.entities = (List<List<Tag>>)currentTag.getData();
                                }
                                //else if (tagName.Equals("TileEntities"))
                                //{
                                //    newChunk.tileEntities = (List<List<Tag>>)currentTag.getData();
                                //}
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
                //}
            }
            return null;
        }


        public static void saveChunk(Chunk chunk, FileStream fileStream)
        {
            Tag chunkTag = new Tag(TagID.Compound, "chunk", null, TagID.Compound);
            writeTag(chunkTag, fileStream);

            byte[] chunkXpos = BitConverter.GetBytes(chunk.XPos);
            Tag XPos = new Tag(TagID.Int, "XPos", chunkXpos, TagID.Int);
            writeTag(XPos, fileStream);

            byte[] chunkYpos = BitConverter.GetBytes(chunk.YPos);
            Tag YPos = new Tag(TagID.Int, "YPos", chunkYpos, TagID.Int);
            writeTag(YPos, fileStream);

            byte[] chunkLastUpdate = BitConverter.GetBytes(chunk.lastUpdate);
            Tag LastUpdate = new Tag(TagID.Long, "LastUpdate", chunkLastUpdate, TagID.Long);
            writeTag(LastUpdate, fileStream);

            byte terrainByte = (byte)chunk.terrainPopulated;
            byte[] chunkTerrainPopulated = { terrainByte };
            Tag TerrainPopulated = new Tag(TagID.Byte, "TerrainPopulated", chunkTerrainPopulated, TagID.Byte);
            writeTag(TerrainPopulated, fileStream);

            byte[] chunkInhabitedTime = BitConverter.GetBytes(chunk.inhabitedTime);
            Tag InhabitedTime = new Tag(TagID.Long, "InhabitedTime", chunkInhabitedTime, TagID.Long);
            writeTag(InhabitedTime, fileStream);

            Tag Biomes = new Tag(TagID.ByteArray, "Biomes", chunk.biomes, TagID.ByteArray);
            writeTag(Biomes, fileStream);

            Tag Blocks = new Tag(TagID.ByteArray, "Blocks", chunk.blocks, TagID.ByteArray);
            writeTag(Blocks, fileStream);

            Tag Objects = new Tag(TagID.ByteArray, "Objects", chunk.objects, TagID.ByteArray);
            writeTag(Objects, fileStream);

            Tag Entities = new Tag(TagID.List, "Entities", chunk.entities, TagID.Compound);
            writeTag(Entities, fileStream);

            //Tag TileEntities = new Tag(TagID.List, "TileEntities", chunk.tileEntities, TagID.Compound);
            //writeTag(TileEntities, fileStream);

            Tag End = new Tag(TagID.End, null, null, TagID.End);
            writeTag(End, fileStream);

        }

        public static Tag readTag(FileStream fileStream)
        {
            TagID tagID = (TagID)fileStream.ReadByte();
            Tag returnTag;

            if (tagID.Equals(TagID.End))
            {

                returnTag = new Tag(TagID.End, null, null, TagID.End);
                return returnTag; //No payload
            }

            byte[] byte2 = new byte[2];
            fileStream.Read(byte2, 0, 2);
            short stringLength = BitConverter.ToInt16(byte2, 0);
            byte[] byteString = new byte[stringLength];
            fileStream.Read(byteString, 0, stringLength);
            String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
            byte[] payload;

            //Payload
            switch (tagID)
            {
                case TagID.End:
                    returnTag = new Tag(tagID, null, null, tagID);
                    return returnTag; //No payload
                    break;
                case TagID.Byte:

                    payload = new byte[Globals.dataTypeSizes[(int)tagID]]; //changed for each tag
                    payload[0] = (byte)fileStream.ReadByte();
                    returnTag = new Tag(tagID, tagIdentifier, payload, tagID);
                    return returnTag;

                    break;
                case TagID.Short:

                    payload = new byte[Globals.dataTypeSizes[(int)tagID]]; //changed for each tag
                    fileStream.Read(payload, 0, Globals.dataTypeSizes[(int)tagID]);
                    returnTag = new Tag(tagID, tagIdentifier, payload, tagID);
                    return returnTag;

                    break;
                case TagID.Int:

                    payload = new byte[Globals.dataTypeSizes[(int)tagID]]; //changed for each tag
                    fileStream.Read(payload, 0, Globals.dataTypeSizes[(int)tagID]);
                    returnTag = new Tag(tagID, tagIdentifier, payload, tagID);
                    return returnTag;

                    break;
                case TagID.Long:

                    payload = new byte[Globals.dataTypeSizes[(int)tagID]]; //changed for each tag
                    fileStream.Read(payload, 0, Globals.dataTypeSizes[(int)tagID]);
                    returnTag = new Tag(tagID, tagIdentifier, payload, tagID);
                    return returnTag;

                    break;
                case TagID.Float:
                    
                    payload = new byte[Globals.dataTypeSizes[(int)tagID]]; //changed for each tag
                    fileStream.Read(payload, 0, Globals.dataTypeSizes[(int)tagID]);
                    returnTag = new Tag(tagID, tagIdentifier, payload, tagID);
                    return returnTag;

                    break;
                case TagID.Double:

                    payload = new byte[Globals.dataTypeSizes[(int)tagID]]; //changed for each tag
                    fileStream.Read(payload, 0, Globals.dataTypeSizes[(int)tagID]);
                    returnTag = new Tag(tagID, tagIdentifier, payload, tagID);
                    return returnTag;

                    break;
                case TagID.ByteArray:

                    byte[] sizeArray = new byte[4]; //changed for each tag
                    fileStream.Read(sizeArray, 0, 4);
                    int arraySizeNumber = BitConverter.ToInt32(sizeArray, 0);
                    payload = new byte[4 + arraySizeNumber]; //changed for each tag
                    payload[0] = sizeArray[0];
                    payload[1] = sizeArray[1];
                    payload[2] = sizeArray[2];
                    payload[3] = sizeArray[3];
                    fileStream.Read(payload, 4, arraySizeNumber);
                    returnTag = new Tag(tagID, tagIdentifier, payload, tagID);
                    return returnTag;

                    break;
                case TagID.String:

                    byte[] sizeArray2 = new byte[2]; //changed for each tag
                    fileStream.Read(sizeArray2, 0, 2);
                    short stringSizeNumber = BitConverter.ToInt16(sizeArray2, 0);
                    payload = new byte[2 + stringSizeNumber]; //changed for each tag
                    payload[0] = sizeArray2[0];
                    payload[1] = sizeArray2[1];
                    fileStream.Read(payload, 2, stringSizeNumber);
                    returnTag = new Tag(tagID, tagIdentifier, payload, tagID);
                    return returnTag;

                    break;
                case TagID.List:

                    //sbyte listTagID = (sbyte)fileStream.ReadByte();
                    byte[] elementArray = new byte[4];
                    fileStream.Read(elementArray, 0, 4);
                    int elementsInList = BitConverter.ToInt32(elementArray, 0); //number of List<Tag>s
                    List<List<Tag>> tagListList = new List<List<Tag>>();

                    if (elementsInList > 0)
                    {
                        //if (!listTagID.Equals(TagID.Compound))
                        //{
                        //    byte payloadElementSize = Globals.dataTypeSizes[listTagID];

                        //    List<byte[]> byteArrayList = new List<byte[]>();

                        //    for (int i = 0; i < elementsInList; i++)
                        //    {
                        //        byte[] element = new byte[payloadElementSize];
                        //        fileStream.Read(element, 0, payloadElementSize);
                        //        byteArrayList.Add(element);
                        //    }

                        //    returnTag = new Tag(tagID, tagIdentifier, byteArrayList, (TagID)listTagID);
                        //    return returnTag;
                        //}
                        //else
                        //{
                            

                            for (int i = 0; i < elementsInList; i++)
                            {
                                List<Tag> tagList = new List<Tag>();
                                bool stillInList = true;
                                while (stillInList)
                                {
                                    Tag aTag = TagTranslator.readTag(fileStream);
                                    tagList.Add(aTag);
                                    if (aTag.getID().Equals(TagID.End))
                                    {
                                        stillInList = false;
                                    }
                                }
                                tagListList.Add(tagList);

                                
                            }

                            returnTag = new Tag(tagID, tagIdentifier, tagListList, TagID.Compound);
                            return returnTag;


                        //}
                    }
                    returnTag = new Tag(tagID, tagIdentifier, tagListList, TagID.Compound); //change
                    return returnTag;

                    break;
                case TagID.Compound:
                    returnTag = new Tag(tagID, tagIdentifier, null, tagID);
                    return returnTag; //No payload
                    break;
                case TagID.IntArray:

                    byte[] sizeArray3 = new byte[4]; //changed for each tag
                    fileStream.Read(sizeArray3, 0, 4);
                    int arraySizeNumber2 = BitConverter.ToInt32(sizeArray3, 0);

                    payload = new byte[4 + arraySizeNumber2 * 4]; //changed for each tag
                    payload[0] = sizeArray3[0];
                    payload[1] = sizeArray3[1];
                    payload[2] = sizeArray3[2];
                    payload[3] = sizeArray3[3];
                    fileStream.Read(payload, 4, arraySizeNumber2 * 4);
                    returnTag = new Tag(tagID, tagIdentifier, payload, tagID);
                    return returnTag;

                    break;
                default:
                    returnTag = new Tag(tagID, tagIdentifier, null, tagID);
                    return returnTag; //No payload
                    break;
            }

            //if (tagID.Equals(TagID.Byte))
            //{
            //    byte[] byte2 = new byte[2];
            //    fileStream.Read(byte2, 0, 2);
            //    short stringLength = BitConverter.ToInt16(byte2, 0);
            //    byte[] byteString = new byte[stringLength];
            //    fileStream.Read(byteString, 0, stringLength);
            //    String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
            //    //Payload



            //    byte[] payload = new byte[Globals.dataTypeSizes[(int)tagID]]; //changed for each tag
            //    payload[0] = (byte)fileStream.ReadByte();



            //    returnTag = new Tag(tagID, tagIdentifier, payload, tagID);
            //    return returnTag;
            //}
            //else if (tagID.Equals(TagID.Short))
            //{
            //    byte[] byte2 = new byte[2];
            //    fileStream.Read(byte2, 0, 2);
            //    short stringLength = BitConverter.ToInt16(byte2, 0);
            //    byte[] byteString = new byte[stringLength];
            //    fileStream.Read(byteString, 0, stringLength);
            //    String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
            //    //Payload


            //    byte[] payload = new byte[Globals.dataTypeSizes[(int)tagID]]; //changed for each tag
            //    fileStream.Read(payload, 0, Globals.dataTypeSizes[(int)tagID]);

            //    returnTag = new Tag(tagID, tagIdentifier, payload, tagID);
            //    return returnTag;
            //}
            //else if (tagID.Equals(TagID.Int))
            //{
            //    byte[] byte2 = new byte[2];
            //    fileStream.Read(byte2, 0, 2);
            //    short stringLength = BitConverter.ToInt16(byte2, 0);
            //    byte[] byteString = new byte[stringLength];
            //    fileStream.Read(byteString, 0, stringLength);
            //    String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
            //    //Payload


            //    byte[] payload = new byte[Globals.dataTypeSizes[(int)tagID]]; //changed for each tag
            //    fileStream.Read(payload, 0, Globals.dataTypeSizes[(int)tagID]);

            //    returnTag = new Tag(tagID, tagIdentifier, payload, tagID);
            //    return returnTag;
            //}
            //else if (tagID.Equals(TagID.Long))
            //{
            //    byte[] byte2 = new byte[2];
            //    fileStream.Read(byte2, 0, 2);
            //    short stringLength = BitConverter.ToInt16(byte2, 0);
            //    byte[] byteString = new byte[stringLength];
            //    fileStream.Read(byteString, 0, stringLength);
            //    String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
            //    //Payload


            //    byte[] payload = new byte[Globals.dataTypeSizes[(int)tagID]]; //changed for each tag
            //    fileStream.Read(payload, 0, Globals.dataTypeSizes[(int)tagID]);

            //    returnTag = new Tag(tagID, tagIdentifier, payload, tagID);
            //    return returnTag;
            //}
            //else if (tagID.Equals(TagID.Float))
            //{
            //    byte[] byte2 = new byte[2];
            //    fileStream.Read(byte2, 0, 2);
            //    short stringLength = BitConverter.ToInt16(byte2, 0);
            //    byte[] byteString = new byte[stringLength];
            //    fileStream.Read(byteString, 0, stringLength);
            //    String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
            //    //Payload


            //    byte[] payload = new byte[Globals.dataTypeSizes[(int)tagID]]; //changed for each tag
            //    fileStream.Read(payload, 0, Globals.dataTypeSizes[(int)tagID]);

            //    returnTag = new Tag(tagID, tagIdentifier, payload, tagID);
            //    return returnTag;
            //}
            //else if (tagID.Equals(TagID.Double))
            //{
            //    byte[] byte2 = new byte[2];
            //    fileStream.Read(byte2, 0, 2);
            //    short stringLength = BitConverter.ToInt16(byte2, 0);
            //    byte[] byteString = new byte[stringLength];
            //    fileStream.Read(byteString, 0, stringLength);
            //    String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
            //    //Payload


            //    byte[] payload = new byte[Globals.dataTypeSizes[(int)tagID]]; //changed for each tag
            //    fileStream.Read(payload, 0, Globals.dataTypeSizes[(int)tagID]);

            //    returnTag = new Tag(tagID, tagIdentifier, payload, tagID);
            //    return returnTag;
            //}
            //else if (tagID.Equals(TagID.ByteArray))
            //{
            //    byte[] byte2 = new byte[2];
            //    fileStream.Read(byte2, 0, 2);
            //    short stringLength = BitConverter.ToInt16(byte2, 0);
            //    byte[] byteString = new byte[stringLength];
            //    fileStream.Read(byteString, 0, stringLength);
            //    String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
            //    //Payload



            //    byte[] sizeArray = new byte[4]; //changed for each tag
            //    fileStream.Read(sizeArray, 0, 4);
            //    int arraySizeNumber = BitConverter.ToInt32(sizeArray, 0);

            //    byte[] payload = new byte[4 + arraySizeNumber]; //changed for each tag
            //    payload[0] = sizeArray[0];
            //    payload[1] = sizeArray[1];
            //    payload[2] = sizeArray[2];
            //    payload[3] = sizeArray[3];
            //    fileStream.Read(payload, 4, arraySizeNumber);


            //    returnTag = new Tag(tagID, tagIdentifier, payload, tagID);
            //    return returnTag;
            //}
            //else if (tagID.Equals(TagID.String))
            //{
            //    byte[] byte2 = new byte[2];
            //    fileStream.Read(byte2, 0, 2);
            //    short stringLength = BitConverter.ToInt16(byte2, 0);
            //    byte[] byteString = new byte[stringLength];
            //    fileStream.Read(byteString, 0, stringLength);
            //    String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
            //    //Payload



            //    byte[] sizeArray = new byte[2]; //changed for each tag
            //    fileStream.Read(sizeArray, 0, 2);
            //    short stringSizeNumber = BitConverter.ToInt16(sizeArray, 0);

            //    byte[] payload = new byte[2 + stringSizeNumber]; //changed for each tag
            //    payload[0] = sizeArray[0];
            //    payload[1] = sizeArray[1];
            //    fileStream.Read(payload, 2, stringSizeNumber);


            //    returnTag = new Tag(tagID, tagIdentifier, payload, tagID);
            //    return returnTag;
            //}
            //else if (tagID.Equals(TagID.List)) //måste fixas så att compound tag sparar extra.   typ, om de e compund tag så körs readTag här inne i en whileloop som breakas genom end tag sen läggs datan mellan varje compund och end in i en bytearray som läggs i en lista av bytearrays.
            //{
            //    byte[] byte2 = new byte[2];
            //    fileStream.Read(byte2, 0, 2);
            //    short stringLength = BitConverter.ToInt16(byte2, 0);
            //    byte[] byteString = new byte[stringLength];
            //    fileStream.Read(byteString, 0, stringLength);
            //    String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
            //    //Payload



            //    byte listTagID = (byte)fileStream.ReadByte();
            //    byte[] elementArray = new byte[4];
            //    fileStream.Read(elementArray, 0, 4);
            //    int elementsInList = BitConverter.ToInt32(elementArray, 0);

            //    if (elementsInList > 0)
            //    {
            //        if (!listTagID.Equals(TagID.Compound))
            //        {
            //            byte payloadElementSize = Globals.dataTypeSizes[listTagID];

            //            List<byte[]> byteArrayList = new List<byte[]>();

            //            for (int i = 0; i < elementsInList; i++)
            //            {
            //                byte[] element = new byte[payloadElementSize];
            //                fileStream.Read(element, 0, payloadElementSize);
            //                byteArrayList.Add(element);
            //            }

            //            returnTag = new Tag(tagID, tagIdentifier, byteArrayList, (TagID)listTagID);
            //            return returnTag;
            //        }
            //        else
            //        {

            //        }
            //    }

            //    //if (!listTagID.Equals(TagID.Compound))
            //    //{

            //    //    byte[] sizeArray = new byte[4];
            //    //    fileStream.Read(sizeArray, 0, 4);
            //    //    int arraySizeNumber = BitConverter.ToInt32(sizeArray, 0);

            //    //    byte payloadElementSize = Globals.dataTypeSizes[listTagID];

            //    //    byte[] payload = new byte[1 + 4 + arraySizeNumber * payloadElementSize]; //ID + length + payload

            //    //    payload[0] = listTagID;
            //    //    payload[1] = sizeArray[0];
            //    //    payload[2] = sizeArray[1];
            //    //    payload[3] = sizeArray[2];
            //    //    payload[4] = sizeArray[3];
            //    //    fileStream.Read(payload, 5, arraySizeNumber * payloadElementSize);


            //    //    returnTag = new Tag(tagID, tagIdentifier, payload);
            //    //}
            //    //else
            //    //{
            //    returnTag = new Tag(tagID, tagIdentifier, null, tagID); //change
            //    //}
            //    return returnTag;
            //}
            //else if (tagID.Equals(TagID.Compound))
            //{
            //    byte[] byte2 = new byte[2];
            //    fileStream.Read(byte2, 0, 2);
            //    short stringLength = BitConverter.ToInt16(byte2, 0);
            //    byte[] byteString = new byte[stringLength];
            //    fileStream.Read(byteString, 0, stringLength);
            //    String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
            //    // no Payload

            //    returnTag = new Tag(tagID, tagIdentifier, null, tagID);
            //    return returnTag;


            //}
            //else if (tagID.Equals(TagID.IntArray))
            //{
            //    byte[] byte2 = new byte[2];
            //    fileStream.Read(byte2, 0, 2);
            //    short stringLength = BitConverter.ToInt16(byte2, 0);
            //    byte[] byteString = new byte[stringLength];
            //    fileStream.Read(byteString, 0, stringLength);
            //    String tagIdentifier = Encoding.UTF8.GetString(byteString, 0, stringLength);
            //    //Payload



            //    byte[] sizeArray = new byte[4]; //changed for each tag
            //    fileStream.Read(sizeArray, 0, 4);
            //    int arraySizeNumber = BitConverter.ToInt32(sizeArray, 0);

            //    byte[] payload = new byte[4 + arraySizeNumber * 4]; //changed for each tag
            //    payload[0] = sizeArray[0];
            //    payload[1] = sizeArray[1];
            //    payload[2] = sizeArray[2];
            //    payload[3] = sizeArray[3];
            //    fileStream.Read(payload, 4, arraySizeNumber * 4);


            //    returnTag = new Tag(tagID, tagIdentifier, payload, tagID);
            //    return returnTag;
            //}
            //else
            //{
            //    returnTag = new Tag(tagID, null, null, tagID);
            //    return returnTag;
            //}

        }

        public static void writeTag(Tag tag, FileStream fileStream)
        {
            TagID tagID = tag.getID();
            String tagName = tag.getName();
            var data = tag.getRawData();

            fileStream.WriteByte((byte)tagID);

            if (tagID.Equals(TagID.End))
            {
                return; //No payload
            }

            byte[] byteArray3 = BitConverter.GetBytes((short)tagName.Length);
            byte[] buffer3 = Encoding.UTF8.GetBytes(tagName);


            
            fileStream.WriteByte(byteArray3[0]);
            fileStream.WriteByte(byteArray3[1]);
            fileStream.Write(buffer3, 0, (short)tagName.Length);

            byte[] dataInBytes;
            //Payload
            switch (tagID)
            {
                case TagID.End:
                    return; //No payload
                    break;
                case TagID.Byte:
                    unchecked
                    {
                        fileStream.Write((byte[])data, 0, 1);
                    }
                    return;
                    break;
                case TagID.Short:
                    //dataInBytes = BitConverter.GetBytes((short)data);
                    fileStream.Write((byte[])data, 0, 2);
                    return;

                    break;
                case TagID.Int:

                    //dataInBytes = BitConverter.GetBytes((int)data);
                    fileStream.Write((byte[])data, 0, 4);
                    return;

                    break;
                case TagID.Long:
                    //dataInBytes = BitConverter.GetBytes((long)data);
                    fileStream.Write((byte[])data, 0, 8);
                    return;

                    break;
                case TagID.Float:
                    //dataInBytes = BitConverter.GetBytes((float)data);
                    fileStream.Write((byte[])data, 0, 4);
                    return;

                    break;
                case TagID.Double:
                    //dataInBytes = BitConverter.GetBytes((double)data);
                    fileStream.Write((byte[])data, 0, 8);
                    return;

                    break;
                case TagID.ByteArray:

                    int sizeArray = ((byte[])data).Length; //changed for each tag
                    byte[] sizeArrayArray = BitConverter.GetBytes((int)sizeArray);

                    fileStream.Write(sizeArrayArray, 0, 4);
                    fileStream.Write((byte[])data, 0, sizeArray);
                    return;
                    break;
                case TagID.String:
                    short stringLength = (short)((byte[])data).Length;
                    byte[] stringArrayLength = BitConverter.GetBytes(stringLength);


                    fileStream.WriteByte(stringArrayLength[0]);
                    fileStream.WriteByte(stringArrayLength[1]);
                    fileStream.Write((byte[])data, 0, stringLength);


                    return;

                    break;
                case TagID.List:
                    //fix this
                    
                        List<List<Tag>> theData = ((List<List<Tag>>)data);

                        //fileStream.WriteByte((byte)tag.getListID());
                        int elementsInList = theData.Count;
                        byte[] elementArray = BitConverter.GetBytes(elementsInList);
                        fileStream.Write(elementArray, 0, 4);

                        if (elementsInList > 0)
                        {
                            if (tag.getListID().Equals(TagID.Compound))
                            {
                                foreach (List<Tag> list in theData)
                                {
                                    foreach (Tag innerTag in list)
                                    {
                                        TagTranslator.writeTag(innerTag, fileStream);
                                    }
                                }
                            }
                        }

                    break;
                case TagID.Compound:
                    return; //No payload
                    break;
                case TagID.IntArray:
                //    fix this

                //    byte[] sizeArray3 = new byte[4]; //changed for each tag
                //    fileStream.Read(sizeArray3, 0, 4);
                //    int arraySizeNumber2 = BitConverter.ToInt32(sizeArray3, 0);

                //    payload = new byte[4 + arraySizeNumber2 * 4]; //changed for each tag
                //    payload[0] = sizeArray3[0];
                //    payload[1] = sizeArray3[1];
                //    payload[2] = sizeArray3[2];
                //    payload[3] = sizeArray3[3];
                //    fileStream.Read(payload, 4, arraySizeNumber2 * 4);
                //    returnTag = new Tag(tagID, tagIdentifier, payload, tagID);
                //    return returnTag;

                //    break;
                //default:
                //    returnTag = new Tag(tagID, tagIdentifier, null, tagID);
                    return; //No payload
                    break;
            }
        }

        public static void overwriteRegionStream(Region region, int index)
        {

            if (region != null)
            {
                FileStream fileStream = region.fileStream;
                bool allChunksLoaded = !Array.Exists(region.chunksLoaded, delegate(bool x) { return !x; }); //checks if all chunks are loaded

                if (allChunksLoaded)
                {
                    


                    fileStream.SetLength(0);
                    fileStream.Position = 0;
                    Tag chunkTag = new Tag(TagID.Compound, "region", null, TagID.Compound);
                    writeTag(chunkTag, fileStream);

                    int xindex = index % 3;
                    int yindex = index / 3;

                    for (int innerIndex = 0; innerIndex < 16; innerIndex++)
                    {
                        int xinnedIndex = innerIndex % 4;
                        int yinnedIndex = innerIndex / 4;

                        Chunk currentChunk = ChunkManager.chunkArray[(((yindex * 4) + yinnedIndex) * 12) + ((xindex * 4) + xinnedIndex)];
                        if (currentChunk != null)
                        {
                            saveChunk(currentChunk, fileStream);
                        }
                    }

                    Tag endTag = new Tag(TagID.End, null, null, TagID.End);
                    writeTag(endTag, fileStream);

                    
                }

                fileStream.Close();
            }
            
        }

        public static Entity getUnloadedEntity(List<Tag> entity)
        {
            Entity newEntity = new Goblin(Vector2.Zero); // will never be used
            bool isReturnable = false;
            foreach (Tag e in entity)
            {
                String tagName = e.getName();
                TagID tagID = e.getID();
                var data = e.getRawData();
                

                switch (tagID)
                {
                    case TagID.End:
                            //end of unloaded chunk
                            //this means its now added return new chunk
                            Console.WriteLine("entity now loaded");
                            isReturnable = true;
                        break;
                    case TagID.Byte:
                        
                        break;
                    case TagID.Short:
                        break;
                    case TagID.Int:
                        if (tagName.Equals("ID"))
                        {
                            byte[] byteData = (byte[])e.getRawData();
                            int dataInt = BitConverter.ToInt32(byteData, 0);
                            EntityID ID = (EntityID)dataInt;
                            switch (ID)
                            {
                                case EntityID.Player:
                                    break;
                                case EntityID.Goblin:
                                    newEntity = new Goblin(Vector2.Zero);
                                    break;
                                case EntityID.Zombie:
                                    newEntity = new Zombie(Vector2.Zero);
                                    break;
                                case EntityID.Deer:
                                    newEntity = new Deer(Vector2.Zero);
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (tagName.Equals("EntityXPos"))
                        {
                            byte[] byteData = (byte[])e.getRawData();
                            int dataInt = BitConverter.ToInt32(byteData, 0);
                            newEntity.position.X = (float)dataInt;
                        }
                        else if (tagName.Equals("EntityYPos"))
                        {
                            byte[] byteData = (byte[])e.getRawData();
                            int dataInt = BitConverter.ToInt32(byteData, 0);
                            newEntity.position.Y = (float)dataInt;
                            //byte[] data = (byte[])e.getRawData();
                            //float dataFloat = BitConverter.ToSingle(data, 0);
                            //newEntity.position.Y = dataFloat;
                        }
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
                        break;
                    case TagID.IntArray:
                        break;
                    default:
                        break;
                }
            }

            if (isReturnable)
            {
                return newEntity;
            }
            else
            {
                return null;
            }
        }
    }
}

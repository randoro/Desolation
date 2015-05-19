using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Desolation
{
    public class ChunkManager
    {

        public static FileLoader fileLoader;
        public static Chunk[] chunkArray;
        public static Region[] regionArray;
        public static List<Entity> entityList;
        public static List<Structure> structureList;
        public static List<Room> roomList;
        public static Counter syncCounter;

        int lastRegionX;
        int lastRegionY;
        int lastChunkX;
        int lastChunkY;

        int newRegionX;
        int newRegionY;
        int newChunkX;
        int newChunkY;

        public ChunkManager()
        {
            fileLoader = new FileLoader();
            regionArray = new Region[9];
            entityList = new List<Entity>();
            roomList = new List<Room>();
            structureList = new List<Structure>();

            syncCounter = new Counter(); //debug
            chunkArray = new Chunk[144];

            

        }


        public void update(GameTime gameTime, GameWindow window)
        {
            syncCounter.Update(gameTime);

            for (int i = ChunkManager.entityList.Count - 1; i >= 0; i--)
            {

                ChunkManager.entityList[i].Update(gameTime);
                if (ChunkManager.entityList[i].health <= 0)
                {
                    ChunkManager.entityList.RemoveAt(i);
                }
            }

            Globals.currentStructureID = 0;
            for (int i = ChunkManager.roomList.Count - 1; i >= 0; i--)
            {
                if (ChunkManager.roomList[i].area.Contains((int)Globals.playerPos.X, (int)Globals.playerPos.Y))
                {
                    Globals.currentStructureID = ChunkManager.roomList[i].structureID;
                    break;
                }
            }

                
                

            
        }


        public void syncUpdate(GameTime gameTime)
        {
            syncCounter.increaseCounter();

            foreach (Entity e in entityList)
            {
                e.syncUpdate(gameTime); //must be done before chunkManager shifting to not interfear with movement and collision
            }

                newRegionX = Globals.getRegionValue(Globals.playerPos.X);
                newRegionY = Globals.getRegionValue(Globals.playerPos.Y);

                newChunkX = Globals.getChunkValue(Globals.playerPos.X);
                newChunkY = Globals.getChunkValue(Globals.playerPos.Y);
                


                //time for new chunkLoad
                #region ChunkLoading
                for (int i = 0; i < 9; i++)
                {
                    if (regionArray[i] != null)
                    {
                        bool[] chunksLoaded = regionArray[i].chunksLoaded;

                        bool allChunksLoaded = !Array.Exists(chunksLoaded, delegate(bool x) { return !x; }); //checks if all chunks are loaded

                        if (!allChunksLoaded)
                        {
                            Chunk newChunk = TagTranslator.getUnloadedChunk(regionArray[i]);

                            if (newChunk == null || newChunk.terrainPopulated == 0)
                            {
                                if(fileLoader.currentWorldFolder.Equals(@"mainworld\")) 
                                {
                                    newChunk = Generator.createForrestChunk(regionArray[i]);
                                }
                                else if (fileLoader.currentWorldFolder.Equals(@"desert\"))
                                {
                                    newChunk = Generator.createDesertChunk(regionArray[i]);
                                }
                                else if (fileLoader.currentWorldFolder.Equals(@"tundra\"))
                                {

                                }
                            }

                            if (newChunk != null)
                            {
                                byte innerIndex = newChunk.innerIndex;
                                chunkArray[((innerIndex / 4) + (i / 3) * 4) * 12 + (innerIndex % 4) + (i % 3) * 4] = newChunk;

                                if (newChunk.entities != null)
                                {
                                    for (int j = 0; j < newChunk.entities.Count; j++)
                                    {
                                    List<Tag> e = newChunk.entities[j];
                                    
                                        Entity newEntity = TagTranslator.getUnloadedEntity(e);
                                        if (newEntity != null)
                                        {
                                            //if (newEntity.position.X == 0 && newEntity.position.Y == 0)
                                            //{
                                            //    int test = 1;
                                            //}
                                            entityList.Add(newEntity);
                                        }
                                    }
                                    newChunk.entities.Clear();
                                }

                                if (newChunk.rooms != null)
                                {
                                    for (int j = 0; j < newChunk.rooms.Count; j++)
                                    {
                                        List<Tag> e = newChunk.rooms[j];

                                        Room newRoom = TagTranslator.getUnloadedRoom(e);
                                        if (newRoom != null)
                                        {
                                            roomList.Add(newRoom);
                                        }
                                    }
                                    newChunk.rooms.Clear();
                                }
                            }
                        }
                        else
                        {
                            //all chunks loaded
                            for (int k = 0; k < 16; k++)
                            {
                                if (chunkArray[((k / 4) + (i / 3) * 4) * 12 + (k % 4) + (i % 3) * 4].structurePopulated == 0)
                                {
                                    Generator.tryToGenerateStructure(chunkArray[((k / 4) + (i / 3) * 4) * 12 + (k % 4) + (i % 3) * 4]);
                                }
                            }
                        }


                    }
                    else
                    {
                        if (newRegionX == lastRegionX && newRegionY == lastRegionY)
                        {
                            int currentRegionLoadX = Globals.getRegionValue(Globals.playerPos.X + 1024 * ( -1 + i % 3));
                            int currentRegionLoadY = Globals.getRegionValue(Globals.playerPos.Y + 1024 * ( -1 + i / 3));
                            regionArray[i] = fileLoader.loadRegionFile(currentRegionLoadX, currentRegionLoadY);
                        }
                    }
                }
                #endregion

                #region ShiftRegions
                if (newRegionX != lastRegionX || newRegionY != lastRegionY)
                {

                    if (newRegionX < lastRegionX)
                    {
                        if (newRegionY < lastRegionY)
                        {
                        #region Northwest

                            //2, 5, 8, 7, 6
                            saveListToRegion(Lists.Entities, 1024, 1024, 2, 5, 8, 7, 6);
                            saveListToRegion(Lists.Rooms, 1024, 1024, 2, 5, 8, 7, 6);
                            
                            lastRegionX = newRegionX;
                            lastRegionY = newRegionY;

                            TagTranslator.overwriteRegionStream(regionArray[2], 2);
                            TagTranslator.overwriteRegionStream(regionArray[5], 5);
                            TagTranslator.overwriteRegionStream(regionArray[8], 8);
                            TagTranslator.overwriteRegionStream(regionArray[7], 7);
                            TagTranslator.overwriteRegionStream(regionArray[6], 6);


                            Globals.shiftRegionsDown(ref regionArray);
                            Globals.shiftChunksDown(ref chunkArray);

                            Globals.shiftRegionsRight(ref regionArray);
                            Globals.shiftChunksRight(ref chunkArray);
                            #endregion
                        }
                        else if (newRegionY > lastRegionY)
                        {
                        #region SouthWest

                            //0, 1, 2, 5, 8
                            saveListToRegion(Lists.Entities, 1024, -1024, 0, 1, 2, 5, 8);
                            saveListToRegion(Lists.Rooms, 1024, -1024, 0, 1, 2, 5, 8);

                            lastRegionX = newRegionX;
                            lastRegionY = newRegionY;

                            TagTranslator.overwriteRegionStream(regionArray[0], 0);
                            TagTranslator.overwriteRegionStream(regionArray[1], 1);
                            TagTranslator.overwriteRegionStream(regionArray[2], 2);
                            TagTranslator.overwriteRegionStream(regionArray[5], 5);
                            TagTranslator.overwriteRegionStream(regionArray[8], 8);

                            Globals.shiftRegionsUp(ref regionArray);
                            Globals.shiftChunksUp(ref chunkArray);

                            Globals.shiftRegionsRight(ref regionArray);
                            Globals.shiftChunksRight(ref chunkArray);
                        #endregion
                        }
                        else
                        {
                        #region West

                            //2, 5, 8
                            saveListToRegion(Lists.Entities, 1024, 0, 2, 5, 8);
                            saveListToRegion(Lists.Rooms, 1024, 0, 2, 5, 8);

                            lastRegionX = newRegionX;

                            TagTranslator.overwriteRegionStream(regionArray[2], 2);
                            TagTranslator.overwriteRegionStream(regionArray[5], 5);
                            TagTranslator.overwriteRegionStream(regionArray[8], 8);

                            Globals.shiftRegionsRight(ref regionArray);
                            Globals.shiftChunksRight(ref chunkArray);
                        #endregion
                        }

                    }
                    else if (newRegionX > lastRegionX)
                    {
                        if (newRegionY < lastRegionY)
                        {
                        #region NorthEast

                            //0, 3, 6, 7, 8
                            saveListToRegion(Lists.Entities, -1024, 1024, 0, 3, 6, 7, 8);
                            saveListToRegion(Lists.Rooms, -1024, 1024, 0, 3, 6, 7, 8);

                            lastRegionX = newRegionX;
                            lastRegionY = newRegionY;

                            TagTranslator.overwriteRegionStream(regionArray[0], 0);
                            TagTranslator.overwriteRegionStream(regionArray[3], 3);
                            TagTranslator.overwriteRegionStream(regionArray[6], 6);
                            TagTranslator.overwriteRegionStream(regionArray[7], 7);
                            TagTranslator.overwriteRegionStream(regionArray[8], 8);

                            Globals.shiftRegionsLeft(ref regionArray);
                            Globals.shiftChunksLeft(ref chunkArray);
                            Globals.shiftRegionsDown(ref regionArray);
                            Globals.shiftChunksDown(ref chunkArray);
                        #endregion
                        }
                        else if (newRegionY > lastRegionY)
                        {
                        #region SouthEast

                            //0, 1, 2, 3, 6
                            saveListToRegion(Lists.Entities, -1024, -1024, 0, 1, 2, 3, 6);
                            saveListToRegion(Lists.Rooms, -1024, -1024, 0, 1, 2, 3, 6);

                            lastRegionX = newRegionX;
                            lastRegionY = newRegionY;

                            TagTranslator.overwriteRegionStream(regionArray[0], 0);
                            TagTranslator.overwriteRegionStream(regionArray[1], 1);
                            TagTranslator.overwriteRegionStream(regionArray[2], 2);
                            TagTranslator.overwriteRegionStream(regionArray[3], 3);
                            TagTranslator.overwriteRegionStream(regionArray[6], 6);

                            Globals.shiftRegionsUp(ref regionArray);
                            Globals.shiftChunksUp(ref chunkArray);
                            Globals.shiftRegionsLeft(ref regionArray);
                            Globals.shiftChunksLeft(ref chunkArray);
                        #endregion
                        }
                        else
                        {
                        #region East


                            //0, 3, 6
                            saveListToRegion(Lists.Entities, -1024, 0, 0, 3, 6);
                            saveListToRegion(Lists.Rooms, -1024, 0, 0, 3, 6);

                            lastRegionX = newRegionX;

                            TagTranslator.overwriteRegionStream(regionArray[0], 0);
                            TagTranslator.overwriteRegionStream(regionArray[3], 3);
                            TagTranslator.overwriteRegionStream(regionArray[6], 6);

                            Globals.shiftRegionsLeft(ref regionArray);
                            Globals.shiftChunksLeft(ref chunkArray);
                        #endregion
                        }

                    }
                    else
                    {
                        if (newRegionY < lastRegionY)
                        {
                        #region North

                            //6, 7, 8
                            saveListToRegion(Lists.Entities, 0, 1024, 6, 7, 8);
                            saveListToRegion(Lists.Rooms, 0, 1024, 6, 7, 8);

                            lastRegionY = newRegionY;

                            TagTranslator.overwriteRegionStream(regionArray[6], 6);
                            TagTranslator.overwriteRegionStream(regionArray[7], 7);
                            TagTranslator.overwriteRegionStream(regionArray[8], 8);

                            Globals.shiftRegionsDown(ref regionArray);
                            Globals.shiftChunksDown(ref chunkArray);
                        #endregion
                        }
                        else if (newRegionY > lastRegionY)
                        {
                        #region South

                            //0, 1, 2
                            saveListToRegion(Lists.Entities, 0, -1024, 0, 1, 2);
                            saveListToRegion(Lists.Rooms, 0, -1024, 0, 1, 2);

                            lastRegionY = newRegionY;

                            TagTranslator.overwriteRegionStream(regionArray[0], 0);
                            TagTranslator.overwriteRegionStream(regionArray[1], 1);
                            TagTranslator.overwriteRegionStream(regionArray[2], 2);

                            Globals.shiftRegionsUp(ref regionArray);
                            Globals.shiftChunksUp(ref chunkArray);
                        #endregion
                        }
                    }

                }
                #endregion


                int nr = Game1.player.getCurrentChunkNrInArray(Globals.playerPos, Globals.playerPos); //must be done after shifting
                if (chunkArray[nr] != null)
                {
                    Globals.currentPlayerChunkXPos = chunkArray[nr].XPos;
                    Globals.currentPlayerChunkYPos = chunkArray[nr].YPos;
                }
                
                        
              


        }

        public void draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 144; i++)
			{
                if(chunkArray[i] != null) 
                {
                    if (chunkArray[i].XPos > Globals.currentPlayerChunkXPos - 5 && chunkArray[i].XPos < Globals.currentPlayerChunkXPos + 5 && chunkArray[i].YPos > Globals.currentPlayerChunkYPos - 4 && chunkArray[i].YPos < Globals.currentPlayerChunkYPos + 4)
                    {
                        chunkArray[i].draw(spriteBatch);
                    }
                }
            }

            foreach (Entity e in entityList)
            {
                e.Draw(spriteBatch);
            }

            foreach (Room e in roomList)
            {
                e.draw(spriteBatch); //draws roof
            }
        }


        private static void saveEntityToChunk(ref Entity e, int chunkNr)
        {
            Chunk curChunk = chunkArray[chunkNr];
            if (curChunk != null)
            {
                List<Tag> newList = new List<Tag>();
                e.getTagList(ref newList);
                curChunk.entities.Add(newList);
            }
        }


        private static void saveRoomToChunk(ref Room e, int chunkNr)
        {
            Chunk curChunk = chunkArray[chunkNr];
            if (curChunk != null)
            {
                List<Tag> newList = new List<Tag>();
                e.getTagList(ref newList);
                curChunk.rooms.Add(newList);
            }
        }



        public static void changeWorld(String worldName)
        {
            //unloading
            saveList(Lists.Entities);
            saveList(Lists.Rooms);

            saveAndUnloadRegions();
            

            //changing world
            String newWorldName = worldName;
            String newWorldFolder = newWorldName+@"\";
            String regionFolder = @"region\";
            fileLoader.checkAndCreateFolder(newWorldFolder);
            fileLoader.checkAndCreateFolder(newWorldFolder + regionFolder);
            fileLoader.currentWorldFolder = newWorldFolder;

        }

        public static void saveList(Lists list)
        {
            switch (list)
            {
                case Lists.Entities:
                    for (int i = entityList.Count - 1; i >= 0; i--)
                    {

                        Entity e = entityList[i];
                        Vector2 pos = e.position;
                        int regionX = Globals.getRegionValue(pos.X);
                        int regionY = Globals.getRegionValue(pos.Y);
                        //entity is inside unloading regions cause all regions are unloading
                        int chunkNr = e.getCurrentChunkNrInArray(e.position, Globals.playerPos);
                        if (chunkNr != -1)
                        {
                                saveEntityToChunk(ref e, chunkNr);
                                entityList.Remove(e);
                        }
                    }
                    break;
                case Lists.Rooms:
                    for (int i = roomList.Count - 1; i >= 0; i--)
                    {

                        Room e = roomList[i];
                        Vector2 pos = new Vector2(e.area.X, e.area.Y);
                        int regionX = Globals.getRegionValue(pos.X);
                        int regionY = Globals.getRegionValue(pos.Y);
                        //entity is inside unloading regions cause all regions are unloading
                        int chunkNr = Game1.player.getCurrentChunkNrInArray(pos, Globals.playerPos);
                        if (chunkNr != -1)
                        {
                            saveRoomToChunk(ref e, chunkNr);
                            roomList.Remove(e);
                        }
                    }
                    break;
                case Lists.TileEntities:
                    break;
                default:
                    break;
            }
            
        }

        public static void saveAndUnloadRegions()
        {
            for (int i = 0; i < 9; i++)
            {
                TagTranslator.overwriteRegionStream(regionArray[i], i);
                regionArray[i] = null;
            }
        }

        public void saveListToRegion(Lists list, int xOffset, int yOffset, int regionIndex1, int regionIndex2, int regionIndex3, int regionIndex4, int regionIndex5)
        {
            switch (list)
            {
                case Lists.Entities:
                    for (int i = entityList.Count - 1; i >= 0; i--)
                    {

                        Entity e = entityList[i];
                        Vector2 pos = e.position;
                        int regionX = Globals.getRegionValue(pos.X);
                        int regionY = Globals.getRegionValue(pos.Y);
                        if (regionArray[regionIndex1] != null && regionArray[regionIndex2] != null && regionArray[regionIndex3] != null && regionArray[regionIndex4] != null && regionArray[regionIndex5] != null)
                        {
                            if ((regionX == regionArray[regionIndex1].xPosRegion && regionY == regionArray[regionIndex1].yPosRegion) || (regionX == regionArray[regionIndex2].xPosRegion && regionY == regionArray[regionIndex2].yPosRegion) || (regionX == regionArray[regionIndex3].xPosRegion && regionY == regionArray[regionIndex3].yPosRegion) || (regionX == regionArray[regionIndex4].xPosRegion && regionY == regionArray[regionIndex4].yPosRegion) || (regionX == regionArray[regionIndex5].xPosRegion && regionY == regionArray[regionIndex5].yPosRegion))
                            {
                                //entity is inside unloading regions
                                int chunkNr = e.getCurrentChunkNrInArray(e.position, new Vector2(Globals.playerPos.X + xOffset, Globals.playerPos.Y + yOffset));
                                if (chunkNr != -1)
                                {
                                    saveEntityToChunk(ref e, chunkNr);
                                    entityList.Remove(e);
                                }
                            }
                        }
                    }
                    break;
                case Lists.Rooms:
                    for (int i = roomList.Count - 1; i >= 0; i--)
                    {

                        Room e = roomList[i];
                        Vector2 pos = new Vector2(e.area.X, e.area.Y);
                        int regionX = Globals.getRegionValue(pos.X);
                        int regionY = Globals.getRegionValue(pos.Y);
                        if (regionArray[regionIndex1] != null && regionArray[regionIndex2] != null && regionArray[regionIndex3] != null && regionArray[regionIndex4] != null && regionArray[regionIndex5] != null)
                        {
                            if ((regionX == regionArray[regionIndex1].xPosRegion && regionY == regionArray[regionIndex1].yPosRegion) || (regionX == regionArray[regionIndex2].xPosRegion && regionY == regionArray[regionIndex2].yPosRegion) || (regionX == regionArray[regionIndex3].xPosRegion && regionY == regionArray[regionIndex3].yPosRegion) || (regionX == regionArray[regionIndex4].xPosRegion && regionY == regionArray[regionIndex4].yPosRegion) || (regionX == regionArray[regionIndex5].xPosRegion && regionY == regionArray[regionIndex5].yPosRegion))
                            {
                                //entity is inside unloading regions
                                int chunkNr = Game1.player.getCurrentChunkNrInArray(pos, new Vector2(Globals.playerPos.X + xOffset, Globals.playerPos.Y + yOffset));
                                if (chunkNr != -1)
                                {
                                    saveRoomToChunk(ref e, chunkNr);
                                    roomList.Remove(e);
                                }
                            }
                        }
                    }
                    break;
                case Lists.TileEntities:
                    break;
                default:
                    break;
            }
        }

        public void saveListToRegion(Lists list, int xOffset, int yOffset, int regionIndex1, int regionIndex2, int regionIndex3)
        {
            switch (list)
            {
                case Lists.Entities:
                    for (int i = entityList.Count - 1; i >= 0; i--)
                    {

                        Entity e = entityList[i];
                        Vector2 pos = e.position;
                        int regionX = Globals.getRegionValue(pos.X);
                        int regionY = Globals.getRegionValue(pos.Y);
                        if (regionArray[regionIndex1] != null && regionArray[regionIndex2] != null && regionArray[regionIndex3] != null)
                        {
                            if ((regionX == regionArray[regionIndex1].xPosRegion && regionY == regionArray[regionIndex1].yPosRegion) || (regionX == regionArray[regionIndex2].xPosRegion && regionY == regionArray[regionIndex2].yPosRegion) || (regionX == regionArray[regionIndex3].xPosRegion && regionY == regionArray[regionIndex3].yPosRegion))
                            {
                                //entity is inside unloading regions
                                int chunkNr = e.getCurrentChunkNrInArray(e.position, new Vector2(Globals.playerPos.X + xOffset, Globals.playerPos.Y + yOffset));
                                if (chunkNr != -1)
                                {
                                    saveEntityToChunk(ref e, chunkNr);
                                    entityList.Remove(e);
                                }
                            }
                        }
                    }
                    break;
                case Lists.Rooms:
                    for (int i = roomList.Count - 1; i >= 0; i--)
                    {

                        Room e = roomList[i];
                        Vector2 pos = new Vector2(e.area.X, e.area.Y);
                        int regionX = Globals.getRegionValue(pos.X);
                        int regionY = Globals.getRegionValue(pos.Y);
                        if (regionArray[regionIndex1] != null && regionArray[regionIndex2] != null && regionArray[regionIndex3] != null)
                        {
                            if ((regionX == regionArray[regionIndex1].xPosRegion && regionY == regionArray[regionIndex1].yPosRegion) || (regionX == regionArray[regionIndex2].xPosRegion && regionY == regionArray[regionIndex2].yPosRegion) || (regionX == regionArray[regionIndex3].xPosRegion && regionY == regionArray[regionIndex3].yPosRegion))
                            {
                                //entity is inside unloading regions
                                int chunkNr = Game1.player.getCurrentChunkNrInArray(pos, new Vector2(Globals.playerPos.X + xOffset, Globals.playerPos.Y + yOffset));
                                if (chunkNr != -1)
                                {
                                    saveRoomToChunk(ref e, chunkNr);
                                    roomList.Remove(e);
                                }
                            }
                        }
                    }
                    break;
                case Lists.TileEntities:
                    break;
                default:
                    break;

            }
        }
    }
}

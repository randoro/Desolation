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
                                newChunk = TempChunkCreator.createChunk(regionArray[i]);
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
                                            if (newEntity.position.X == 0 && newEntity.position.Y == 0)
                                            {
                                                int test = 1;
                                            }
                                            entityList.Add(newEntity);
                                        }
                                    }
                                    newChunk.entities.Clear();
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

                            for (int i = entityList.Count - 1; i >= 0; i--)
                            {

                                Entity e = entityList[i];
                                if (e.position.X == 0 && e.position.Y == 0)
                                {
                                    int test = 1;
                                }
                                Vector2 pos = e.position;
                                int regionX = Globals.getRegionValue(pos.X);
                                int regionY = Globals.getRegionValue(pos.Y);
                                if (regionArray[2] != null && regionArray[5] != null && regionArray[8] != null && regionArray[7] != null && regionArray[6] != null)
                                {
                                    if ((regionX == regionArray[2].xPosRegion && regionY == regionArray[2].yPosRegion) || (regionX == regionArray[5].xPosRegion && regionY == regionArray[5].yPosRegion) || (regionX == regionArray[8].xPosRegion && regionY == regionArray[8].yPosRegion) || (regionX == regionArray[7].xPosRegion && regionY == regionArray[7].yPosRegion) || (regionX == regionArray[6].xPosRegion && regionY == regionArray[6].yPosRegion))
                                    {
                                        //entity is inside unloading regions
                                        int chunkNr = e.getCurrentChunkNrInArray(e.position, new Vector2(Globals.playerPos.X + 1024, Globals.playerPos.Y + 1024));
                                        if (chunkNr != -1)
                                        {
                                            saveEntityToChunk(ref e, chunkNr);
                                            entityList.Remove(e);
                                        }
                                    }
                                }
                            }
                            
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

                            for (int i = entityList.Count - 1; i >= 0; i--)
                            {

                                Entity e = entityList[i];
                                if (e.position.X == 0 && e.position.Y == 0)
                                {
                                    int test = 1;
                                }
                                Vector2 pos = e.position;
                                int regionX = Globals.getRegionValue(pos.X);
                                int regionY = Globals.getRegionValue(pos.Y);
                                if (regionArray[0] != null && regionArray[1] != null && regionArray[2] != null && regionArray[5] != null && regionArray[8] != null)
                                {
                                    if ((regionX == regionArray[0].xPosRegion && regionY == regionArray[0].yPosRegion) || (regionX == regionArray[1].xPosRegion && regionY == regionArray[1].yPosRegion) || (regionX == regionArray[2].xPosRegion && regionY == regionArray[2].yPosRegion) || (regionX == regionArray[5].xPosRegion && regionY == regionArray[5].yPosRegion) || (regionX == regionArray[8].xPosRegion && regionY == regionArray[8].yPosRegion))
                                    {
                                        //entity is inside unloading regions
                                        int chunkNr = e.getCurrentChunkNrInArray(e.position, new Vector2(Globals.playerPos.X + 1024, Globals.playerPos.Y - 1024));
                                        if (chunkNr != -1)
                                        {
                                            saveEntityToChunk(ref e, chunkNr);
                                            entityList.Remove(e);
                                        }
                                    }
                                }
                            }

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

                            for (int i = entityList.Count - 1; i >= 0; i--)
                            {

                                Entity e = entityList[i];
                                if (e.position.X == 0 && e.position.Y == 0)
                                {
                                    int test = 1;
                                }
                                Vector2 pos = e.position;
                                int regionX = Globals.getRegionValue(pos.X);
                                int regionY = Globals.getRegionValue(pos.Y);
                                if (regionArray[2] != null && regionArray[5] != null && regionArray[8] != null)
                                {
                                    if ((regionX == regionArray[2].xPosRegion && regionY == regionArray[2].yPosRegion) || (regionX == regionArray[5].xPosRegion && regionY == regionArray[5].yPosRegion) || (regionX == regionArray[8].xPosRegion && regionY == regionArray[8].yPosRegion))
                                    {
                                        //entity is inside unloading regions
                                        int chunkNr = e.getCurrentChunkNrInArray(e.position, new Vector2(Globals.playerPos.X + 1024, Globals.playerPos.Y));
                                        if (chunkNr != -1)
                                        {
                                            saveEntityToChunk(ref e, chunkNr);
                                            entityList.Remove(e);
                                        }

                                        
                                    }
                                }
                            }

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

                            for (int i = entityList.Count - 1; i >= 0; i--)
                            {

                                Entity e = entityList[i];
                                if (e.position.X == 0 && e.position.Y == 0)
                                {
                                    int test = 1;
                                }
                                Vector2 pos = e.position;
                                int regionX = Globals.getRegionValue(pos.X);
                                int regionY = Globals.getRegionValue(pos.Y);
                                if (regionArray[0] != null && regionArray[3] != null && regionArray[6] != null && regionArray[7] != null && regionArray[8] != null)
                                {
                                    if ((regionX == regionArray[0].xPosRegion && regionY == regionArray[0].yPosRegion) || (regionX == regionArray[3].xPosRegion && regionY == regionArray[3].yPosRegion) || (regionX == regionArray[6].xPosRegion && regionY == regionArray[6].yPosRegion) || (regionX == regionArray[7].xPosRegion && regionY == regionArray[7].yPosRegion) || (regionX == regionArray[8].xPosRegion && regionY == regionArray[8].yPosRegion))
                                    {
                                        //entity is inside unloading regions
                                        int chunkNr = e.getCurrentChunkNrInArray(e.position, new Vector2(Globals.playerPos.X - 1024, Globals.playerPos.Y + 1024));
                                        if (chunkNr != -1)
                                        {
                                            saveEntityToChunk(ref e, chunkNr);
                                            entityList.Remove(e);
                                        }
                                    }
                                }
                            }

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

                            for (int i = entityList.Count - 1; i >= 0; i--)
                            {

                                Entity e = entityList[i];
                                if (e.position.X == 0 && e.position.Y == 0)
                                {
                                    int test = 1;
                                }
                                Vector2 pos = e.position;
                                int regionX = Globals.getRegionValue(pos.X);
                                int regionY = Globals.getRegionValue(pos.Y);
                                if (regionArray[0] != null && regionArray[1] != null && regionArray[2] != null && regionArray[3] != null &&regionArray[6] != null)
                                {
                                    if ((regionX == regionArray[0].xPosRegion && regionY == regionArray[0].yPosRegion) || (regionX == regionArray[1].xPosRegion && regionY == regionArray[1].yPosRegion) || (regionX == regionArray[2].xPosRegion && regionY == regionArray[2].yPosRegion) || (regionX == regionArray[3].xPosRegion && regionY == regionArray[3].yPosRegion) || (regionX == regionArray[6].xPosRegion && regionY == regionArray[6].yPosRegion))
                                    {
                                        //entity is inside unloading regions
                                        int chunkNr = e.getCurrentChunkNrInArray(e.position, new Vector2(Globals.playerPos.X - 1024, Globals.playerPos.Y - 1024));
                                        if (chunkNr != -1)
                                        {
                                            saveEntityToChunk(ref e, chunkNr);
                                            entityList.Remove(e);
                                        }
                                    }
                                }
                            }

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
                            

                            for (int i = entityList.Count - 1; i >= 0; i--)
                            {

                                Entity e = entityList[i];
                                if (e.position.X == 0 && e.position.Y == 0)
                                {
                                    int test = 1;
                                }
                                Vector2 pos = e.position;
                                int regionX = Globals.getRegionValue(pos.X);
                                int regionY = Globals.getRegionValue(pos.Y);
                                if (regionArray[0] != null && regionArray[3] != null && regionArray[6] != null)
                                {
                                    if ((regionX == regionArray[0].xPosRegion && regionY == regionArray[0].yPosRegion) || (regionX == regionArray[3].xPosRegion && regionY == regionArray[3].yPosRegion) || (regionX == regionArray[6].xPosRegion && regionY == regionArray[6].yPosRegion))
                                    {
                                        //entity is inside unloading regions
                                        int chunkNr = e.getCurrentChunkNrInArray(e.position, new Vector2(Globals.playerPos.X - 1024, Globals.playerPos.Y));
                                        if (chunkNr != -1)
                                        {
                                            saveEntityToChunk(ref e, chunkNr);
                                            entityList.Remove(e);
                                        }
                                    }
                                }
                            }

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

                            for (int i = entityList.Count - 1; i >= 0; i--)
                            {

                                Entity e = entityList[i];
                                if (e.position.X == 0 && e.position.Y == 0)
                                {
                                    int test = 1;
                                }
                                Vector2 pos = e.position;
                                int regionX = Globals.getRegionValue(pos.X);
                                int regionY = Globals.getRegionValue(pos.Y);
                                if (regionArray[6] != null && regionArray[7] != null && regionArray[8] != null)
                                {
                                    if ((regionX == regionArray[6].xPosRegion && regionY == regionArray[6].yPosRegion) || (regionX == regionArray[7].xPosRegion && regionY == regionArray[7].yPosRegion) || (regionX == regionArray[8].xPosRegion && regionY == regionArray[8].yPosRegion))
                                    {
                                        //entity is inside unloading regions
                                        int chunkNr = e.getCurrentChunkNrInArray(e.position, new Vector2(Globals.playerPos.X, Globals.playerPos.Y + 1024));
                                        if (chunkNr != -1)
                                        {
                                            saveEntityToChunk(ref e, chunkNr);
                                            entityList.Remove(e);
                                        }
                                    }
                                }
                            }

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

                            for (int i = entityList.Count - 1; i >= 0; i--)
                            {

                                Entity e = entityList[i];
                                if (e.position.X == 0 && e.position.Y == 0)
                                {
                                    int test = 1;
                                }
                                Vector2 pos = e.position;
                                int regionX = Globals.getRegionValue(pos.X);
                                int regionY = Globals.getRegionValue(pos.Y);
                                if (regionArray[0] != null && regionArray[1] != null && regionArray[2] != null)
                                {
                                    if ((regionX == regionArray[0].xPosRegion && regionY == regionArray[0].yPosRegion) || (regionX == regionArray[1].xPosRegion && regionY == regionArray[1].yPosRegion) || (regionX == regionArray[2].xPosRegion && regionY == regionArray[2].yPosRegion))
                                    {
                                        //entity is inside unloading regions
                                        int chunkNr = e.getCurrentChunkNrInArray(e.position, new Vector2(Globals.playerPos.X, Globals.playerPos.Y - 1024));
                                        if (chunkNr != -1)
                                        {
                                            saveEntityToChunk(ref e, chunkNr);
                                            entityList.Remove(e);
                                        }
                                    }
                                }
                            }

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
        }

        public Chunk[] tempGetChunkArray()
        {
            return chunkArray;
        }


        private void saveEntityToChunk(ref Entity e, int chunkNr)
        {
            Chunk curChunk = chunkArray[chunkNr];
            if (curChunk != null)
            {
                List<Tag> newList = new List<Tag>();
                e.getTagList(ref newList);
                curChunk.entities.Add(newList);
            }
        }



        public static void changeWorld(String worldName)
        {
            //unloading
            saveEntities();

            saveAndUnloadRegions();
            

            //changing world
            String newWorldName = worldName;
            String newWorldFolder = newWorldName+@"\";
            String regionFolder = @"region\";
            fileLoader.checkAndCreateFolder(newWorldFolder);
            fileLoader.checkAndCreateFolder(newWorldFolder + regionFolder);
            fileLoader.currentWorldFolder = newWorldFolder;

        }

        public static void saveEntities()
        {
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
                    Chunk curChunk = chunkArray[chunkNr];
                    if (curChunk != null)
                    {
                        List<Tag> newList = new List<Tag>();
                        e.getTagList(ref newList);
                        curChunk.entities.Add(newList);
                        entityList.Remove(e);
                    }
                }
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


    }
}

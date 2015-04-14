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

        FileLoader fileLoader;
        public static Chunk[] chunkArray;
        public static Region[] regionArray;
        public static List<Entity> entityList;
        long ticksLastChunkLoad;
        int currentChunkForEntityLoad;
        int currentEntityForEntityLoad;
        int currentRegionPerTick;
        int regionSave;

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
            ticksLastChunkLoad = DateTime.Now.Ticks;
            regionArray = new Region[9];
            entityList = new List<Entity>();


                //Region tempRegion0 = fileLoader.loadRegionFile(-1, -1);
                //Region tempRegion1 = fileLoader.loadRegionFile(0, -1);
                //Region tempRegion2 = fileLoader.loadRegionFile(1, -1);
                //Region tempRegion3 = fileLoader.loadRegionFile(-1, 0);
                //Region tempRegion4 = fileLoader.loadRegionFile(0, 0);
                //Region tempRegion5 = fileLoader.loadRegionFile(1, 0);
                //Region tempRegion6 = fileLoader.loadRegionFile(-1, 1);
                //Region tempRegion7 = fileLoader.loadRegionFile(0, 1);
                //Region tempRegion8 = fileLoader.loadRegionFile(1, 1);

                //bool newGame = false;

                //if (newGame)
                //{
                //    TempChunkCreator.makeEmptyChunk(tempRegion0);
                //    TempChunkCreator.makeEmptyChunk(tempRegion1);
                //    TempChunkCreator.makeEmptyChunk(tempRegion2);
                //    TempChunkCreator.makeEmptyChunk(tempRegion3);
                //    TempChunkCreator.makeEmptyChunk(tempRegion4);
                //    TempChunkCreator.makeEmptyChunk(tempRegion5);
                //    TempChunkCreator.makeEmptyChunk(tempRegion6);
                //    TempChunkCreator.makeEmptyChunk(tempRegion7);
                //    TempChunkCreator.makeEmptyChunk(tempRegion8);
                //}

            
                //regionArray[0] = tempRegion0;
                //regionArray[1] = tempRegion1;
                //regionArray[2] = tempRegion2;
                //regionArray[3] = tempRegion3;
                //regionArray[4] = tempRegion4;
                //regionArray[5] = tempRegion5;
                //regionArray[6] = tempRegion6;
                //regionArray[7] = tempRegion7;
                //regionArray[8] = tempRegion8;


            chunkArray = new Chunk[144];
            currentChunkForEntityLoad = 0;
            currentEntityForEntityLoad = 0;
            currentRegionPerTick = 0;
            regionSave = 0;

            //for (int i = 0; i < 144; i++)
            //{
            //    chunkArray[i] = i;
            //}
            //Globals.shiftChunksDown(ref chunkArray);
            //int temp = 0;
        }


        public void update(GameTime gameTime, GameWindow window)
        {
               
                foreach (Entity e in entityList)
                {
                    e.Update(gameTime);
                }

                window.Title = "Nr or entities: "+entityList.Count;
                //if (regionArray[currentRegionPerTick] != null)
                //{
                //    bool[] chunksLoaded = regionArray[currentRegionPerTick].chunksLoaded;
                //    bool allChunksLoaded = !Array.Exists(chunksLoaded, delegate(bool x) { return !x; }); //checks if all chunks are loaded

                //    if (allChunksLoaded)
                //    {
                //        TagTranslator.overwriteRegionStream(ChunkManager.regionArray[currentRegionPerTick], currentRegionPerTick);
                //        ChunkManager.regionArray[currentRegionPerTick] = null;
                //        currentRegionPerTick++;
                //        currentRegionPerTick %= 9;
                //    }
                //}

            
        }

        public void syncUpdate(GameTime gameTime)
        {
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
                                    foreach (List<Tag> e in newChunk.entities)
                                    {
                                        Entity newEntity = TagTranslator.getUnloadedEntity(e);
                                        if (newEntity != null)
                                        {
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
                                Vector2 pos = e.position;
                                int regionX = Globals.getRegionValue(pos.X);
                                int regionY = Globals.getRegionValue(pos.Y);
                                if (regionArray[2] != null && regionArray[5] != null && regionArray[8] != null && regionArray[7] != null && regionArray[6] != null)
                                {
                                    if ((regionX == regionArray[2].xPosRegion && regionY == regionArray[2].yPosRegion) || (regionX == regionArray[5].xPosRegion && regionY == regionArray[5].yPosRegion) || (regionX == regionArray[8].xPosRegion && regionY == regionArray[8].yPosRegion) || (regionX == regionArray[7].xPosRegion && regionY == regionArray[7].yPosRegion) || (regionX == regionArray[6].xPosRegion && regionY == regionArray[6].yPosRegion))
                                    {
                                        //entity is inside unloading regions
                                        int chunkNr = e.getCurrentChunkNrInArray(new Vector2(Globals.playerPos.X + 1024, Globals.playerPos.Y + 1024));
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
                        #region Southwest

                            for (int i = entityList.Count - 1; i >= 0; i--)
                            {

                                Entity e = entityList[i];
                                Vector2 pos = e.position;
                                int regionX = Globals.getRegionValue(pos.X);
                                int regionY = Globals.getRegionValue(pos.Y);
                                if (regionArray[0] != null && regionArray[1] != null && regionArray[2] != null && regionArray[5] != null && regionArray[8] != null)
                                {
                                    if ((regionX == regionArray[0].xPosRegion && regionY == regionArray[0].yPosRegion) || (regionX == regionArray[1].xPosRegion && regionY == regionArray[1].yPosRegion) || (regionX == regionArray[2].xPosRegion && regionY == regionArray[2].yPosRegion) || (regionX == regionArray[5].xPosRegion && regionY == regionArray[5].yPosRegion) || (regionX == regionArray[8].xPosRegion && regionY == regionArray[8].yPosRegion))
                                    {
                                        //entity is inside unloading regions
                                        int chunkNr = e.getCurrentChunkNrInArray(new Vector2(Globals.playerPos.X + 1024, Globals.playerPos.Y - 1024));
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
                                Vector2 pos = e.position;
                                int regionX = Globals.getRegionValue(pos.X);
                                int regionY = Globals.getRegionValue(pos.Y);
                                if (regionArray[2] != null && regionArray[5] != null && regionArray[8] != null)
                                {
                                    if ((regionX == regionArray[2].xPosRegion && regionY == regionArray[2].yPosRegion) || (regionX == regionArray[5].xPosRegion && regionY == regionArray[5].yPosRegion) || (regionX == regionArray[8].xPosRegion && regionY == regionArray[8].yPosRegion))
                                    {
                                        //entity is inside unloading regions
                                        int chunkNr = e.getCurrentChunkNrInArray(new Vector2 (Globals.playerPos.X + 1024, Globals.playerPos.Y));
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
                                Vector2 pos = e.position;
                                int regionX = Globals.getRegionValue(pos.X);
                                int regionY = Globals.getRegionValue(pos.Y);
                                if (regionArray[0] != null && regionArray[3] != null && regionArray[6] != null && regionArray[7] != null && regionArray[8] != null)
                                {
                                    if ((regionX == regionArray[0].xPosRegion && regionY == regionArray[0].yPosRegion) || (regionX == regionArray[3].xPosRegion && regionY == regionArray[3].yPosRegion) || (regionX == regionArray[6].xPosRegion && regionY == regionArray[6].yPosRegion) || (regionX == regionArray[7].xPosRegion && regionY == regionArray[7].yPosRegion) || (regionX == regionArray[8].xPosRegion && regionY == regionArray[8].yPosRegion))
                                    {
                                        //entity is inside unloading regions
                                        int chunkNr = e.getCurrentChunkNrInArray(new Vector2(Globals.playerPos.X - 1024, Globals.playerPos.Y + 1024));
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

                        }
                        else if (newRegionY > lastRegionY)
                        {
                            //southeast

                            for (int i = entityList.Count - 1; i >= 0; i--)
                            {

                                Entity e = entityList[i];
                                Vector2 pos = e.position;
                                int regionX = Globals.getRegionValue(pos.X);
                                int regionY = Globals.getRegionValue(pos.Y);
                                if (regionArray[0] != null && regionArray[1] != null && regionArray[2] != null && regionArray[3] != null &&regionArray[6] != null)
                                {
                                    if ((regionX == regionArray[0].xPosRegion && regionY == regionArray[0].yPosRegion) || (regionX == regionArray[1].xPosRegion && regionY == regionArray[1].yPosRegion) || (regionX == regionArray[2].xPosRegion && regionY == regionArray[2].yPosRegion) || (regionX == regionArray[3].xPosRegion && regionY == regionArray[3].yPosRegion) || (regionX == regionArray[6].xPosRegion && regionY == regionArray[6].yPosRegion))
                                    {
                                        //entity is inside unloading regions
                                        int chunkNr = e.getCurrentChunkNrInArray(new Vector2(Globals.playerPos.X - 1024, Globals.playerPos.Y - 1024));
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

                        }
                        else
                        {
                            //east
                            

                            for (int i = entityList.Count - 1; i >= 0; i--)
                            {

                                Entity e = entityList[i];
                                Vector2 pos = e.position;
                                int regionX = Globals.getRegionValue(pos.X);
                                int regionY = Globals.getRegionValue(pos.Y);
                                if (regionArray[0] != null && regionArray[3] != null && regionArray[6] != null)
                                {
                                    if ((regionX == regionArray[0].xPosRegion && regionY == regionArray[0].yPosRegion) || (regionX == regionArray[3].xPosRegion && regionY == regionArray[3].yPosRegion) || (regionX == regionArray[6].xPosRegion && regionY == regionArray[6].yPosRegion))
                                    {
                                        //entity is inside unloading regions
                                        int chunkNr = e.getCurrentChunkNrInArray(new Vector2(Globals.playerPos.X - 1024, Globals.playerPos.Y));
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
                            }

                            lastRegionX = newRegionX;

                            TagTranslator.overwriteRegionStream(regionArray[0], 0);
                            TagTranslator.overwriteRegionStream(regionArray[3], 3);
                            TagTranslator.overwriteRegionStream(regionArray[6], 6);

                            Globals.shiftRegionsLeft(ref regionArray);
                            Globals.shiftChunksLeft(ref chunkArray);

                        }

                    }
                    else
                    {
                        if (newRegionY < lastRegionY)
                        {
                            //north

                            for (int i = entityList.Count - 1; i >= 0; i--)
                            {

                                Entity e = entityList[i];
                                Vector2 pos = e.position;
                                int regionX = Globals.getRegionValue(pos.X);
                                int regionY = Globals.getRegionValue(pos.Y);
                                if (regionArray[6] != null && regionArray[7] != null && regionArray[8] != null)
                                {
                                    if ((regionX == regionArray[6].xPosRegion && regionY == regionArray[6].yPosRegion) || (regionX == regionArray[7].xPosRegion && regionY == regionArray[7].yPosRegion) || (regionX == regionArray[8].xPosRegion && regionY == regionArray[8].yPosRegion))
                                    {
                                        //entity is inside unloading regions
                                        int chunkNr = e.getCurrentChunkNrInArray(new Vector2(Globals.playerPos.X, Globals.playerPos.Y + 1024));
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
                            }

                            lastRegionY = newRegionY;

                            TagTranslator.overwriteRegionStream(regionArray[6], 6);
                            TagTranslator.overwriteRegionStream(regionArray[7], 7);
                            TagTranslator.overwriteRegionStream(regionArray[8], 8);

                            Globals.shiftRegionsDown(ref regionArray);
                            Globals.shiftChunksDown(ref chunkArray);

                        }
                        else if (newRegionY > lastRegionY)
                        {
                            //south

                            for (int i = entityList.Count - 1; i >= 0; i--)
                            {

                                Entity e = entityList[i];
                                Vector2 pos = e.position;
                                int regionX = Globals.getRegionValue(pos.X);
                                int regionY = Globals.getRegionValue(pos.Y);
                                if (regionArray[0] != null && regionArray[1] != null && regionArray[2] != null)
                                {
                                    if ((regionX == regionArray[0].xPosRegion && regionY == regionArray[0].yPosRegion) || (regionX == regionArray[1].xPosRegion && regionY == regionArray[1].yPosRegion) || (regionX == regionArray[2].xPosRegion && regionY == regionArray[2].yPosRegion))
                                    {
                                        //entity is inside unloading regions
                                        int chunkNr = e.getCurrentChunkNrInArray(new Vector2(Globals.playerPos.X, Globals.playerPos.Y - 1024));
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
                            }

                            lastRegionY = newRegionY;

                            TagTranslator.overwriteRegionStream(regionArray[0], 0);
                            TagTranslator.overwriteRegionStream(regionArray[1], 1);
                            TagTranslator.overwriteRegionStream(regionArray[2], 2);

                            Globals.shiftRegionsUp(ref regionArray);
                            Globals.shiftChunksUp(ref chunkArray);

                        }
                    }

                }
                #endregion


                
                        
              


        }

        public void draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 144; i++)
			{
                if(chunkArray[i] != null) 
                { 
                    chunkArray[i].draw(spriteBatch);
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


    }
}

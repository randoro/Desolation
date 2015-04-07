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

            
        }

        public void syncUpdate(GameTime gameTime)
        {
                newRegionX = Globals.getRegionValue(Globals.playerPos.X);
                newRegionY = Globals.getRegionValue(Globals.playerPos.Y);

                newChunkX = Globals.getChunkValue(Globals.playerPos.X);
                newChunkY = Globals.getChunkValue(Globals.playerPos.Y);


                Chunk curChunk = chunkArray[currentChunkForEntityLoad];
                if (curChunk != null)
                {
                    List<List<Tag>> listListTag = new List<List<Tag>>();
                    foreach (Entity e in entityList)
                    {
                        Vector2 pos = e.position;
                        int ChunkX = Globals.getChunkValue(pos.X);
                        int ChunkY = Globals.getChunkValue(pos.Y);
                        if (curChunk.XPos == ChunkX && curChunk.YPos == ChunkY)
                        {
                            List<Tag> newList = new List<Tag>();
                            e.getTagList(ref newList);
                            listListTag.Add(newList);

                        }
                    }
                    curChunk.entities = listListTag;
                }
                currentChunkForEntityLoad++;
                currentChunkForEntityLoad %= 144;


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
                                        entityList.Add(TagTranslator.getUnloadedEntity(e));
                                    }
                                }
                            }
                        }


                    }
                    else
                    {
                        if (newRegionX == lastRegionX && newRegionY == lastRegionY)
                        {
                            regionArray[i] = fileLoader.loadRegionFile(newRegionX - 1 + i % 3, newRegionY - 1 + i / 3);
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
                            //northwest
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

                            //Region tempRegion0 = fileLoader.loadRegionFile(newRegionX - 1, newRegionY - 1);
                            //if (tempRegion0 != null)
                            //{
                            //    //regionArray[0] = tempRegion0;
                            //}
                            //Region tempRegion1 = fileLoader.loadRegionFile(newRegionX, newRegionY - 1);
                            //if (tempRegion1 != null)
                            //{
                            //    //regionArray[1] = tempRegion1;
                            //}
                            //Region tempRegion2 = fileLoader.loadRegionFile(newRegionX + 1, newRegionY - 1);
                            //if (tempRegion2 != null)
                            //{
                            //    //regionArray[2] = tempRegion2;
                            //}
                            //Region tempRegion3 = fileLoader.loadRegionFile(newRegionX - 1, newRegionY);
                            //if (tempRegion3 != null)
                            //{
                            //    //regionArray[3] = tempRegion3;
                            //}
                            //Region tempRegion6 = fileLoader.loadRegionFile(newRegionX - 1, newRegionY + 1);
                            //if (tempRegion6 != null)
                            //{
                            //    //regionArray[6] = tempRegion6;
                            //}
                        }
                        else if (newRegionY > lastRegionY)
                        {
                            //southwest
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

                            //Region tempRegion6 = fileLoader.loadRegionFile(newRegionX - 1, newRegionY + 1);
                            //if (tempRegion6 != null)
                            //{
                            //    //regionArray[6] = tempRegion6;
                            //}
                            //Region tempRegion7 = fileLoader.loadRegionFile(newRegionX, newRegionY + 1);
                            //if (tempRegion7 != null)
                            //{
                            //    //regionArray[7] = tempRegion7;
                            //}
                            //Region tempRegion8 = fileLoader.loadRegionFile(newRegionX + 1, newRegionY + 1);
                            //if (tempRegion8 != null)
                            //{
                            //    //regionArray[8] = tempRegion8;
                            //}
                            //Region tempRegion0 = fileLoader.loadRegionFile(newRegionX - 1, newRegionY - 1);
                            //if (tempRegion0 != null)
                            //{
                            //    //regionArray[0] = tempRegion0;
                            //}

                            //Region tempRegion3 = fileLoader.loadRegionFile(newRegionX - 1, newRegionY);
                            //if (tempRegion3 != null)
                            //{
                            //    //regionArray[3] = tempRegion3;
                            //}
                        }
                        else
                        {
                            //west
                            lastRegionX = newRegionX;

                            TagTranslator.overwriteRegionStream(regionArray[2], 2);
                            TagTranslator.overwriteRegionStream(regionArray[5], 5);
                            TagTranslator.overwriteRegionStream(regionArray[8], 8);

                            Globals.shiftRegionsRight(ref regionArray);
                            Globals.shiftChunksRight(ref chunkArray);

                            //Region tempRegion0 = fileLoader.loadRegionFile(newRegionX - 1, newRegionY - 1);
                            //if (tempRegion0 != null)
                            //{
                            //    //regionArray[0] = tempRegion0;
                            //}

                            //Region tempRegion3 = fileLoader.loadRegionFile(newRegionX - 1, newRegionY);
                            //if (tempRegion3 != null)
                            //{
                            //    //regionArray[3] = tempRegion3;
                            //}
                            //Region tempRegion6 = fileLoader.loadRegionFile(newRegionX - 1, newRegionY + 1);
                            //if (tempRegion6 != null)
                            //{
                            //    //regionArray[6] = tempRegion6;
                            //}
                        }

                    }
                    else if (newRegionX > lastRegionX)
                    {
                        if (newRegionY < lastRegionY)
                        {
                            //northeast
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


                            //Region tempRegion0 = fileLoader.loadRegionFile(newRegionX - 1, newRegionY - 1);
                            //if (tempRegion0 != null)
                            //{
                            //    //regionArray[0] = tempRegion0;
                            //}
                            //Region tempRegion1 = fileLoader.loadRegionFile(newRegionX, newRegionY - 1);
                            //if (tempRegion1 != null)
                            //{
                            //    //regionArray[1] = tempRegion1;
                            //}

                            //Region tempRegion2 = fileLoader.loadRegionFile(newRegionX + 1, newRegionY - 1);
                            //if (tempRegion2 != null)
                            //{
                            //    //regionArray[2] = tempRegion2;
                            //}
                            //Region tempRegion5 = fileLoader.loadRegionFile(newRegionX + 1, newRegionY);
                            //if (tempRegion5 != null)
                            //{
                            //    //regionArray[5] = tempRegion5;
                            //}
                            //Region tempRegion8 = fileLoader.loadRegionFile(newRegionX + 1, newRegionY + 1);
                            //if (tempRegion8 != null)
                            //{
                            //    //regionArray[8] = tempRegion8;
                            //}

                        }
                        else if (newRegionY > lastRegionY)
                        {
                            //southeast
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

                            //Region tempRegion6 = fileLoader.loadRegionFile(newRegionX - 1, newRegionY + 1);
                            //if (tempRegion6 != null)
                            //{
                            //    //regionArray[6] = tempRegion6;
                            //}
                            //Region tempRegion7 = fileLoader.loadRegionFile(newRegionX, newRegionY + 1);
                            //if (tempRegion7 != null)
                            //{
                            //    //regionArray[7] = tempRegion7;
                            //}
                            //Region tempRegion8 = fileLoader.loadRegionFile(newRegionX + 1, newRegionY + 1);
                            //if (tempRegion8 != null)
                            //{
                            //    //regionArray[8] = tempRegion8;
                            //}
                            //Region tempRegion2 = fileLoader.loadRegionFile(newRegionX + 1, newRegionY - 1);
                            //if (tempRegion2 != null)
                            //{
                            //    //regionArray[2] = tempRegion2;
                            //}
                            //Region tempRegion5 = fileLoader.loadRegionFile(newRegionX + 1, newRegionY);
                            //if (tempRegion5 != null)
                            //{
                            //    //regionArray[5] = tempRegion5;
                            //}
                        }
                        else
                        {
                            //east
                            lastRegionX = newRegionX;

                            TagTranslator.overwriteRegionStream(regionArray[0], 0);
                            TagTranslator.overwriteRegionStream(regionArray[3], 3);
                            TagTranslator.overwriteRegionStream(regionArray[6], 6);

                            Globals.shiftRegionsLeft(ref regionArray);
                            Globals.shiftChunksLeft(ref chunkArray);

                            //Region tempRegion2 = fileLoader.loadRegionFile(newRegionX + 1, newRegionY - 1);
                            //if (tempRegion2 != null)
                            //{
                            //    //regionArray[2] = tempRegion2;
                            //}
                            //Region tempRegion5 = fileLoader.loadRegionFile(newRegionX + 1, newRegionY);
                            //if (tempRegion5 != null)
                            //{
                            //    //regionArray[5] = tempRegion5;
                            //}
                            //Region tempRegion8 = fileLoader.loadRegionFile(newRegionX + 1, newRegionY + 1);
                            //if (tempRegion8 != null)
                            //{
                            //    //regionArray[8] = tempRegion8;
                            //}
                        }

                    }
                    else
                    {
                        if (newRegionY < lastRegionY)
                        {
                            //north
                            lastRegionY = newRegionY;

                            TagTranslator.overwriteRegionStream(regionArray[6], 6);
                            TagTranslator.overwriteRegionStream(regionArray[7], 7);
                            TagTranslator.overwriteRegionStream(regionArray[8], 8);

                            Globals.shiftRegionsDown(ref regionArray);
                            Globals.shiftChunksDown(ref chunkArray);

                            //Region tempRegion0 = fileLoader.loadRegionFile(newRegionX - 1, newRegionY - 1);
                            //if (tempRegion0 != null)
                            //{
                            //    //regionArray[0] = tempRegion0;
                            //}
                            //Region tempRegion1 = fileLoader.loadRegionFile(newRegionX, newRegionY - 1);
                            //if (tempRegion1 != null)
                            //{
                            //    //regionArray[1] = tempRegion1;
                            //}
                            //Region tempRegion2 = fileLoader.loadRegionFile(newRegionX + 1, newRegionY - 1);
                            //if (tempRegion2 != null)
                            //{
                            //    //regionArray[2] = tempRegion2;
                            //}
                        }
                        else if (newRegionY > lastRegionY)
                        {
                            //south
                            lastRegionY = newRegionY;

                            TagTranslator.overwriteRegionStream(regionArray[0], 0);
                            TagTranslator.overwriteRegionStream(regionArray[1], 1);
                            TagTranslator.overwriteRegionStream(regionArray[2], 2);

                            Globals.shiftRegionsUp(ref regionArray);
                            Globals.shiftChunksUp(ref chunkArray);

                            //Region tempRegion6 = fileLoader.loadRegionFile(newRegionX - 1, newRegionY + 1);
                            //if (tempRegion6 != null)
                            //{
                            //   // regionArray[6] = tempRegion6;
                            //}
                            //Region tempRegion7 = fileLoader.loadRegionFile(newRegionX, newRegionY + 1);
                            //if (tempRegion7 != null)
                            //{
                            //   // regionArray[7] = tempRegion7;
                            //}
                            //Region tempRegion8 = fileLoader.loadRegionFile(newRegionX + 1, newRegionY + 1);
                            //if (tempRegion8 != null)
                            //{
                            //   // regionArray[8] = tempRegion8;
                            //}
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

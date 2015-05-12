using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace Desolation
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ChunkManager chunkManager;
        TextureManager textureManager;
        Counter frameXNACounter;
        public static Player player;

        float[,] noise;

        public static GameWindow gameWindow;
        public static Vector2 mousePosOnScreen;
        public static int chunkmusnr;
        bool debug = false;

        AnimationEngine animationEngine;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


        }

        protected override void Initialize()
        {
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = Globals.screenX;
            graphics.PreferredBackBufferHeight = Globals.screenY;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.IsMouseVisible = true;
            Globals.rand = new Random();
            Globals.font = Content.Load<SpriteFont>("font");
            player = new Player(new Vector2(2000, 2000));
            Globals.playerPos = player.position;
            Globals.oldPlayerPos = player.position;
            chunkManager = new ChunkManager();
            textureManager = new TextureManager(Content, graphics.GraphicsDevice);
            animationEngine = new AnimationEngine();
            frameXNACounter = new Counter(); //debug


            noise = Generator.createNoise(320, 320); //debug
            //Generator.createNoise(); //debug

            ChunkManager.entityList.Add(new Zombie(new Vector2(2050, 2050)));

            for (int i = 0; i < 10; i++)
            {
                ChunkManager.entityList.Add(new Goblin(new Vector2(i * 0.1f + 2000, i * 0.1f + 2000)));
            }

            ChunkManager.entityList.Add(new Deer(new Vector2(2020, 2020)));


            for (int i = -50; i < 100; i++)
            {
                Console.WriteLine(Globals.getUniquePositiveFromAny(i));
            }

            //for (int i = -50; i < 100; i++)
            //{
            //    Console.WriteLine(Globals.getUniqueNumber((uint)i, (uint)10));
            //}

            //for (int i = 62767; i < 62867; i++)
            //{
            //    uint nr = Globals.getUniqueNumber((uint)i, (uint)i);
            //    long[] realNr = Globals.getUniqueNumberReverse(nr);
            //    Console.WriteLine("first:" + realNr[0] + " second:" + realNr[1]);
            //}

        }
        protected override void UnloadContent()
        {
            for (int i = ChunkManager.entityList.Count - 1; i >= 0; i--)
            {

                Entity e = ChunkManager.entityList[i];
                Vector2 pos = e.position;
                int regionX = Globals.getRegionValue(pos.X);
                int regionY = Globals.getRegionValue(pos.Y);
                //entity is inside unloading regions cause all regions are unloading
                int chunkNr = e.getCurrentChunkNrInArray(e.position, Globals.playerPos);
                if (chunkNr != -1)
                {
                    Chunk curChunk = ChunkManager.chunkArray[chunkNr];
                    if (curChunk != null)
                    {
                        List<Tag> newList = new List<Tag>();
                        e.getTagList(ref newList);
                        curChunk.entities.Add(newList);
                        ChunkManager.entityList.Remove(e);
                    }
                }
            }

            for (int i = ChunkManager.roomList.Count - 1; i >= 0; i--)
            {

                Room e = ChunkManager.roomList[i];
                Vector2 pos = new Vector2(e.area.X, e.area.Y);
                int regionX = Globals.getRegionValue(pos.X);
                int regionY = Globals.getRegionValue(pos.Y);
                //entity is inside unloading regions cause all regions are unloading
                int chunkNr = player.getCurrentChunkNrInArray(pos, Globals.playerPos);
                if (chunkNr != -1)
                {
                    Chunk curChunk = ChunkManager.chunkArray[chunkNr];
                    if (curChunk != null)
                    {
                        List<Tag> newList = new List<Tag>();
                        e.getTagList(ref newList);
                        curChunk.rooms.Add(newList);
                        ChunkManager.roomList.Remove(e);
                    }
                }
            }


            for (int i = 0; i < 9; i++)
            {
                TagTranslator.overwriteRegionStream(ChunkManager.regionArray[i], i);
            }
        }
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            animationEngine.update(gameTime);

            player.Update(gameTime);
            chunkManager.update(gameTime, Window);

            frameXNACounter.Update(gameTime); //debug


            #region SaveSync
            long now = DateTime.Now.Ticks;
            if (now > Globals.ticksLastChunkLoad + Globals.ticksPerChunkLoad)
            {

                Globals.playerPos = player.position;
                Globals.ticksLastChunkLoad = now;
                chunkManager.syncUpdate(gameTime); //must be done before player sync for shifting to not interfear with player movement and collision

                player.syncUpdate(gameTime); //must be done after chunkManager shifting to not interfear with movement and collision


                //textureManager.runTimeLoading();

                Globals.oldPlayerPos = Globals.playerPos;

            }
            #endregion

            #region Controls
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    player.moveDirection(Direction.NorthWest);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    player.moveDirection(Direction.NorthEast);
                }
                else
                {
                    player.moveDirection(Direction.North);
                }

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    player.moveDirection(Direction.SouthWest);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    player.moveDirection(Direction.SouthEast);
                }
                else
                {
                    player.moveDirection(Direction.South);
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.moveDirection(Direction.West);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                player.moveDirection(Direction.East);
            }
            else
            {
                player.moveDirection(Direction.None);
            }


            if (KeyMouseReader.KeyPressed(Keys.F3))
            {
                if (debug)
                {
                    debug = false;
                }
                else
                {
                    //for (int i = ChunkManager.entityList.Count - 1; i >= 0; i--)
                    //{
                    //    ChunkManager.entityList[i].speed = 0;
                    //}
                    noise = Generator.createNoise(320, 320);
                    debug = true;
                }
            }
            else if (KeyMouseReader.KeyPressed(Keys.F4))
            {
                animationEngine.animations.Add(new Animation(AnimationType.FadeOutAndIn, new Vector2(Globals.cameraPos.X, Globals.cameraPos.Y), 100, 100));
                ChunkManager.changeWorld(@"heaven\");
            }
            else if (KeyMouseReader.KeyPressed(Keys.F5))
            {
                animationEngine.animations.Add(new Animation(AnimationType.FadeOutAndIn, new Vector2(Globals.cameraPos.X, Globals.cameraPos.Y), 100, 100));
                ChunkManager.changeWorld(@"mainworld\");
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {


                //ta bort blocks på musens position

                MouseState mouse = Mouse.GetState();
                mousePosOnScreen = new Vector2(mouse.X, mouse.Y);
                float offset = 0f;
                if (mousePosOnScreen.Y > ((float)(Globals.screenY) / 2f))
                {
                    offset = 8f;
                }
                Vector2 mousePosInGame = new Vector2(Globals.playerPos.X - (float)(Globals.screenX) / 2f + mousePosOnScreen.X, Globals.playerPos.Y - ((float)(Globals.screenY) / 2f) + mousePosOnScreen.Y);
                int chunkNr = Game1.player.getCurrentChunkNrInArray(new Vector2(mousePosInGame.X, mousePosInGame.Y + offset), Globals.playerPos);
                chunkmusnr = chunkNr;
                if (chunkNr != -1)
                {
                    Chunk tempChunk = ChunkManager.chunkArray[chunkNr];
                    if (tempChunk != null)
                    {
                        //float bajs = 3.0f;
                        //float testX = (int)((Globals.screenX/mousePosOnScreen.X));
                        //float testY = (int)((Globals.screenY / mousePosOnScreen.Y));

                        int testX = Globals.getBlockValue(mousePosInGame.X); //nr som ska bli 0 - 15 broende på musens x kordinat 
                        int testY = Globals.getBlockValue(mousePosInGame.Y + offset);//nr som ska bli 0 - 15 broende på musens y kordinat 
                        int test = (testY * 16) + testX;//0 255
                        if (test < (tempChunk.objects.Length) && test >= 0)
                        {
                            tempChunk.objects[(int)test] = 1;
                        }
                    }
                }

                // ta bort blocks på musens position

            }

            //{
            //    int startpos = Globals.rand.Next(0, 255);
            //    int chunkNr = Game1.player.getCurrentChunkNrInArray(Globals.playerPos, Globals.playerPos);
            //    Chunk tempChunk = ChunkManager.chunkArray[chunkNr];
            //    if (tempChunk != null)
            //    {

            //        #region horesentel
            //        tempChunk.objects[startpos] = 1;
            //        for (int i = 0; i < Globals.rand.Next(15, 18); i++)
            //        {
            //            bool fis = true;
            //            int test5 = startpos + i;
            //            if (test5 < (tempChunk.objects.Length) && test5 >= 0 && fis)
            //            {
            //                tempChunk = ChunkManager.chunkArray[chunkNr];
            //                tempChunk.objects[test5] = 1;
            //            }


            //        }
            //        #endregion
            //        for (int i = 0; i < Globals.rand.Next(5, 15); i++)
            //        {
            //            bool fis = true;
            //            int test5 = startpos + ((i * 16) + 15);
            //            if (test5 < (tempChunk.objects.Length) && test5 >= 0 && fis)
            //            {
            //                tempChunk = ChunkManager.chunkArray[chunkNr];
            //                tempChunk.objects[test5] = 1;
            //            }

            //        }

            //    }
            //}

            else if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                MouseState mouse = Mouse.GetState();
                mousePosOnScreen = new Vector2(mouse.X, mouse.Y);
                float offset = 0f;
                if (mousePosOnScreen.Y > ((float)(Globals.screenY) / 2f))
                {
                    offset = 8f;
                }
                Vector2 mousePosInGame = new Vector2(Globals.playerPos.X - (float)(Globals.screenX) / 2f + mousePosOnScreen.X, Globals.playerPos.Y - ((float)(Globals.screenY) / 2f) + mousePosOnScreen.Y);
                int chunkNr = Game1.player.getCurrentChunkNrInArray(new Vector2(mousePosInGame.X, mousePosInGame.Y + offset), Globals.playerPos);
                chunkmusnr = chunkNr;
                if (chunkNr != -1)
                {
                    Chunk tempChunk = ChunkManager.chunkArray[chunkNr];
                    if (tempChunk != null)
                    {
                        //float bajs = 3.0f;
                        //float testX = (int)((Globals.screenX/mousePosOnScreen.X));
                        //float testY = (int)((Globals.screenY / mousePosOnScreen.Y));

                        int testX = Globals.getBlockValue(mousePosInGame.X); //nr som ska bli 0 - 15 broende på musens x kordinat 
                        int testY = Globals.getBlockValue(mousePosInGame.Y + offset);//nr som ska bli 0 - 15 broende på musens y kordinat 
                        int test = (testY * 16) + testX;//0 255
                        if (test < (tempChunk.objects.Length) && test >= 0)
                        {
                            tempChunk.objects[(int)test] = 0;
                        }
                    }
                }

            }
            else if (KeyMouseReader.KeyPressed(Keys.F8))
            {

                Structure tempStruct = new Structure(125, 125);
                tempStruct.generateRooms();
                ChunkManager.structureList.Add(tempStruct);
            }

            else if (KeyMouseReader.KeyPressed(Keys.Delete) && KeyMouseReader.keyState.IsKeyDown(Keys.LeftShift))
            {
                //delete current world

                ChunkManager.saveList(Lists.Entities);
                ChunkManager.saveList(Lists.Rooms);

                ChunkManager.saveAndUnloadRegions();

                for (int i = 0; i < 144; i++)
                {
                    ChunkManager.chunkArray[i] = null;
                }

                try
                {
                    var dir = new DirectoryInfo(Globals.gamePath + ChunkManager.fileLoader.currentWorldFolder + ChunkManager.fileLoader.regionFolder);
                    dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                    dir.Delete(true);

                    ChunkManager.fileLoader.checkAndCreateFolder(ChunkManager.fileLoader.currentWorldFolder + ChunkManager.fileLoader.regionFolder);
                }
                catch (IOException e)
                {

                }
                KeyMouseReader.Update();
                base.Update(gameTime);
            }
            #endregion


            KeyMouseReader.Update();
            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            frameXNACounter.increaseCounter();
            Matrix theMatrix = Matrix.CreateTranslation(Globals.screenX / 2 - player.position.X, Globals.screenY / 2 - player.position.Y, 0);
            Vector3 CamPos3 = theMatrix.Translation;
            Globals.cameraPos = new Vector2(-CamPos3.X, -CamPos3.Y);

            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, theMatrix);

            if (debug)
            {
                int xChunkPos = 0;
                int yChunkPos = 0;
                int chunknr = Game1.player.getCurrentChunkNrInArray(Globals.playerPos, Globals.playerPos);
                if (ChunkManager.chunkArray[chunknr] != null)
                {
                    xChunkPos = ChunkManager.chunkArray[chunknr].XPos;
                    yChunkPos = ChunkManager.chunkArray[chunknr].YPos;
                }


                int textPos = 5;
                spriteBatch.DrawString(Globals.font, "PlayerX:" + Globals.playerPos.X, new Vector2(Globals.cameraPos.X + 10, Globals.cameraPos.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "PlayerY:" + Globals.playerPos.Y, new Vector2(Globals.cameraPos.X + 10, Globals.cameraPos.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "PlayerHP:" + Game1.player.health, new Vector2(Globals.cameraPos.X + 10, Globals.cameraPos.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "XNAFrameRate:" + frameXNACounter.frameRate, new Vector2(Globals.cameraPos.X + 10, Globals.cameraPos.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "SyncCounter:" + ChunkManager.syncCounter.frameRate, new Vector2(Globals.cameraPos.X + 10, Globals.cameraPos.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                if (ChunkManager.regionArray[4] != null)
                {
                    spriteBatch.DrawString(Globals.font, "RegionX:" + ChunkManager.regionArray[4].xPosRegion, new Vector2(Globals.cameraPos.X + 10, Globals.cameraPos.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                    textPos += 15;
                }
                if (ChunkManager.regionArray[4] != null)
                {
                    spriteBatch.DrawString(Globals.font, "RegionY:" + ChunkManager.regionArray[4].yPosRegion, new Vector2(Globals.cameraPos.X + 10, Globals.cameraPos.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                    textPos += 15;
                }
                spriteBatch.DrawString(Globals.font, "ChunkX:" + xChunkPos, new Vector2(Globals.cameraPos.X + 10, Globals.cameraPos.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "ChunkY:" + yChunkPos, new Vector2(Globals.cameraPos.X + 10, Globals.cameraPos.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "muschunk:" + chunkmusnr, new Vector2(Globals.cameraPos.X + 10, Globals.cameraPos.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "Empty:", new Vector2(Globals.cameraPos.X + 10, Globals.cameraPos.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "Empty:", new Vector2(Globals.cameraPos.X + 10, Globals.cameraPos.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "Empty:", new Vector2(Globals.cameraPos.X + 10, Globals.cameraPos.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "Empty:", new Vector2(Globals.cameraPos.X + 10, Globals.cameraPos.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "Empty:", new Vector2(Globals.cameraPos.X + 10, Globals.cameraPos.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "Empty:", new Vector2(Globals.cameraPos.X + 10, Globals.cameraPos.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "Loaded Entities:" + ChunkManager.entityList.Count, new Vector2(Globals.cameraPos.X + 10, Globals.cameraPos.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;

                for (int i = 0; i < 320; i++)
                {
                    for (int j = 0; j < 320; j++)
                    {
                        spriteBatch.Draw(TextureManager.fillingTexture, new Vector2(Globals.cameraPos.X + 100 + j * 2, Globals.cameraPos.Y + 100 + i * 2), new Rectangle(0, 0, 2, 2), Generator.GetColor(Color.Black, Color.White, noise[j, i]), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
                        if (i % 16 == 0 && j % 16 == 0)
                        {
                            spriteBatch.Draw(TextureManager.fillingTexture, new Vector2(Globals.cameraPos.X + 100 + j * 2, Globals.cameraPos.Y + 100 + i * 2), new Rectangle(0, 0, 2, 2), Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                        }
                    }
                }


            }


            chunkManager.draw(spriteBatch);
            player.Draw(spriteBatch);
            animationEngine.draw(spriteBatch);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

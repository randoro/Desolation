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
        public static GameWindow gameWindow;

        bool debug = false;



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
            chunkManager = new ChunkManager();
            textureManager = new TextureManager(Content);

            frameXNACounter = new Counter(); //debug


            player = new Player(new Vector2(2000, 2000));
            Globals.playerPos = player.position;
            Globals.oldPlayerPos = player.position;
            ChunkManager.entityList.Add(new Zombie(new Vector2(2050, 2050)));
            for (int i = 0; i < 10; i++)
            {
                ChunkManager.entityList.Add(new Goblin(new Vector2(i * 0.1f + 2000, i * 0.1f + 2000)));
            }

            ChunkManager.entityList.Add(new Deer(new Vector2(2020, 2020)));
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
                int chunkNr = e.getCurrentChunkNrInArray(Globals.playerPos);
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
                    debug = true;
                }
            }
            else if (KeyMouseReader.KeyPressed(Keys.F4))
            {
                ChunkManager.changeWorld(@"heaven\");
            }
            else if (KeyMouseReader.KeyPressed(Keys.F5))
            {
                ChunkManager.changeWorld(@"mainworld\");
            }
            else if (KeyMouseReader.KeyPressed(Keys.F6))
            {
                int chunkNr = Game1.player.getCurrentChunkNrInArray(Globals.playerPos);
                Chunk tempChunk = ChunkManager.chunkArray[chunkNr];
                if (tempChunk != null)
                {
                    for (int i = 0; i < 48; i++)
                    {
                        int test = i * 16;
                        int test2 =(i * 16)+15;
                         int test3 = i+15 * 16;
                         int test4 = i ;



                      
                        if (test < (tempChunk.objects.Length))
                        {
                            tempChunk.objects[test] = 1;
                        }
                        if (test2 < (tempChunk.objects.Length)&&test2>=0)
                        {
                            tempChunk.objects[test2] = 1;

                        }
                        if (test3 < (tempChunk.objects.Length) && test3 >= 0)
                        {
                            tempChunk.objects[test3] = 1;
                        }
                        if (test4 < 16)
                        {
                            tempChunk.objects[test4] = 1;
                        }


                        //tempChunk.objects[129] = 1;

                        //tempChunk.objects[130] = 1;
                        //tempChunk.objects[131] = 1;
                        //tempChunk.objects[132] = 1;
                        //tempChunk.objects[133] = 1;
                        //tempChunk.objects[134] = 1;
                        //tempChunk.objects[135] = 1;
                    }
                }
            }
            #endregion

            KeyMouseReader.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            frameXNACounter.increaseCounter();

            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Matrix.CreateTranslation(Globals.screenX / 2 - player.position.X, Globals.screenY / 2 - player.position.Y, 0));

            if (debug)
            {
                Matrix tempMatrix = Matrix.CreateTranslation(Globals.screenX / 2 - player.position.X, Globals.screenY / 2 - player.position.Y, 0);
                Vector3 CamPos3 = tempMatrix.Translation;
                Vector2 CamPos2 = new Vector2(-CamPos3.X, -CamPos3.Y);
                int textPos = 5;
                spriteBatch.DrawString(Globals.font, "PlayerX:" + Globals.playerPos.X, new Vector2(CamPos2.X + 10, CamPos2.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "PlayerY:" + Globals.playerPos.Y, new Vector2(CamPos2.X + 10, CamPos2.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "PlayerHP:" + Game1.player.health, new Vector2(CamPos2.X + 10, CamPos2.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "XNAFrameRate:" + frameXNACounter.frameRate, new Vector2(CamPos2.X + 10, CamPos2.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "SyncCounter:" + ChunkManager.syncCounter.frameRate, new Vector2(CamPos2.X + 10, CamPos2.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "Empty:", new Vector2(CamPos2.X + 10, CamPos2.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "Empty:", new Vector2(CamPos2.X + 10, CamPos2.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "Empty:", new Vector2(CamPos2.X + 10, CamPos2.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "Empty:", new Vector2(CamPos2.X + 10, CamPos2.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "Empty:", new Vector2(CamPos2.X + 10, CamPos2.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "Empty:", new Vector2(CamPos2.X + 10, CamPos2.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "Empty:", new Vector2(CamPos2.X + 10, CamPos2.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "Empty:", new Vector2(CamPos2.X + 10, CamPos2.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "Empty:", new Vector2(CamPos2.X + 10, CamPos2.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "Empty:", new Vector2(CamPos2.X + 10, CamPos2.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;
                spriteBatch.DrawString(Globals.font, "Loaded Entities:" + ChunkManager.entityList.Count, new Vector2(CamPos2.X + 10, CamPos2.Y + textPos), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                textPos += 15;

            }


            chunkManager.draw(spriteBatch);
            player.Draw(spriteBatch);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

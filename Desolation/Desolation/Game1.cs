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
        Camera camera;
        TextureManager textureManager;

        Texture2D spriteSheet;
        Texture2D zombieSheet, gobSheet;
        public static Player player;
        public static GameWindow gameWindow;



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
            chunkManager = new ChunkManager();

            camera = new Camera(GraphicsDevice.Viewport);
            textureManager = new TextureManager(Content);


            player = new Player(new Vector2(200, 200));
            ChunkManager.entityList.Add(new Zombie(player, new Vector2(500, 500)));
            for (int i = 0; i < 150; i++)
            {
            ChunkManager.entityList.Add(new Goblin(new Vector2(i*0.1f, i*0.1f)));
            }

            ChunkManager.entityList.Add(new Deer(player, new Vector2(100, 100)));
        }
        protected override void UnloadContent()
        {
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


            #region SaveSync
            long now = DateTime.Now.Ticks;
            if (now > Globals.ticksLastChunkLoad + Globals.ticksPerChunkLoad)
            {
                Globals.playerPos = player.position;
                Globals.ticksLastChunkLoad = now;

                chunkManager.syncUpdate(gameTime);

                textureManager.runTimeLoading();

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
            #endregion

            KeyMouseReader.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Matrix.CreateTranslation(Globals.screenX / 2 - player.position.X, Globals.screenY / 2 - player.position.Y, 0));


            chunkManager.draw(spriteBatch);
            player.Draw(spriteBatch);
            

            

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

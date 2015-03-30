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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
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

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = Globals.screenX;
            graphics.PreferredBackBufferHeight = Globals.screenY;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.IsMouseVisible = true;
            // TODO: use this.Content to load your game content here
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
            // TODO: Unload any non ContentManager content here
            for (int i = 0; i < 9; i++)
            {
            TagTranslator.overwriteRegionStream(ChunkManager.regionArray[i], i);
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
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

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Matrix.CreateTranslation(Globals.screenX / 2 - player.position.X, Globals.screenY / 2 - player.position.Y, 0));


            chunkManager.draw(spriteBatch);
            player.Draw(spriteBatch);
            

            

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

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
        FileLoader fileLoader;
        ChunkManager chunkManager;
        Camera camera;
        TextureManager textureManager;

        Texture2D spriteSheet;
        Texture2D zombieSheet, gobSheet;
        Zombie zombie;
        Player player;
        Goblin goblin;
        GameObject go;



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
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
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
            
            fileLoader = new FileLoader();
            chunkManager = new ChunkManager();

            camera = new Camera(GraphicsDevice.Viewport);
            textureManager = new TextureManager(Content);

            Region tempRegion = fileLoader.loadRegionFile(0, 0);
            Globals.rand = new Random();
            TempChunkCreator tempChunkCreator = new TempChunkCreator(tempRegion.fileStream);
            chunkManager.addRegion(tempRegion);

            spriteSheet = Content.Load<Texture2D>("testSheet");
            zombieSheet = Content.Load<Texture2D>("ZombieSheet");
            gobSheet = Content.Load<Texture2D>("npcSheet");
            Globals.tempsheet = Content.Load<Texture2D>("tempblocksheet");

            player = new Player(new Vector2(200, 200));
            zombie = new Zombie(player, new Vector2(500, 500));
            goblin = new Goblin(new Vector2(350, 250));
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

            camera.update(new Vector2(player.position.X - 500, player.position.Y - 500));
            player.Update(gameTime);
            goblin.Update(gameTime);
            zombie.Update(gameTime);
            chunkManager.update(gameTime);

            textureManager.runTimeLoading();

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

            // TODO: Add your update logic here
            KeyMouseReader.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);


            chunkManager.draw(spriteBatch);
            player.Draw(spriteBatch);
            
            zombie.Draw(spriteBatch);
            goblin.Draw(spriteBatch);

            

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

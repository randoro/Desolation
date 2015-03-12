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

        Texture2D spriteSheet;
        Texture2D zombieSheet, gobSheet;
        Zombie zombie;
       public static Player player;
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
            Region tempRegion = fileLoader.loadRegionFile(0, 0);
            Globals.rand = new Random();
            TempChunkCreator tempChunkCreator = new TempChunkCreator(tempRegion.fileStream);
            chunkManager.addRegion(tempRegion);

            spriteSheet = Content.Load<Texture2D>("testSheet");
            zombieSheet = Content.Load<Texture2D>("ZombieSheet");
            gobSheet = Content.Load<Texture2D>("npcSheet");
            Globals.tempsheet = Content.Load<Texture2D>("tempblocksheet");

            player = new Player(spriteSheet, new Rectangle() , new Vector2());
            zombie = new Zombie(zombieSheet, new Rectangle(), new Vector2());
            goblin = new Goblin(gobSheet, new Rectangle(), new Vector2());
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

            player.Update(gameTime);
            goblin.Update(gameTime);
            zombie.Update(gameTime);
            chunkManager.update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();


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

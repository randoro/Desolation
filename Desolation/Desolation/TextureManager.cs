using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Desolation
{
    public class TextureManager
    {
        public static Texture2D playerSheet { set; get; }
        public static Texture2D zombieSheet { set; get; }
        public static Texture2D npcSheet { set; get; }
        public static Texture2D deerSheet { set; get; }
        public static Texture2D blocksheet { set; get; }
        public static Texture2D objectsheet { set; get; }
        public static Texture2D roofsheet { set; get; }

        public static Texture2D leaf { set; get; }

        public static Texture2D leaf2 { set; get; }

        public static Texture2D leaf3 { set; get; }

        public static Texture2D fillingTexture { set; get; }


        ContentManager contentManager;

        byte currentskin;

        public TextureManager(ContentManager contentManager, GraphicsDevice graphics)
        {
            this.contentManager = contentManager;

            playerSheet = contentManager.Load<Texture2D>("playerSheet2");
            zombieSheet = contentManager.Load<Texture2D>("ZombieSheet");
            npcSheet = contentManager.Load<Texture2D>("npcSheet");
            deerSheet = contentManager.Load<Texture2D>("DeerspriteShite");
            blocksheet = contentManager.Load<Texture2D>("tempblocksheet");
            objectsheet = contentManager.Load<Texture2D>("tempobjectsheet");
            roofsheet = contentManager.Load<Texture2D>("temproofsheet");
            leaf = contentManager.Load<Texture2D>("leaf");
            leaf2 = contentManager.Load<Texture2D>("leaf2");
            leaf3 = contentManager.Load<Texture2D>("leaf3");

            fillingTexture = new Texture2D(graphics, 1, 1);
            fillingTexture.SetData(new[] { Color.White });
            currentskin = 0;
        }

        public void runTimeLoading()
        {
            if (currentskin == 0)
            {
                currentskin = 1;
                playerSheet = contentManager.Load<Texture2D>("npcSheet");
            }
            else
            {
                currentskin = 0;
                playerSheet = contentManager.Load<Texture2D>("testSheet");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        ContentManager contentManager;

        public TextureManager(ContentManager contentManager)
        {
            this.contentManager = contentManager;

            playerSheet = contentManager.Load<Texture2D>("testSheet");
            zombieSheet = contentManager.Load<Texture2D>("ZombieSheet");
            npcSheet = contentManager.Load<Texture2D>("npcSheet");
            deerSheet = contentManager.Load<Texture2D>("DeerspriteShite");
        }

        public void runTimeLoading()
        {
            if (Globals.rand.Next(0, 2) == 0)
            {
                playerSheet = contentManager.Load<Texture2D>("npcSheet");
            }
            else
            {
                playerSheet = contentManager.Load<Texture2D>("testSheet");
            }
        }
    }
}

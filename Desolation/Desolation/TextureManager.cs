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
        public static Texture2D blocksheet { set; get; }

        public static Texture2D redlazer { get; set; }
        ContentManager contentManager;

        byte currentskin;

        public TextureManager(ContentManager contentManager)
        {
            this.contentManager = contentManager;

            playerSheet = contentManager.Load<Texture2D>("playerSheet2");
            zombieSheet = contentManager.Load<Texture2D>("ZombieSheet");
            npcSheet = contentManager.Load<Texture2D>("npcSheet");
            deerSheet = contentManager.Load<Texture2D>("DeerspriteShite");
            blocksheet = contentManager.Load<Texture2D>("tempblocksheet");
            redlazer = contentManager.Load<Texture2D>("redlazer");
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

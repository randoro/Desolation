using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Desolation
{
    class TextureManager
    {
        Texture2D runTimetestSheet;
        ContentManager contentManager;

        public TextureManager(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }

        public void runTime()
        {
            //runTimetestSheet = contentManager.Load<Texture2D>("testSheet");
        }
    }
}

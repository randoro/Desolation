using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Desolation
{
    class Player
    {
        Texture2D pSheet;
        Rectangle pRect;
        Vector2 pPos;
        SpriteEffects pFx;

        int frame;
        double frameTimer, frameInterval = 100;

        public Player(Texture2D pSheet, Rectangle pRect, Vector2 pPos)
        {
            this.pSheet = pSheet;
            this.pPos = new Vector2(100, 100);
            this.pRect = new Rectangle(0, 16, 16, 16);



        }

        public void Update(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
                pRect.Y = (frame % 4) * 16;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pSheet, pPos, pRect, Color.White, 0f, new Vector2(), 1f, pFx, 1);
        }

       
    }
}

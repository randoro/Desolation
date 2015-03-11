﻿using System;
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
    class Player:GameObject
    {
        Rectangle pRect;
        Vector2 pPos;
        Texture2D sheet;
        
        int frame;
        double frameTimer, frameInterval = 100;
        public Player(Texture2D sheet, Rectangle rect, Vector2 pos)
            :base(sheet, rect, pos)
        {
            this.pRect = new Rectangle(0, 16, 16, 16);
            this.pPos = new Vector2(100, 100);
            this.sheet = sheet;
        }

        public override void Update(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
            }

            if(Keyboard.GetState().IsKeyDown(Keys.W))
            {
                pPos.Y--;
                pRect.X = 2 * 16;
                pRect.Y = (frame % 4) * 16;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                pPos.Y++;
                pRect.X = 0 * 16;
                pRect.Y = (frame % 4) * 16;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                pPos.X++;
                pRect.X = 3 * 16;
                pRect.Y = (frame % 4) * 16;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                pPos.X--;
                pRect.X = 1 * 16;
                pRect.Y = (frame % 4) * 16;
            }
            

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sheet, pPos, pRect, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
        }
  
    }
}

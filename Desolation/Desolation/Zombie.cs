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
   public class Zombie :Entity
    {
        Texture2D sheet;
        Rectangle zRect;
        Vector2 zPos;
        Player player = Game1.player;
        int frame;
        double frameTimer, frameInterval = 100;
        
        public Zombie(Texture2D sheet, Rectangle rect, Vector2 pos)

            : base(sheet, rect, pos)
        {
            this.sheet = sheet;
            this.zRect = rect;
            this.zPos = pos;
            zRect = new Rectangle(0, 0, 16, 16);
            zPos = new Vector2(400, 300);
        }
        public override void Update(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
            }
           if  (player.getPOS().X > zPos.X)
           {
               zPos.X++;
               zRect.X = 3 * 16;
               zRect.Y = (frame % 4) * 16;

           }
            if(player.getPOS().X < zPos.X)
            {
                zPos.X--;
                zRect.X = 1 * 16;
                zRect.Y = (frame % 4) * 16;
            }
            if (player.getPOS().Y < zPos.Y)
            {
                zPos.Y--;
                zRect.X = 2 * 16;
                zRect.Y = (frame % 4) * 16;
            }
            if (player.getPOS().Y > zPos.Y)
            {
                zPos.Y++;
                zRect.X = 0 * 16;
                zRect.Y = (frame % 4) * 16;
            }

            //if (Keyboard.GetState().IsKeyDown(Keys.Up))
            //{
            //    zPos.Y--;
            //    zRect.X = 2 * 16;
            //    zRect.Y = (frame % 4) * 16;
            //}
            //if (Keyboard.GetState().IsKeyDown(Keys.Down))
            //{
            //    zPos.Y++;
            //    zRect.X = 0 * 16;
            //    zRect.Y = (frame % 4) * 16;
            //}
            //if (Keyboard.GetState().IsKeyDown(Keys.Right))
            //{
            //    zPos.X++;
            //    zRect.X = 3 * 16;
            //    zRect.Y = (frame % 4) * 16;
            //}
            //if (Keyboard.GetState().IsKeyDown(Keys.Left))
            //{
            //    zPos.X--;
            //    zRect.X = 1 * 16;
            //    zRect.Y = (frame % 4) * 16;
            //}
           
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sheet, zPos, zRect, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
           
        }

    }

}

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
    class Player  :   Entity
    {
      
        int frame;
        double frameTimer, frameInterval = 100;
        public Player(Vector2 position)
            : base(position)
        {
            this.position = position;
            sourceRect = new Rectangle(0, 16, 16, 16);
        }

        public override void Update(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
            }

            /*if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position.X--;

                sourceRect.X = 1 * 16;
                sourceRect.Y = (frame % 4) * 16;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.X++;

                sourceRect.X = 3 * 16;
                sourceRect.Y = (frame % 4) * 16;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Y++;

                sourceRect.X = 0 * 16;
                sourceRect.Y = (frame % 4) * 16;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position.Y--;

                sourceRect.X = 2 * 16;
                sourceRect.Y = (frame % 4) * 16;
            }*/
        }

        public override void moveDirection(Direction direction)
        {
            base.moveDirection(direction);
        }
        
       

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.playerSheet, position, sourceRect, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
        }
  
    }
}

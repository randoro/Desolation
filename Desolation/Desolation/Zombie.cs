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
    class Zombie :Entity
    {
        int frame;
        double frameTimer, frameInterval = 100;
        int range = 250;
        Player player;
        Direction currentDirection;
        public Zombie(Player player, Vector2 pos)
            : base(pos)
        {
            sourceRect = new Rectangle(0, 0, 16, 16);
            position = new Vector2(400, 300);
            this.player = player;
        }
        public override void moveDirection(Direction direction)
        {
            currentDirection = direction;
            base.moveDirection(direction);
        }

        public override void Update(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

            moveDirection(currentDirection);
            //måste fixas
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
            }
            if (player.position.X > position.X && ((player.position.X - position.X)) < range)
           {
               currentDirection = Direction.East; 
              // position.X += 0.5f;
               sourceRect.X = 3 * 16;
               sourceRect.Y = (frame % 4) * 16;

           }
            if (player.position.X < position.X && ((player.position.X - position.X)) > -range)
            {
                currentDirection = Direction.West; 
               // position.X -= 0.5f;
                sourceRect.X = 1 * 16;
                sourceRect.Y = (frame % 4) * 16;
            }
            if (player.position.Y < position.Y && ((player.position.Y - position.Y)) > -range)
            {
                currentDirection = Direction.North; 
                //  position.Y -= 0.5f;
                sourceRect.X = 2 * 16;
                sourceRect.Y = (frame % 4) * 16;
            }
            if (player.position.Y > position.Y && ((player.position.Y - position.Y)) < range)
            {
                currentDirection = Direction.South; 
               // position.Y += 0.5f;
                sourceRect.X = 0 * 16;
                sourceRect.Y = (frame % 4) * 16;
            }

           
           
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.zombieSheet, position, sourceRect, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
           
        }

    }

}

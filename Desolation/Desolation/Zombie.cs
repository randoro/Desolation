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
    class Zombie : Entity
    {
        int frame;
        double frameTimer, frameInterval = 100;
        int range = 450;
        Player player;
        Direction currentDirection;
        public Zombie(Player player, Vector2 pos)
            : base(pos)
        {
            sourceRect = new Rectangle(0, 0, 16, 16);
            position = new Vector2(400, 300);
            this.player = player;

            speed = 1;
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
            if (player.position.Y < position.Y -1 && ((player.position.Y - position.Y)) < range)//Y
            {

                // position.X += 0.5f;
                sourceRect.X = 2 * 16;
                sourceRect.Y = (frame % 4) * 16;
                if (player.position.X < position.X -1 && ((player.position.X - position.X)) < range)
                {
                    currentDirection = Direction.NorthWest;
                }
                else if (player.position.X > position.X +1 && ((player.position.X - position.X)) < range)
                {
                    currentDirection = Direction.NorthEast;
                }
                else
                {
                    currentDirection = Direction.North;
                }


            }
            else if (player.position.Y > position.Y +1 && ((player.position.Y - position.Y)) > -range)
            {

                // position.X -= 0.5f;
                sourceRect.X = 0 * 16;
                sourceRect.Y = (frame % 4) * 16;
                if (player.position.X < position.X -1 && ((player.position.Y - position.Y)) < range)
                {
                    currentDirection = Direction.SouthWest;

                }
                else if (player.position.X > position.X +1 && ((player.position.X - position.X)) < range)
                {
                    currentDirection = Direction.SouthEast;
                }
                else
                {
                    currentDirection = Direction.South;
                }
            }
            else if (player.position.X < position.X -1 && ((player.position.X - position.X)) > -range)
            {
                currentDirection = Direction.West;
                //  position.Y -= 0.5f;
                sourceRect.X = 1 * 16;
                sourceRect.Y = (frame % 4) * 16;

            }
            else if (player.position.X > position.X +1 && ((player.position.X - position.X)) < range)
            {
                currentDirection = Direction.East;
                //// position.Y += 0.5f;
                sourceRect.X = 3 * 16;
                sourceRect.Y = (frame % 4) * 16;

            }
            else
            {
                currentDirection = Direction.None;
                sourceRect.X = 0 * 16;
            }





        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.zombieSheet, position, sourceRect, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

        }

    }

}

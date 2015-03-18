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
    public class Goblin:Entity
    {
        Random rnd = new Random();

        Direction currentDirection;

        double totalElapsedSeconds = 2;
        const double MovementChangeTimeSeconds = 1.0; //seconds

        int frame;
        double frameTimer, frameInterval = 100;

        public Goblin(Vector2 position)
            : base(position)
        {
            this.position = position;
            this.sourceRect = new Rectangle(0, 16, 16, 16);
            speed = 3;
        }

        public override void Update(GameTime gameTime)
        {
            totalElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

            if (totalElapsedSeconds >= MovementChangeTimeSeconds)
            {
                totalElapsedSeconds -= MovementChangeTimeSeconds;
                currentDirection = GetRandomDirection();
            }
            base.moveDirection(currentDirection);

            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
            }

            Game1.gameWindow.Title = "GoblinX:"+position.X+" GoblinY:"+position.Y;

            switch (currentDirection)
            {
                case Direction.North:
                    sourceRect.X = 2 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    break;
                case Direction.NorthEast:
                    sourceRect.X = 2 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    break;
                case Direction.NorthWest:
                    sourceRect.X = 2 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    break;
                case Direction.South:
                    sourceRect.X = 0 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    break;
                case Direction.SouthEast:
                    sourceRect.X = 0 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    break;
                case Direction.SouthWest:
                    sourceRect.X = 0 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    break;
                case Direction.East:
                    sourceRect.X = 3 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    break;
                case Direction.West:
                    sourceRect.X = 1 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    break;
                case Direction.None:
                    break;
            }
        }
          
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.npcSheet, position, sourceRect, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
        }

        public Direction GetRandomDirection()
        {
            int randomDirection = Globals.rand.Next(8);

            switch (randomDirection)
            {
                case 0:
                    return Direction.North;
                case 1:
                    return Direction.NorthEast;
                case 2:
                    return Direction.East;
                case 3:
                    return Direction.SouthEast;
                case 4:
                    return Direction.South;
                case 5:
                    return Direction.SouthWest;
                case 6:
                    return Direction.West;
                case 7:
                    return Direction.NorthWest;
                default:
                    return Direction.None;
            }



        }

        //public override void moveDirection(Direction direction)
        //{
        //    if (direction < 0)
        //    {
        //        if (direction < 0)
        //            direction = Direction.NorthWest;
        //        else if (direction > 0)
        //            direction = Direction.NorthEast;
        //        else
        //            direction = Direction.North;
        //    }
        //    else if (direction > 0)
        //    {
        //        if (direction < 0)
        //            direction = Direction.SouthWest;
        //        else if (direction > 0)
        //            direction = Direction.SouthEast;
        //        else
        //            direction = Direction.South;
        //    }
        //    else
        //    {
        //        if (direction < 0)
        //            direction = Direction.West;
        //        else if (direction > 0)
        //            direction = Direction.East;
        //        else
        //            direction = Direction.None;
        //    }
        //}
    }
}

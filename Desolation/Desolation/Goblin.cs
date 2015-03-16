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
    class Goblin:Entity
    {
        Vector2 gDir;
        Random rnd = new Random();

        Direction currentDirection;

        double totalElapsedSeconds = 2;
        const double MovementChangeTimeSeconds = 2.0; //seconds

        int frame;
        double frameTimer, frameInterval = 100;

        public Goblin(Vector2 position)
            : base(position)
        {

            this.position = position;
            this.sourceRect = new Rectangle(0, 16, 16, 16);

            
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
            //position += gDir;

            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
            }

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
                    break;
                case 1:
                    return Direction.NorthEast;
                    break;
                case 2:
                    return Direction.East;
                    break;
                case 3:
                    return Direction.SouthEast;
                    break;
                case 4:
                    return Direction.South;
                    break;
                case 5:
                    return Direction.SouthWest;
                    break;
                case 6:
                    return Direction.West;
                    break;
                case 7:
                    return Direction.NorthWest;
                    break;
                default:
                    return Direction.None;
                    break;
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

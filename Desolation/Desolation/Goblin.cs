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

        double totalElapsedSeconds = 0;
        const double MovementChangeTimeSeconds = 1.0; //seconds

        int frame;
        double frameTimer, frameInterval = 100;

        public Goblin(Vector2 position)
            : base(position)
        {

            this.position = position;
            this.sourceRect = new Rectangle(0, 16, 16, 16);

            base.getCurrentChunkNrInArray();
        }
        

        public override void Update(GameTime gameTime)
        {
            totalElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;
           

            if (totalElapsedSeconds >= MovementChangeTimeSeconds)
            {
                totalElapsedSeconds -= MovementChangeTimeSeconds;
                this.gDir = GetRandomDirection();
            }

            //position = gDir;

            position += gDir;

            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
            }
        }
        public override void moveDirection(Direction direction)
        {
            currentDirection = direction;
            base.moveDirection(direction);
        }
          
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.npcSheet, position, sourceRect, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

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

        Vector2 GetRandomDirection()
        {
            Random random = new Random();
            int randomDirection = random.Next(8);

            switch (randomDirection)
            {
                case 1:
                    return new Vector2(-1, 0);
                case 2:
                    return new Vector2(1, 0);
                case 3:
                    return new Vector2(0, -1);
                case 4:
                    return new Vector2(0, 1);
                case 5:
                    return new Vector2(-1, -1);
                case 6:
                    return new Vector2(1, 1);
                default:
                    return Vector2.Zero;
            }
        }
        enum MoveDirection
        {
            Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight, None
        }

        MoveDirection GetMoveDirection(Vector2 direction)
        {
            if (direction.Y < 0)
            {
                if (direction.X < 0)
                    return MoveDirection.UpLeft;
                else if (direction.X > 0)
                    return MoveDirection.UpRight;
                else
                    return MoveDirection.Up;
            }
            else if (direction.Y > 0)
            {
                if (direction.X < 0)
                    return MoveDirection.DownLeft;
                else if (direction.X > 0)
                    return MoveDirection.DownRight;
                else
                    return MoveDirection.Down;
            }
            else
            {
                if (direction.X < 0)
                    return MoveDirection.Left;
                else if (direction.X > 0)
                    return MoveDirection.Right;
                else
                    return MoveDirection.None;
            }
        }
    }
}

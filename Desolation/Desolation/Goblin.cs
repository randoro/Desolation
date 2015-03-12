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
        Rectangle gRect;
        Vector2 gPos, gDir;
        TextureManager textureManager;
        Random rnd = new Random();

        double totalElapsedSeconds = 0;
        const double MovementChangeTimeSeconds = 1.0; //seconds

        int frame;
        double frameTimer, frameInterval = 100;

        public Goblin(Rectangle rect, Vector2 pos)
            : base(rect, pos)
        {
            this.gRect = new Rectangle(0, 16, 16, 16);
            this.gPos = new Vector2(350, 250);
        }

        public override void Update(GameTime gameTime)
        {
            totalElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;
           

            if (totalElapsedSeconds >= MovementChangeTimeSeconds)
            {
                totalElapsedSeconds -= MovementChangeTimeSeconds;
                this.gDir = GetRandomDirection();
            }

            gPos += gDir;

            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
            }

            MoveDirection moveDirection = GetMoveDirection(this.gDir);
            switch (moveDirection)
            {
                case MoveDirection.Up:
                    gRect.X = 2 * 16;
                    gRect.Y = (frame % 4) * 16;
                    break;
                case MoveDirection.UpRight:
                    gRect.X = 2 * 16;
                    gRect.Y = (frame % 4) * 16;
                    break;
                case MoveDirection.UpLeft:
                    gRect.X = 2 * 16;
                    gRect.Y = (frame % 4) * 16;
                    break;
                case MoveDirection.Down:
                    gRect.X = 0 * 16;
                    gRect.Y = (frame % 4) * 16;
                    break;
                case MoveDirection.DownRight:
                    gRect.X = 0 * 16;
                    gRect.Y = (frame % 4) * 16;
                    break;
                case MoveDirection.DownLeft:
                    gRect.X = 0 * 16;
                    gRect.Y = (frame % 4) * 16;
                    break;
                case MoveDirection.Right:
                    gRect.X = 3 * 16;
                    gRect.Y = (frame % 4) * 16;
                    break;
                case MoveDirection.Left:
                    gRect.X = 1 * 16;
                    gRect.Y = (frame % 4) * 16;
                    break;

            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.npcSheet, gPos, gRect, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
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

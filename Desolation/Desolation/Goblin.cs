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
        Texture2D sheet;
        Random rnd = new Random();

        double totalElapsedSeconds = 0;
        const double MovementChangeTimeSeconds = 1.0; //seconds

        public Goblin(Texture2D sheet, Rectangle rect, Vector2 pos)
            :base(sheet, rect, pos)
        {
            this.gRect = new Rectangle(0, 16, 16, 16);
            this.gPos = new Vector2(350, 250);
            this.sheet = sheet;
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

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sheet, gPos, gRect, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
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

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
    class Deer : Entity
    {
        int frame;
        double frameTimer, frameInterval = 100;
        float range = 300;
        Player player;
        bool InRange = false;
        Direction currentDirection;
        #region Constructor
        public Deer(Vector2 pos)
            : base(pos)
        {
            sourceRect = new Rectangle(0, 0, 16, 32);
            position = new Vector2(100, 100); 
            player = Game1.player;

            speed = 2;
        }
        #endregion
        #region Methods
        public override void moveDirection(Direction direction)
        {
            currentDirection = direction;
            base.moveDirection(direction);
        }
        public override void Update(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if ((player.position.X - position.X) * (player.position.X - position.X) + (player.position.Y - position.Y) * (player.position.Y - position.Y) < (range * range))
            {
                InRange = true;
                speed=speed+0.01f;
                if (speed >= 2)
                {
                    speed = 2;
                }
            }
            else
            {
                InRange = false;
            }
            moveDirection(currentDirection);

            #region SaveSync
            long now = DateTime.Now.Ticks;
            if (now > Globals.ticksLastChunkLoad + Globals.ticksPerChunkLoad)
            {

                base.checkCollision();
            }
            #endregion
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
            }
            #region InRange
            if (InRange)
            {

                if (player.position.Y > position.Y - 5)
                {
                    //sourceRect.X = 2 * 16;
                    //sourceRect.Y = (frame % 4) * 16;
                    if (player.position.X > position.X - 5)
                    {
                        currentDirection = Direction.NorthWest;
                    }
                    else if (player.position.X < position.X + 5)
                    {
                        currentDirection = Direction.NorthEast;
                    }
                    else
                    {
                        currentDirection = Direction.North;
                    }
                }
                else if (player.position.Y < position.Y + 5)
                {
                    //sourceRect.X = 0 * 16;
                    //sourceRect.Y = (frame % 4) * 16;
                    if (player.position.X > position.X - 5)
                    {
                        currentDirection = Direction.SouthWest;

                    }
                    else if (player.position.X < position.X + 5)
                    {
                        currentDirection = Direction.SouthEast;
                    }
                    else
                    {
                        currentDirection = Direction.South;
                    }
                }
                else if (player.position.X > position.X - 5)
                {
                    currentDirection = Direction.West;
                    //sourceRect.X = 1 * 16;
                    //sourceRect.Y = (frame % 4) * 16;

                }
                else if (player.position.X < position.X + 5)
                {
                    currentDirection = Direction.East;
                    //sourceRect.X = 3 * 16;
                    //sourceRect.Y = (frame % 4) * 16;

                }
                else
                {
                    currentDirection = Direction.None;
                    sourceRect.X = 0 * 16;
                }
            }
            else
            {
                speed=speed-0.01f;
                if(speed<=0)
                {
                    speed = 0;
                }
            }
            #endregion
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.deerSheet, new Vector2(position.X - 8, position.Y - 15), sourceRect, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

        }


        public override void getTagList(ref List<Tag> individualList)
        {
            Tag compound = new Tag(TagID.Compound, "Deer", null, TagID.Compound);
            individualList.Add(compound);

            int theInt = (int)EntityID.Deer;
            byte[] entityID = BitConverter.GetBytes(theInt);
            Tag ID = new Tag(TagID.Int, "ID", entityID, TagID.Int);
            individualList.Add(ID);

            base.getTagList(ref individualList);

            Tag end = new Tag(TagID.End, null, null, TagID.End);
            individualList.Add(end);
        }
        #endregion

    }


}

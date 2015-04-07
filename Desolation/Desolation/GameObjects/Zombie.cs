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
        float range = 200;
        Player player;
        bool InRange = false;
        Direction currentDirection;
        #region Constructor
        public Zombie(Vector2 pos)
            : base(pos)
        {
            sourceRect = new Rectangle(0, 0, 16, 16);
            position = new Vector2(400, 300);
            player = Game1.player;

            speed = 1;
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
            if ((player.position.X - position.X) * (player.position.X - position.X) + (player.position.Y - position.Y) * (player.position.Y - position.Y)<(range*range))
            {
                InRange = true;
            }
            else
            {
                InRange = false;
                currentDirection = Direction.None;

            }
            moveDirection(currentDirection);
            
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
            }
            #region CheckRange
            if (InRange)
            {
                if (player.position.Y < position.Y - 1)
                {
                    sourceRect.X = 2 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    if (player.position.X < position.X - 1)
                    {
                        currentDirection = Direction.NorthWest;
                    }
                    else if (player.position.X > position.X + 1)
                    {
                        currentDirection = Direction.NorthEast;
                    }
                    else
                    {
                        currentDirection = Direction.North;
                    }
                }
                else if (player.position.Y > position.Y + 1 )
                {    
                    sourceRect.X = 0 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    if (player.position.X < position.X - 1)
                    {
                        currentDirection = Direction.SouthWest;

                    }
                    else if (player.position.X > position.X + 1)
                    {
                        currentDirection = Direction.SouthEast;
                    }
                    else
                    {
                        currentDirection = Direction.South;
                    }
                }
                else if (player.position.X < position.X - 1)
                {
                    currentDirection = Direction.West;
                    sourceRect.X = 1 * 16;
                    sourceRect.Y = (frame % 4) * 16;

                }
                else if (player.position.X > position.X + 1)
                {
                    currentDirection = Direction.East;
                    sourceRect.X = 3 * 16;
                    sourceRect.Y = (frame % 4) * 16;

                }
                else
                {
                    currentDirection = Direction.None;
                    sourceRect.X = 0 * 16;
                }
            }
            #endregion
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.zombieSheet, new Vector2(position.X - 8, position.Y - 15), sourceRect, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
        }


        public void getTagList(ref List<Tag> individualList)
        {
            Tag compound = new Tag(TagID.Compound, "Zombie", null, TagID.Compound);
            individualList.Add(compound);

            Tag ID = new Tag(TagID.Byte, "ID", (sbyte)EntityID.Zombie, TagID.Byte);
            individualList.Add(ID);

            base.getTagList(ref individualList);

            Tag end = new Tag(TagID.End, null, null, TagID.End);
            individualList.Add(end);
        }

        #endregion
    }
}

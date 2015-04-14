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

        #region Constructor
        public Goblin(Vector2 position)
            : base(position)
        {
            this.position = position;
            this.sourceRect = new Rectangle(0, 16, 16, 16);
            speed = 0;
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            totalElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

            if (totalElapsedSeconds >= MovementChangeTimeSeconds)
            {
                totalElapsedSeconds -= MovementChangeTimeSeconds;
                currentDirection = GetRandomDirection();
            }
 
            
            base.moveDirection(currentDirection);

            #region SaveSync
            long now = DateTime.Now.Ticks;
            if (now > Globals.ticksLastChunkLoad + Globals.ticksPerChunkLoad)
            {
                
                base.checkCollision();
            }
            #endregion

            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
            }

            #region CurrentDirection
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
            #endregion
        }
          
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.npcSheet, new Vector2(position.X - 8, position.Y - 15), sourceRect, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
        }

        public Direction GetRandomDirection()
        {
            int randomDirection = Globals.rand.Next(9);

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
                case 8:
                    return Direction.None;
                default:
                    return Direction.None;
            }
        }


        public override void getTagList(ref List<Tag> individualList)
        {
            Tag compound = new Tag(TagID.Compound, "Goblin", null, TagID.Compound);
            individualList.Add(compound);


            int theInt = (int)EntityID.Goblin;
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

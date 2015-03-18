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
   abstract public class Entity :   GameObject
    {

        protected float speed;
        protected float realSpeed;
        public Entity(Vector2 pos)
           : base(pos)
        {
            //speed = 0;
            
        }

        abstract public override void Update(GameTime gameTime);

        abstract public override void Draw(SpriteBatch spriteBatch);

        public virtual void moveDirection(Direction direction)
        {
            Vector2 oldPosition = position;
            

            int currentBlockX;
            int currentBlockY;
            if (position.X >= 0)
            {
                currentBlockX = ((int)position.X / 16) % 16;
            }
            else
            {
                currentBlockX = 15 + ((int)position.X + 1) / 16 % 16;
            }

            if (position.Y >= 0)
            {
                currentBlockY = ((int)position.Y / 16) % 16;
            }
            else
            {
                currentBlockY = 15 + ((int)position.Y + 1) / 16 % 16;
            }
            int chunkIndex = getCurrentChunkNrInArray();
            if (chunkIndex != -1)
            {
                Chunk currentChunk = ChunkManager.chunkArray[chunkIndex];
                if (currentChunk != null)
                {
                    currentChunk.blocks[currentBlockX + currentBlockY * 16] = (byte)1;

                    realSpeed = (float)((Math.Sqrt((speed * speed) + (speed * speed))) / 2);
                    switch (direction)
                    {
                        case Direction.North:
                            position.Y -= speed;

                            break;
                        case Direction.NorthEast:
                            position.X += realSpeed;
                            position.Y -= realSpeed;
                            break;
                        case Direction.East:
                            position.X += speed;
                            break;
                        case Direction.SouthEast:
                            position.X += realSpeed;
                            position.Y += realSpeed;
                            break;
                        case Direction.South:
                            position.Y += speed;
                            break;
                        case Direction.SouthWest:
                            position.X -= realSpeed;
                            position.Y += realSpeed;
                            break;
                        case Direction.West:
                            position.X -= speed;
                            break;
                        case Direction.NorthWest:
                            position.X -= realSpeed;
                            position.Y -= realSpeed;
                            break;
                        case Direction.None:
                            break;
                        default:
                            break;
                    }
                }

            }
            //Chunk currentChunk = ChunkManager.chunkArray[chunkIndex];
            //Game1.gameWindow.Title = "currentBlockX:" + currentBlockX + " currentBlockY:" + currentBlockY;
        }


       public int getCurrentChunkNrInArray() 
       {
           //Vector2 tempPos = new Vector2(1050, 1050);
           //int playerRegionX = Globals.getRegionValue(Globals.playerPos.X);
           //int playerRegionY = Globals.getRegionValue(Globals.playerPos.Y);

           int playerChunkX = Globals.getChunkValue(Globals.playerPos.X);
           int playerChunkY = Globals.getChunkValue(Globals.playerPos.Y);

           int entityChunkX = Globals.getChunkValue(position.X);
           int entityChunkY = Globals.getChunkValue(position.Y);

           int chunkOffsetX = entityChunkX - playerChunkX;
           int chunkOffsetY = entityChunkY - playerChunkY;

           //window.Title = "chunkOffsetX:" + chunkOffsetX + " chunkOffsetY:" + chunkOffsetY;

           int internChunkPosInRegionX;
           int internChunkPosInRegionY;

           if (playerChunkX >= 0)
           {
               internChunkPosInRegionX = playerChunkX % 4;
           }
           else
           {
               internChunkPosInRegionX = 3 + (playerChunkX + 1) % 4;
           }

           if (playerChunkY >= 0)
           {
               internChunkPosInRegionY = playerChunkY % 4;
           }
           else
           {
               internChunkPosInRegionY = 3 + (playerChunkY + 1) % 4;
           }

           int chunkNrInArrayX = 4 + internChunkPosInRegionX + chunkOffsetX;
           int chunkNrInArrayY = 4 + internChunkPosInRegionY + chunkOffsetY;

           if (chunkNrInArrayX > 0 && chunkNrInArrayX < 11 && chunkNrInArrayY > 0 && chunkNrInArrayY < 11)
           {
               return chunkNrInArrayX + chunkNrInArrayY * 12;
           }
           else
           {
               return -1;
           }

           

           //if (chunkManager.tempGetChunkArray()[chunkNrInArrayX + chunkNrInArrayY * 12] != null)
           //    for (int i = 0; i < 256; i++)
           //    {
           //         chunkManager.tempGetChunkArray()[chunkNrInArrayX + chunkNrInArrayY * 12].blocks[i] = 1;
           //    }

           //window.Title = "chunkNrInArrayX:" + chunkNrInArrayX + " chunkNrInArrayY:" + chunkNrInArrayY;

           
       }

    }
}

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


       public void getCurrentChunkNrInArray(ref ChunkManager chunkManager, GameWindow window) 
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

           if (chunkManager.tempGetChunkArray()[chunkNrInArrayX + chunkNrInArrayY * 12] != null)
               for (int i = 0; i < 256; i++)
               {
                    chunkManager.tempGetChunkArray()[chunkNrInArrayX + chunkNrInArrayY * 12].blocks[i] = 1;
               }

           window.Title = "chunkNrInArrayX:" + chunkNrInArrayX + " chunkNrInArrayY:" + chunkNrInArrayY;

           
       }

    }
}

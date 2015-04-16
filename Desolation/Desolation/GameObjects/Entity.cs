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
    abstract public class Entity : GameObject
    {

        protected float speed;
        protected float realSpeed;
        protected Vector2 oldPosition;
        protected int Arange;
        protected float rotation;
        protected Item[] equipment; //0 = main hand, 1 = off hand
        public int health;
        #region Constructor
        public Entity(Vector2 pos)
            : base(pos)
        {
            //speed = 0;
            Arange = 5;
            equipment = new Item[10];
        }
        #endregion

        #region Methods
        abstract public override void Update(GameTime gameTime);

        abstract public void syncUpdate(GameTime gameTime);
        

        abstract public override void Draw(SpriteBatch spriteBatch);
        
        public abstract void checkAttack();

        public virtual void moveDirection(Direction direction)
        {
            //if (Globals.checkRange(position, Globals.playerPos, 5512))
            //{
                oldPosition = position;
                //rörelse
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
            //}
        }
        public virtual void getAngle(Vector2 target)
        {
            MouseState mouse = Mouse.GetState();
            Vector2 deltaVect = Vector2.Zero;

            deltaVect.X = target.X - position.X;
            deltaVect.Y = target.Y - position.Y;

            
            //rotation = (float)(Math.Atan2(deltaVect.Y, deltaVect.X)* 180 / Math.PI);
            //rotation = (float)(Math.Atan2(deltaVect.Y, deltaVect.X)); //* 180 / Math.PI);
            rotation = (float)(Math.Atan2(deltaVect.Y, deltaVect.X * Math.PI));
            //if (target.Length() <= MathHelper.ToDegrees(30))
            //{
                
            //}
            //if (target.Length() <= MathHelper.ToDegrees(60))
            //{

            //}
            //if (target.Length() <= MathHelper.ToDegrees(120))
            //{

            //}
            //if (target.Length() <= MathHelper.ToDegrees(150))
            //{

            //}
            //if (target.Length() <= MathHelper.ToDegrees(210))
            //{

            //}
            //if (target.Length() <= MathHelper.ToDegrees(240))
            //{

            //}
            //if (target.Length() <= MathHelper.ToDegrees(300))
            //{

            //}
            //if (target.Length() >= MathHelper.ToDegrees(330))
            //{

            //}

        }

        public void damageEntity(int damage)
        {
            health -= damage;
        }

        public void checkCollision()
        {

            //time for new chunkLoad
            //Console.WriteLine(now);


            //kolla om entity är laddad
            int currentBlockX;
            int currentBlockY;
            if (position.X >= 0)
            {
                currentBlockX = ((int)position.X / 16) % 16;
            }
            else
            {
                currentBlockX = 15 + ((int)position.X) / 16 % 16;
            }

            if (position.Y >= 0)
            {
                currentBlockY = ((int)position.Y / 16) % 16;
            }
            else
            {
                currentBlockY = 15 + ((int)position.Y) / 16 % 16;
            }
            int chunkIndex = getCurrentChunkNrInArray(Globals.oldPlayerPos);
            if (chunkIndex != -1)
            {
                Chunk currentChunk = ChunkManager.chunkArray[chunkIndex];
                //Chunk currentChunk0 = ChunkManager.chunkArray[chunkIndex - 13];
                //Chunk currentChunk1 = ChunkManager.chunkArray[chunkIndex - 12];
                //Chunk currentChunk2 = ChunkManager.chunkArray[chunkIndex - 11];
                //Chunk currentChunk3 = ChunkManager.chunkArray[chunkIndex - 1];
                //Chunk currentChunk4 = ChunkManager.chunkArray[chunkIndex];
                //Chunk currentChunk5 = ChunkManager.chunkArray[chunkIndex + 1];
                //Chunk currentChunk6 = ChunkManager.chunkArray[chunkIndex + 11];
                //Chunk currentChunk7 = ChunkManager.chunkArray[chunkIndex + 12];
                //Chunk currentChunk8 = ChunkManager.chunkArray[chunkIndex + 13];

                //if (currentBlockX < 1)
                if (currentChunk != null)
                {
                    currentChunk.blocks[currentBlockX + currentBlockY * 16] = (byte)1;


                    if (currentChunk.objects[currentBlockX + currentBlockY * 16] == 1)
                    {
                        position = oldPosition; //undo'ar rörelse
                    }

                }

            }
            else
            {
                position = oldPosition; //undo'ar rörelse
            }
            //Chunk currentChunk = ChunkManager.chunkArray[chunkIndex];
            //Game1.gameWindow.Title = "currentBlockX:" + currentBlockX + " currentBlockY:" + currentBlockY;
        }

        public int getCurrentChunkNrInArray(Vector2 playerPos)
        {
            //Vector2 tempPos = new Vector2(1050, 1050);
            //int playerRegionX = Globals.getRegionValue(Globals.playerPos.X);
            //int playerRegionY = Globals.getRegionValue(Globals.playerPos.Y);
            int playerChunkX;
            int playerChunkY;

            if (this is Player)
            {
                playerChunkX = Globals.getChunkValue(position.X);
                playerChunkY = Globals.getChunkValue(position.Y);
            }
            else
            {
                playerChunkX = Globals.getChunkValue(playerPos.X);
                playerChunkY = Globals.getChunkValue(playerPos.Y);
            }


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


        public virtual void getTagList(ref List<Tag> individualList)
        {
            byte[] entityXpos = BitConverter.GetBytes((int)position.X);
            Tag xPos = new Tag(TagID.Int, "EntityXPos", entityXpos, TagID.Int);
            individualList.Add(xPos);
            byte[] entityYpos = BitConverter.GetBytes((int)position.Y);
            Tag yPos = new Tag(TagID.Int, "EntityYPos", entityYpos, TagID.Int);
            individualList.Add(yPos);
        }

        #endregion

    }
}

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

        public float speed;
        protected float realSpeed;
        public Vector2 oldPosition;
        protected int Arange;
    public float rotation;
        protected Item[] equipment; //0 = main hand, 1 = off hand
        public int health;
        protected Direction currentDirection;
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
            //if (position.X > Globals.oldPlayerPos.X - 960 && position.X < Globals.oldPlayerPos.X + 960 && position.Y > Globals.oldPlayerPos.Y - 540 && position.Y < Globals.oldPlayerPos.Y + 540)
            //{
            if (position.X > Globals.oldPlayerPos.X - 1000 && position.X < Globals.oldPlayerPos.X + 1000 && position.Y > Globals.oldPlayerPos.Y - 1000 && position.Y < Globals.oldPlayerPos.Y + 1000)
            {
                currentDirection = direction;
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
            }
        }
        public virtual float getAngle(Vector2 target)
        {
            //float returnAngle;
            //MouseState mouse = Mouse.GetState();
            Vector2 deltaVect = Vector2.Zero;

            deltaVect.X = target.X - position.X;
            deltaVect.Y = target.Y - position.Y;

            
            //returnAngle = (float)(Math.Atan2(deltaVect.Y, deltaVect.X * Math.PI));
            double compassBearing = Math.Atan2(deltaVect.Y, deltaVect.X);
            if (compassBearing < 0)
            {
                compassBearing = Math.PI * 2 + compassBearing;
            }
            return (float)compassBearing;
        }

        public void damageEntity(int damage)
        {
            health -= damage;
        }

        public void checkCollision()
        {
            //Få blocket entitien står i
            byte[] surroundingBlocks = new byte[9];
            byte[] surroundingObjects = new byte[9];
            Point[] surroundingBlocksPos = new Point[9];
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    surroundingBlocksPos[((i + 1) * 3 + j + 1)] = new Point(Globals.getBlockValue(position.X + j * 16), Globals.getBlockValue(position.Y + i * 16));
                    int chunkIndex = getCurrentChunkNrInArray(new Vector2(position.X + j * 16, position.Y + i * 16), Globals.oldPlayerPos);
                    if (chunkIndex != -1)
                    {
                        Chunk currentChunk = ChunkManager.chunkArray[chunkIndex];
                        if (currentChunk != null)
                        {
                            surroundingBlocks[((i + 1) * 3 + j + 1)] = currentChunk.blocks[surroundingBlocksPos[((i + 1) * 3 + j + 1)].X + surroundingBlocksPos[((i + 1) * 3 + j + 1)].Y * 16];
                            surroundingObjects[((i + 1) * 3 + j + 1)] = currentChunk.objects[surroundingBlocksPos[((i + 1) * 3 + j + 1)].X + surroundingBlocksPos[((i + 1) * 3 + j + 1)].Y * 16];

                            if ((this is Player))
                            {
                                if (surroundingObjects[4] == 1)
                                {
                                    moveDirection(Globals.getOppositeDirection(currentDirection));
                                    currentDirection = Direction.None;
                                }
                            }
                            else
                            {
                                if (surroundingObjects[4] == 1)
                                {
                                    position = oldPosition;
                                    moveDirection(Globals.getOppositeDirection(currentDirection));
                                    
                                }
                                else
                                {
                                        oldPosition = position;
                                }
                            }
                            
                                
                            

                            //switch (currentDirection)
                            //{
                            //    case Direction.North:
                            //        if (surroundingObjects[1] == 1) //hårdkodat måste fixas
                            //        {
                            //            //position = oldPosition;
                            //            currentDirection = Direction.None;
                            //            moveDirection(Direction.South);
                            //        }
                            //        break;
                            //    case Direction.NorthEast:
                            //        if (surroundingObjects[2] == 1 || (surroundingObjects[1] == 1 && surroundingObjects[5] == 1))
                            //        {
                            //            //position = oldPosition;
                            //            currentDirection = Direction.None;
                            //            moveDirection(Direction.SouthWest);
                            //        }
                            //        break;
                            //    case Direction.East:
                            //        if (surroundingObjects[3] == 1) //hårdkodat måste fixas
                            //        {
                            //            //position = oldPosition;
                            //            currentDirection = Direction.None;
                            //            moveDirection(Direction.West);
                            //        }
                            //        break;
                            //    case Direction.SouthEast:
                            //        if (surroundingObjects[8] == 1 || (surroundingObjects[7] == 1 && surroundingObjects[5] == 1))
                            //        {
                            //            //position = oldPosition;
                            //            currentDirection = Direction.None;
                            //            moveDirection(Direction.NorthWest);
                            //        }
                            //        break;
                            //    case Direction.South:
                            //        if (surroundingObjects[7] == 1) //hårdkodat måste fixas
                            //        {
                            //            //position = oldPosition;
                            //            currentDirection = Direction.None;
                            //            moveDirection(Direction.North);
                            //        }
                            //        break;
                            //    case Direction.SouthWest:
                            //        if (surroundingObjects[6] == 1 || (surroundingObjects[7] == 1 && surroundingObjects[3] == 1))
                            //        {
                            //            //position = oldPosition;
                            //            currentDirection = Direction.None;
                            //            moveDirection(Direction.NorthEast);
                            //        }
                            //        break;
                            //    case Direction.West:
                            //        if (surroundingObjects[3] == 1) //hårdkodat måste fixas
                            //        {
                            //            //position = oldPosition;
                            //            currentDirection = Direction.None;
                            //            moveDirection(Direction.East);
                            //        }
                            //        break;
                            //    case Direction.NorthWest:
                            //        if (surroundingObjects[0] == 1 || (surroundingObjects[1] == 1 && surroundingObjects[3] == 1))
                            //        {
                            //            //position = oldPosition;
                            //            currentDirection = Direction.None;
                            //            moveDirection(Direction.SouthEast);
                            //        }
                            //        break;
                            //    case Direction.None:
                            //        break;
                            //    default:
                            //        break;
                            //}
                            //if (currentChunk.objects[currentBlockX + currentBlockY * 16] == 1)
                            //{
                            //    position = oldPosition; //undo'ar rörelse
                            //}

                        }
                        else
                        {
                            //position = oldPosition; //undo'ar rörelse
                        }

                    }
                    else
                    {
                       // position = oldPosition; //undo'ar rörelse
                    }
                }
            }
        }
            //int currentBlockX = Globals.getBlockValue(position.X);
            //int currentBlockY = Globals.getBlockValue(position.Y);

            //int chunkIndex = getCurrentChunkNrInArray(position, Globals.oldPlayerPos);
        //    if (chunkIndex != -1)
        //    {
        //        Chunk currentChunk = ChunkManager.chunkArray[chunkIndex];
        //        //Chunk currentChunk0 = ChunkManager.chunkArray[chunkIndex - 13];
        //        //Chunk currentChunk1 = ChunkManager.chunkArray[chunkIndex - 12];
        //        //Chunk currentChunk2 = ChunkManager.chunkArray[chunkIndex - 11];
        //        //Chunk currentChunk3 = ChunkManager.chunkArray[chunkIndex - 1];
        //        //Chunk currentChunk4 = ChunkManager.chunkArray[chunkIndex];
        //        //Chunk currentChunk5 = ChunkManager.chunkArray[chunkIndex + 1];
        //        //Chunk currentChunk6 = ChunkManager.chunkArray[chunkIndex + 11];
        //        //Chunk currentChunk7 = ChunkManager.chunkArray[chunkIndex + 12];
        //        //Chunk currentChunk8 = ChunkManager.chunkArray[chunkIndex + 13];

        //        //if (currentBlockX < 1)
        //        if (currentChunk != null)
        //        {
        //            currentChunk.blocks[currentBlockX + currentBlockY * 16] = (byte)1;


        //            if (currentChunk.objects[currentBlockX + currentBlockY * 16] == 1)
        //            {
        //                position = oldPosition; //undo'ar rörelse
        //            }

        //        }
        //        else
        //        {
        //            position = oldPosition; //undo'ar rörelse
        //        }

        //    }
        //    else
        //    {
        //        position = oldPosition; //undo'ar rörelse
        //    }
        //    //Chunk currentChunk = ChunkManager.chunkArray[chunkIndex];
        //    //Game1.gameWindow.Title = "currentBlockX:" + currentBlockX + " currentBlockY:" + currentBlockY;
        //}

        public int getCurrentChunkNrInArray(Vector2 entityPos, Vector2 playerPos)
        {
            //Vector2 tempPos = new Vector2(1050, 1050);
            //int playerRegionX = Globals.getRegionValue(Globals.playerPos.X);
            //int playerRegionY = Globals.getRegionValue(Globals.playerPos.Y);
            int playerChunkX;
            int playerChunkY;
            int chunkOffsetX;
            int chunkOffsetY;
            
                playerChunkX = Globals.getChunkValue(playerPos.X);
                playerChunkY = Globals.getChunkValue(playerPos.Y);

                int entityChunkX = Globals.getChunkValue(entityPos.X);
                int entityChunkY = Globals.getChunkValue(entityPos.Y);

                chunkOffsetX = entityChunkX - playerChunkX;
                chunkOffsetY = entityChunkY - playerChunkY;
            


            

            

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

            if (chunkNrInArrayX >= 0 && chunkNrInArrayX <= 11 && chunkNrInArrayY >= 0 && chunkNrInArrayY <= 11)
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

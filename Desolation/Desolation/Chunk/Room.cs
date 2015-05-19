using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Desolation
{
    public class Room
    {
        public Rectangle area;
        public uint structureID { set; get; }

        public Room(int innerPositionX, int innerPositionY, int width, int height, uint structureID)
        {
            area = new Rectangle(innerPositionX, innerPositionY, width, height);
            this.structureID = structureID;

        }


        public void draw(SpriteBatch spriteBatch)
        {
            //if (!(Globals.currentStructureID == structureID))
            //{
            //    for (int i = 0; i <= area.Width; i += 16)
            //    {
            //        for (int j = 0; j <= area.Height; j += 16)
            //        {
            //            if (i != 0 && j != area.Height && i != area.Width)
            //            {
                            
                        
            //            spriteBatch.Draw(TextureManager.roofsheet, new Vector2(area.X+j, area.Y+i-16 ), new Rectangle(0, 16, 16, 16), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1.0f);

            //            }

            //            if (area.Width == j && i==0)
            //            {
            //                spriteBatch.Draw(TextureManager.roofsheet, new Vector2(area.X, area.Y-16), new Rectangle(0, j, j, 16), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1.0f);
            //                spriteBatch.Draw(TextureManager.roofsheet, new Vector2(area.X, area.Y + (j-32)), new Rectangle(128, j, j, 16), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1.0f);
            //                // spriteBatch.Draw(TextureManager.roofsheet, new Vector2(area.X + i, area.Y + i - 16), new Rectangle(16, 16, Globals.blockSize * 3, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1.0f);
            //            }
                        
            //        }
            //    }
            //}



            if (!(Globals.currentStructureID == structureID))
            {
                for (int i = 0; i < area.Height; i += 16)
                {
                    for (int j = 0; j < area.Width; j += 16)
                    {
                        if (area.X + j > Globals.playerPos.X - (Globals.screenX / 2) - 16 && area.X + j < Globals.playerPos.X + (Globals.screenX / 2) + 16 && area.Y + i > Globals.playerPos.Y - (Globals.screenY / 2) - 16 && area.Y + i < Globals.playerPos.Y + (Globals.screenY / 2) + 16)
                        {
                            if (i == 0)
                            {

                                if (j == 0)
                                {
                                    spriteBatch.Draw(TextureManager.roofsheet, new Vector2(area.X + j, area.Y + i - 16), new Rectangle(16, 16, Globals.blockSize * 3, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.3 + 0.0001f * (area.Y + i - Globals.cameraPos.Y) + 0.00001f * (area.X + j - Globals.cameraPos.X)));

                                }
                                else if (j == area.Width - 16)
                                {
                                    spriteBatch.Draw(TextureManager.roofsheet, new Vector2(area.X + j - 16 * 2, area.Y + i - 16), new Rectangle(64, 16, Globals.blockSize * 3, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.3 + 0.0001f * (area.Y + i - Globals.cameraPos.Y) + 0.00001f * (area.X + j - Globals.cameraPos.X)));

                                }
                                else if (j > 16 * 2 && j < area.Width - 16 * 3)
                                {
                                    spriteBatch.Draw(TextureManager.roofsheet, new Vector2(area.X + j, area.Y + i - 16), new Rectangle(0, 16, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.3 + 0.0001f * (area.Y + i - Globals.cameraPos.Y) + 0.00001f * (area.X + j - Globals.cameraPos.X)));

                                }
                            }
                            else if (i == area.Height - 16)
                            {
                                if (j == 0)
                                {
                                    spriteBatch.Draw(TextureManager.roofsheet, new Vector2(area.X + j, area.Y + i - 16), new Rectangle(16, 0, Globals.blockSize * 3, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.3 + 0.0001f * (area.Y + i - Globals.cameraPos.Y) + 0.00001f * (area.X + j - Globals.cameraPos.X)));

                                }
                                else if (j == area.Width - 16)
                                {
                                    spriteBatch.Draw(TextureManager.roofsheet, new Vector2(area.X + j - 16 * 2, area.Y + i - 16), new Rectangle(64, 0, Globals.blockSize * 3, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.3 + 0.0001f * (area.Y + i - Globals.cameraPos.Y) + 0.00001f * (area.X + j - Globals.cameraPos.X)));

                                }
                                else if (j > 16 * 2 && j < area.Width - 16 * 3)
                                {
                                    spriteBatch.Draw(TextureManager.roofsheet, new Vector2(area.X + j, area.Y + i - 16), new Rectangle(0, 32, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.3 + 0.0001f * (area.Y + i - Globals.cameraPos.Y) + 0.00001f * (area.X + j - Globals.cameraPos.X)));

                                }
                            }
                            else
                            {
                                if (j < 16 * 3)
                                {
                                    spriteBatch.Draw(TextureManager.roofsheet, new Vector2(area.X + j, area.Y + i - 16), new Rectangle(0, 48, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.3 + 0.0001f * (area.Y + i - Globals.cameraPos.Y) + 0.00001f * (area.X + j - Globals.cameraPos.X)));
                            
                                }
                                else if (j > area.Width - 16 * 4)
                                {
                                    spriteBatch.Draw(TextureManager.roofsheet, new Vector2(area.X + j, area.Y + i - 16), new Rectangle(0, 64, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.3 + 0.0001f * (area.Y + i - Globals.cameraPos.Y) + 0.00001f * (area.X + j - Globals.cameraPos.X)));
                            
                                }
                                else
                                {
                                    spriteBatch.Draw(TextureManager.roofsheet, new Vector2(area.X + j, area.Y + i - 16), new Rectangle(0, 0, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.3 + 0.0001f * (area.Y + i - Globals.cameraPos.Y) + 0.00001f * (area.X + j - Globals.cameraPos.X)));
                            
                                }
                            }
                        }
                    }
                }

                //spriteBatch.Draw(TextureManager.playerSheet, new Vector2(area.X, area.Y), area , Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1.0f);
            }
        }

        public void generateRoom()
        {
            for (int i = 0; i < area.Height; i += 16)
            {
                for (int j = 0; j < area.Width; j += 16)
                {

                    float xpos;
                    float ypos;
                    if (area.X + j > 0)
                    {
                        xpos = area.X + j;
                    }
                    else
                    {
                        xpos = area.X + j + 16;
                    }
                    if (area.Y + i > 0)
                    {
                        ypos = area.Y + i;
                    }
                    else
                    {
                        ypos = area.Y + i + 16;
                    }
                    Vector2 currentBlockPos = new Vector2(xpos, ypos);
                    int chunkNr = Game1.player.getCurrentChunkNrInArray(currentBlockPos, Globals.playerPos);
                    if (chunkNr != -1)
                    {
                        Chunk tempChunk = ChunkManager.chunkArray[chunkNr];
                        if (tempChunk != null)
                        {
                            int xBlockPos = Globals.getBlockValue(currentBlockPos.X);
                            int yBlockPos = Globals.getBlockValue(currentBlockPos.Y);
                            //if (currentBlockPos.X > 0)
                            //{
                            //    xBlockPos = Globals.getBlockValue(currentBlockPos.X);
                            //}
                            //else
                            //{
                            //    xBlockPos = Globals.getBlockValue(currentBlockPos.X);
                            //}
                            //if (currentBlockPos.Y > 0)
                            //{
                            //    yBlockPos = Globals.getBlockValue(currentBlockPos.Y);
                            //}
                            //else
                            //{
                            //    yBlockPos = Globals.getBlockValue(currentBlockPos.Y);
                            //}

                            

                            if ((i == 0 || j == 0 || i == area.Height - 16 || j == area.Width - 16) && !(i == area.Height - 16 && j == (area.Width / 2 - 16)))
                            {
                                if (tempChunk.blocks[(yBlockPos * 16 + xBlockPos)] != (int)BlockID.WoodPlanks)
                                {
                                    tempChunk.objects[(yBlockPos * 16 + xBlockPos)] = (int)ObjectID.Planks;
                                }
                            }
                            else
                            {
                                tempChunk.objects[(yBlockPos * 16 + xBlockPos)] = (int)ObjectID.Air;
                            }

                            tempChunk.blocks[(yBlockPos * 16 + xBlockPos)] = (int)BlockID.WoodPlanks;
                        }
                    }
                }
            }
        }

        public virtual void getTagList(ref List<Tag> individualList)
        {
            Tag compound = new Tag(TagID.Compound, "Room", null, TagID.Compound);
            individualList.Add(compound);


            int theInt = (int)structureID;
            byte[] structureIDArray = BitConverter.GetBytes(theInt);
            Tag ID = new Tag(TagID.Int, "ID", structureIDArray, TagID.Int);
            individualList.Add(ID);

            byte[] structureXpos = BitConverter.GetBytes((int)area.X);
            Tag xPos = new Tag(TagID.Int, "StructureXPos", structureXpos, TagID.Int);
            individualList.Add(xPos);

            byte[] structureYpos = BitConverter.GetBytes((int)area.Y);
            Tag yPos = new Tag(TagID.Int, "StructureYPos", structureYpos, TagID.Int);
            individualList.Add(yPos);

            byte[] structureWidth = BitConverter.GetBytes((int)area.Width);
            Tag width = new Tag(TagID.Int, "StructureWidth", structureWidth, TagID.Int);
            individualList.Add(width);

            byte[] structureHeight = BitConverter.GetBytes((int)area.Height);
            Tag height = new Tag(TagID.Int, "StructureHeight", structureHeight, TagID.Int);
            individualList.Add(height);

            Tag end = new Tag(TagID.End, null, null, TagID.End);
            individualList.Add(end);



        }

    }
}

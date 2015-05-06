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
            if (!(Globals.currentStructureID == structureID))
            {
                for (int i = 0; i < area.Height; i+=16)
                {
                    for (int j = 0; j < area.Width; j+= 16)
                    {
                        if (area.X + j > Globals.playerPos.X - (Globals.screenX / 2) - 16 && area.X + j < Globals.playerPos.X + (Globals.screenX / 2) + 16 && area.Y + i > Globals.playerPos.Y - (Globals.screenY / 2) - 16 && area.Y + i < Globals.playerPos.Y + (Globals.screenY / 2) + 16)
                        {
                            if (i == 0)
                            {

                                if (j == 0)
                                {
                                    spriteBatch.Draw(TextureManager.roofsheet, new Vector2(area.X + j, area.Y + i - 16), new Rectangle(16, 16, Globals.blockSize * 3, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1.0f);

                                }
                                else if (j == area.Width - 16)
                                {
                                    spriteBatch.Draw(TextureManager.roofsheet, new Vector2(area.X + j - 16 * 2, area.Y + i - 16), new Rectangle(64, 16, Globals.blockSize * 3, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1.0f);

                                }
                                else if (j > 16 * 2 && j < area.Width - 16 * 3)
                                {
                                    spriteBatch.Draw(TextureManager.roofsheet, new Vector2(area.X + j, area.Y + i - 16), new Rectangle(0, 16, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1.0f);

                                }
                            }
                            else if (i == area.Height - 16)
                            {
                                if (j == 0)
                                {
                                    spriteBatch.Draw(TextureManager.roofsheet, new Vector2(area.X + j, area.Y + i - 16), new Rectangle(16, 0, Globals.blockSize * 3, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1.0f);

                                }
                                else if (j == area.Width - 16)
                                {
                                    spriteBatch.Draw(TextureManager.roofsheet, new Vector2(area.X + j - 16 * 2, area.Y + i - 16), new Rectangle(64, 0, Globals.blockSize * 3, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1.0f);

                                }
                                else if (j > 16 * 2 && j < area.Width - 16 * 3)
                                {
                                    spriteBatch.Draw(TextureManager.roofsheet, new Vector2(area.X + j, area.Y + i - 16), new Rectangle(0, 32, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1.0f);

                                }
                            }
                            else
                            {
                                spriteBatch.Draw(TextureManager.roofsheet, new Vector2(area.X + j, area.Y + i - 16), new Rectangle(0, 0, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1.0f);
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
                    Vector2 currentBlockPos = new Vector2(area.X + j, area.Y + i);
                    int chunkNr = Game1.player.getCurrentChunkNrInArray(currentBlockPos, Globals.playerPos);
                    if (chunkNr != -1)
                    {
                        Chunk tempChunk = ChunkManager.chunkArray[chunkNr];
                        if (tempChunk != null)
                        {
                            int xBlockPos = Globals.getBlockValue(currentBlockPos.X);
                            int yBlockPos = Globals.getBlockValue(currentBlockPos.Y);
                            tempChunk.blocks[(yBlockPos * 16 + xBlockPos)] = 3;

                            if ((i == 0 || j == 0 || i == area.Height - 16 || j == area.Width - 16) && !(i == area.Height - 16 && j == (area.Width/2 - 16)))
                            {
                                tempChunk.objects[(yBlockPos * 16 + xBlockPos)] = 1;
                            }
                            else 
                            {
                            tempChunk.objects[(yBlockPos * 16 + xBlockPos)] = 0;
                            }
                        }
                    }
                }
            }
        }

    }
}

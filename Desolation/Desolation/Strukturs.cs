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
    public class Strukturs
    {
        public Strukturs()
        {

        }
        public void Update(GameTime gameTime)
        {

        }
        public void Hustest()
        {
            int startpos = Globals.rand.Next(0, 255);

            int chunkNr = Game1.player.getCurrentChunkNrInArray(Globals.playerPos, Globals.playerPos);
            Chunk tempChunk = ChunkManager.chunkArray[chunkNr];
            if (tempChunk != null)
            {

                for (int i = 0; i < Globals.rand.Next(100, 255); i++)
                {
                    if (chunkNr != -1)
                    {
                        //kålla för varje blåck om det är i ny chunk 
                        tempChunk = ChunkManager.chunkArray[chunkNr];

                        Vector2 vec = new Vector2((i + startpos) / 16, (i + startpos) % 16);

                        chunkNr = Game1.player.getCurrentChunkNrInArray(vec, Globals.playerPos);

                        tempChunk.objects[i] = 1;
                    }
                }
            }
        }

        public void Hus()
        {

            int startpos = Globals.rand.Next(0, 255);
            int chunkNr = Game1.player.getCurrentChunkNrInArray(Globals.playerPos, Globals.playerPos);
            Chunk tempChunk = ChunkManager.chunkArray[chunkNr];
            if (tempChunk != null)
            {

                #region horesentel
                tempChunk.objects[startpos] = 1;
                for (int i = 0; i < Globals.rand.Next(15, 18); i++)
                {
                    bool fis = true;
                    int test5 = startpos + i;
                    if (test5 < (tempChunk.objects.Length) && test5 >= 0 && fis)
                    {
                        tempChunk = ChunkManager.chunkArray[chunkNr];
                        tempChunk.objects[test5] = 1;
                    }


                }
                #endregion
                for (int i = 0; i < Globals.rand.Next(5, 15); i++)
                {
                    bool fis = true;
                    int test5 = startpos + ((i * 16) + 15);
                    if (test5 < (tempChunk.objects.Length) && test5 >= 0 && fis)
                    {
                        tempChunk = ChunkManager.chunkArray[chunkNr];
                        tempChunk.objects[test5] = 1;
                    }

                }

            }


        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }

    }
}

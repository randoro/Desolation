using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Desolation
{
    public class Chunk
    {
        public int XPos { set; get; }
        public int YPos { set; get; }
        public long lastUpdate { set; get; }
        public sbyte terrainPopulated { set; get; }
        public long inhabitedTime { set; get; }
        public byte[] biomes { set; get; }
        public byte[] blocks { set; get; }
        public byte[] objects { set; get; }
        public List<Tag>[] entities { set; get; }
        public List<Tag>[] tileEntities { set; get; }

        public byte innerIndex; //0-16 which chunk in regionFile

        public Chunk()
        {

        }

        public void update(GameTime gameTime)
        {

        }

        public void draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 256; i++)
            {

                if (blocks[i] == 0)
                {
                    spriteBatch.Draw(Globals.tempsheet, new Vector2(XPos * 256 + (i % 16) * 16, YPos * 256 + (i / 16) * 16), new Rectangle(0, 0, 16, 16), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                }
                else
                {
                    spriteBatch.Draw(Globals.tempsheet, new Vector2(XPos * 256 + (i % 16) * 16, YPos * 256 + (i / 16) * 16), new Rectangle(16, 0, 16, 16), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                }

                if (objects[i] == 0)
                {
                    
                }
                else
                {
                    spriteBatch.Draw(Globals.tempsheet, new Vector2(XPos * 256 + (i % 16) * 16, YPos * 256 + (i / 16) * 16), new Rectangle(16, 0, 16, 16), Color.Red, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                }
            }
        }

        
    }
}

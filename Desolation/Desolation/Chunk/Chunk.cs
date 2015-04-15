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
        public int XPos { set; get; } //saved in file
        public int YPos { set; get; } //saved in file
        public long lastUpdate { set; get; } //saved in file
        public sbyte terrainPopulated { set; get; } //saved in file
        public long inhabitedTime { set; get; } //saved in file
        public byte[] biomes { set; get; } //saved in file
        public byte[] blocks { set; get; } //saved in file
        public byte[] objects { set; get; } //saved in file
        public List<List<Tag>> entities { set; get; } //saved in file
        public List<List<Tag>> tileEntities { set; get; } //saved in file

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
                Vector2 pos = new Vector2(XPos * 256 + (i % 16) * 16, YPos * 256 + (i / 16) * 16);
                RenderObject.draw(spriteBatch, ref pos, RenderType.blocks, ref blocks[i]);

                RenderObject.draw(spriteBatch, ref pos, RenderType.objects, ref objects[i]);
                //if (blocks[i] == 0)
                //{
                //    spriteBatch.Draw(TextureManager.blocksheet, pos, new Rectangle(0, 0, 16, 16), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1);

                //}
                //else
                //{
                //    spriteBatch.Draw(TextureManager.blocksheet, pos, new Rectangle(16, 0, 16, 16), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1);

                //}

                //if (objects[i] == 0)
                //{

                //}
                //else
                //{
                //    spriteBatch.Draw(TextureManager.blocksheet, pos, new Rectangle(16, 0, 16, 16), Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1);

                //}
            }

        }
    }
}

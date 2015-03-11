﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Desolation
{
    class Chunk
    {
        public int XPos { set; get; }
        public int YPos { set; get; }
        public long lastUpdate { set; get; }
        public byte terrainPopulated { set; get; }
        public long inhabitedTime { set; get; }
        public byte[] biomes { set; get; }
        public byte[] blocks { set; get; }
        public byte[] objects { set; get; }
        public List<Tag>[] entities { set; get; }
        public List<Tag>[] tileEntities { set; get; }

        public Chunk()
        {

        }

        public void update(GameTime gameTime)
        {

        }

        public void draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(sheet, gPos, gRect, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
        }

        
    }
}

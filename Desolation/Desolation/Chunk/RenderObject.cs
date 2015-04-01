﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Desolation
{
    public static class RenderObject
    {

        public static void draw(SpriteBatch spriteBatch, Vector2 position, RenderType renderType, byte id)
        {
            switch (renderType)
            {
                case RenderType.blocks:
                    drawBlock(spriteBatch, position, id);
                    break;
                case RenderType.objects:
                    drawObject(spriteBatch, position, id);
                    break;
                default:
                    break;
            }
        }


        private static void drawBlock(SpriteBatch spriteBatch, Vector2 position, byte id) 
        {
            switch (id)
            {
                case 0:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(0, 0, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
                    break;
                case 1:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(16, 0, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
                    break;
                default:
                    break;
            }
            

        }

        private static void drawObject(SpriteBatch spriteBatch, Vector2 position, byte id)
        {
            //spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(0, 0, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Desolation
{
    public static class RenderObject
    {

        public static void draw(SpriteBatch spriteBatch, ref Vector2 position, RenderType renderType, ref byte id, ref byte color)
        {
            switch (renderType)
            {
                case RenderType.blocks:
                    drawBlock(spriteBatch, ref position, ref id, ref color);
                    break;
                case RenderType.objects:
                    drawObject(spriteBatch, ref position, ref id);
                    break;
                default:
                    break;
            }
        }


        private static void drawBlock(SpriteBatch spriteBatch, ref Vector2 position, ref byte id, ref byte color) 
        {
            switch (id)
            {
                case 0:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(48, 0, Globals.blockSize, Globals.blockSize), Generator.GetColor(Color.White, Color.Black, color), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.7f);
                    break;
                case 1:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(16, 0, Globals.blockSize, Globals.blockSize), Generator.GetColor(Color.White, Color.Black, color), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.7f);
                    //myrornas krig new Color((float)Globals.rand.NextDouble() * 4, (float)Globals.rand.NextDouble() * 4, (float)Globals.rand.NextDouble() * 4)
                    break;
                case 2:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(64, 0, Globals.blockSize, Globals.blockSize), Generator.GetColor(Color.White, Color.Black, color), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.7f);
                    //myrornas krig new Color((float)Globals.rand.NextDouble() * 4, (float)Globals.rand.NextDouble() * 4, (float)Globals.rand.NextDouble() * 4)
                    break;
                case 3:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(0, 16, Globals.blockSize, Globals.blockSize), Generator.GetColor(Color.White, Color.Black, color), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.7f);
                    //myrornas krig new Color((float)Globals.rand.NextDouble() * 4, (float)Globals.rand.NextDouble() * 4, (float)Globals.rand.NextDouble() * 4)
                    break;
               
                default:
                    break;
            }
            

        }

        private static void drawObject(SpriteBatch spriteBatch, ref Vector2 position, ref byte id)
        {
            switch (id)
            {
                case 0:
                    break;
                case 1:
                    spriteBatch.Draw(TextureManager.objectsheet, position, new Rectangle(0, 0, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.75f);
                    break;
                case 3:
                    spriteBatch.Draw(TextureManager.objectsheet, position, new Rectangle(0,288,80, 112), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.95f);
                    break;
                default:
                    break;
            }
        }

    }
}

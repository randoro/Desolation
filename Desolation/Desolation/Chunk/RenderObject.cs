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
                    drawBlock(spriteBatch, ref position, (BlockID)id, ref color);
                    break;
                case RenderType.objects:
                    drawObject(spriteBatch, ref position, (ObjectID)id);
                    break;
                default:
                    break;
            }
        }


        private static void drawBlock(SpriteBatch spriteBatch, ref Vector2 position, BlockID id, ref byte color)
        {
            switch (id)
            {
                case BlockID.Grass:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(0, 0, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.LightGrass:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(16, 0, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.DarkGrass:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(32, 0, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.DryGrass:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(48, 0, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.SwampGrass:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(64, 0, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.Sand:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(0, 16, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.RoughSand:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(16, 16, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.GreySand:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(32, 16, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.WaveSand:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(48, 16, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.QuickSand:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(64, 16, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.Dirt:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(0, 32, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.DarkDirt:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(16, 32, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.LightDirt:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(32, 32, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.Mud:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(48, 32, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.Gravel:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(64, 32, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.Snow:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(0, 48, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.BlueSnow:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(16, 48, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.YellowSnow:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(32, 48, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.GreySnow:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(48, 48, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.CrystalIce:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(64, 48, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.HardIce:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(80, 48, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.Water:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(0, 64, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.SaltWater:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(16, 64, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.FreshWater:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(32, 64, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.VoidWater:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(48, 64, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.WoodPlanks:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(0, 80, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.Sandston:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(80, 16, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                case BlockID.Grasston:
                    spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(80, 0, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));

                    break;
                default:
                    break;
            }
            //switch (id)
            //{
            //    case 0:
            //        spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(48, 0, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f*(position.Y - Globals.cameraPos.Y)));
            //        break;
            //    case 1:
            //        spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(16, 0, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));
            //        //myrornas krig new Color((float)Globals.rand.NextDouble() * 4, (float)Globals.rand.NextDouble() * 4, (float)Globals.rand.NextDouble() * 4)
            //        break;
            //    case 2:
            //        spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(64, 0, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));
            //        //myrornas krig new Color((float)Globals.rand.NextDouble() * 4, (float)Globals.rand.NextDouble() * 4, (float)Globals.rand.NextDouble() * 4)
            //        break;
            //    case 3:
            //        spriteBatch.Draw(TextureManager.blocksheet, position, new Rectangle(0, 16, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.2 + 0.0001f * (position.Y - Globals.cameraPos.Y)));
            //        //myrornas krig new Color((float)Globals.rand.NextDouble() * 4, (float)Globals.rand.NextDouble() * 4, (float)Globals.rand.NextDouble() * 4)
            //        break;

            //    default:
            //        break;
            //}


        }

        private static void drawObject(SpriteBatch spriteBatch, ref Vector2 position, ObjectID oid)
        {
            switch (oid)
            {
                case ObjectID.Air:
                    //nothing to draw
                    break;
                case ObjectID.Planks:
                    spriteBatch.Draw(TextureManager.objectsheet, position, new Rectangle(0, 0, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.3 + 0.0001f * (position.Y - Globals.cameraPos.Y) + 0.00001f * (position.X - Globals.cameraPos.X)));

                    break;
                case ObjectID.Skull:
                    spriteBatch.Draw(TextureManager.objectsheet, position, new Rectangle(16, 0, Globals.blockSize, Globals.blockSize), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.3 + 0.0001f * (position.Y - Globals.cameraPos.Y) + 0.00001f * (position.X - Globals.cameraPos.X)));
                    break;
                case ObjectID.Bricks:
                    break;
                case ObjectID.Windows:
                    break;
                case ObjectID.Marmor:
                    break;
                case ObjectID.Oak:
                    spriteBatch.Draw(TextureManager.objectsheet, new Vector2(position.X - 64, position.Y - 112), new Rectangle(0, 272, 128, 128), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.3 + 0.0001f * (position.Y - Globals.cameraPos.Y) + 0.00001f * (position.X - Globals.cameraPos.X)));

                    break;
                case ObjectID.Snowpine:
                    spriteBatch.Draw(TextureManager.objectsheet, new Vector2(position.X - 54, position.Y - 130), new Rectangle(128, 256, 128, 144), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.3 + 0.0001f * (position.Y - Globals.cameraPos.Y) + 0.00001f * (position.X - Globals.cameraPos.X)));

                    break;
                case ObjectID.Palm:
                    break;
                case ObjectID.LeafLessTree:
                    spriteBatch.Draw(TextureManager.objectsheet, new Vector2(position.X - 44, position.Y - 130), new Rectangle(256, 256, 128, 144), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.3 + 0.0001f * (position.Y - Globals.cameraPos.Y) + 0.00001f * (position.X - Globals.cameraPos.X)));

                    break;
                case ObjectID.Pine:
                    spriteBatch.Draw(TextureManager.objectsheet, new Vector2(position.X - 58, position.Y - 130), new Rectangle(384, 256, 128, 144), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.3 + 0.0001f * (position.Y - Globals.cameraPos.Y) + 0.00001f * (position.X - Globals.cameraPos.X)));
                    break;
                case ObjectID.DarkCactus:
                    spriteBatch.Draw(TextureManager.objectsheet, new Vector2(position.X - 12, position.Y - 130), new Rectangle(515, 256, 32, 144), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.3 + 0.0001f * (position.Y - Globals.cameraPos.Y) + 0.00001f * (position.X - Globals.cameraPos.X)));
                    break;
                case ObjectID.LightCactus:
                    spriteBatch.Draw(TextureManager.objectsheet, new Vector2(position.X - 6, position.Y - 134), new Rectangle(550, 256, 32, 144), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)(0.3 + 0.0001f * (position.Y - Globals.cameraPos.Y) + 0.00001f * (position.X - Globals.cameraPos.X)));
                    break;
                default:
                    break;
            }

        }

    }
}

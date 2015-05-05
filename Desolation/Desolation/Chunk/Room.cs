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
                for (int i = 0; i < area.Height; i++)
                {
                    for (int j = 0; j < area.Width; j++)
                    {
                        spriteBatch.Draw(TextureManager.playerSheet, new Vector2(area.X + j * 16, area.Y + i * 16), new Rectangle(0, 0, 16, 16), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1.0f);
                    }
                }
            
                //spriteBatch.Draw(TextureManager.playerSheet, new Vector2(area.X, area.Y), area , Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1.0f);
            }
        }
    }
}

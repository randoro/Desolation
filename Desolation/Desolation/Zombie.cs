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
    class Zombie :Entity
    {
        Texture2D sheet;
        Rectangle zRect;
        Vector2 zPos;
        
        public Zombie(Texture2D sheet, Rectangle rect, Vector2 pos)

            : base(sheet, rect, pos)
        {
            this.sheet = sheet;
            this.zRect = rect;
            this.zPos = pos;
            zRect = new Rectangle(0, 0, 16, 16);
            zPos = new Vector2(400, 300);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sheet, zPos, zRect, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
           
        }

    }

}

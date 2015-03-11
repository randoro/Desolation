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
    class Goblin:Entity
    {
        Rectangle gRect;
        Vector2 gPos;
        Texture2D sheet;

        public Goblin(Texture2D sheet, Rectangle rect, Vector2 pos)
            :base(sheet, rect, pos)
        {
            this.gRect = new Rectangle(0, 16, 16, 16);
            this.gPos = new Vector2(200, 200);
            this.sheet = sheet;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sheet, gPos, gRect, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
        }

    }
}

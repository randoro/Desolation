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
    class Deer :Entity 
    {
        int frame;
        double frameTimer, frameInterval = 100;
        float range = 200;
        Player player;
        bool InRange = false;
        Direction currentDirection;
        public Deer(Player player, Vector2 pos)
            : base(pos)
        {
            sourceRect = new Rectangle(0, 0, 16, 32);
            position = new Vector2(100, 100);
            this.player = player;

            speed = 3;
        }
        public override void moveDirection(Direction direction)
        {
            currentDirection = direction;
            base.moveDirection(direction);
        }
        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.deerSheet, new Vector2(position.X - 8, position.Y - 15), sourceRect, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

        }
    }


}

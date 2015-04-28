using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Desolation
{
    class Animation
    {
        public AnimationType animationType { get; set; }
        public Vector2 position { get; set; }
        public double TTL { get; set; }

        private double startTTL;
        public Animation(AnimationType animationType, Vector2 position, double TTL)
        {
            this.animationType = animationType;
            this.position = position;
            this.TTL = TTL;
            this.startTTL = TTL;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            switch (animationType)
            {
                case AnimationType.FadeOutAndIn:
                    spriteBatch.Draw(TextureManager.fillingTexture, new Vector2(position.X - 10, position.Y - 10), new Rectangle(-10, -10, Globals.screenX + 20, Globals.screenY + 20), new Color(1.0f, 1.0f, 1.0f, 1.0f - (float)(TTL / startTTL)), 0f, Vector2.Zero, 1f, SpriteEffects.None, 1.0f);
                    break;
                case AnimationType.Smoke:
                    //uses particleEngine
                    break;
                default:
                    break;
            }
        }


    }
}

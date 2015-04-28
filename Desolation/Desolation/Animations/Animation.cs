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

        public double startTTL { get; set; }

        public double alpha { get; set; }
        public Animation(AnimationType animationType, Vector2 position, double TTL, double totalTTL)
        {
            this.animationType = animationType;
            this.position = position;
            this.startTTL = totalTTL;
            this.TTL = TTL;
            
        }

        public void draw(SpriteBatch spriteBatch)
        {
            switch (animationType)
            {
                case AnimationType.FadeOutAndIn:
                    spriteBatch.Draw(TextureManager.fillingTexture, new Vector2(position.X - 10, position.Y - 10), new Rectangle(-10, -10, Globals.screenX + 20, Globals.screenY + 20), new Color((float)alpha, (float)alpha, (float)alpha, (float)alpha), 0f, Vector2.Zero, 1f, SpriteEffects.None, 1.0f);
                    break; //(float)((double)TTL / (double)startTTL)
                case AnimationType.Smoke:
                    //uses particleEngine
                    break;
                default:
                    break;
            }
        }


    }
}

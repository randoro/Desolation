using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Desolation
{
    public class Particle
    {
        public Texture2D Text { get; set; }
        public Vector2 Pos { get; set; }
        public Vector2 Vel { get; set; }
        public float Angle { get; set; }
        public float AngularVel { get; set; }
        public Color Color { get; set; }
        public float Size { get; set; }
        public int TTL { get; set; } //TTL = time to live för partiklarna 

        public Particle(Texture2D text, Vector2 pos, Vector2 vel, float angle, float angularVel, Color color, float size, int ttl)
        {
            Text = text;
            Pos = pos;
            Vel = vel;
            Angle = angle;
            AngularVel = angularVel;
            Color = color;
            Size = size;
            TTL = ttl;
        }

        public void Update()
        {
            TTL--;
            Pos += Vel;
            Angle += AngularVel;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle srcRect = new Rectangle(0, 0, Text.Width, Text.Height);
            Vector2 origin = new Vector2(Text.Width / 2, Text.Height / 2);

            spriteBatch.Draw(Text, Pos, srcRect, Color, Angle, origin, Size, SpriteEffects.None, 1f);
        }
    }
}

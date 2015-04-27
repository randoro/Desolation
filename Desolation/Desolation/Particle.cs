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
        public Texture2D text { get; set; }
        public Vector2 pos { get; set; }
        public Vector2 vel { get; set; }
        public float angle { get; set; }
        public float angularVel { get; set; }
        public Color Color { get; set; }
        public float size { get; set; }
        public int TTL { get; set; } //TTL = time to live för partiklarna 

        public Particle(Texture2D text, Vector2 pos, Vector2 vel, float angle, float angularVel, Color Color, float size, int ttl)
        {
            this.text = text;
            this.pos = pos;
            this.vel = vel;
            this.angle = angle;
            this.angularVel = angularVel;
            this.Color = Color;
            this.size = size;
            this.TTL = ttl;

        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}

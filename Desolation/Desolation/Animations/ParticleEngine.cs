﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Desolation
{
    public class ParticleEngine
    {
        public Vector2 emitterLocation { get; set; }

        public List<Particle> particles;

        public ParticleEngine(Vector2 loaction, int particleType)
        {
            emitterLocation = loaction;
            this.particles = new List<Particle>();
            
        }

        public void update(GameTime gameTime)
        {
            

            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public Particle GenerateSmokeParticle()
        {
            Vector2 pos = emitterLocation;
            Vector2 vel = new Vector2(1f * (float)(Globals.rand.NextDouble() * 2 - 1), 1f * (float)(Globals.rand.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVel = 0.1f * (float)(Globals.rand.NextDouble() * 2 - 1);
            Color color = Color.White; //((float)Globals.rand.NextDouble(), (float)Globals.rand.NextDouble(), (float)Globals.rand.NextDouble());
            float size = (float)Globals.rand.NextDouble();
            int ttl = 20 + Globals.rand.Next(40);

            return new Particle(TextureManager.leaf, pos, vel, angle, angularVel, color, size, ttl);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Desolation
{
    public class AnimationEngine
    {
        private Random rand;
        public Vector2 EmitterLocation { get; set; }

        private List<Particle> particles;
        private List<Texture2D> textures;

        public AnimationEngine(List<Texture2D> textures, Vector2 loaction)
        {
            EmitterLocation = loaction;
            this.textures = textures;
            this.particles = new List<Particle>();
            rand = new Random();
            
        }

        public void Update()
        {
            int total = 10;

            for (int i = 0; i < total; i++)
            {
                particles.Add(GenerateNewParticle());
            }

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

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
        }

        private Particle GenerateNewParticle()
        {
            Texture2D text = textures[rand.Next(textures.Count)];
            Vector2 pos = EmitterLocation;
            Vector2 vel = new Vector2(1f * (float)(rand.NextDouble() * 2 - 1), 1f * (float)(rand.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVel = 0.1f * (float)(rand.NextDouble() * 2 - 1);
            Color color = new Color((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble());
            float size = (float)rand.NextDouble();
            int ttl = 20 + rand.Next(40);

            return new Particle(text, pos, vel, angle, angularVel, color, size, ttl);
        }
    }
}

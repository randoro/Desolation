using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Desolation
{
    class AnimationEngine
    {
        ParticleEngine particleEngine;
        int total;


        public AnimationEngine()
        {
            particleEngine = new ParticleEngine(new Vector2(2100, 2100), 0);
            total = 10;
        }

        public void update(GameTime gameTime)
        {

            for (int i = 0; i < total; i++)
            {
                particleEngine.particles.Add(particleEngine.GenerateSmokeParticle());
            }

            particleEngine.update(gameTime);

        }

        public void draw(SpriteBatch spriteBatch)
        {
            particleEngine.draw(spriteBatch);
        }
    }
}

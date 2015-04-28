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

        List<Animation> animations;

        public AnimationEngine()
        {
            particleEngine = new ParticleEngine(new Vector2(2100, 2100), 0);
            animations = new List<Animation>();

            animations.Add(new Animation(AnimationType.Smoke, new Vector2(2100, 2100), 200));

            animations.Add(new Animation(AnimationType.FadeOutAndIn, new Vector2(Globals.cameraPos.X, Globals.cameraPos.Y), 2000));
        }

        public void update(GameTime gameTime)
        {
            for (int i = animations.Count - 1; i >= 0; i--)
            {
                animations[i].TTL--;
                if (animations[i].TTL < 0)
                {
                    animations.RemoveAt(i);
                }
                else
                {
                    addParticlesToAnimation(animations[i]);
                }

            }

            particleEngine.update(gameTime);

        }

        public void addParticlesToAnimation(Animation animation)
        {
            AnimationType type = animation.animationType;
            switch (type)
            {
                case AnimationType.FadeOutAndIn:
                    animation.position = new Vector2(Globals.cameraPos.X, Globals.cameraPos.Y);
                    break;
                case AnimationType.Smoke:
                    for (int i = 0; i < 10; i++)
                    {
                        particleEngine.particles.Add(particleEngine.GenerateSmokeParticle());
                    }
                    break;
                default:
                    break;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            particleEngine.draw(spriteBatch);

            foreach(Animation e in animations) 
            {
                e.draw(spriteBatch);
            }
        }
    }
}

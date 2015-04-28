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

        public List<Animation> animations;

        public AnimationEngine()
        {
            particleEngine = new ParticleEngine(new Vector2(2100, 2100), 0);
            animations = new List<Animation>();

            animations.Add(new Animation(AnimationType.Smoke, new Vector2(2100, 2100), 200));

            animations.Add(new Animation(AnimationType.FadeOutAndIn, new Vector2(Globals.cameraPos.X, Globals.cameraPos.Y), 100));
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
                    if(animation.TTL < animation.startTTL / 8) 
                    {
                        animation.alpha = (float)((double)animation.TTL / ((double)animation.startTTL / 8));
                    }
                    else if(animation.TTL > 7 * animation.startTTL / 8) 
                    {
                        animation.alpha = 1.0f - (float)(((double)animation.TTL - (7 * animation.startTTL / 8)) / ((double)animation.startTTL / 8));
                    }
                    else
                    {
                        animation.alpha = 1.0f;
                    }
                    
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

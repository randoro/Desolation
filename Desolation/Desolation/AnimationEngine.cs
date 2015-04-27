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

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}

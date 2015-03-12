using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Desolation
{
  public  abstract class GameObject
    {
        TextureManager textureManager;
        Rectangle rect;
        Vector2 pos;
        public GameObject(Rectangle rect, Vector2 pos)
        {
            this.rect = rect;
            this.pos = pos;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
        

        
    }
}

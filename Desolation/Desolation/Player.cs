using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Desolation
{

    class Player
    {
  
 
        float rotation;
        public Rectangle srcRec;
        public Rectangle pacmanhitbox;
        Vector2 pos;
        //KeyboardState oldKeyBordState;
        //KeyboardState newKeyBordState;
    
        public Vector2 spawnpos;
        public Player(Vector2 pos)
        {
            this.pos = pos;
    

          
      

        }
        public void Update(GameTime gameTime)
        {
           
        }
        public void Draw()
        {

        }


    }

}

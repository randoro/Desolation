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
   abstract public class Entity :   GameObject
    {
        public Entity(Vector2 pos)
           : base(pos)
        {

        }

        abstract public override void Update(GameTime gameTime);

        abstract public override void Draw(SpriteBatch spriteBatch);

        public void moveDirection()
        {
            switch (Globals.dir)
            {
                case Direction.North:
                    position.Y--;
                    break;
                case Direction.NorthEast:
                    break;
                case Direction.East:
                  
                    break;
                case Direction.SouthEast:
                    break;
                case Direction.South:
                    break;
                case Direction.SouthWest:
                    break;
                case Direction.West:
                    //position.X--;
                    break;
                case Direction.NorthWest:
                    break;
                default:
                    break;
            }
        }

    }
}

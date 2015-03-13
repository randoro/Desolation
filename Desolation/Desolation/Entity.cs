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

        public virtual void moveDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    position.Y--;
                    
                    break;
                case Direction.NorthEast:
                    position.X++;
                    position.Y--;
                    break;
                case Direction.East:
                    position.X++;
                    break;
                case Direction.SouthEast:
                    position.X++;
                    position.Y++;
                    break;
                case Direction.South:
                    position.Y++;
                    break;
                case Direction.SouthWest:
                    position.X--;
                    position.Y++;
                    break;
                case Direction.West:
                    position.X--;
                    break;
                case Direction.NorthWest:
                    position.X--;
                    position.Y--;
                    break;
                case Direction.None:
                    break;
                default:
                    break;
            }
        }

    }
}

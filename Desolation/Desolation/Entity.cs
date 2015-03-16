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
                    position.Y -= 5;
                    
                    break;
                case Direction.NorthEast:
                    position.X += 5;
                    position.Y -= 5;
                    break;
                case Direction.East:
                    position.X += 5;
                    break;
                case Direction.SouthEast:
                    position.X += 5;
                    position.Y += 5;
                    break;
                case Direction.South:
                    position.Y += 5;
                    break;
                case Direction.SouthWest:
                    position.X -= 5;
                    position.Y += 5;
                    break;
                case Direction.West:
                    position.X -= 5;
                    break;
                case Direction.NorthWest:
                    position.X -= 5;
                    position.Y -= 5;
                    break;
                case Direction.None:
                    break;
                default:
                    break;
            }
        }


       public void getCurrentChunkNrInArray() 
       {
           int playerRegionX = Globals.getRegionValue(Globals.playerPos.X);
           int playerRegionY = Globals.getRegionValue(Globals.playerPos.Y);

           int playerChunkX = Globals.getChunkValue(Globals.playerPos.X);
           int playerChunkY = Globals.getChunkValue(Globals.playerPos.Y);

           int entityRegionX = Globals.getRegionValue(position.X);
           int entityRegionY = Globals.getRegionValue(position.Y);

           int regionOffsetX = entityRegionX - playerRegionX;
           int regionOffsetY = entityRegionX - playerRegionX;




       }

    }
}

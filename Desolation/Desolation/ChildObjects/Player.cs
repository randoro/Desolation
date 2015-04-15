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
    public class Player  :   Entity
    {
      
        int frame;
        double frameTimer, frameInterval = 100;
        Direction currentDirection;
        #region Constructor
        public Player(Vector2 position)
            : base(position)
        {
            this.position = position;
            sourceRect = new Rectangle(0, 16, 16, 16);
            health = 100;
            speed = 3;
        }
        #endregion

        #region Methods

        public override void syncUpdate(GameTime gameTime)
        {
            base.checkCollision();
        }

        public override void Update(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
            }
            MouseState mouse = Mouse.GetState();
            Vector2 mousePosOnScreen = new Vector2(mouse.X, mouse.Y);

            Vector2 mousePosInGame = new Vector2(Globals.playerPos.X - Globals.screenX / 2 + mousePosOnScreen.X, Globals.playerPos.Y - Globals.screenY / 2 + mousePosOnScreen.Y);
            getAngle(mousePosInGame);
            
        }

        public override void checkAttack()
        {

        }

        public override void moveDirection(Direction direction)
        {
            currentDirection = direction;
            oldPosition = position;
            base.moveDirection(direction);
        }

        public override void getAngle(Vector2 target)
        {
            
            //rotation = (float)Math.Atan2(target.X, target.Y);

            base.getAngle(target);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.playerSheet, new Vector2(position.X - 8, position.Y -15), sourceRect, Color.White, rotation, new Vector2(), 1f, SpriteEffects.None, 1);

            switch (currentDirection)
            {
                case Direction.North:
                    sourceRect.X = 2 * 16;
                    sourceRect.Y = (frame % 4) * 16; 
                    break;
                case Direction.NorthEast:
                    sourceRect.X = 2 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    break;
                case Direction.NorthWest:
                    sourceRect.X = 2 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    break;
                case Direction.South:
                    sourceRect.X = 0 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    break;
                case Direction.SouthEast:
                    sourceRect.X = 0 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    break;
                case Direction.SouthWest:
                    sourceRect.X = 0 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    break;
                case Direction.East:
                    sourceRect.X = 3 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    break;
                case Direction.West:
                    sourceRect.X = 1 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    break;
                case Direction.None:
                    break;
            }
        }

        public override void getTagList(ref List<Tag> individualList)
        {
        }

        //public void CheckAngle(MouseState mousePos)
        //{
        //    if ( mousePos.X >= position.X && mousePos.Y >= position.Y)
        //    {
              
        //    }
        //}
        #endregion

    }
}

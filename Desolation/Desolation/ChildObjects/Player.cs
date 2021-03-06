﻿using System;
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
    public class Player : Entity
    {

        int frame;
        double frameTimer, frameInterval = 100;
        int attackspeed = 0;
        int meleeRange = 15;
        int rangedRange = 25;
        Direction watchingDirection;
        public ItemID leftitem = ItemID.Asultrifle;
        public ItemID rightitem = ItemID.Asultrifle;
        #region Constructor
        public Player(Vector2 position)
            : base(position)
        {
            this.position = position;
            sourceRect = new Rectangle(0, 16, 16, 16);
            health = 100;
           

            speed = 3;
            watchingDirection = Direction.None;

        }
        #endregion

        #region Methods

        public override void syncUpdate(GameTime gameTime)
        {
            checkAttack();
            oldPosition = position;
        }

        public override void Update(GameTime gameTime)
        {
            base.checkCollision();
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            equipment[0] = new Item((int)leftitem);
            equipment[1] = new Item((int)rightitem);

            MouseState mouse = Mouse.GetState();
            Vector2 mousePosOnScreen = new Vector2(mouse.X, mouse.Y);

            Vector2 mousePosInGame = new Vector2(Globals.playerPos.X - Globals.screenX / 2 + mousePosOnScreen.X, Globals.playerPos.Y - Globals.screenY / 2 + mousePosOnScreen.Y);
            rotation = getAngle(mousePosInGame);

            float degree = (float)(rotation * (180.0 / Math.PI));

            if ((degree < 22.5f && degree >= 0) || (degree > 337.5f && degree <= 360))
            {
                watchingDirection = Direction.East;
            }
            else if (degree < 67.5f && degree >= 22.5f)
            {
                watchingDirection = Direction.SouthEast;
            }
            else if (degree < 112.5f && degree >= 67.5f)
            {
                watchingDirection = Direction.South;
            }
            else if (degree < 157.5f && degree >= 112.5f)
            {
                watchingDirection = Direction.SouthWest;
            }
            else if (degree < 202.5f && degree >= 157.5f)
            {
                watchingDirection = Direction.West;
            }
            else if (degree < 247.5f && degree >= 202.5f)
            {
                watchingDirection = Direction.NorthWest;
            }
            else if (degree < 292.5f && degree >= 247.5f)
            {
                watchingDirection = Direction.North;
            }
            else if (degree < 337.5f && degree >= 292.5f)
            {
                watchingDirection = Direction.NorthEast;
            }




        }

        public override void checkAttack()
        {

            Item lefttempItem = equipment[0];
            Item righttempItem = equipment[1];
            attackspeed--;
            if (KeyMouseReader.RightHold())
            {
                if (!lefttempItem.twohandded&&!righttempItem.twohandded)
                {

                    if (attackspeed <= 0)
                    {
                        foreach (Entity e in ChunkManager.entityList)
                        {





                            if (righttempItem != null)
                            {
                                if (righttempItem.itemType.Equals(ItemType.Melee))
                                {
                                    if (Globals.checkRange(e.position, position, righttempItem.range + meleeRange))//kålla vilka enntytis är inom rench och det är de man skadar
                                    {
                                        float directAngle = getAngle(e.position);
                                        if (rotation >= directAngle - righttempItem.skadearea && rotation <= directAngle + righttempItem.skadearea)
                                        {
                                            e.damageEntity(righttempItem.damge);
                                            attackspeed = righttempItem.attackspeed;

                                        }
                                    }

                                }
                                else if (righttempItem.itemType.Equals(ItemType.Ranged))
                                {
                                    if (Globals.checkRange(e.position, position, righttempItem.range + rangedRange))
                                    {
                                        // läg till under i if satsen ....att get angel(pleyer mus)= getangel (player entety)
                                        //float currentAngle = getAngle(position);
                                        //getAngle(e.position);
                                        float directAngle = getAngle(e.position);

                                        if (rotation >= directAngle - righttempItem.skadearea && rotation <= directAngle + righttempItem.skadearea)
                                        {
                                            e.damageEntity(righttempItem.damge);
                                            attackspeed = righttempItem.attackspeed;
                                        }
                                        //if (attackspeed <= 0)
                                        //{
                                        //    e.life--;
                                        //    attackspeed = 3;
                                        //}

                                    }
                                }
                                else if (lefttempItem.itemType.Equals(ItemType.Effect))
                                {

                                }
                            }
                        }
                    }
                } 
            }
            if (KeyMouseReader.LeftHold())
            {

                if (attackspeed <= 0)
                {
                    foreach (Entity e in ChunkManager.entityList)
                    {





                        if (lefttempItem != null)
                        {
                            if (lefttempItem.itemType.Equals(ItemType.Melee))
                            {
                                if (Globals.checkRange(e.position, position, lefttempItem.range + meleeRange))//kålla vilka enntytis är inom rench och det är de man skadar
                                {
                                    float directAngle = getAngle(e.position);
                                    if (rotation >= directAngle - lefttempItem.skadearea && rotation <= directAngle + lefttempItem.skadearea)
                                    {
                                        e.damageEntity(lefttempItem.damge);
                                        attackspeed = lefttempItem.attackspeed;

                                    }
                                }

                            }
                            else if (lefttempItem.itemType.Equals(ItemType.Ranged))
                            {
                                if (Globals.checkRange(e.position, position, lefttempItem.range + rangedRange))
                                {
                                    // läg till under i if satsen ....att get angel(pleyer mus)= getangel (player entety)
                                    //float currentAngle = getAngle(position);
                                    //getAngle(e.position);
                                    float directAngle = getAngle(e.position);

                                    if (rotation >= directAngle - lefttempItem.skadearea && rotation <= directAngle + lefttempItem.skadearea)
                                    {
                                        e.damageEntity(lefttempItem.damge);
                                        attackspeed = lefttempItem.attackspeed;
                                    }
                                    //if (attackspeed <= 0)
                                    //{
                                    //    e.life--;
                                    //    attackspeed = 3;
                                    //}

                                }
                            }
                            else if (lefttempItem.itemType.Equals(ItemType.Effect))
                            {

                            }
                        }
                    }
                }
            }




        }

        public override void moveDirection(Direction direction)
        {
            currentDirection = direction;
            if (direction != Direction.None)
            {
                if (frameTimer <= 0)
                {
                    frameTimer = frameInterval;
                    frame++;
                }
            }

            base.moveDirection(direction);
        }

        public override float getAngle(Vector2 target)
        {
            return base.getAngle(target);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.playerSheet, new Vector2(position.X - 8, position.Y - 15), sourceRect, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, (float)(0.3 + (0.0001f * (position.Y - Globals.cameraPos.Y))+ (0.00001f * (position.X - Globals.cameraPos.X)) - 0.0004));

            switch (watchingDirection)
            {
                case Direction.North:
                    sourceRect.X = 2 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    break;
                case Direction.NorthEast:
                    sourceRect.X = 7 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    break;
                case Direction.NorthWest:
                    sourceRect.X = 6 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    break;
                case Direction.South:
                    sourceRect.X = 0 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    break;
                case Direction.SouthEast:
                    sourceRect.X = 4 * 16;
                    sourceRect.Y = (frame % 4) * 16;
                    break;
                case Direction.SouthWest:
                    sourceRect.X = 5 * 16;
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

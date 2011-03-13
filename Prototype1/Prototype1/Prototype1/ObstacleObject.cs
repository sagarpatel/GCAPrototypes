using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Prototype1
{
    class ObstacleObject:GameObject
    {

        public float inertia;

        public double last_time;

        public double checkcooldown;

        public ObstacleObject(Texture2D tex):base(tex)
        {
            inertia = 1.0f; //1.0 means max does not move
            checkcooldown = 1000;
        }


        public bool CheckPlayerCollision(PlayerObject player)
        {
            if (rect.Intersects(player.rect))
                return true;
            else
                return false;
        }


        public void HandlePlayerCollision(PlayerObject player, GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > last_time + checkcooldown)
            {
                if (CheckPlayerCollision(player))
                {
                    last_time = gameTime.TotalGameTime.TotalMilliseconds;

                    Vector2 colDir = center - player.center;
                    colDir.Normalize();

                    if (colDir.X > 0 && colDir.Y < 0)///Upper right
                        if (Math.Abs(colDir.X) < Math.Abs(colDir.Y))
                            player.velocity.Y = -player.velocity.Y;
                        else
                            player.velocity.X = -player.velocity.X;


                    if (colDir.X > 0 && colDir.Y > 0)///Bottom right
                        if (Math.Abs(colDir.X) < Math.Abs(colDir.Y))
                            player.velocity.Y = -player.velocity.Y;
                        else
                            player.velocity.X = -player.velocity.X;


                    if (colDir.X < 0 && colDir.Y < 0) ///Upper left
                        if (Math.Abs(colDir.X) > Math.Abs(colDir.Y))
                            player.velocity.X = -player.velocity.X;
                        else
                            player.velocity.Y = -player.velocity.Y;

                    if (colDir.X < 0 && colDir.Y > 0) /// Bottom left
                        if (Math.Abs(colDir.X) > Math.Abs(colDir.Y))
                            player.velocity.X = -player.velocity.X;
                        else
                            player.velocity.Y = -player.velocity.Y;

                }
            }

        }

    }
}

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

        public ObstacleObject(Texture2D tex):base(tex)
        {
            inertia = 1.0f; //1.0 means max does not move
        }


        public bool CheckPlayerCollision(PlayerObject player)
        {
            if (rect.Intersects(player.rect))
                return true;
            else
                return false;
        }


        public void HandlePlayerCollision(PlayerObject player)
        {

            if (CheckPlayerCollision(player))
            {
                Vector2 normal = new Vector2((float)Math.Sin((double)rotation),
                                             (float)Math.Cos((double)rotation));
                normal.Normalize();

                velocity += (1f-inertia) * player.velocity;
                Vector2.Reflect(player.velocity, normal);
            }

        }

    }
}

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
    class TargetObject:GameObject
    {
        public bool isInsideMe;
        public bool isTouchingMe;

        public Texture2D texOn;
        public Texture2D texOff;


        public TargetObject(Texture2D tex):base( tex)
        {
            isInsideMe = false;
            isTouchingMe = false;
            texOn = tex;

        }

        public override void UpdatePV()
        {
            base.UpdatePV();
            //CheckInside(player);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //base.Draw(spriteBatch);
            if (isAlive)
            {
                if (isInsideMe || isTouchingMe)
                    spriteBatch.Draw(texOn, position, Color.White);
                else
                    spriteBatch.Draw(texOff, position, Color.White);
            }

        }


        public bool CheckInside(Rectangle other)
        {
            if (rect.Contains(other))
                return true;
            else
                return false;

        }

        public bool CheckInside(Vector2 othercenter, float otherradius)
        {
            float dist = Vector2.Distance(center, othercenter);

            if (otherradius + dist < radius) //checks if other is inside of this
                return true;
            else
                return false;
       
        }


        public void HandlePlayerInside(PlayerObject player)
        {
            Vector2 othercenter = player.center;
            float otherradius = player.radius;

            if (CheckInside(othercenter, otherradius))
            {
                isInsideMe = true;
                player.isScoring = true;
                player.score += 100;
            }
            else
            {
             //   player.isScoring = false;
                isInsideMe = false;
            }
       

        }


        public void HandlePlayerCollision(PlayerObject player)
        {
            Vector2 othercenter = player.center;
            float otherradius = player.radius;

            if (CheckCircleCollision(othercenter, otherradius))
            {
                player.score += 10;
                player.isScoring = true;
                isTouchingMe = true;
            }
            else
            {
              //  player.isScoring = false;
                isTouchingMe = false;
            }

            


        }


    }
}

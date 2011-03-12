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
    class PlayerObject:GameObject
    {

        public int bounceCount;

        public int score;
        public SpriteFont scoreFont;
        public Vector2 scorePosition;
        public bool isScoring;

        public bool isFlicked;

        public float minVelocity;

        public PlayerObject(Texture2D tex):base( tex)
        {
            bounceCount = 0;
            score = 0;
            isScoring = false;
            scorePosition = new Vector2(position.X, position.Y + 20f);
            isFlicked = false;
            minVelocity = 200f;
           // radius = radius / 2;
        }


        public override void UpdatePV()
        {
            base.UpdatePV();
            scorePosition = new Vector2(position.X + radius, position.Y - 50f);
            if (velocity.Length() < minVelocity)
            {
                velocity = new Vector2(0, 0);
            }
        }




        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            
            if(isFlicked)
                spriteBatch.DrawString(scoreFont, score.ToString(), scorePosition, Color.Orchid);
        }



        

    }
}

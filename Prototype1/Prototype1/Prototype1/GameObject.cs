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
    class GameObject
    {

        public Texture2D texture;

        public bool isAlive;

        public Vector2 position;
        public Vector2 velocity;

        public float speed;

        public GameObject()
        {
            isAlive = false;
            position = new Vector2(0, 0);
            velocity = new Vector2(0, 0);

            speed = 0.005f;

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);

        }

        public void UpdatePV()
        {

            position += velocity * speed;
        }

        public void WallBounce( int W, int H)
        {
            if ((position.X + texture.Width) > W || position.X < 0)
            {
                velocity.X = -velocity.X;
            }

            if ((position.Y + texture.Height) > H || position.Y < 0)
            {
                velocity.Y = -velocity.Y;
            }



        }

    }





}

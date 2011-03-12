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

        public Vector2 center;
        public float radius;

        public float speed;
        //public float gravity;
        public float scale;
        public float rotation;
        public float friction;

        public Rectangle rect;


        public GameObject(Texture2D tex)
        {
            texture = tex;

            isAlive = false;
            position = new Vector2(0, 0);
            velocity = new Vector2(0, 0);

            speed = 0.005f;
            scale = 1.0f;
            rotation = 0.0f;
          //  gravity = 9.8f;
            friction = 0.01f;

            rect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            center = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);

            radius = texture.Width/2;
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(texture, position, null, Color.White, rotation, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            

        }

        public virtual void UpdatePV()
        {
            velocity -= friction * velocity; //takes too long to slow down
            
            position += velocity * speed;
            rect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            center = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);

          
        }


        public void WallBounce( int W, int H)
        {
            if ((position.X + texture.Width) > W || position.X < 0)
            {
                velocity.X = -velocity.X;
                //scale -= 0.2f;
            }

            if ((position.Y + texture.Height) > H || position.Y < 0)
            {
                velocity.Y = -velocity.Y;
              //  scale -= 0.2f;
            }

        }

        public bool CheckCircleCollision(Vector2 othercenter, float otherradius)
        {
            float dist = Vector2.Distance(center, othercenter);

            if (dist < radius+otherradius)
                return true;
            else
                return false;
        }
    }



        



    }





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

using Box2D.XNA;

namespace _2bBoxex2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D ball;
        World world;

        Texture2D wallTex;


        List<Body> ballBodies;

        Body groundBody;

        const float ScaleFactor = 0.01f;

        Random r = new Random();

        MouseState prevMouseState;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 800;

        }

     


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            world = new World(new Vector2(0,10), true);

            ballBodies = new List<Body>();

            base.Initialize();
        }

      


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ball = Content.Load<Texture2D>("Sprites/curling_ball");

            wallTex = Content.Load<Texture2D>("Sprites/Brick_400x100_Up");

            CreateGroundAndWalls();

            // TODO: use this.Content to load your game content here
        }

     


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }





        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            world.Step(1 / 60f, 10, 10);

            HandleClick();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);



            spriteBatch.Begin();

            foreach (var ballBody in ballBodies)
            {

                spriteBatch.Draw(ball, ballBody.Position / ScaleFactor, null, Color.Orange, ballBody.Rotation,

                                 new Vector2(ball.Width / 2f, ball.Height / 2f), 1, SpriteEffects.None, 0);


            }

            spriteBatch.Draw(wallTex, groundBody.Position / ScaleFactor, null, Color.Orange, groundBody.Rotation,

                                 new Vector2(wallTex.Width / 2f, wallTex.Height / 2f), 1, SpriteEffects.None, 0);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }



        private void HandleClick()
        {

            var state = Mouse.GetState();

            if (prevMouseState.LeftButton == ButtonState.Released && state.LeftButton == ButtonState.Pressed)
            {

                CreateBall();

            }

            prevMouseState = state;

        }


        private void CreateBall()
        {

            var bodyDef = new BodyDef();

            bodyDef.type = BodyType.Dynamic;

            var ballShape = new CircleShape();

            ballShape._radius = (ball.Width / 2f) * ScaleFactor;

            var ballFixture = new FixtureDef();

            ballFixture.friction = 0.0f; // no friction

            ballFixture.restitution = 1.0f; // give the ball a perfect bounce

            ballFixture.density = 1.0f;

            ballFixture.shape = ballShape;

            var ballBody = world.CreateBody(bodyDef);

            ballBody.CreateFixture(ballFixture);

            ballBody.Position = new Vector2(((float)r.NextDouble() * 4.5f + .3f), (float)r.NextDouble() * 4.5f + .3f);

            ballBodies.Add(ballBody);

        }




        private void CreateGroundAndWalls()
        {

            var grounDef = new BodyDef();
            grounDef.type = BodyType.Static;

            var groundFix = new FixtureDef();
            groundFix.restitution = 1.0f;
            groundFix.friction = 0.0f;
            groundFix.density = 0.0f;

            var groundShape = new PolygonShape();
            //groundShape.SetAsEdge(new Vector2(0, 8), new Vector2(4.8f, 8.0f));
            groundShape.SetAsBox(wallTex.Width *ScaleFactor/2f, wallTex.Height*ScaleFactor/2f);
            
            groundBody = world.CreateBody(grounDef);
            groundBody.Position = new Vector2(2.4f, 4);
            groundFix.shape = groundShape;
            groundBody.CreateFixture(groundFix);
            
            
            
            
            
            groundShape.SetAsEdge(new Vector2(0, 0), new Vector2(0f, 8.0f));



            var leftBody = world.CreateBody(grounDef);
            groundFix.shape = groundShape;
            leftBody.CreateFixture(groundFix);
            groundShape.SetAsEdge(new Vector2(4.8f, 0), new Vector2(4.8f, 8.0f));

            var rightBody = world.CreateBody(grounDef);
            groundFix.shape = groundShape;
            rightBody.CreateFixture(groundFix);
            groundShape.SetAsEdge(new Vector2(0, 0), new Vector2(4.8f, 0));

            var topBody = world.CreateBody(grounDef);
            groundFix.shape = groundShape;
            topBody.CreateFixture(groundFix);

        }



    }
}

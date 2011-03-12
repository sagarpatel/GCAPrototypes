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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        PlayerObject player1;
        TargetObject target1;

        int screenHeight;
        int screenWidth;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 800;

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);


            TouchPanel.EnabledGestures = GestureType.Flick;

            

        }



        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }



        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            screenHeight = graphics.GraphicsDevice.Viewport.Height;
            screenWidth = graphics.GraphicsDevice.Viewport.Width;
            
            // TODO: use this.Content to load your game content here

            player1 = new PlayerObject(Content.Load<Texture2D>("Sprites/enemy1V1"));

            player1.position = new Vector2(200, 600);


            target1 = new TargetObject(Content.Load<Texture2D>("Sprites/gsq"));
     
            target1.position = new Vector2(0, 0);


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

            // TODO: Add your update logic here


            HandleInputs();

            player1.UpdatePV();
            player1.WallBounce(screenWidth, screenHeight);

            target1.UpdatePV();
            //if(target1.CheckInside(player1.rect))
            //    this.Exit();

            if (target1.CheckInside(player1.center, player1.radius))
                this.Exit();

            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            player1.Draw(spriteBatch);
            target1.Draw(spriteBatch);

            spriteBatch.End();


            base.Draw(gameTime);
        }



        private void HandleInputs()
        {

            while (TouchPanel.IsGestureAvailable)
            {

                GestureSample gs = TouchPanel.ReadGesture();

                player1.velocity = gs.Delta;
                break;

            }



        }


    }
}

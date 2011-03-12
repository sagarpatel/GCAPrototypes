using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Microsoft.Devices.Sensors;
using Microsoft.Phone.Controls;
using Microsoft.Devices;
using System.Threading;


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
        int max_points;
        int min_points;

        int levelCount;


        int player1_scoreCount;
        //VibrateController

        

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

            levelCount = 0;
            max_points = 9001;
            min_points = 1000;
            player1_scoreCount = 0;
            
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

            player1 = new PlayerObject(Content.Load<Texture2D>("Sprites/stone_64"));
            player1.scoreFont = Content.Load<SpriteFont>("Fonts/player1scoreFont");
            player1.position = new Vector2(200, 700);
            

            target1 = new TargetObject(Content.Load<Texture2D>("Sprites/target_128_green"));
            target1.texOff = Content.Load<Texture2D>("Sprites/target_128");
            target1.position = new Vector2(200, 50);
            target1.isAlive = true;
            levelCount = 0;
            LoadLevel(0);
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


            if (target1.isAlive)
            {
                target1.UpdatePV();
                target1.HandlePlayerInside(player1);
                target1.HandlePlayerCollision(player1);
                //if(target1.CheckInside(player1.rect))
                //    this.Exit();
            }

            HandleGameFlow();


            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            player1.Draw(spriteBatch);
            target1.Draw(spriteBatch);
           // spriteBatch.DrawString(player1.scoreFont, player1.score.ToString(), player1.scorePosition, Color.Orchid);

            spriteBatch.End();


            base.Draw(gameTime);
        }



        private void HandleInputs()
        {

            while (TouchPanel.IsGestureAvailable)
            {
                if (player1.isFlicked == false)
                {
                    GestureSample gs = TouchPanel.ReadGesture();
                    player1.velocity = gs.Delta;
                    player1.isFlicked = true;
                    break;
                }
                break;
            }

        }



        private void HandleGameFlow()
        {
            if (player1.isFlicked )///Wait until stonr is moving
            {

                if (player1.score > max_points)
                {
                    Thread.Sleep(500);
                    levelCount++;
                    player1_scoreCount += player1.score;
                    player1.score = 0;
                    player1.isScoring = false;
                    target1.isAlive = false;

                    LoadLevel(levelCount);

                }


                if ((player1.score < min_points) && (player1.velocity.Length() == 0))
                {
                    if(player1.isScoring==false)
                    {
                        Thread.Sleep(500);
                        // levelCount++;
                        //  player1_scoreCount += player1.score;
                        player1.score = 0;
                        player1.isScoring = false;
                        target1.isAlive = false;

                        LoadLevel(levelCount);
                    }
                }


                else if ((player1.score> min_points)&&(player1.velocity.Length()==0)) 
                {
                    if (player1.isScoring == false)
                    {
                        Thread.Sleep(500);
                        levelCount++;
                        player1_scoreCount += player1.score;
                        player1.score = 0;
                        player1.isScoring = false;
                        target1.isAlive = false;

                        LoadLevel(levelCount);
                    }
                }
                

                
            }

        }

        private void LoadLevel(int lvl)
        {
            switch (lvl)
            {

                case (0):
                    player1.isScoring = false;
                    player1.position = new Vector2(200, 700);
                    target1.position = new Vector2(200, 50);
                    player1.isFlicked = false;

                    target1.isAlive = true;
                    target1.isInsideMe = false;
                    target1.isTouchingMe = false;
                    break;


                case(1):
                    player1.isScoring = false;
                    player1.position = new Vector2(320, 720);
                    target1.position = new Vector2(0, 0);
                    player1.isFlicked = false;

                    target1.isAlive = true;
                    target1.isInsideMe = false;
                    target1.isTouchingMe = false;
                    break;

                    
            }

        }


    }
}

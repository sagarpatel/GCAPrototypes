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

using Box2D.XNA;



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

        ObstacleObject obz1;
        ObstacleObject obz2;

        Texture2D bg1;
        Texture2D bg2;
        Texture2D bg3;
        Texture2D bg4;
        Texture2D bg5;

        int max_points;
        int min_points;

        int levelCount;

        int player1_scoreCount;
        //VibrateController

 
        int screenHeight;
        int screenWidth;


        World world;

        float ScaleFactor;

        SpriteFont totalscoreFont;

        Song BGM1;



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


            ScaleFactor = 0.01f;

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

            player1 = new PlayerObject(Content.Load<Texture2D>("Sprites/Curling_ Ball_Green_64"));
            player1.scoreFont = Content.Load<SpriteFont>("Fonts/player1scoreFont");
            //player1.position = new Vector2(200, 700);
            player1.isAlive = true;
            


            target1 = new TargetObject(Content.Load<Texture2D>("Sprites/target_192_green"));
            target1.texOff = Content.Load<Texture2D>("Sprites/target_192");
            target1.position = new Vector2(200, 50);
            target1.isAlive = true;

          //  obz1 = new ObstacleObject(Content.Load<Texture2D>("Sprites/Brick_200x50_Side"));
          //  obz1.isAlive = false;

            bg1 = Content.Load<Texture2D>("Sprites/Level_4");
            bg2 = Content.Load<Texture2D>("Sprites/Level_4_fire");
            bg3 = Content.Load<Texture2D>("Sprites/Level_4_Multi_color");
            bg4 = Content.Load<Texture2D>("Sprites/Level_4_Uversion");
            bg5 = Content.Load<Texture2D>("Sprites/Omega level");

            totalscoreFont = Content.Load<SpriteFont>("Fonts/TotalscoreFont");


            BGM1 = Content.Load<Song>("Audio/Curling_Mega_Sound_Track");


            world = new World(new Vector2(0, 0), true);

            player1.ball =  player1.CreateBall(world, ScaleFactor);
           // player1.ball.Position = new Vector2(1, 1);


            CreateGroundAndWalls();

            levelCount = 0;
            LoadLevel(0);

            MediaPlayer.Play(BGM1);

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

            player1.UpdatePV_Player(ScaleFactor);
          //  player1.WallBounce(screenWidth, screenHeight);


            if (target1.isAlive)
            {
                target1.UpdatePV();
                target1.HandlePlayerInside(player1);
                target1.HandlePlayerCollision(player1);
                //if(target1.CheckInside(player1.rect))
                //    this.Exit();
            }

            //if (levelCount > 1)
            //{
            //    if (obz1.isAlive)
            //    {
            //      //  obz1.UpdatePV();
            //        //   obz1.HandlePlayerCollision(player1,gameTime);
            //    }
            //}

            HandleGameFlow();

            world.Step( 1/60f, 1, 1);
         //   world.ClearForces();

            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

           //priteBatch.Draw(bg1, new Vector2(0, 0), Color.White);

            switch (levelCount)
            {
                case (0):
                    spriteBatch.Draw(bg1, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 1f);
                    break;

                case (1):
                    spriteBatch.Draw(bg1, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 1f);
                    break;

                case (2):
                    spriteBatch.Draw(bg2, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    break;

                case (3):
                    spriteBatch.Draw(bg3, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    break;

                case (4):
                    spriteBatch.Draw(bg4, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    break;

                case (5):
                    spriteBatch.Draw(bg5, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 1f);
                    break;
            }


        

            player1.DrawPlayer(spriteBatch, ScaleFactor);

            spriteBatch.DrawString(totalscoreFont,  "Total Score: "+player1_scoreCount.ToString() , new Vector2(250,10), Color.NavajoWhite);
         //   player1.Draw(spriteBatch);


            target1.Draw(spriteBatch);

            if (levelCount == 2)
            {
                obz1.DrawObz(spriteBatch, ScaleFactor);
            }

            if (levelCount == 3)
            {
                obz2.DrawObz(spriteBatch, ScaleFactor);
            }

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
                    //player1.velocity = gs.Delta;
                    Vector2 pos = player1.ball.GetPosition();

                    //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    //spriteBatch.DrawString(player1.scoreFont,"TESSSSSSSS",player1.position,Color.Red);
                    //spriteBatch.End();
                    //player1.velocity = gs.Delta*player1.speed;
                    player1.ball.SetLinearVelocity(gs.Delta * ScaleFactor * player1.speed);
                    //player1.ball.ApplyLinearImpulse(gs.Delta*ScaleFactor * player1.speed, pos );

                    // player1.ball.ApplyLinearImpulse(new Vector2(1, 1), pos);

                    player1.isFlicked = true;
                    break;
                }
                else
                    LoadLevel(levelCount);

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
                        if (levelCount > 1)
                        {
                          //  obz1.isAlive = false;
                        }
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
                    player1.score = 0;
                    player1.ball.Position = new Vector2(320, 720) * ScaleFactor;
                    target1.position = new Vector2(50, 50);
                    player1.isFlicked = false;

                    target1.isAlive = true;
                    target1.isInsideMe = false;
                    target1.isTouchingMe = false;

                   



                    break;


                case(1):

                     player1.isScoring = false;
                    player1.score = 0;
                   // player1.position = new Vector2(200, 700);
                    player1.ball.Position = new Vector2(400, 50)*ScaleFactor;
 

                    player1.isFlicked = false;

                    target1.position = new Vector2(50, 600);
                    target1.isAlive = true;
                    target1.isInsideMe = false;
                    target1.isTouchingMe = false;
                    





                    break;

                case (2):
                    player1.isScoring = false;
                    player1.score = 0;
                    player1.ball.Position = new Vector2(200, 720)*ScaleFactor;
                    target1.position = new Vector2(200, 50);
                    player1.isFlicked = false;

                    target1.isAlive = true;
                    target1.isInsideMe = false;
                    target1.isTouchingMe = false;

                    obz1 = new ObstacleObject(Content.Load<Texture2D>("Sprites/Brick_200x50_Side"));
                    obz1.isAlive = true;

                    obz1.wall = obz1.CreateWall(world, ScaleFactor);
                    obz1.wall.Position  = new Vector2(200, 600)*ScaleFactor;
                    break;


                case (3):
                    player1.isScoring = false;
                    player1.score = 0;
                    player1.ball.Position = new Vector2(10, 400) * ScaleFactor;
                    target1.position = new Vector2(300, 700);
                    player1.isFlicked = false;

                    target1.isAlive = true;
                    target1.isInsideMe = false;
                    target1.isTouchingMe = false;
                    target1.position = new Vector2(300, 500);

                   //world.DestroyBody(obz1.wall);

                    obz2 = new ObstacleObject(Content.Load<Texture2D>("Sprites/Brick_600x150_Up"));
                    obz2.isAlive = true;

                    obz2.wall = obz2.CreateWall(world, ScaleFactor);
                    obz2.wall.Position = new Vector2(200, 400) * ScaleFactor;
                    break; 

                case(4):
                    this.Exit();
                    break;
            }

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
            groundShape.SetAsEdge(new Vector2(0, 8), new Vector2(4.8f, 8.0f));

            var groundBody = world.CreateBody(grounDef);
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

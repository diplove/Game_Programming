﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace ass1 {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {

        public int SCREEN_WIDTH;
        public int SCREEN_HEIGHT; 

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        BasicEffect effect;
        WorldModelManager worldModelManager;

        Player player;

        MouseState prevMouseState;

        SpriteFont informationFont;

        Random rand = new Random();

        int enemiesKilled;

        public Camera camera;

        bool gameOver;


        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            //graphics.ToggleFullScreen();
            Content.RootDirectory = "Content";
            gameOver = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {

            camera = new Camera(this, new Vector3(0, 200, 75), Vector3.Zero, Vector3.Up);
            Components.Add(camera);

            worldModelManager = new WorldModelManager(this);
            Components.Add(worldModelManager);

            player = new Player(this);

            prevMouseState = Mouse.GetState();

            enemiesKilled = 0;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            effect = new BasicEffect(GraphicsDevice);

            informationFont = Content.Load<SpriteFont>(@"Fonts\playerInfoFont");

            SCREEN_HEIGHT = Window.ClientBounds.Height;
            SCREEN_WIDTH = Window.ClientBounds.Width;

            Debug.WriteLine("Screen height = " + SCREEN_HEIGHT + " Screen width = " + SCREEN_WIDTH);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //THE LOGIC FOR DETERMINING THE POSITION OF THE MOUSE RELATIVE TO GROUND PLANE
            MouseState mouseState = Mouse.GetState();

            Vector3 nearsource = new Vector3((float)mouseState.Position.X, (float)mouseState.Position.Y, 0f);
            Vector3 farsource = new Vector3((float)mouseState.Position.X, (float)mouseState.Position.Y, 1f);

            Matrix world = Matrix.CreateTranslation(0, 0, 0);

            Vector3 nearPoint = GraphicsDevice.Viewport.Unproject(nearsource, camera.projection, camera.view, world);
            Vector3 farPoint = GraphicsDevice.Viewport.Unproject(farsource, camera.projection, camera.view, world);

            // Create a ray from the near clip plane to the far clip plane.
            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();
            Ray pickRay = new Ray(nearPoint, direction);

            // calcuate distance of plane intersection point from ray origin
            float? distance = pickRay.Intersects(Ground.groundPlane);

            if (distance != null) {
                Vector3 pickedPosition = nearPoint + direction * (float)distance;

                worldModelManager.selectionCube.ChangeSelectionPosition(new Vector3(pickedPosition.X, -pickedPosition.Z, pickedPosition.Y));
                //Debug.WriteLine("Cube position is now: X: " + pickedPosition.X + " Y: " + -pickedPosition.Z + " Z: " + pickedPosition.Y);
                //CREATION OF THE TURRET ON CLICK
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released) {
                    if (player.HasSuffucientMoney(Turret.COST)) {
                        worldModelManager.CreateTurret(new Vector3(pickedPosition.X, -pickedPosition.Z, pickedPosition.Y));
                        player.SpendMoney(Turret.COST);
                    } else {
                        //Player does not have enough money for turret
                    }
                    
                }

            }

            prevMouseState = mouseState;

            //Random enemy creation every frame, 1 in 100 chance of spawing
            if (rand.Next() % 100  == 0) {
                worldModelManager.CreateEnemy();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);

            spriteBatch.Begin();

            //Current round timer on screen
            spriteBatch.DrawString(informationFont, "Time: " + gameTime.TotalGameTime.Seconds, new Vector2(SCREEN_WIDTH - 100, 20), Color.Black);

            //Player and Tower information on screen
            player.DrawText(spriteBatch, informationFont);
            worldModelManager.tower.DrawText(spriteBatch, informationFont);

            //Message displayed when game is over
            if (gameOver) {
                spriteBatch.DrawString(informationFont, "THE TOWER HAS BEEN DESTROYED", new Vector2(SCREEN_WIDTH / 2 - 100, SCREEN_HEIGHT / 2), Color.Black);
            }



            spriteBatch.End();
        }

        /// <summary>
        /// Sets the game over value to be true. Will be called by another class when a losing
        /// condition is triggered
        /// </summary>
        public void GameOver() {
            gameOver = true;
        }

        /// <summary>
        /// Is called when a player has killed an enemy
        /// </summary>
        /// <param name="rewardForKilled"></param>
        public void EnemyKilled(int rewardForKilled) {
            player.GiveMoney(rewardForKilled);
            enemiesKilled++;
        }

    }


}

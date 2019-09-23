using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MJC_PE_CollisionDetection
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D beeTexture;
        Texture2D flowerTexture;
        Rectangle beeRec = new Rectangle(40, 40, 236, 330);
        int width;
        int height;
        Random random = new Random();
        List<Rectangle> flowers = new List<Rectangle>();



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 960;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            width = graphics.GraphicsDevice.Viewport.Width;
            height = graphics.GraphicsDevice.Viewport.Height;
            for (int i = 0; i < 5; i++)
            {
                Rectangle flower = new Rectangle(random.Next(0, width - 150), random.Next(0, height - 150), 300, 300);
                flowers.Add(flower);
            }

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            beeTexture = Content.Load<Texture2D>("bee");
            flowerTexture = Content.Load<Texture2D>("flower");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for(int i = 0; i < flowers.Count; i++)
            {
                Rectangle flowerHolder = flowers[i];
                if(i < 3)
                {
                    flowerHolder.X += 12;
                }
                else
                {
                    flowerHolder.Y += 12;
                }

                //Screen wrapping for flowers
                if(flowerHolder.Y < -150)
                {
                    flowerHolder.Y += height;
                }
                if (flowerHolder.Y > height - 150)
                {
                    flowerHolder.Y -= height;
                }
                if(flowerHolder.X < -150)
                {
                    flowerHolder.X += width;
                }
                if(flowerHolder.X > width - 150)
                {
                    flowerHolder.X -= width;
                }
                
                flowers[i] = flowerHolder;
            }

            ProcessInput();

            //Screen wrapping for player
            if (beeRec.Y < -150)
            {
                beeRec.Y += height;
            }
            if (beeRec.Y > height - 150)
            {
                beeRec.Y -= height;
            }
            if (beeRec.X < -150)
            {
                beeRec.X += width;
            }
            if (beeRec.X > width - 150)
            {
                beeRec.X -= width;
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(beeTexture, beeRec, Color.White);
            for(int i = 0; i < flowers.Count; i++)
            {
                if(beeRec.Intersects(flowers[i]) == true)
                {
                    spriteBatch.Draw(flowerTexture, flowers[i], Color.Blue);
                }
                else
                {
                    spriteBatch.Draw(flowerTexture, flowers[i], Color.White);
                }
                
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Method for processing keyboard input
        /// </summary>
        protected void ProcessInput()
        {
            KeyboardState kbState = Keyboard.GetState();


            if (kbState.IsKeyDown(Keys.Up) == true)
            {
                beeRec.Y -= 8;

            }
            if (kbState.IsKeyDown(Keys.Left) == true)
            {
                beeRec.X -= 8;
            }
            if (kbState.IsKeyDown(Keys.Down) == true)
            {
                beeRec.Y += 8;
            }
            if (kbState.IsKeyDown(Keys.Right) == true)
            {
                beeRec.X += 8;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Mundo7.Manager;
using Mundo7.Objects;

namespace Mundo7
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static float multi = 0;
        public static float interval = 0.003f;
        bool flag = false;
        Color blue = new Color(100, 149, 237);

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            //graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            SceneManager.Initialize(GraphicsDevice,
                Content,
                graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);

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

            // TODO: use this.Content to load your game content here
            SceneManager.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            KeyboardState keyState = Keyboard.GetState();


            if (keyState.IsKeyDown(Keys.Space))
            {
                rs.FillMode = FillMode.WireFrame;
                GraphicsDevice.RasterizerState = rs;

            }
            if (keyState.IsKeyUp(Keys.Space))
            {
                rs.FillMode = FillMode.Solid;
                GraphicsDevice.RasterizerState = rs;

            }

            // TODO: Add your update logic here
            SceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (multi >= 1 && !flag) flag = true;
            if (multi <= 0 && flag) flag = false;

            if (flag) multi -= interval; else multi += interval;

            blue.R = (byte)(100 + MathHelper.Lerp(0, 155, multi));
            blue.G = (byte)(149 + MathHelper.Lerp(0, 106, multi));
            blue.B = (byte)(237 + MathHelper.Lerp(0, 18, multi));

            GraphicsDevice.Clear(blue);

            // TODO: Add your drawing code here
            SceneManager.Draw();

            base.Draw(gameTime);
        }
    }
}

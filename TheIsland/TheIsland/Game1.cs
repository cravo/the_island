﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheIsland
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int mapWidth = 256;
        int mapHeight = 256;
        Texture2D texture;
        Texture2D whiteTexture;
        Map map;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            // TODO: Add your initialization logic here
            GenerateWhiteTexture();

            texture = null;
            map = new Map(mapWidth, mapHeight);
            map.BeginGeneration();

            base.Initialize();
        }

        private void GenerateWhiteTexture()
        {
            whiteTexture = new Texture2D(GraphicsDevice, 16, 16);
            Color[] colData = new Color[whiteTexture.Width * whiteTexture.Height];
            for (var i = 0; i < colData.Length; ++i)
            {
                colData[i] = Color.White;
            }
            whiteTexture.SetData<Color>(colData);
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

            UpdateMapGeneration();

            base.Update(gameTime);
        }

        void UpdateMapGeneration()
        {
            if(texture == null && map.Generated)
            {

                texture = new Texture2D(GraphicsDevice, mapWidth, mapHeight);
                Color[] colData = new Color[mapWidth * mapHeight];

                for (var y = 0; y < mapHeight; ++y)
                {
                    for (var x = 0; x < mapWidth; ++x)
                    {
                        var index = x + (mapWidth * y);
                        var data = map.GetMapDataAt(x, y);

                        if (data.Height <= 0.01f)
                        {
                            colData[index] = Color.Blue;
                        }
                        else
                        {
                            colData[index] = new Color(data.Height, data.Height, data.Height);
                        }
                    }
                }

                texture.SetData<Color>(colData);
            }
        }

        void DrawProgressBar(int x, int y, int w, int h)
        {
            spriteBatch.Draw(whiteTexture, new Rectangle(x, y, w, h), Color.White);
            spriteBatch.Draw(whiteTexture, new Rectangle(x + 4, y + 4, w - 8, h - 8), Color.Black);

            float proportionDone = map.GenerationProgress;

            spriteBatch.Draw(whiteTexture, new Rectangle(x + 6, y + 6, (int)(proportionDone * (float)(w - 12)), h - 12), Color.White);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            if(texture != null)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Height, GraphicsDevice.Viewport.Height), Color.White);
                spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin();
                DrawProgressBar(32, GraphicsDevice.Viewport.Height / 2 - 16, GraphicsDevice.Viewport.Width - 64, 32);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}

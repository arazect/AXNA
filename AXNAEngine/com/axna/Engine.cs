using System;
using System.Runtime.InteropServices;
using AXNAEngine.com.axna.managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna
{
    public class Engine : Game
    {

        protected readonly GraphicsDeviceManager Graphics;

        public Engine(int width, int height, bool isMouseVisible = true)
        {
            Graphics = new GraphicsDeviceManager(this);
            AXNA.WorldManager = new WorldManager();

            Content.RootDirectory = "Content";

            Graphics.PreferredBackBufferWidth = width;
            Graphics.PreferredBackBufferHeight = height;
            Graphics.ApplyChanges();

            IsMouseVisible = isMouseVisible;
        }

        protected override void Initialize()
        {
            base.Initialize();
           
            Type type = typeof(OpenTKGameWindow);
            System.Reflection.FieldInfo field = type.GetField("window", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null)
            {
                OpenTK.GameWindow window = field.GetValue(Window) as OpenTK.GameWindow;
                if (window != null)
                {
                    window.X = 0;
                    window.Y = 0;
                }
            }
        }

        protected override void LoadContent()
        {
            AXNA.Game = this;
            AXNA.Content = Content;
            AXNA.GraphicsDevice = Graphics.GraphicsDevice;

            AXNA.SpriteBatch = new SpriteBatch(Graphics.GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update(gameTime);
            AXNA.WorldManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            AXNA.WorldManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
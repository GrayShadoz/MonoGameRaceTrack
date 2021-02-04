using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MonoTube.Sprites;
using MonoTube.Models;
using System;
using System.Linq;
using MonoTube.States;
using MonoTube;

namespace MonoTube
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Random random;

        public static int ScreenWidth = 1920;
        public static int ScreenHeight = 1080;

        private State _currentState;
        private State _nextState;

        private List<Sprite> _sprites;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            random = new Random();

            this.Window.AllowUserResizing = false;
            this.Window.Title = "Top Down Racing!";
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            IsMouseVisible = true;

            // if (!GraphicsDevice.IsFullScreen)
            //GraphicsDevice.ToggleFullScreen();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentState = new MenuState(this, GraphicsDevice, Content);
            _currentState.LoadContent();
            _nextState = null;
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime) 
        {
            {
                if (_nextState != null)
                {
                    _currentState = _nextState;
                    _currentState.LoadContent();

                    _nextState = null;
                }

                _currentState.Update(gameTime);

                _currentState.PostUpdate(gameTime);
            }
            base.Update(gameTime);
        }

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentState.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoTube.Controls;
using MonoTube.States;
using MonoTube.Sprites;
using MonoTube.Models;
using MonoGame;

namespace MonoTube.States
{
    public class HighscoresState : State
    {
        private List<Component> components;

        private SpriteFont font;

        public HighscoresState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Font");

            var replayButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 200),
                Text = "Replay",
            };

            replayButton.Click += Button_Replay_Click;

            var mainMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 300),
                Text = "Back To Main Menu",
            };

            mainMenuButton.Click += Button_MainMenu_Click;

            components = new List<Component>()
            {
                replayButton,
                mainMenuButton,
            };
        }

        public override void LoadContent()
        {
            font = _content.Load<SpriteFont>("Font");
        }


        private void Button_Replay_Click(object sender, EventArgs args)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void Button_MainMenu_Click(object sender, EventArgs args)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Button_MainMenu_Click(this, new EventArgs());

            foreach (var component in components)
                component.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(font, "Highscores:\n" + GameState.carScore, new Vector2(400, 100), Color.Black);

            spriteBatch.End();
        }
    }
}
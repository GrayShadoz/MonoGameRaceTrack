﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoTube.Sprites;
using MonoTube.Models;
using MonoTube.States;
using MonoTube.Controls;
using System.ComponentModel;

namespace MonoTube.States
{
    public class MenuState : State
    {
        private List<Component> _components;

        private Texture2D menuBackGroundTexture;

        public MenuState(Game1 game, ContentManager content)
            : base(game, content)
        {
        }

        public override void LoadContent()
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("ButtonFonts/Font");
            menuBackGroundTexture = _content.Load<Texture2D>("Background/MainMenu");

            _components = new List<Component>()
            {

        new Button(buttonTexture, buttonFont)
        {
          Text = "1 Player",
          Position = new Vector2(Game1.ScreenWidth / 2, 400),
          Click = new EventHandler(Button_1Player_Clicked),
          //Layer = 0.1f
        },
        new Button(buttonTexture, buttonFont)
        {
          Text = "Highscores",
          Position = new Vector2(Game1.ScreenWidth / 2, 480),
          Click = new EventHandler(Button_Highscores_Clicked),
          //Layer = 0.1f
        },
        new Button(buttonTexture, buttonFont)
        {
          Text = "Quit",
          Position = new Vector2(Game1.ScreenWidth / 2, 520),
          Click = new EventHandler(Button_Quit_Clicked),
          //Layer = 0.1f
        },
      };
        }

        private void Button_1Player_Clicked(object sender, EventArgs args)
        {
            _game.ChangeState(new GameState(_game, _content)
            {
                PlayerCount = 1,
            });
        }

        private void Button_HighScores_Clicked(object sender, EventArgs args)
        {
            _game.ChangeState(new HighscoreState(_game, _content));
        }

        private void Button_Quit_Clicked(object sender, EventArgs args)
        {
            _game.Exit();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
        public override void PostUpdate(GameTime gameTime)
        {
            
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(menuBackGroundTexture, new Vector2(0, 0), Color.White);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}

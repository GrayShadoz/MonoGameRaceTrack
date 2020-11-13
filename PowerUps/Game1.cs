using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MonoTube.Sprites;
using MonoTube.Models;
using System;
using System.Linq;

namespace MonoTube
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;

        private ScoreManager scoreManager;

        private List<Sprite> _sprites;

        private float timer;

        public static Random Random;

        private Texture2D powerUpTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Random = new Random();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var carTexture = Content.Load<Texture2D>("Car_Right");

            powerUpTexture = Content.Load<Texture2D>("PowerUp");

            scoreManager = ScoreManager.Load();

            _sprites = new List<Sprite>()
      {
        new Car(carTexture)
        {
          Position = new Vector2(100, 100),
          Bullet = new Bullet(Content.Load<Texture2D>("Bullet")),
        },
      };
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime) 
        {
            foreach (var sprite in _sprites)
                sprite.Update(gameTime, _sprites);

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var sprite in _sprites.ToArray())
                sprite.Update(gameTime, _sprites);


            PostUpdate(gameTime);

            SpawnPowerUp();
        }

        private void SpawnPowerUp()
        {
            if (timer > 1)
            {
                timer = 0;

                var xPos = Random.Next(0, ScreenWidth - powerUpTexture.Width);
                var yPos = Random.Next(0, ScreenHeight - powerUpTexture.Height);

                _sprites.Add(new Sprite(powerUpTexture)
                {
                    Position = new Vector2(xPos, yPos),
                });
            }
        }

        public void PostUpdate(GameTime gameTime)
        {
            var collidableSprites = _sprites.Where(c => c is ICollidable);

            foreach (var spriteA in collidableSprites)
            {
                foreach (var spriteB in collidableSprites)
                {
                    // Don't do anything if they're the same sprite!
                    if (spriteA == spriteB)
                        continue;

                    if (!spriteA.CollisionArea.Intersects(spriteB.CollisionArea))
                        continue;

                    if (spriteA.Intersects(spriteB))
                        ((ICollidable)spriteA).OnCollide(spriteB);
                }
            }

            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (var sprite in _sprites)
                sprite.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
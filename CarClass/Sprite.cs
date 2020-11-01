using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyMonoGame.Models;

namespace MyMonoGame.Sprites
{
    public class Sprite
    {
        protected Texture2D _texture;

        public Vector2 Origin;
        public Vector2 Position;
        public Vector2 Velocity;
        protected float _rotation;

        public float RotationVelocity = 3f;
        public float LinearVelocity = 4f;

        public Input Input;
        public bool IsRemoved = false;

        protected KeyboardState _currentKey;
        protected KeyboardState _previousKey;

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public float Rotation
        {
            get { return _rotation; }
            set
            {
                _rotation = value;
            }
        }

        public Matrix Trasnform
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-Origin, 0)) *
                    Matrix.CreateRotationZ(_rotation) *
                    Matrix.CreateTranslation(new Vector3(Position, 0));
            }
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
            {
                spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, Origin, 1, SpriteEffects.None, 0);
            }
        }
    }
}
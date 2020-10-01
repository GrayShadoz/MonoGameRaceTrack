using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyMonoGame.Sprites;
using MyMonoGame.Models;

namespace MyMonoGame.Sprites
{
    public class Car : Sprite, ICollidable
    {
        public int Health { get; set; }

        public float Speed { get; set; }

        public int Score;

        public bool isDead
        {
            get
            {
                return Health <= 0;
            }
        }

        public Car(Texture2D texture)
            : base(texture)
        {
            Speed = 5f;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();

            if (isDead)
                return;

            foreach (var sprite in sprites)
            {
                if (sprite is Car)
                    continue;
                
                if (sprite.Rectangle.Intersects(this.Rectangle))
                {
                    Speed++;
                    sprite.IsRemoved = true;
                }
            }
        }

        public virtual void OnCollide(Sprite sprite)
        {
            throw new NotImplementedException();
        }

        private void Move()
        {
            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                _rotation -= MathHelper.ToRadians(RotationVelocity);
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
                _rotation += MathHelper.ToRadians(RotationVelocity);

            var direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                Position += direction * LinearVelocity;

            if (Keyboard.GetState().IsKeyDown(Keys.S))
                Position -= direction * LinearVelocity;

            Position = Vector2.Clamp(Position, new Vector2(0, 0), new Vector2(Game1.ScreenWidth - this.Rectangle.Width, Game1.ScreenHeight - this.Rectangle.Height));
        }
    }
}

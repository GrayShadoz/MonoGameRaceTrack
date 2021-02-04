using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTube
{
    public class Score
    {
        public int Score1;

        private SpriteFont scorefont;

        public Score(SpriteFont font)
        {
            scorefont = font;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(scorefont, Score1.ToString(), new Vector2(320, 70), Color.Black);
        }
    }
}
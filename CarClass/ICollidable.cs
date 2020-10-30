using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace MyMonoGame.Sprites
{
    public interface ICollidable
    {
        void OnCollide(Sprite sprite);
    }
}

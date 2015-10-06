using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SociaGrounds.Model.World
{
    public abstract class Asset
    {
        // The position of the asset
        protected Vector2 _Position;

        // The rectangle of the asset for collsion detection
        protected Rectangle _Rect;
        public Rectangle Rect => _Rect;

        protected Asset(Vector2 position)
        {
            _Position = position;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SociaGrounds.Model.World
{
    public abstract class Asset
    {
        // The position of the asset
        protected Vector2 position;

        // The rectangle of the asset for collsion detection
        protected Rectangle rect;
        public Rectangle Rect
        {
            get { return rect; }
        }

        protected Asset(Vector2 position)
        {
            this.position = position;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}

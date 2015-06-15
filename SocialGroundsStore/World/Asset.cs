using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SocialGroundsStore.World
{
    public abstract class Asset
    {
        // The position of the asset
        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        // The rectangle of the asset for collsion detection
        protected Rectangle rect;
        public Rectangle Rect
        {
            get { return rect; }
        }

        // Boolean to check if the player can walk through the object or not
        protected bool isSolid;
        public bool IsSolid
        {
            get { return isSolid; }
        }


        public Asset(Vector2 position)
        {
            this.position = position;
        }


        public abstract void Draw(SpriteBatch spriteBatch);
    }
}

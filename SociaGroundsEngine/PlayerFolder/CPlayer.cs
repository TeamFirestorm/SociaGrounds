using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SociaGroundsEngine.GUI;
using SociaGroundsEngine.World;

namespace SociaGroundsEngine.PlayerFolder
{
    public abstract class CPlayer
    {
        protected CAnimation animation;
        public CAnimation Animation
        {
            get { return animation; }
        }

        protected NetConnection connection;
        public NetConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        protected int speed;
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        protected Rectangle rect;
        public Rectangle Rect
        {
            get { return rect; }
        }

        // Abstract method to update the player
        public abstract void Update(GameTime gameTime, Ui ui, Viewport viewPort, Map map);

        // Abstract method to draw the player
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}

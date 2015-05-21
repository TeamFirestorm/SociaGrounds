using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SociaGroundsEngine
{
    abstract class CPlayer
    {
        protected CAnimation animation;
        public CAnimation Animation
        {
            get { return animation; }
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
        public abstract void update(GameTime gameTime);

        // Abstract method to draw the player
        public abstract void draw(SpriteBatch spriteBatch);
    }
}

using System.Collections.Generic;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SocialGroundsStore.GUI;
using SocialGroundsStore.World;

namespace SocialGroundsStore.PlayerFolder
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

        // Chat properties
        protected SpriteFont font;
        protected float chatCounter;

        protected string chatMessage;

        protected bool changedText;

        public string ChatMessage
        {
            get { return chatMessage; }
            set
            {
                chatMessage = value;
                changedText = true;
            }
        }

        // Abstract method to update the player
        public abstract void Update(GameTime gameTime, Ui ui, Viewport viewPort, Map map, KeyboardState keyState);

        // Abstract method to draw the player
        public abstract void Draw(SpriteBatch spriteBatch);

        public int Id { get; set; }

        protected Queue<Vector2> newQPosition;
    }
}

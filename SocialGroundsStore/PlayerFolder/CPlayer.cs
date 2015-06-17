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
        // For the animation of the player
        protected CAnimation animation;
        public CAnimation Animation
        {
            get { return animation; }
        }

        // 
        protected NetConnection connection;
        public NetConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        // The walkspeed of the player
        protected int speed;
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        // The current position of the player
        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        // The rectangle of the player used for collision detection
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

        // ID of the player for the server
        public int Id { get; set; }

        // A queue that 
        protected Queue<Vector2> newQPosition;
    }
}

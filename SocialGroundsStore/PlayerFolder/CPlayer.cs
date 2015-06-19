using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SocialGroundsStore.GUI;
using SocialGroundsStore.Multiplayer.Lidgren.Network;
using SocialGroundsStore.World;

namespace SocialGroundsStore.PlayerFolder
{
    /// <summary>
    /// This is the abstract class for both the myPlayer and Foreignplayer class.
    /// It's used to tie these two together.
    /// </summary>
    public abstract class CPlayer
    {
        // For the animation of the player
        protected CAnimation animation;

        protected NetConnection connection;
        public NetConnection Connection
        {
            get { return connection; }
        }

        // The walkspeed of the player
        protected int speed;

        // The current position of the player
        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }

        // The rectangle of the player used for collision detection
        protected Rectangle rect;

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
        public abstract void Update(GameTime gameTime, Gui ui, Viewport viewPort, Map map, KeyboardState keyState);

        // Abstract method to draw the player
        public abstract void Draw(SpriteBatch spriteBatch);

        // ID of the player for the server
        public int Id { get; set; }

        // A queue that holds the new position of the foreign player
        protected Queue<Vector2> newQPosition;

        public void DrawText(SpriteBatch spriteBatch)
        {
            if (!string.IsNullOrEmpty(ChatMessage))
            {
                spriteBatch.DrawString(font, chatMessage, new Vector2(position.X - (chatMessage.Length * 4) + 20, position.Y - 10), Color.White);
            }
        }
    }
}

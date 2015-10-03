using System.Collections.Generic;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SociaGrounds.Model.Controllers;
using SociaGrounds.Model.World;

namespace SociaGrounds.Model.Players
{
    /// <summary>
    /// This is the abstract class for both the myPlayer and Foreignplayer class.
    /// It's used to tie these two together.
    /// </summary>
    public abstract class CPlayer
    {
        // For the animation of the player
        protected CAnimation Animation;

        protected NetConnection connection;
        public NetConnection Connection
        {
            get { return connection; }
        }

        // The walkspeed of the player
        protected int Speed;

        // The current position of the player
        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }

        // The rectangle of the player used for collision detection
        protected Rectangle Rect;

        protected float ChatCounter;

        protected string chatMessage;

        protected bool ChangedText;

        public string ChatMessage
        {
            get { return chatMessage; }
            set
            {
                chatMessage = value;
                ChangedText = true;
            }
        }

        // Abstract method to update the player
        public abstract void Update(GameTime gameTime, Map map, KeyboardState state = default(KeyboardState));

        // Abstract method to draw the player
        public abstract void Draw(SpriteBatch spriteBatch);

        // ID of the player for the server
        public int Id { get; set; }

        // A queue that holds the new position of the foreign player
        protected Queue<Vector2> NewQPosition;

        public void DrawText(SpriteBatch spriteBatch)
        {
            if (!string.IsNullOrEmpty(ChatMessage))
            {
                Vector2 textSize = Fonts.SmallFont.MeasureString(chatMessage);

                spriteBatch.DrawString(Fonts.SmallFont, chatMessage, new Vector2(position.X - (textSize.X /2f) + 32, position.Y - 10), Color.White);
            }
        }
    }
}

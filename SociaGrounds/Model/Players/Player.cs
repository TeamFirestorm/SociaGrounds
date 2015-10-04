using System.Collections.Generic;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SociaGrounds.Model.Controllers;

namespace SociaGrounds.Model.Players
{
    /// <summary>
    /// This is the abstract class for both the myPlayer and Foreignplayer class.
    /// It's used to tie these two together.
    /// </summary>
    public abstract class Player
    {
        // For the animation of the player
        protected Animation _Animation;

        public NetConnection Connection { get; set; }

        // The walkspeed of the player
        protected int _Speed;

        protected Vector2 _Position;

        // The current position of the player
        public Vector2 Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        // The rectangle of the player used for collision detection
        protected Rectangle _Rect;

        protected float _ChatCounter;

        protected string _ChatMessage;

        protected bool _ChangedText;

        public string ChatMessage
        {
            get { return _ChatMessage; }
            set
            {
                _ChatMessage = value;
                _ChangedText = true;
            }
        }

        protected SpriteFont _Font = Fonts.Medium;

        // Abstract method to update the player
        public virtual void Update(GameTime gameTime, KeyboardState state = default(KeyboardState))
        {
            _Animation.Position = _Position;

            _Rect = new Rectangle((int)_Position.X, (int)_Position.Y, 64, 64);

            // Show the chat message for a certain amount of time
            if (!string.IsNullOrEmpty(_ChatMessage))
            {
                _ChatCounter += gameTime.ElapsedGameTime.Milliseconds;

                if (_ChangedText)
                {
                    _ChatCounter = 0;
                    _ChangedText = false;
                }

                // Then flush the chat message and reset the counter
                if (_ChatCounter >= 8000)
                {
                    _ChatMessage = "";
                    _ChatCounter = 0;
                }
            }
        }

        // Abstract method to draw the player
        public abstract void Draw(SpriteBatch spriteBatch);

        // ID of the player for the server
        public int Id { get; set; }

        // A queue that holds the new position of the foreign player
        protected Queue<Vector2> _NewQPosition;

        public void DrawText(SpriteBatch spriteBatch)
        {
            if (!string.IsNullOrEmpty(ChatMessage))
            {
                Vector2 textSize = _Font.MeasureString(_ChatMessage);

                spriteBatch.DrawString(_Font, _ChatMessage, new Vector2(Position.X - (textSize.X /2f) + 32, Position.Y - 10), Color.White);
            }
        }
    }
}

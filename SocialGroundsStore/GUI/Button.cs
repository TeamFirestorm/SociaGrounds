using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SocialGroundsStore.GUI
{
    /// <summary>
    /// This class is used to manage all the buttons of the application.
    /// </summary>
    class Button
    {
        // Position of the button
        public Vector2 Position { get; set; }

        // Scaling properties
        private readonly float _scale;
        private readonly float _width;

        public float Width
        {
            get { return _width; }
        }

        // Neutral textures
        private readonly Texture2D _left;
        private readonly Texture2D _mid;
        private readonly Texture2D _right;

        // Text
        private readonly SpriteFont _font;
        private readonly string _text;
        public bool ClickedThis { get; set; }

        // Rectangle for detection
        private Rectangle _rect;

        private readonly float _textPosition;
        
        public Button(ContentManager content, Vector2 position, string text, float scale)
        {
            // Neutral textures initialize
            _left = content.Load<Texture2D>("GUI/Button/StandardButtonLeft");
            _mid = content.Load<Texture2D>("GUI/Button/StandardButtonMiddle");
            _right = content.Load<Texture2D>("GUI/Button/StandardButtonRight");

            // Text
            _text = text;
            _font = content.Load<SpriteFont>("SociaGroundsFont");

            // The position, width and scale
            Position = position;

            _width = text.Length;

            _scale = scale;

            _textPosition = 22;

            _rect = new Rectangle((int)(position.X * scale), (int)(position.Y * scale), (int)((_left.Width * scale) + ((_mid.Width * _width) * scale) + (_right.Width * scale)), (int)(_mid.Height * scale));
        }

        public Button(ContentManager content, Vector2 position, string text, float scale, float fixedWidth)
        {
            // Neutral textures initialize
            _left = content.Load<Texture2D>("GUI/Button/StandardButtonLeft");
            _mid = content.Load<Texture2D>("GUI/Button/StandardButtonMiddle");
            _right = content.Load<Texture2D>("GUI/Button/StandardButtonRight");

            // Text
            _text = text;
            _font = content.Load<SpriteFont>("SociaGroundsFont");

            // The position, width and scale
            Position = position;
            _width = fixedWidth;
            _scale = scale;

            _textPosition = (((_width * _mid.Width) /2) - ((text.Length * _mid.Width) /3f));

            _rect = new Rectangle((int)(position.X * scale), (int)(position.Y * scale), (int)((_left.Width * scale) + ((_mid.Width * _width) * scale) + (_right.Width * scale)), (int)(_mid.Height * scale));
        }

        public void Update(MouseState mouseState)
        {
            IsClicked(mouseState);
        }

        // Trigger if the button is released
        // Use this method as the event trigger
        public bool IsClicked(MouseState mouseState)
        {
            // Check if the position is pressed within the button
            if (mouseState.Position.X >= _rect.Left &&
                mouseState.Position.X <= _rect.Right &&
                mouseState.Position.Y >= _rect.Top &&
                mouseState.Position.Y <= _rect.Bottom)
            {
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {       
                // Drawing the left part
                spriteBatch.Draw(_left, Position, null, Color.White, 0f, new Vector2(0, 0), new Vector2(_scale, _scale), SpriteEffects.None, 0f);

                // Drawing the mid part
                for (int i = 0; i < _width; i++)
                {
                    spriteBatch.Draw(_mid, new Vector2(Position.X + (_left.Width * _scale) + ((_mid.Width * _scale) * i), Position.Y), null, Color.White, 0f, new Vector2(0, 0), new Vector2(_scale, _scale), SpriteEffects.None, 0f);
                }

                // Drawing the right part
                spriteBatch.Draw(_right, new Vector2(Position.X + (_left.Width * _scale) + ((_mid.Width * _scale) * _width), Position.Y), null, Color.White, 0f, new Vector2(0, 0), new Vector2(_scale, _scale), SpriteEffects.None, 0f);

                // Drawing the text
                spriteBatch.DrawString(_font, _text, new Vector2(Position.X + (_textPosition * _scale), Position.Y + (40 * _scale)), Color.Black, 0f, new Vector2(0, 0), new Vector2(_scale / 0.5f, _scale / 0.5f), SpriteEffects.None, 0f);
            }
        }
}

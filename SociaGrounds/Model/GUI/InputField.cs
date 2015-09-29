using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SociaGrounds.Model.GUI
{
    public class InputField
    {
        /// <summary>
        /// Textures for the inputfield
        /// </summary>
        private readonly Texture2D _textFieldEnd;
        private readonly Texture2D _textFieldMiddle;

        // Position of the inputfield
        private Vector2 _position;

        public Vector2 Position
        {
            set { _position = value; }
        }

        // String that will be updated in the textfield
        private readonly SpriteFont _font;
        private string _text;

        // Amount of middle pieces in the textfield
        private readonly int _width;

        // Scale of the inputfield
        private readonly float _scale;

        public InputField(ContentManager content, Vector2 position, int width, float scale)
        {
            // Loading the textures
            _textFieldEnd = content.Load<Texture2D>("SociaGrounds/GUI/Inputfield/LeftTextField");
            _textFieldMiddle = content.Load<Texture2D>("SociaGrounds/GUI/Inputfield/MiddleTextfield");

            // Initializing the position
            _position = position;

            // Loading the text stuff
            _font = content.Load<SpriteFont>("SociaGrounds/SociaGroundsFont");
            _text = "";

            _width = width;
            _scale = scale;
        }

        public void Update(string text)
        {
            // Update the text in the textfield
            _text = text;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Left area draw
            spriteBatch.Draw(_textFieldEnd, _position, null, Color.White, 0f, new Vector2(0, 0), new Vector2(_scale, _scale), SpriteEffects.None, 0f);

            // Middle area draw
            for (int i = 0; i < _width; i++)
            {
                spriteBatch.Draw(_textFieldMiddle, new Vector2(_position.X + (_textFieldEnd.Width * _scale) + ((_textFieldMiddle.Width * _scale) * i), _position.Y), null, Color.White, 0f, new Vector2(0, 0), new Vector2(_scale, _scale), SpriteEffects.None, 0f);
            }

            // Right area draw
            spriteBatch.Draw(_textFieldEnd, new Vector2(_position.X + (_textFieldEnd.Width * _scale) + ((_textFieldMiddle.Width * _scale) * _width), _position.Y), null, Color.White, 0f, new Vector2(0, 0), new Vector2(_scale, _scale), SpriteEffects.None, 0f);

            // Text draw
            spriteBatch.DrawString(_font, _text, new Vector2(_position.X + (30 * _scale), _position.Y + (80 * _scale)), Color.White, 0f, new Vector2(0, 0), new Vector2(_scale / 0.07f, _scale / 0.07f), SpriteEffects.None, 0f);
        }
    }
}

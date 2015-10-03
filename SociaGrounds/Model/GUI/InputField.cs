using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SociaGrounds.Model.Controllers;

namespace SociaGrounds.Model.GUI
{
    public class InputField
    {
        /// <summary>
        /// Textures for the inputfield
        /// </summary>
        private readonly Texture2D _textFieldMiddle;

        // Position of the inputfield
        private Vector2 _position;

        // String that will be updated in the textfield
        private string _text;

        // Amount of middle pieces in the textfield
        private readonly int _width;

        // Scale of the inputfield
        private readonly float _scale;

        public InputField(ContentManager content, Viewport viewport, int width, float scale)
        {
            // Loading the textures
            _textFieldMiddle = content.Load<Texture2D>("SociaGrounds/GUI/Inputfield/MiddleTextfield");

            _text = "";

            _width = width;
            _scale = scale;

            // Initializing the position
            _position.X = ((viewport.Width / 2f) - (((_textFieldMiddle.Width * _scale) * width ) ) /2f);
            _position.Y = (viewport.Height - (viewport.Height / 5f));
        }

        public void Update(string text)
        {
            // Update the text in the textfield
            _text = text;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Middle area draw
            for (int i = 0; i < _width; i++)
            {
                spriteBatch.Draw(_textFieldMiddle, new Vector2(_position.X + ((_textFieldMiddle.Width * _scale) * i), _position.Y), null, Color.White, 0f, new Vector2(0, 0), new Vector2(_scale, _scale), SpriteEffects.None, 0f);
            }

            // Text draw
            spriteBatch.DrawString(Fonts.LargeFont, _text, new Vector2(_position.X + (30 * _scale), _position.Y + (80 * _scale)), Color.White);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SociaGrounds.Model.GUI
{
    public class InputField
    {
        /// <summary>
        /// Textures for the inputfield
        /// </summary>
        private static readonly Texture2D[][] TEXTURES;

        private readonly SpriteFont _font;

        // Position of the inputfield
        private Vector2 _position;

        public float PositionY
        {
            get { return _position.Y; }
            set { _position.Y = value; }
        } 

        // String that will be updated in the textfield
        private string _text;

        // Amount of pieces in the textfield
        private readonly int _width;

        // Scale of the inputfield
        private readonly float _scale;

        static InputField()
        {
            TEXTURES = new[]
            {
                new []
                {
                    Game1.StaticContent.Load<Texture2D>("SociaGrounds/GUI/Inputfield/MiddleTextfield")
                }
            };
        }

        public InputField(Vector2 position, int width, float scale, SpriteFont font)
        {
            _text = "";
            _width = width;
            _scale = scale;
            _font = font;
            _position = position;
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
                spriteBatch.Draw(TEXTURES[0][0], new Vector2(_position.X + ((TEXTURES[0][0].Width * _scale) * i), _position.Y), null, Color.White, 0f, new Vector2(0, 0), new Vector2(_scale, _scale), SpriteEffects.None, 0f);
            }

            // Text draw
            spriteBatch.DrawString(_font, _text, new Vector2(_position.X + 10 * _scale, _position.Y + (24 * _scale)), Color.White);
        }
    }
}

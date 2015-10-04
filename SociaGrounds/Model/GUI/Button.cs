using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using SociaGrounds.Model.Controllers;
using static SociaGrounds.Model.Controllers.SMouse;
using static SociaGrounds.Model.Controllers.STouch;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace SociaGrounds.Model.GUI
{
    /// <summary>
    /// This class is used to manage all the buttons of the application.
    /// </summary>
    public class Button
    {
        // Position of the button
        public Vector2 Position { get; set; }

        // Scaling properties
        private readonly float _scale;

        public float Parts { get; }

        //Textures        
        public static readonly Texture2D[][] Textures;

        private readonly string _text;

        // Rectangle for detection
        private readonly Rectangle _hitBox;

        //The currentstate of the button
        private int _buttonState;

        private Vector2 _textPosition;

        private bool _isTouched;

        private readonly SpriteFont _font;

        private Vector2 _textureSize;

        static Button()
        {
            Textures = new[]
            {
                new []
                {
                    Game1.StaticContent.Load<Texture2D>("SociaGrounds/GUI/Button/StandardButtonLeft"),
                    Game1.StaticContent.Load<Texture2D>("SociaGrounds/GUI/Button/StandardButtonMiddle"),
                    Game1.StaticContent.Load<Texture2D>("SociaGrounds/GUI/Button/StandardButtonRight")
                },
                new []
                {
                    Game1.StaticContent.Load<Texture2D>("SociaGrounds/GUI/Button/PressedButtonLeft"),
                    Game1.StaticContent.Load<Texture2D>("SociaGrounds/GUI/Button/PressedButtonMiddle"),
                    Game1.StaticContent.Load<Texture2D>("SociaGrounds/GUI/Button/PressedButtonRight")
                },
            };
        }

        public Button(Vector2 position, string text, float scale, float parts, SpriteFont font)
        {
            // The position, width and scale
            Position = position;
            Parts = parts + 2;
            _scale = scale;

            // Text and font
            _text = text;
            _font = font;


            _textureSize = new Vector2(Textures[0][1].Width * _scale, Textures[0][1].Height * _scale);

            //Sets the position of the text
            Vector2 textSize = (_font.MeasureString(_text) * scale);

            _textPosition = new Vector2((((Parts * _textureSize.X) - textSize.X) / 2f) * scale, ((_textureSize.Y - textSize.Y) / 2));

            //Sets the position of this buttons hitbox
            _hitBox = new Rectangle((int)(position.X), (int)(position.Y), (int)(_textureSize.X * Parts), (int)_textureSize.Y);

            //Set buttonstate to not clicked
            _buttonState = 0;
        }



        ///// <summary>
        ///// CCreates a new button with the lenght of the text as width
        ///// </summary>
        ///// <param name="content"></param>
        ///// <param name="position"></param>
        ///// <param name="text"></param>
        ///// <param name="scale"></param>
        //public Button(ContentManager content, Vector2 position, string text, float scale) : this(content, position, text, scale, text.Length)
        //{
        //}

        //public Button(ContentManager content, Viewport viewport, string text, float scale): this(content, new Vector2(0 + 20, viewport.Height - (viewport.Height /8f)), text, scale, Fonts.LargeFont.MeasureString(text).X)
        //{
        //    Vector2 temp = new Vector2(0 + 20, viewport.Height - (viewport.Height/8f));

        //    _hitBox = new Rectangle((int)temp.X, (int)temp.Y, (int)((_tetxures[0][0].Width * scale) + ((_tetxures[0][1].Width * Width) * scale) + (_tetxures[0][2].Width * scale)), (int)(_tetxures[0][1].Height * scale));
        //}

        ///// <summary>
        ///// Creates a new Button with a fixed width
        ///// </summary>
        ///// <param name="content"></param>
        ///// <param name="position"></param>
        ///// <param name="text"></param>
        ///// <param name="scale"></param>
        ///// <param name="fixedWidth"></param>
        //public Button(ContentManager content, Vector2 position, string text, float scale, float fixedWidth)
        //{
        //    // Text
        //    _text = text;

        //    // The position, width and scale
        //    Position = position;
        //    Width = fixedWidth;
        //    _scale = scale;

        //    Vector2 textSize = Fonts.LargeFont.MeasureString(_text);

        //    _textPosition = new Vector2(position.X + ((((Width + 2)* _tetxures[0][1].Width) - textSize.X)/2f), position.Y + ((_tetxures[0][1].Height - textSize.Y)/2));

        //    _hitBox = new Rectangle((int)(position.X * scale), (int)(position.Y * scale), (int)((_tetxures[0][0].Width * scale) + ((_tetxures[0][1].Width * Width) * scale) + (_tetxures[0][2].Width * scale)), (int)(_tetxures[0][1].Height * scale));

        //    _buttonState = 0;
        //}

        public bool CheckButtonSelected()
        {
            if (Static.ThisDevice != "Windows.Desktop")
            {
                return IsTouchReleased();
            }

            return IsClicked();
        }

        public bool IsTouchReleased()
        {
            if (NewTouchLocations.Count == 0 && _isTouched)
            {
                _isTouched = false;
                _buttonState = 0;
                return true;
            }

            if (NewTouchLocations.Count == 0) return false;

            // Loop through all the locations where touch is possible
            foreach (TouchLocation touch in NewTouchLocations)
            {
                // Check if the position is pressed within the button
                if (new Rectangle((int)touch.Position.X, (int)touch.Position.Y, 1, 1).Intersects(_hitBox))
                {
                    _buttonState = 1;
                    _isTouched = true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the mousecursor hovers over this buttons hitbox
        /// </summary>
        /// <returns></returns>
        private bool IsClicked()
        {
            // Check if the current mouse position is over hitbox
            if (new Rectangle(NewMouseState.X, NewMouseState.Y, 1, 1).Intersects(_hitBox))
            {
                _buttonState = 1;
                if (NewMouseState.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
                return false;
            }
            _buttonState = 0;
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 tempPos = Position;

            // Drawing the left part
            spriteBatch.Draw(Textures[_buttonState][0], tempPos, null, Color.White, 0f, new Vector2(0, 0), new Vector2(_scale, _scale), SpriteEffects.None, 0f);

            tempPos.X += _textureSize.X;

            // Drawing the mid part
            for (int i = 1; i < Parts -1; i++)
            {
                spriteBatch.Draw(Textures[_buttonState][1], tempPos, null, Color.White, 0f, new Vector2(0, 0), new Vector2(_scale, _scale), SpriteEffects.None, 0f);
                tempPos.X += _textureSize.X;
            }

            // Drawing the right part
            spriteBatch.Draw(Textures[_buttonState][2], tempPos, null, Color.White, 0f, new Vector2(0, 0), new Vector2(_scale, _scale), SpriteEffects.None, 0f);

            // Drawing the text
            spriteBatch.DrawString(_font, _text, new Vector2(Position.X + _textPosition.X, Position.Y + _textPosition.Y), Color.Black);
        }
    }
}

using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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

        public float Width { get; }

        //Textures        
        private readonly Texture2D[][] _tetxures;

        // Text
        private readonly SpriteFont _font;
        private readonly string _text;

        // Rectangle for detection
        private readonly Rectangle _hitBox;

        //The currentstate of the button
        private int _buttonState;

        private readonly float _textPosition;

        private bool _isTouched;
        

        /// <summary>
        /// CCreates a new button with the lenght of the text as width
        /// </summary>
        /// <param name="content"></param>
        /// <param name="position"></param>
        /// <param name="text"></param>
        /// <param name="scale"></param>
        public Button(ContentManager content, Vector2 position, string text, float scale) 
            : this(content, position, text, scale, text.Length)
        {
        }

        public Button(ContentManager content, Viewport viewport, string text, float scale): this(content, new Vector2(0 + 20, viewport.Height - (viewport.Height /8f)), text, scale, text.Length)
        {
            Vector2 temp = new Vector2(0 + 20, viewport.Height - (viewport.Height/8f));

            _hitBox = new Rectangle((int)temp.X, (int)temp.Y, (int)((_tetxures[0][0].Width * scale) + ((_tetxures[0][1].Width * Width) * scale) + (_tetxures[0][2].Width * scale)), (int)(_tetxures[0][1].Height * scale));
        }

        /// <summary>
        /// Creates a new Button with a fixed width
        /// </summary>
        /// <param name="content"></param>
        /// <param name="position"></param>
        /// <param name="text"></param>
        /// <param name="scale"></param>
        /// <param name="fixedWidth"></param>
        public Button(ContentManager content, Vector2 position, string text, float scale, float fixedWidth)
        {
            _tetxures = new[]
            {
                new []
                {
                    content.Load<Texture2D>("SociaGrounds/GUI/Button/StandardButtonLeft"),
                    content.Load<Texture2D>("SociaGrounds/GUI/Button/StandardButtonMiddle"),
                    content.Load<Texture2D>("SociaGrounds/GUI/Button/StandardButtonRight")
                },
                new []
                {
                    content.Load<Texture2D>("SociaGrounds/GUI/Button/PressedButtonLeft"),
                    content.Load<Texture2D>("SociaGrounds/GUI/Button/PressedButtonMiddle"),
                    content.Load<Texture2D>("SociaGrounds/GUI/Button/PressedButtonRight")
                },
            };

            // Text
            _text = text;
            _font = content.Load<SpriteFont>("SociaGrounds/SociaGroundsFont");

            // The position, width and scale
            Position = position;
            Width = fixedWidth;
            _scale = scale;

            _textPosition = (((Width * _tetxures[0][1].Width) /2) - ((text.Length * _tetxures[0][1].Width) /3f));

            _hitBox = new Rectangle((int)(position.X * scale), (int)(position.Y * scale), (int)((_tetxures[0][0].Width * scale) + ((_tetxures[0][1].Width * Width) * scale) + (_tetxures[0][2].Width * scale)), (int)(_tetxures[0][1].Height * scale));

            _buttonState = 0;
        }

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
                // Drawing the left part
                spriteBatch.Draw(_tetxures[_buttonState][0], Position, null, Color.White, 0f, new Vector2(0, 0), new Vector2(_scale, _scale), SpriteEffects.None, 0f);

                // Drawing the mid part
                for (int i = 0; i < Width; i++)
                {
                    spriteBatch.Draw(_tetxures[_buttonState][1], new Vector2(Position.X + (_tetxures[0][0].Width * _scale) + ((_tetxures[0][1].Width * _scale) * i), Position.Y), null, Color.White, 0f, new Vector2(0, 0), new Vector2(_scale, _scale), SpriteEffects.None, 0f);
                }

                // Drawing the right part
                spriteBatch.Draw(_tetxures[_buttonState][2], new Vector2(Position.X + (_tetxures[0][0].Width * _scale) + ((_tetxures[0][1].Width * _scale) * Width), Position.Y), null, Color.White, 0f, new Vector2(0, 0), new Vector2(_scale, _scale), SpriteEffects.None, 0f);

                // Drawing the text
                spriteBatch.DrawString(_font, _text, new Vector2(Position.X + (_textPosition * _scale), Position.Y + (40 * _scale)), Color.Black, 0f, new Vector2(0, 0), new Vector2(_scale / 0.5f, _scale / 0.5f), SpriteEffects.None, 0f);
            }
        }
}

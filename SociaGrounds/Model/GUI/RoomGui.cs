using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SociaGrounds.Model.Controllers;
using static SociaGrounds.Model.Controllers.Static;

namespace SociaGrounds.Model.GUI
{
    public class RoomGui
    {
        private readonly InputField _inputField;
        private readonly Button _showHideButton;

        // Boolean to check if the keyboard is up
        private bool _isKeyboardUp;

        private readonly List<Keys> _virtualKeys;

        /// <summary>
        /// This class is used to build the user interface of the application
        /// </summary>
        public RoomGui()
        {
            _virtualKeys = new List<Keys>();

            // The stats for the inputfield
            float scale = 0.5f;
            Vector2 sprite = new Vector2(20 * scale, 120 * scale);
            int width = (int) (((ScreenSize.Width/2d)/sprite.X) + 6);
            Vector2 position = new Vector2(((ScreenSize.Width) - (sprite.X * width))/2, ScreenSize.Height - sprite.Y - 80);

            // Initialize controls
            _inputField = new InputField(position, width, scale, Fonts.LargeFont);
            string text = "Show/Hide";

            _showHideButton = new Button(new Vector2(20, position.Y), text, scale, text.Length, Fonts.SmallFont);

            // Creates the bbuttons of the virtual keyboard
            CreateKeyBoard(position, sprite, scale);

            // Set the virtualkeyboard to not show
            _isKeyboardUp = false;
        }

        /// <summary>
        /// Update method for the keyboard
        /// </summary>
        public void Update(GameTime gameTime)
        {
            _inputField.Update(SociaKeyBoard.TextBuffer);

            if (_showHideButton.Update(gameTime))
            {
                int value = 320;

                if (_isKeyboardUp)
                {
                    _isKeyboardUp = false;
                    _inputField.PositionY += value;
                }
                else
                {
                    _isKeyboardUp = true;
                    _inputField.PositionY -= value;
                }
            }

            if (!_isKeyboardUp) return;

            foreach (Button button in KeyBoardButtons)
            {
                if (button.Update(gameTime))
                {
                    _virtualKeys.Add(button.Key);
                }
            }

            KeyboardState state = new KeyboardState(_virtualKeys.ToArray());
            _virtualKeys.Clear();

            SociaKeyBoard.CheckKeyState(gameTime,state);
        }

        /// <summary>
        /// Draw the text and show it above the corresponding player's head
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            _inputField.Draw(spriteBatch);
            _showHideButton.Draw(spriteBatch);

            if (!_isKeyboardUp) return;

            foreach (Button button in KeyBoardButtons)
            {
                button.Draw(spriteBatch);
            }
        }

        private void CreateKeyBoard(Vector2 position, Vector2 sprite, float scale)
        {
            position.Y += (sprite.Y + 20) - 320;
            float startX = position.X;

            KeyBoardButtons.Add(new Button(position, "`", scale, 4, Fonts.SmallFont));
            position.X += sprite.X * 6;

            KeyBoardButtons.Add(new Button(position, "1", Keys.D1, scale, 4, Fonts.SmallFont));
            position.X += sprite.X * 6;

            KeyBoardButtons.Add(new Button(position, "2", Keys.D2, scale, 4, Fonts.SmallFont));
            position.X += sprite.X * 6;

            KeyBoardButtons.Add(new Button(position, "3", Keys.D3, scale, 4, Fonts.SmallFont));
            position.X += sprite.X * 6;

            KeyBoardButtons.Add(new Button(position, "4", Keys.D4, scale, 4, Fonts.SmallFont));
            position.X += sprite.X * 6;

            KeyBoardButtons.Add(new Button(position, "5", Keys.D5, scale, 4, Fonts.SmallFont));
            position.X += sprite.X * 6;

            KeyBoardButtons.Add(new Button(position, "6", Keys.D6, scale, 4, Fonts.SmallFont));
            position.X += sprite.X * 6;

            KeyBoardButtons.Add(new Button(position, "7", Keys.D7, scale, 4, Fonts.SmallFont));
            position.X += sprite.X * 6;

            KeyBoardButtons.Add(new Button(position, "8", Keys.D8, scale, 4, Fonts.SmallFont));
            position.X += sprite.X * 6;

            KeyBoardButtons.Add(new Button(position, "9", Keys.D9, scale, 4, Fonts.SmallFont));
            position.X += sprite.X * 6;

            KeyBoardButtons.Add(new Button(position, "0", Keys.D0, scale, 4, Fonts.SmallFont));
            position.X += sprite.X * 6;

            KeyBoardButtons.Add(new Button(position, "-", Keys.OemMinus, scale, 4, Fonts.SmallFont));
            position.X += sprite.X * 6;

            KeyBoardButtons.Add(new Button(position, "+", Keys.OemPlus, scale, 4, Fonts.SmallFont));
            position.X += sprite.X * 6;

            KeyBoardButtons.Add(new Button(position, "backspace", Keys.Back, scale, 10, Fonts.SmallFont));

            position.X = startX;
            position.Y += sprite.Y;
            KeyBoardButtons.Add(new Button(position, "Tab", Keys.Tab, scale, 7, Fonts.SmallFont));

            position.X += sprite.X * 9;
            KeyBoardButtons.Add(new Button(position, "Q", Keys.Q, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "W", Keys.W, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "E", Keys.E, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "R", Keys.R, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "T", Keys.T, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "Y", Keys.Y, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "U", Keys.U, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "I", Keys.I, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "O", Keys.O, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "P", Keys.P, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "[", Keys.OemOpenBrackets, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "]", Keys.OemCloseBrackets, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "\\", Keys.OemBackslash, scale, 7, Fonts.SmallFont));

            position.X = startX;
            position.Y += sprite.Y;
            KeyBoardButtons.Add(new Button(position, "Caps Lock", Keys.CapsLock, scale, 9, Fonts.SmallFont));

            position.X += sprite.X * 11;
            KeyBoardButtons.Add(new Button(position, "A", Keys.A, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "S", Keys.S, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "D", Keys.D, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "F", Keys.F, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "G", Keys.G, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "H", Keys.H, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "J", Keys.J, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "K", Keys.K, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "L", Keys.L, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, ";", Keys.OemSemicolon, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "'", Keys.OemQuotes, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "Enter", Keys.Enter, scale, 11, Fonts.SmallFont));

            position.X = startX;
            position.Y += sprite.Y;
            KeyBoardButtons.Add(new Button(position, "Shift", Keys.LeftShift, scale, 12, Fonts.SmallFont));

            position.X += sprite.X * 14;
            KeyBoardButtons.Add(new Button(position, "Z", Keys.Z, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "X", Keys.X, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "C", Keys.C, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "V", Keys.V, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "B", Keys.B, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "N", Keys.N, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "M", Keys.M, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, ",", Keys.OemComma, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, ".", Keys.OemPeriod, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "?", Keys.OemQuestion, scale, 4, Fonts.SmallFont));

            position.X += sprite.X * 6;
            KeyBoardButtons.Add(new Button(position, "Shift", Keys.RightShift, scale, 14, Fonts.SmallFont));

            position.X = startX;
            position.Y += sprite.Y;
            KeyBoardButtons.Add(new Button(position, "Control", Keys.LeftControl, scale, 6, Fonts.SmallFont));

            position.X += sprite.X * 8;
            KeyBoardButtons.Add(new Button(position, "Windows", Keys.LeftWindows, scale, 6, Fonts.SmallFont));

            position.X += sprite.X * 8;
            KeyBoardButtons.Add(new Button(position, "Alt", Keys.LeftAlt, scale, 6, Fonts.SmallFont));

            position.X += sprite.X * 8;
            KeyBoardButtons.Add(new Button(position, "Space", Keys.Space, scale, 32, Fonts.SmallFont));

            position.X += sprite.X * 34;
            KeyBoardButtons.Add(new Button(position, "Alt", Keys.RightAlt, scale, 6, Fonts.SmallFont));

            position.X += sprite.X * 8;
            KeyBoardButtons.Add(new Button(position, "Windows", Keys.RightWindows, scale, 6, Fonts.SmallFont));

            position.X += sprite.X * 8;
            KeyBoardButtons.Add(new Button(position, "Copy", Keys.OemCopy, scale, 6, Fonts.SmallFont));

            position.X += sprite.X * 8;
            KeyBoardButtons.Add(new Button(position, "Control", Keys.RightControl, scale, 6, Fonts.SmallFont));
        }
    }
}

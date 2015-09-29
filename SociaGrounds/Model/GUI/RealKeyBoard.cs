using System;
using Microsoft.Xna.Framework.Input;
using SociaGrounds.Model.Controllers;

namespace SociaGrounds.Model.GUI
{
    /// <summary>
    /// This class is used for the desktop version of the application to manage the keyboard input
    /// </summary>
    public class RealKeyBoard : AKeyBoard
    {
        private bool _isUppercase;
        private bool _isCapsLock;

        /// <summary>
        /// Constructor of the keyboard, setting the textbuffer, 
        /// wether it's clicked or not, the height of the field and making a vector object out of it
        /// </summary>
        public RealKeyBoard()
        {
            _isUppercase = false;
            _isCapsLock = false;
        }

        /// <summary>
        /// Method that adds text to the textbuffer
        /// </summary>
        /// <param name="value">The value that needs to be added</param>
        private void AddText(char value)
        {
            TextBuffer = TextBuffer + value;
        }

        /// <summary>
        /// method that keeps check of the current keystates
        /// </summary>
        public override void CheckKeyState()
        {
            KeyboardState newState = Keyboard.GetState();

            switch (Static.CurrentScreenState)
            {
                case ScreenState.AboutScreen:
                case ScreenState.SettingsScreen:
                    if (OldKeyboardState.IsKeyDown(Keys.Escape) && newState.IsKeyUp(Keys.Escape))
                    {
                        Static.CurrentScreenState = ScreenState.HomeScreen;
                    }
                    break;
                case ScreenState.RoomScreen:
                    RoomKeyBoard(newState);
                    break;
            }

            OldKeyboardState = newState;
        }

        private void RoomKeyBoard(KeyboardState newState)
        {
            if (OldKeyboardState.IsKeyDown(Keys.CapsLock) && newState.IsKeyUp(Keys.CapsLock))
            {
                switch (_isUppercase)
                {
                    case true:
                        _isUppercase = false;
                        _isCapsLock = false;
                        break;
                    case false:
                        _isUppercase = true;
                        _isCapsLock = true;
                        break;
                }
            }

            foreach (Keys key in AllKeys)
            {
                if (!OldKeyboardState.IsKeyDown(key) || !newState.IsKeyUp(key)) continue;

                if (_isUppercase)
                {
                    AddText(_isCapsLock
                        ? UpperCapsCase[Array.IndexOf(AllKeys, key)]
                        : UpperCase[Array.IndexOf(AllKeys, key)]);
                }
                else
                {
                    AddText(LowerCase[Array.IndexOf(AllKeys, key)]);
                }
            }

            if (OldKeyboardState.IsKeyDown(Keys.Enter) && newState.IsKeyUp(Keys.Enter))
            {
                if (string.IsNullOrEmpty(TextBuffer)) return;

                Static.Players[0].ChatMessage = TextBuffer;
                TextBuffer = "";
            }

            if (OldKeyboardState.IsKeyDown(Keys.Back) && newState.IsKeyUp(Keys.Back))
            {
                if (TextBuffer.Length > 0)
                {
                    char[] text = TextBuffer.ToCharArray();
                    TextBuffer = new string(text, 0, text.Length - 1);
                }
            }

            if (newState.IsKeyDown(Keys.LeftShift))
            {
                _isUppercase = !_isCapsLock;
            }
            else if (OldKeyboardState.IsKeyDown(Keys.LeftShift) && newState.IsKeyUp(Keys.LeftShift))
            {
                _isUppercase = _isCapsLock;
            }
        }
    }
}

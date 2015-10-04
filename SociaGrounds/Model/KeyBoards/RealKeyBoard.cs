using System;
using Microsoft.Xna.Framework.Input;
using SociaGrounds.Model.Controllers;

namespace SociaGrounds.Model.KeyBoards
{
    /// <summary>
    /// This class is used for the desktop version of the application to manage the keyboard input
    /// </summary>
    public class RealKeyBoard : AKeyBoard
    {
        /// <summary>
        /// method that keeps check of the current keystates
        /// </summary>
        public override void CheckKeyState(KeyboardState state = default(KeyboardState))
        {
            if (state == default(KeyboardState))
            {
                state = Keyboard.GetState();
            }

            switch (Static.CurrentScreenState)
            {
                case ScreenState.AboutScreen:
                case ScreenState.SettingsScreen:
                    if (OldKeyboardState.IsKeyDown(Keys.Escape) && state.IsKeyUp(Keys.Escape))
                    {
                        Static.CurrentScreenState = ScreenState.HomeScreen;
                    }
                    break;
                case ScreenState.RoomScreen:
                    RoomKeyBoard(state);
                    break;
            }

            OldKeyboardState = state;
        }

        private void RoomKeyBoard(KeyboardState newState)
        {
            if (OldKeyboardState.IsKeyDown(Keys.CapsLock) && newState.IsKeyUp(Keys.CapsLock))
            {
                switch (_IsUppercase)
                {
                    case true:
                        _IsUppercase = false;
                        _IsCapsLock = false;
                        break;
                    case false:
                        _IsUppercase = true;
                        _IsCapsLock = true;
                        break;
                }
            }

            foreach (Keys key in AllKeys)
            {
                if (!OldKeyboardState.IsKeyDown(key) || !newState.IsKeyUp(key)) continue;

                if (_IsUppercase)
                {
                    AddText(_IsCapsLock
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
                _IsUppercase = !_IsCapsLock;
            }
            else if (OldKeyboardState.IsKeyDown(Keys.LeftShift) && newState.IsKeyUp(Keys.LeftShift))
            {
                _IsUppercase = _IsCapsLock;
            }
        }
    }
}

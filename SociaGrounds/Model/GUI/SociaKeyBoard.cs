using System;
using Microsoft.Xna.Framework.Input;
using SociaGrounds.Model.Controllers;

namespace SociaGrounds.Model.GUI
{
    /// <summary>
    /// Class is used to connect the windows phone & desktop keyboards.
    /// Making it easier to migrate between the platforms.
    /// </summary>
    public class SociaKeyBoard
    {
        protected bool _IsUppercase;
        protected bool _IsCapsLock;

        protected static readonly Keys[] AllKeys =
        {
            Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T, Keys.Y, Keys.U, Keys.I, Keys.O, Keys.P,
            Keys.A, Keys.S , Keys.D, Keys.F, Keys.G, Keys.H, Keys.J, Keys.K, Keys.L,
            Keys.Z, Keys.X, Keys.C, Keys.V, Keys.B, Keys.N, Keys.M, Keys.Space,
            Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7,
            Keys.D8, Keys.D9, Keys.D0
        };

        protected static readonly char[] UpperCase =
        {
            'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M',' ',
            '!','@','#','$','%','^','&','*','(',')'
        };

        protected static readonly char[] UpperCapsCase =
        {
            'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M',' ',
            '1','2','3','4','5','6','7','8','9','0'
        };

        protected static readonly char[] LowerCase =
        {
            'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m',' ',
            '1','2','3','4','5','6','7','8','9','0'
        };

        public string TextBuffer { get; protected set; }

        public KeyboardState OldKeyboardState { get; private set; }

        public SociaKeyBoard()
        {
            TextBuffer = "";
            _IsUppercase = false;
            _IsCapsLock = false;
        }

        /// <summary>
        /// Method that adds text to the textbuffer
        /// </summary>
        /// <param name="value">The value that needs to be added</param>
        protected virtual void AddText(char value)
        {
            TextBuffer = TextBuffer + value;
        }

        /// <summary>
        /// method that keeps check of the current keystates
        /// </summary>
        public void CheckKeyState(KeyboardState state = default(KeyboardState))
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
                default:
                    throw new ArgumentOutOfRangeException();
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
                    AddText(_IsCapsLock ? UpperCapsCase[Array.IndexOf(AllKeys, key)] : UpperCase[Array.IndexOf(AllKeys, key)]);
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

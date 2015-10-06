using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SociaGrounds.Model.Controllers;

namespace SociaGrounds.Model.GUI
{
    /// <summary>
    /// Class is used to connect the windows phone & desktop keyboards.
    /// Making it easier to migrate between the platforms.
    /// </summary>
    public static class SociaKeyBoard
    {
        private static bool _isUppercase;
        private static bool _isCapsLock;

        private static DelayedAction _wait;

        private static readonly Keys[] ALL_KEYS =
        {
            Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T, Keys.Y, Keys.U, Keys.I, Keys.O, Keys.P,
            Keys.A, Keys.S , Keys.D, Keys.F, Keys.G, Keys.H, Keys.J, Keys.K, Keys.L,
            Keys.Z, Keys.X, Keys.C, Keys.V, Keys.B, Keys.N, Keys.M, Keys.Space,
            Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7,
            Keys.D8, Keys.D9, Keys.D0
        };

        private static readonly char[] UPPER_CASE =
        {
            'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M',' ',
            '!','@','#','$','%','^','&','*','(',')'
        };

        private static readonly char[] UPPER_CAPS_CASE =
        {
            'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M',' ',
            '1','2','3','4','5','6','7','8','9','0'
        };

        private static readonly char[] LOWER_CASE =
        {
            'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m',' ',
            '1','2','3','4','5','6','7','8','9','0'
        };

        public static string TextBuffer { get; private set; }

        private static KeyboardState _oldKeyboardState;

        static SociaKeyBoard()
        {
            TextBuffer = "";
            _isUppercase = false;
            _isCapsLock = false;
            _wait = new DelayedAction(100);
        }

        /// <summary>
        /// Method that adds text to the textbuffer
        /// </summary>
        /// <param name="value">The value that needs to be added</param>
        private static void AddText(char value)
        {
            TextBuffer = TextBuffer + value;
        }

        /// <summary>
        /// method that keeps check of the current keystates
        /// </summary>
        public static KeyboardState CheckKeyState(GameTime gameTime,KeyboardState state = default(KeyboardState))
        {
            if (state == default(KeyboardState))
            {
                state = Keyboard.GetState();
            }

            _wait.Update(gameTime.ElapsedGameTime.Milliseconds);

            switch (Static.CurrentScreenState)
            {
                case ScreenState.AboutScreen:
                case ScreenState.SettingsScreen:
                    AboutSettingsKeyboard(state);
                    break;
                case ScreenState.RoomScreen:
                    RoomKeyBoard(state);
                    break;
                case ScreenState.HomeScreen:
                    break;
                case ScreenState.LobbyScreen:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _oldKeyboardState = state;

            return _oldKeyboardState;
        }

        private static void AboutSettingsKeyboard(KeyboardState newState)
        {
            if (_oldKeyboardState.IsKeyDown(Keys.Escape) && newState.IsKeyUp(Keys.Escape))
            {
                Static.CurrentScreenState = ScreenState.HomeScreen;
            }
        }

        private static void RoomKeyBoard(KeyboardState newState)
        {
            if (_oldKeyboardState.IsKeyDown(Keys.CapsLock) && newState.IsKeyUp(Keys.CapsLock))
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

            foreach (Keys key in ALL_KEYS)
            {
                if (!_oldKeyboardState.IsKeyDown(key) || !newState.IsKeyUp(key)) continue;

                if (_isUppercase)
                {
                    AddText(_isCapsLock ? UPPER_CAPS_CASE[Array.IndexOf(ALL_KEYS, key)] : UPPER_CASE[Array.IndexOf(ALL_KEYS, key)]);
                }
                else
                {
                    AddText(LOWER_CASE[Array.IndexOf(ALL_KEYS, key)]);
                }
            }

            if (_oldKeyboardState.IsKeyDown(Keys.Enter) && newState.IsKeyUp(Keys.Enter))
            {
                if (string.IsNullOrEmpty(TextBuffer)) return;

                StaticPlayer.ForeignPlayers[0].ChatMessage = TextBuffer;
                TextBuffer = "";
            }

            if (_oldKeyboardState.IsKeyDown(Keys.Back) && newState.IsKeyUp(Keys.Back))
            {
                RemoveLastChar();
            }

            if (_oldKeyboardState.IsKeyDown(Keys.Back) && newState.IsKeyDown(Keys.Back))
            {
                if (CheckIfDelayStarted()) return;
                RemoveLastChar();
            }

            if (newState.IsKeyDown(Keys.LeftShift) || newState.IsKeyDown(Keys.RightShift))
            {
                _isUppercase = !_isCapsLock;
            }
            else if (_oldKeyboardState.IsKeyDown(Keys.LeftShift) && newState.IsKeyUp(Keys.LeftShift) 
                || _oldKeyboardState.IsKeyDown(Keys.RightShift) && newState.IsKeyUp(Keys.RightShift))
            {
                _isUppercase = _isCapsLock;
            }
        }

        private static bool CheckIfDelayStarted()
        {
            if (_wait.Started) return true;

            _wait.Started = true;

            return false;
        }

        private static void RemoveLastChar()
        {
            if (TextBuffer.Length > 0)
            {
                char[] text = TextBuffer.ToCharArray();
                TextBuffer = new string(text, 0, text.Length - 1);
            }
        }
    }
}

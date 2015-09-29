using System;
using Windows.UI.Xaml.Documents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SociaGrounds.Model.Controllers;

namespace SociaGrounds.Model.GUI
{
    /// <summary>
    /// This class is used for the desktop version of the application to manage the keyboard input
    /// </summary>
    public class RealKeyBoard : IKeyBoard
    {
        private KeyboardState _oldState;

        private string _textBuffer;

        private readonly Keys[] _keys = 
        {
            Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T, Keys.Y, Keys.U, Keys.I, Keys.O, Keys.P, 
            Keys.A, Keys.S , Keys.D, Keys.F, Keys.G, Keys.H, Keys.J, Keys.K, Keys.L, 
            Keys.Z, Keys.X, Keys.C, Keys.V, Keys.B, Keys.N, Keys.M, Keys.Space, 
            Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad7,
            Keys.NumPad8, Keys.NumPad9, Keys.NumPad0
        };

        private readonly char[] _upperCase =
        {
            'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M',' ',
            '!','@','#','$','%','^','&','*','(',')'
        };

        private readonly char[] _lowerCase =
        {
            'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m',' ',
            '1','2','3','4','5','6','7','8','9','0'
        };

        private bool _isUppercase;
        private bool _isCapsLock;

        private readonly InputField _inputField;
        private readonly float _inputFieldHeight;

        /// <summary>
        /// Constructor of the keyboard, setting the textbuffer, 
        /// wether it's clicked or not, the height of the field and making a vector object out of it
        /// </summary>
        /// <param name="content">Content of the keyboard</param>
        public RealKeyBoard(ContentManager content)
        {
            _textBuffer = "";
            _inputFieldHeight = 4.7f;
            _inputField = new InputField(content, new Vector2(100, 0), 10, 0.08f);
            _isUppercase = false;
        }

        /// <summary>
        /// Update method
        /// </summary>
        /// <param name="position">Position of the key</param>
        /// <param name="viewport">Your screen</param>
        /// <param name="keyState">State of the keys on the keyboard</param>
        public void Update(Vector2 position, Viewport viewport,KeyboardState keyState)
        {
            _inputField.Position = new Vector2(position.X - (viewport.Width / 11f), position.Y + (viewport.Height / _inputFieldHeight));
            CheckKeyState(keyState);
            _inputField.Update(_textBuffer);
        }

        /// <summary>
        /// Draw method
        /// </summary>
        /// <param name="spriteBatch">Contains the spriteBatch to draw</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            _inputField.Draw(spriteBatch);
        }

        /// <summary>
        /// Method that adds text to the textbuffer
        /// </summary>
        /// <param name="value">The value that needs to be added</param>
        private void AddText(char value)
        {
            _textBuffer = _textBuffer + value;
        }

        /// <summary>
        /// method that keeps check of the current keystates
        /// </summary>
        /// <param name="newState">Input of current keystate</param>
        private void CheckKeyState(KeyboardState newState)
        {
            if (_oldState.IsKeyDown(Keys.CapsLock) && newState.IsKeyUp(Keys.CapsLock))
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

            foreach (Keys key in _keys)
            {
                if (!_oldState.IsKeyDown(key) || !newState.IsKeyUp(key)) continue;

                AddText(_isUppercase ? _upperCase[Array.IndexOf(_keys,key)] : _lowerCase[Array.IndexOf(_keys, key)]);
            }

            if (_oldState.IsKeyDown(Keys.Enter) && newState.IsKeyUp(Keys.Enter))
            {
                if (string.IsNullOrEmpty(_textBuffer)) return;

                Static.Players[0].ChatMessage = _textBuffer;
                _textBuffer = "";
            }

            if (_oldState.IsKeyDown(Keys.Back) && newState.IsKeyUp(Keys.Back))
            {
                if (_textBuffer.Length > 0)
                {
                    char[] text = _textBuffer.ToCharArray();
                    _textBuffer = new string(text, 0, text.Length - 1);
                }
            }

            if (newState.IsKeyDown(Keys.LeftShift))
            {
                _isUppercase = !_isCapsLock;
                
            }
            else if (_oldState.IsKeyDown(Keys.LeftShift) && newState.IsKeyUp(Keys.LeftShift))
            {
                _isUppercase = _isCapsLock;
            }

            _oldState = newState;
        }
    }
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SocialGroundsStore.GUI
{
    public class RealKeyBoard : IKeyBoard
    {
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

        //private readonly char[] _lowerCase =
        //{
        //    'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m',' ',
        //    '1','2','3','4','5','6','7','8','9','0'
        //};

        private readonly bool[] _isClicked;

        private bool _isBackSpace;
        private bool _isEnter;
        private readonly InputField _inputField;
        private readonly float _inputFieldHeight;

        public RealKeyBoard(ContentManager content)
        {
            _textBuffer = "";
            _isClicked = new bool[_upperCase.Length];
            _inputFieldHeight = 4.7f;
            _inputField = new InputField(content, new Vector2(100, 0), 10, 0.08f);
        }

        public void Update(Vector2 position, Viewport viewport, KeyboardState keyState)
        {
            _inputField.Position = new Vector2(position.X - (viewport.Width / 11f), position.Y + (viewport.Height / _inputFieldHeight));
            CheckKeyState(keyState);
            _inputField.Update(_textBuffer);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _inputField.Draw(spriteBatch);
        }

        private void AddText(char value)
        {
            _textBuffer = _textBuffer + value;
        }

        private void OnEnter()
        {
            if (String.IsNullOrEmpty(_textBuffer)) return;

            Game1.players[0].ChatMessage = _textBuffer;
            _textBuffer = "";
        }

        private void CheckKeyState(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Back))
            {
                if (_textBuffer.Length <= 0) return;
                _isBackSpace = true;
            }

            if (keyState.IsKeyDown(Keys.Enter))
            {
                _isEnter = true;
            }

            for (int i = 0; i < _keys.Length - 1; i++)
            {
                if (keyState.IsKeyDown(_keys[i]))
                {
                    _isClicked[i] = true;
                }
            }

            if (keyState.IsKeyUp(Keys.Back))
            {
                if (_isBackSpace)
                {
                    _isBackSpace = false;
                    char[] text = _textBuffer.ToCharArray();
                    _textBuffer = new string(text, 0, text.Length - 1);
                }
            }

            if (keyState.IsKeyUp(Keys.Enter))
            {
                if (_isEnter)
                {
                    _isEnter = false;
                    OnEnter();
                }
            }

            for (int i = 0; i < _keys.Length - 1; i++)
            {
                if (keyState.IsKeyUp(_keys[i]))
                {
                    if (_isClicked[i])
                    {
                        _isClicked[i] = false;
                        AddText(_upperCase[i]);
                    }
                }
            }
        }
    }
}

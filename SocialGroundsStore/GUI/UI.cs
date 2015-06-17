using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SocialGroundsStore.GUI
{
    public class Ui
    {
        // The room menu
        private readonly RoomMenu _roomMenu;
        //private bool _isCapsLock;
        private string _textBuffer;

        private readonly Keys[] _keys = 
        {
            Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T, Keys.Y, Keys.U, Keys.I, Keys.O, Keys.P, 
            Keys.A, Keys.S , Keys.D, Keys.F, Keys.G, Keys.H, Keys.J, Keys.K, Keys.L, 
            Keys.Z, Keys.X, Keys.C, Keys.V, Keys.B, Keys.N, Keys.M, Keys.Space, 
            Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad7,
            Keys.NumPad8, Keys.NumPad9, Keys.NumPad0
        };

        private readonly char[] _lowerCase =
        {
            'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m',' ',
            '1','2','3','4','5','6','7','8','9','0'
        };

        private readonly char[] _upperCase =
        {
            'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M',' ',
            '!','@','#','$','%','^','&','*','(',')'
        };

        private readonly bool[] _isClicked;

        private bool _isBackSpace;
        private bool _isEnter;
        //private bool _isCapsLockDown;

        // Camera centre
        private Vector2 _cameraCentre;

        //public Vector2 CameraCentre
        //{
        //    get { return cameraCentre; }
        //    set { cameraCentre = value; }
        //}

        public Ui(ContentManager content, Viewport viewport)
        {
            _roomMenu = new RoomMenu(content, new Vector2(0, 0), viewport);
            _textBuffer = "";
            //_isCapsLock = false;
            _isClicked = new bool[_upperCase.Length];
        }

        public void Update(Vector2 position, Viewport viewport)
        {
            // Updating the camera centre and the room menu according to the camera centre
            _cameraCentre = position;
            _roomMenu.Update(_cameraCentre, viewport, _textBuffer);
        }

        public void CheckKeyState(KeyboardState keyState)
        {
            bool isUpperCase = false;//keyState.IsKeyDown(Keys.LeftShift) || keyState.IsKeyDown(Keys.RightShift);

            //if (keyState.IsKeyDown(Keys.CapsLock))
            //{
            //    _isCapsLockDown = true;
            //}

            if (keyState.IsKeyDown(Keys.Back))
            {
                if (_textBuffer.Length <= 0) return;
                _isBackSpace = true;
            }

            if (keyState.IsKeyDown(Keys.Enter))
            {
                _isEnter = true;
            }

            for (int i = 0; i < _keys.Length -1; i++)
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

            //if (keyState.IsKeyUp(Keys.CapsLock))
            //{
            //    _isCapsLock = !_isCapsLock;
            //    if (_isCapsLock)
            //    {
            //        isUpperCase = true;
            //    }
            //}

            for (int i = 0; i < _keys.Length - 1; i++)
            {
                if (keyState.IsKeyUp(_keys[i]))
                {
                    if (_isClicked[i])
                    {
                        _isClicked[i] = false;
                        AddText(isUpperCase ? _upperCase[i] : _lowerCase[i]);
                    }
                }
            }
        }

        private void AddText(char value)
        {
            _textBuffer = _textBuffer + value;
        }

        private void OnEnter()
        {
            if (!String.IsNullOrEmpty(_textBuffer)) return;

            Game1.players[0].ChatMessage = _textBuffer;
            _textBuffer = "";
        }

        //public void CheckMouseDown(MouseState mouseState)
        //{
        //    _roomMenu.CheckMouseDown(mouseState);
        //}

        public void Draw(SpriteBatch spriteBatch)
        {
            _roomMenu.Draw(spriteBatch);
        }
    }
}

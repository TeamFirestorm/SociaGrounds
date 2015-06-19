using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SocialGroundsStore.GUI;

namespace SocialGroundsStore.Screens
{
    class LoginScreen
    {
        private readonly List<Button> _buttons;
        private bool _isClicked;

        public bool IsPlayingMusic { get; set; }

        public LoginScreen(ContentManager content, Viewport viewport)
        {
            float middleWidth = viewport.Width/2f;
            float middleHeight = viewport.Height/2f;

            _isClicked = false;
            _buttons = new List<Button>
            {
                new Button(content, new Vector2(middleWidth, middleHeight), "Login", 1.0f)
            };
            
        }

        public void Update(MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (_buttons[0].IsClicked(mouseState))
                {
                    _isClicked = true;
                }              
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Button button in _buttons)
            {
                button.Draw(spriteBatch);
            }
        }

        public bool ToHomeScreen()
        {
            if (_isClicked)
            {
                _isClicked = false;
                return true;
            }
            return false;
        }
    }
}

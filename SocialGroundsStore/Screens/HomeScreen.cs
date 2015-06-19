﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SocialGroundsStore.GUI;

namespace SocialGroundsStore.Screens
{
    public class HomeScreen
    {
        private readonly List<Button> _buttons;
        private readonly Texture2D _background;
        private readonly Texture2D _title;

        public bool IsPlayingMusic { get; set; }

        public HomeScreen(ContentManager content)
        {
            float middleWidth = Game1.Viewport.Width / 2f;
            float middleHeight = Game1.Viewport.Height / 2f;

            _background = content.Load<Texture2D>("Background/background.png");
            _title = content.Load<Texture2D>("Background/Sociagrounds_title.png");

            _buttons = new List<Button>
            {
                new Button(content, new Vector2(middleWidth - 200, middleHeight + 200), "Play the Game", 1.0f),
            };

            _buttons.Add(new Button(content, new Vector2(0 + 200, middleHeight), "ABout", 1.0f, _buttons[0].Width));
            _buttons.Add(new Button(content, new Vector2(Game1.Viewport.Width - 600, middleHeight), "Settings", 1.0f, _buttons[0].Width));
        }

        public void Update(MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                foreach (Button button in _buttons)
                {
                    if (button.IsClicked(mouseState))
                    {
                        button.ClickedThis = true;
                    }
                }
                ChangeScreen();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background,new Vector2(0,0),Color.White);

            spriteBatch.Draw(_title, new Vector2((Game1.Viewport.Width /2f - _title.Width / 2f), 50), Color.White);

            foreach (Button button in _buttons)
            {
                button.Draw(spriteBatch);
            }
        }

        public void ChangeScreen()
        {
            if (_buttons[0].ClickedThis)
            {
                _buttons[0].ClickedThis = false;
                Game1.currentScreenState = Game1.ScreenState.LobbyScreen;
            }
            if (_buttons[1].ClickedThis)
            {
                _buttons[1].ClickedThis = false;
                Game1.currentScreenState = Game1.ScreenState.AboutScreen;
            }
        }
    }
}

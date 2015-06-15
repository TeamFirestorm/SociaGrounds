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
        private readonly List<Button> buttons;
        private bool isClicked;

        public LoginScreen(ContentManager content)
        {
            isClicked = false;
            buttons = new List<Button>
            {
                new Button(content, new Vector2(50, 50), "Start", 1.0f)
            };
        }

        public void Update(ContentManager content, MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (buttons[0].isClicked(mouseState))
                {
                    isClicked = true;
                }              
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Button button in buttons)
            {
                button.draw(spriteBatch);
            }
        }

        public bool ToHomeScreen(GameTime gameTime)
        {
            if (isClicked)
            {
                isClicked = false;
                return true;
            }
            return false;
        }
    }
}

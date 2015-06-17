using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SocialGroundsStore.GUI;
using Microsoft.Xna.Framework.Media;

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
            
            // Play background music
            MediaPlayer.Play(Game1.songList[1]);
        }

        public void Update(ContentManager content, MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (buttons[0].IsClicked(mouseState))
                {
                    isClicked = true;
                }              
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
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

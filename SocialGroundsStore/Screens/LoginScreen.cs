using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SocialGroundsStore.GUI;

namespace SocialGroundsStore.Screens
{
    class LoginScreen : Screen
    {
        List<Button> buttons;
        float timeElapsed;

        public LoginScreen(ContentManager content)
        {
            buttons = new List<Button>();
            buttons.Add(new Button(content, new Vector2(50, 50), "Start", 1.0f));
        }

        public override void Update(ContentManager content)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Button button in buttons)
            {

                button.draw(spriteBatch);
            }
        }

        public bool ToHomeScreen(GameTime gameTime)
        {
            timeElapsed += gameTime.ElapsedGameTime.Milliseconds;

            // Go to the home screen if the button has been pressed
            // Add a slight delay
            if (buttons[0].isTouched() && timeElapsed >= 500)
            {
                timeElapsed = 0;
                return true;
            }

            return false;
        }
    }
}

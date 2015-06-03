using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SociaGroundsEngine.GUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SociaGroundsEngine.Screens
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

        public override void update()
        {

        }

        public override void draw(SpriteBatch spriteBatch)
        {
            foreach (Button button in buttons)
            {
                button.draw(spriteBatch);
            }
        }

        public bool toHomeScreen(GameTime gameTime)
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

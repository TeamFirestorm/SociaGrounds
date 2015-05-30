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

        public LoginScreen(ContentManager content)
        {
            buttons = new List<Button>();
            buttons.Add(new Button(content, new Vector2(50, 50), 4, "Wouter houdt van fietsen!!!"));
            buttons.Add(new Button(content, new Vector2(150, 150), 4, "Thijs niet!!!"));
            buttons.Add(new Button(content, new Vector2(50, 300), 4, "Ik weet niet waarom ik zo luidruchtig ben!!!"));
        }

        public override void update()
        {
            if (buttons[0].isHold())
            {
                Debug.WriteLine("Click!");
            }
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            foreach (Button button in buttons)
            {
                button.draw(spriteBatch);
            }
        }
    }
}

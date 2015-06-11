using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SociaGroundsEngine.GUI;

namespace SociaGroundsEngine.Screens
{
    public class HomeScreen : Screen
    {
        readonly List<Button> buttons;

        public HomeScreen(ContentManager content)
        {
            buttons = new List<Button>();
            buttons.Add(new Button(content, new Vector2(50, 50), "Start Game", 1.0f));
        }

        public override void Update()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Button button in buttons)
            {

                button.draw(spriteBatch);
            }
        }
    }
}

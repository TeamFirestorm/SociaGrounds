using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SocialGroundsStore.GUI;

namespace SocialGroundsStore.Screens
{
    public class HomeScreen
    {
        private readonly List<Button> _buttons;

        public HomeScreen(ContentManager content)
        {
            _buttons = new List<Button>
            {
                new Button(content, new Vector2(50, 50), "Start Game", 1.0f)
            };
        }

        public void Update(ContentManager content)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Button button in _buttons)
            {

                button.Draw(spriteBatch);
            }
        }
    }
}

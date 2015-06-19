using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SocialGroundsStore.Screens
{
    public  class AboutScreen
    {
        private readonly string[] _names;

        public AboutScreen()
        {
            _names = new [] {"Gyllion van Elderen", "Wouter Kosse", "Thijs Reeringh", "Chris Vinkers", "Alwin Masseling"};
        }

        public void Update(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Escape))
            {
                Game1.currentScreenState = Game1.ScreenState.HomeScreen;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            DefaultBackground.Draw(spriteBatch);

            float height = 0;

            spriteBatch.DrawString(Game1.font, "Press escape to return", new Vector2(100,100), Color.Black);

            foreach (string name in _names)
            {
                spriteBatch.DrawString(Game1.font, name, new Vector2(Game1.Viewport.Width / 2f, ((Game1.Viewport.Height / 2f) + height)), Color.Black);
                height += 40;
            }

            spriteBatch.End();
        }
    }
}
